--trigerss
--A trigger in MSSQL is a special type of stored procedure that automatically executes (fires) in response to specific events on a table or view.
USE TEST1;

CREATE TRIGGER trg_AfterInsert_Employee
ON Employees
AFTER INSERT
AS
BEGIN
    PRINT 'Record inserted successfully';
END;


DISABLE TRIGGER trg_AfterInsert_Employee ON Employees;
ENABLE TRIGGER trg_AfterInsert_Employee ON Employees;

DROP TRIGGER trg_AuditUpdate;

