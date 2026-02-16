"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const EmployeeModels_1 = require("./EmployeeModels");
const EmployeeService_1 = require("./EmployeeService");
// Initialize Service
const service = new EmployeeService_1.EmployeeService();
// Create Employees
const emp1 = new EmployeeModels_1.Employee(1, "Amisha Kumari", 22, "Engineering", 75000);
const emp2 = new EmployeeModels_1.Employee(2, "John Doe", 30, "HR", 50000);
const emp3 = new EmployeeModels_1.Employee(3, "Jane Smith", 28, "Engineering", 80000);
// Add Employees
service.addEmployee(emp1);
service.addEmployee(emp2);
service.addEmployee(emp3);
// List all
service.listEmployees();
// List by department (Optional parameter)
service.listEmployees("Engineering");
// Calculate total salary (Default parameter)
console.log(`\nTotal Base Salary: ${service.calculateTotalSalary()}`);
console.log(`Total Salary with Bonus (5000): ${service.calculateTotalSalary(5000)}`);
