USE TEST1;
SELECT DB_NAME();
SELECT * from employees;
---string function
SELECT CONCAT(fname,'-',lname) from employees;

SELECT SUBSTRING('HELLO AMISHA',1,4);

SELECT REPLACE(department,'Human Resources','HR') as depart from employees;

SELECT REVERSE(fname) FROM employees;

SELECT LEN('amisha02');

SELECT UPPER(fname) as fnamee from employees;

SELECT LOWER(fname) as fnamee from employees;

SELECT LEFT('AMISHAA',3);

SELECT RIGHT('AMISHAA',3);

SELECT RTRIM(' Amifqhaaa  !  ');

SELECT LTRIM(' amishaaa ');

SELECT CHARINDEX('sh','mishri');


