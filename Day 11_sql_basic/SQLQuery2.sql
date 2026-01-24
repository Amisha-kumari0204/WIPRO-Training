
--Exercise:
--Write a query to change Grade of mishri from B to A.
--Add a new student to the table:
--Student_ID = 104, Name = 'Alex', Age = 11, Grade = D.
--Write a query to remove 'misha' from the table.
--Write a query to retrieve only the details for the student named 'mishri'.
--Write a query to print/get age of Amisha.
USE TEST1;

UPDATE students
  set grade='A'
  where grade='B';

INSERT INTO students VALUES (104,'Alex',11,'D');

DELETE FROM students
where name='misha';

Select * from students where name='mishri';

select age from students where name ='Amisha';

