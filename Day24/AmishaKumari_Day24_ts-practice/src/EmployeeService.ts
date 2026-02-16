import { IEmployee } from './EmployeeModels';

export class EmployeeService {
    private employees: IEmployee[] = [];

    // Typed Function with return type
    addEmployee(employee: IEmployee): void {
        this.employees.push(employee);
        console.log(`Employee ${employee.name} added successfully.`);
    }

    // Typed Function with optional parameter
    listEmployees(department?: string): void {
        console.log("\n--- Employee List ---");
        const filtered = department 
            ? this.employees.filter(e => e.department === department)
            : this.employees;
        
        filtered.forEach(e => console.log(e.getDetails()));
    }

    // Typed Function with default parameter
    calculateTotalSalary(bonus: number = 0): number {
        return this.employees.reduce((total, e) => total + e.salary, 0) + bonus;
    }
}
