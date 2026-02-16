"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Employee = exports.Person = void 0;
// 2. Base Class
class Person {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
}
exports.Person = Person;
// 3. Inheritance
class Employee extends Person {
    constructor(id, name, age, department, salary) {
        super(name, age);
        this.id = id;
        this.department = department;
        this.salary = salary;
    }
    // Typed Function (Method)
    getDetails() {
        return `[ID: ${this.id}] Name: ${this.name}, Dept: ${this.department}, Salary: ${this.salary}`;
    }
}
exports.Employee = Employee;
