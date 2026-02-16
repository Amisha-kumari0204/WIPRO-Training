-- 1. Setup: Clear existing data for a fresh test
-- DELETE FROM EmployeeSalaryAudit;
-- DELETE FROM Employees;
-- DBCC CHECKIDENT ('Employees', RESEED, 0);
-- DBCC CHECKIDENT ('EmployeeSalaryAudit', RESEED, 0);

PRINT '--- STARTING TEST SCENARIOS ---';

-- 2. US-01 & US-02: Add Employees with Output Parameters
PRINT 'Scenario 1: Adding Employees';
DECLARE @Id1 INT, @Id2 INT, @Id3 INT, @Id4 INT;

EXEC sp_AddEmployee 'John Doe', 50000, @Id1 OUTPUT;
EXEC sp_AddEmployee 'Jane Smith', 65000, @Id2 OUTPUT;
EXEC sp_AddEmployee 'Sam Wilson', 40000, @Id3 OUTPUT;
EXEC sp_AddEmployee 'Error Case', -100, @Id4 OUTPUT; -- Should fail validation

-- 3. US-05: Get Employee by ID
PRINT 'Scenario 2: Fetching Employee by ID';
EXEC sp_GetEmployeeById @EmpId = 1;

-- 4. US-06: High Salary Filter
PRINT 'Scenario 3: Employees with Salary > 45000';
EXEC sp_HighSalary @Salary = 45000;

-- 5. US-03 & US-04 & US-09: Update Salary, Transactions, and Audit Trigger
PRINT 'Scenario 4: Updating Salary and checking Audit Trigger';
EXEC sp_UpdateSalary @EmpId = 1, @Salary = 52000;
EXEC sp_UpdateSalary @EmpId = 2, @Salary = 68000;

PRINT 'Audit Log After Updates:';
SELECT * FROM EmployeeSalaryAudit;

-- 6. US-07: Statistics
PRINT 'Scenario 5: Aggregate Statistics';
DECLARE @TotalEmp INT, @AvgSal INT;
EXEC sp_EmployeeStats @Total = @TotalEmp OUTPUT, @AvgSalary = @AvgSal OUTPUT;
SELECT @TotalEmp AS TotalActive, @AvgSal AS AverageSalary;

-- 7. US-10 & US-13 & US-14: Cursor and Bonus Function
PRINT 'Scenario 6: Applying Annual Bonus via Cursor and Function';
EXEC sp_ApplyAnnualBonus;

PRINT 'Employee Table After Bonus:';
SELECT * FROM Employees;

-- 8. US-08: Soft Delete
PRINT 'Scenario 7: Soft Delete (Deactivation)';
EXEC sp_DeactivateEmployee @EmpId = 3;

PRINT 'Final Employee Table State:';
SELECT * FROM Employees;

PRINT '--- TEST SCENARIOS COMPLETED ---';
