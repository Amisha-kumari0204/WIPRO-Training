USE TEST1;
SELECT DB_NAME();
SELECT * from employees;

--A window function performs a calculation across a set of table rows that are related to the current row, without collapsing rows like GROUP BY.
-- You still get each row, plus an extra calculated column.
SELECT emp_id,first_name,salary,SUM(salary) OVER() as sum_salary from employees;
SELECT emp_id,first_name,salary,SUM(salary) OVER(ORDER BY salary) as sum_salary from employees;
SELECT emp_id,first_name,salary,department,job_title,SUM(salary) OVER(partition by job_title) as sum_salary from employees;

SELECT
    ROW_NUMBER() OVER (ORDER BY emp_id) AS row_no,
    emp_id,
    first_name,
    department
FROM employees;


SELECT
    ROW_NUMBER() OVER (ORDER BY salary) AS row_no,
    emp_id,
    first_name,
    department,salary
FROM employees;

SELECT
    RANK() OVER (ORDER BY department) AS row_no,
    emp_id,
    department,
    salary
FROM employees;

--lag column pe apply hoga
SELECT
    LAG(salary) OVER (ORDER BY department) AS row_no,
    emp_id,
    department,
    salary
FROM employees;

--lead column pe apply hoga
SELECT
    LAG(salary) OVER (ORDER BY salary) AS row_no,
    emp_id,
    department,
    salary
FROM employees;
