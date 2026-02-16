Requirements:

--Each SELECT must have the same number of columns.
--Corresponding columns must have compatible data types.
--Column names are taken from the first SELECT.

--Union vs Union ALL

Union removes the duplicates from combined result

Mumbai

EmployeeID | Name | Department
101 | Amit Sharma | Sales
102 | Priya Singh | Marketing
103 | Rohan Gupta | IT

Delhi

EmployeeID | Name | Department
201 | Sonia Verma | Sales
202 | Karan Mehta | Finance
103 | Rohan Gupta | IT

UNION – Mum & Delhi

EmployeeID | Name | Department
101 | Amit Sharma | Sales
102 | Priya Singh | Marketing
103 | Rohan Gupta | IT
201 | Sonia Verma | Sales
202 | Karan Mehta | Finance
103 | Rohan Gupta | IT