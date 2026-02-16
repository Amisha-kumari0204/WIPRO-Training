-- Step 1: Create Base Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE Employees (
        EmpId INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(100) NOT NULL,
        Salary INT NOT NULL,
        IsActive BIT DEFAULT 1
    );
    PRINT 'Employees table created.';
END
GO

-- Step 7 (Partial): Create Audit Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EmployeeSalaryAudit')
BEGIN
    CREATE TABLE EmployeeSalaryAudit (
        AuditId INT IDENTITY(1,1) PRIMARY KEY,
        EmpId INT,
        OldSalary INT,
        NewSalary INT,
        ChangedOn DATETIME DEFAULT GETDATE()
    );
    PRINT 'EmployeeSalaryAudit table created.';
END
GO

-- Step 2: US-01 + US-02: Insert Employee with Validation + Output Parameter
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddEmployee')
    DROP PROCEDURE sp_AddEmployee;
GO

CREATE PROCEDURE sp_AddEmployee
    @Name VARCHAR(100),
    @Salary INT,
    @EmpId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validation: Salary must be > 0
    IF(@Salary <= 0)
    BEGIN
        RAISERROR('Invalid Salary: Salary must be greater than zero.', 16, 1);
        RETURN;
    END

    INSERT INTO Employees(Name, Salary, IsActive)
    VALUES(@Name, @Salary, 1);

    SET @EmpId = SCOPE_IDENTITY();
    PRINT 'Employee added successfully with ID: ' + CAST(@EmpId AS VARCHAR(10));
END
GO

-- Step 3: US-03 + US-04: Update Salary with Transaction and Error Handling
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateSalary')
    DROP PROCEDURE sp_UpdateSalary;
GO

CREATE PROCEDURE sp_UpdateSalary
    @EmpId INT,
    @Salary INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION

        -- Validation: Check if employee exists
        IF EXISTS(SELECT 1 FROM Employees WHERE EmpId=@EmpId)
        BEGIN
            UPDATE Employees
            SET Salary=@Salary
            WHERE EmpId=@EmpId;
            
            PRINT 'Salary updated for Employee ID: ' + CAST(@EmpId AS VARCHAR(10));
        END
        ELSE
        BEGIN
            RAISERROR('Employee ID %d not found.', 16, 1, @EmpId);
        END

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- Step 4: US-05 + US-06: Get Employee By ID & Salary Filter
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetEmployeeById')
    DROP PROCEDURE sp_GetEmployeeById;
GO

CREATE PROCEDURE sp_GetEmployeeById
    @EmpId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmpId, Name, Salary, IsActive 
    FROM Employees
    WHERE EmpId=@EmpId;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_HighSalary')
    DROP PROCEDURE sp_HighSalary;
GO

CREATE PROCEDURE sp_HighSalary
    @Salary INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmpId, Name, Salary, IsActive 
    FROM Employees
    WHERE Salary > @Salary AND IsActive = 1;
END
GO

-- Step 5: US-07: Aggregate Statistics
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_EmployeeStats')
    DROP PROCEDURE sp_EmployeeStats;
GO

CREATE PROCEDURE sp_EmployeeStats
    @Total INT OUTPUT,
    @AvgSalary INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT @Total=COUNT(*),
           @AvgSalary=AVG(Salary)
    FROM Employees
    WHERE IsActive = 1;
END
GO

-- Step 6: US-08: Soft Delete
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeactivateEmployee')
    DROP PROCEDURE sp_DeactivateEmployee;
GO

CREATE PROCEDURE sp_DeactivateEmployee
    @EmpId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Employees
    SET IsActive=0
    WHERE EmpId=@EmpId;
    
    IF @@ROWCOUNT > 0
        PRINT 'Employee deactivated successfully.';
    ELSE
        PRINT 'Employee ID not found.';
END
GO

-- Step 7: US-09: Salary Audit Trigger
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_AuditSalary')
    DROP TRIGGER trg_AuditSalary;
GO

CREATE TRIGGER trg_AuditSalary
ON Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Audit only if salary has changed
    INSERT INTO EmployeeSalaryAudit(EmpId, OldSalary, NewSalary)
    SELECT d.EmpId, d.Salary, i.Salary
    FROM deleted d
    JOIN inserted i ON d.EmpId = i.EmpId
    WHERE d.Salary <> i.Salary;
END
GO

-- Step 9: US-13 + US-14: Function for Bonus Calculation
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'fn_Bonus')
    DROP FUNCTION fn_Bonus;
GO

CREATE FUNCTION fn_Bonus(@Salary INT)
RETURNS INT
AS
BEGIN
    -- Business Logic: 10% bonus
    RETURN @Salary * 10 / 100;
END
GO

-- Step 8: US-10: Cursor (Row by Row Processing) - Integrated into a SP
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ApplyAnnualBonus')
    DROP PROCEDURE sp_ApplyAnnualBonus;
GO

CREATE PROCEDURE sp_ApplyAnnualBonus
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @EmpId INT;
    DECLARE @CurrentSalary INT;
    DECLARE @BonusAmount INT;

    -- Declare cursor for active employees
    DECLARE emp_cursor CURSOR FOR
    SELECT EmpId, Salary FROM Employees WHERE IsActive = 1;

    OPEN emp_cursor;
    FETCH NEXT FROM emp_cursor INTO @EmpId, @CurrentSalary;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Calculate bonus using function (Step 9/US-14)
        SET @BonusAmount = dbo.fn_Bonus(@CurrentSalary);
        
        -- Update salary
        UPDATE Employees
        SET Salary = Salary + @BonusAmount
        WHERE EmpId = @EmpId;

        PRINT 'Applied bonus of ' + CAST(@BonusAmount AS VARCHAR(10)) + ' to Employee ID: ' + CAST(@EmpId AS VARCHAR(10));

        FETCH NEXT FROM emp_cursor INTO @EmpId, @CurrentSalary;
    END

    CLOSE emp_cursor;
    DEALLOCATE emp_cursor;
    
    PRINT 'Annual bonus processing completed.';
END
GO

-- ===============================================================================
-- DEMONSTRATION / TESTING SCRIPT
-- ===============================================================================
/*
-- 1. Add Employees
DECLARE @NewId1 INT, @NewId2 INT, @NewId3 INT;
EXEC sp_AddEmployee 'Alice Smith', 50000, @NewId1 OUTPUT;
EXEC sp_AddEmployee 'Bob Johnson', 60000, @NewId2 OUTPUT;
EXEC sp_AddEmployee 'Charlie Brown', 45000, @NewId3 OUTPUT;

-- 2. View Employees
SELECT * FROM Employees;

-- 3. Update Salary (Triggers Audit)
EXEC sp_UpdateSalary @EmpId = 1, @Salary = 55000;

-- 4. View Audit Log
SELECT * FROM EmployeeSalaryAudit;

-- 5. Get Stats
DECLARE @Count INT, @Avg INT;
EXEC sp_EmployeeStats @Total = @Count OUTPUT, @AvgSalary = @Avg OUTPUT;
SELECT @Count AS TotalActiveEmployees, @Avg AS AverageSalary;

-- 6. Apply Annual Bonus (Cursor + Function)
EXEC sp_ApplyAnnualBonus;

-- 7. View Final Data
SELECT * FROM Employees;

-- 8. Soft Delete
EXEC sp_DeactivateEmployee @EmpId = 3;
SELECT * FROM Employees;
*/
