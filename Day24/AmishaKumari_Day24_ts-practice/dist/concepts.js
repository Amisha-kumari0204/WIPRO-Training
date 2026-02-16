"use strict";
// 🔹 a) Types & Type Inference
let username = "Amisha";
let age = 22;
let isLoggedIn = true;
// Type inference
let salary = 50000; // TypeScript infers 'number'
const emp = {
    id: 101,
    name: "John",
    department: "IT"
};
// Error check: Removing any property will throw an error
/*
const incompleteEmp: Employee = {
  id: 102,
  name: "Jane"
}; // Error: Property 'department' is missing
*/
// 🔹 c) Typed Functions
function greet(name, age) {
    return age ? `Hello ${name}, Age: ${age}` : `Hello ${name}`;
}
function multiply(a, b = 2) {
    return a * b;
}
console.log(greet("Amisha"));
console.log(greet("John", 25));
console.log(multiply(5)); // Default parameter used: 10
console.log(multiply(5, 3)); // 15
// 🔹 d) Classes + Access Modifiers
class Person {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
    introduce() {
        console.log(`Hi I am ${this.name}`);
    }
}
const person = new Person("Alice", 30);
person.introduce();
// person.name; // Error: Property 'name' is private
// 🔹 e) Inheritance
class CompanyEmployee extends Person {
    constructor(name, age, empId) {
        super(name, age);
        this.empId = empId;
    }
    getDetails() {
        // age is protected, so it's accessible in the subclass
        return `ID: ${this.empId}, Age: ${this.age}`;
    }
}
const companyEmp = new CompanyEmployee("Bob", 28, 1001);
companyEmp.introduce();
console.log(companyEmp.getDetails());
class Rectangle {
    constructor(w, h) {
        this.w = w;
        this.h = h;
    }
    area() {
        return this.w * this.h;
    }
}
const rect = new Rectangle(10, 5);
console.log(`Rectangle Area: ${rect.area()}`);
