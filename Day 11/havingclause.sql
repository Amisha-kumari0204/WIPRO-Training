USE TEST1;
SELECT DB_NAME();
select * from  employees;

--having clause
--Find Departments with More Than 2 Employees
select department,count(emp_id) as count_emp from employees group by department having count(emp_id)>2;
--Find Job Titles with an Average Salary Above 90000
select job_title,avg(salary) as count_avg from employees group by job_title  having avg(salary) >90000;
--Find department with Total Salary Above 300000
select department,sum(salary) as emp_salary from employees group by department having sum(salary)>300000;
