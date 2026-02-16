use test_relational;
SELECT DB_NAME();

--Every row from one table is combined with every row from another table.

SELECT * FROM 
Customers
CROSS JOIN Orders;

SELECT * FROM 
Customers;

SELECT * FROM 
Orders;


--Returns only the rows where there is a match between the specified columns in both the left (or first) and right (or second) tables.
SELECT * from Customers c INNER JOIN Orders o
ON c.customer_id=o.customer_id;

--All rows from the left table are returned, and the matching rows from the right table (if no match, right side is NULL).
SELECT * from Customers c LEFT JOIN Orders o
ON c.customer_id=o.customer_id;

--All rows from the right table are returned, and the matching rows from the left table (if no match, left side is NULL).
SELECT * from Customers c RIGHT JOIN Orders o
ON c.customer_id=o.customer_id;

--Full Outer Join**Returns all rows when there is a match in either the left or right table.
SELECT * from Customers c FULL OUTER JOIN Orders o
ON c.customer_id=o.customer_id;