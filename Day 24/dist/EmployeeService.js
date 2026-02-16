"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EmployeeService = void 0;
class EmployeeService {
    constructor() {
        this.employees = [];
    }
    // Typed Function with return type
    addEmployee(employee) {
        this.employees.push(employee);
        console.log(`Employee ${employee.name} added successfully.`);
    }
    // Typed Function with optional parameter
    listEmployees(department) {
        console.log("\n--- Employee List ---");
        const filtered = department
            ? this.employees.filter(e => e.department === department)
            : this.employees;
        filtered.forEach(e => console.log(e.getDetails()));
    }
    // Typed Function with default parameter
    calculateTotalSalary(bonus = 0) {
        return this.employees.reduce((total, e) => total + e.salary, 0) + bonus;
    }
}
exports.EmployeeService = EmployeeService;
