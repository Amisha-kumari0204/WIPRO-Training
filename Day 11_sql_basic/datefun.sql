USE TEST1;
SELECT DB_NAME();
select * from employees;

--DATE FUNCTION GETDATE()
SELECT GETDATE();

--DATEADD(interval, number, date)
SELECT DATEADD(MONTH,2,GETDATE());

--DATEDIFF(interval, start_date, end_date)
SELECT DATEDIFF(MONTH,2026-09-23,GETDATE());

--DATEPART(interval, date) / YEAR(date), MONTH(date), DAY(date)
SELECT MONTH(GETDATE());
SELECT DAY(GETDATE());
SELECT YEAR(GETDATE());

--formats like 'MM/DD/YYYY' or 'DD-Mon-YYYY
SELECT FORMAT(GETDATE(),'MM-dd-yyyy');


-----------------exercise----------------------
--Find out each employee's 5-year anniversary date.
SELECT emp_id,hire_date,DATEADD(year,5,hire_date) as anniv from employees;

--Find employees hired in March.
SELECT * FROM employees where MONTH(hire_date)=3;

--Show the year, month, and day each employee was hired, separately.
SELECT emp_id, year(hire_date) as yearOfHire,month(hire_date) as monthOfHire, day(hire_date)  as dayOfHire from employees;

--Display the hire date in a standard US format (MM/dd/yyyy)
SELECT FORMAT(GETDATE(),'MM/dd/yyyy');




