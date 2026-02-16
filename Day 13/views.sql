USE TEST1;
select * from employees;
--Creating and using indexes in SQL Server is a powerful way to improve database performance.
--Before running any queries, enable these options
SET STATISTICS TIME ON;
SET STATISTICS IO ON;


--create index on column 
DBCC DROPCLEANBUFFERS

CREATE INDEX i_name
ON employees(salary);

select * from employees where salary=88000;

DROP INDEX i_name ON employees;

--two types of index
--clustered index---like primary key--only one index in clustered index
--non clustered index --we create,multiple non clustered index,act as pointer
