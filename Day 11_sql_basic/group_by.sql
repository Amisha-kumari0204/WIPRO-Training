USE TEST1;
SELECT DB_NAME();
select * from  employees;

--group by
--Find number of employees in each department  
select department,count(emp_id) as count_emp from employees group by department order by count_emp;

--Find number of employees in each city  
select city,count(emp_id) as count_emp from employees group by city order by count_emp;

--Find average salary in each department
select department,
avg(salary) 
from employees 
group by department;

--multi level grouping likee harek department m different city k kitne employees h
select department,city, count(emp_id) as count_emp from employees group by department,city order by department ;
