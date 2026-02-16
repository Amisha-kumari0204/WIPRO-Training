USE TEST1;
SELECT DB_NAME();
select * from employees;
--A stored procedure is a precompiled SQL program stored in the database that performs a specific task.

--1. SP without parameters
CREATE PROCEDURE sp_get_employees
AS
BEGIN
    SELECT emp_id, first_name, lname, department, hire_date, city
    FROM employees
END;


EXEC sp_get_employees

--2. SP with INPUT parameters
CREATE PROCEDURE sp_parm_input
  @p_department VARCHAR(100)
AS
BEGIN
  select * from employees where department=@p_department
END;

EXEC sp_parm_input 'Tech'


----- HOW TO CHECK Existing SP -----

SELECT ROUTINE_NAME
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE'

 --check inside sp   
sp_helptext 'get_employees_sp'
