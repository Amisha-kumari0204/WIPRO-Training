USE TEST1;
SELECT DB_NAME();
--- Aggregate Functions ---
SELECT COUNT(emp_id) AS count_of_emp FROM employees
SELECT MIN(salary) AS min_salary FROM employees
SELECT MAX(salary) AS max_salary FROM employees
SELECT AVG(salary) AS  avg_salary FROM employees
SELECT SUM(salary) AS total_salary FROM employees
