--list down existing databases
EXEC sp_databases;
SELECT name  from sys.databases;

--create database
CREATE DATABASE TEST1;


--use database
USE TEST1;

--show database which i m using currently
SELECT DB_NAME();

--CREATE A TABLE
CREATE TABLE students(
   Id INT primary key,
   Name VARCHAR(100),
   Age INT NOT NULL,
   GRADE CHAR(1)
   );

---insert values  
INSERT INTO students VALUES(1,'Amisha',25,'A');
INSERT INTO students
VALUES
(2,'Amisha',25,'A'),
(3,'misha',24,'B'),
(4,'mishri',21,'B'),
(5,'AmishaKri',25,'C');

--show all databases valuess
SELECT * from students;
