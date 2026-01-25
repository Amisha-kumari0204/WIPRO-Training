EXEC sp_databases;
USE TEST1;
--EXERCISE
--Find different type of departments in database?
Select distinct department from  employees;

--Display records with High-low salary
Select * from employees order by salary desc;

--How to see only top 3 records from a table?
select TOP 3 * from employees order by emp_id DESC;

--Show records where first name start with letter 'A'
select * from employees  where fname like 'A%';

--Show records where length of the lname is 4 characters
select * from employees  where LEN(lname)=4;



