USE TEST1;
SELECT DB_NAME();

--ALTER TABLE--
--ADD Column
ALTER TABLE employees
ADD phn_no int;

--DROP column
ALTER TABLE employees
DROP COLUMN phn_no;

--change datatype of column
ALTER TABLE employees
ALTER COLUMN phn_no VARCHAR(100);

---changing column name
EXEC sp_rename
'employees.fname','first_name', 'COLUMN';

---changing table name
EXEC sp_rename
'staff','employees';

select * from  staff;

--how to set default constraint
ALTER TABLE employees
ADD CONSTRAINT default_val DEFAULT 'trainee'
FOR department;

---check constraint
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    StartDate DATE,
    EndDate DATE,
    CONSTRAINT CK_Orders_DateRange CHECK (EndDate >= StartDate)
);


ALTER TABLE Employees
ADD CONSTRAINT CK_Employees_Age CHECK (Age >= 18);


