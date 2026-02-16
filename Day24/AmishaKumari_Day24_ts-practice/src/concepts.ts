// 🔹 a) Types & Type Inference
let username: string = "Amisha";
let age: number = 22;
let isLoggedIn: boolean = true;

// Type inference
let salary = 50000; // TypeScript infers 'number'

// Error check: Uncommenting the next line will throw a compile-time error
// username = 22; // Error: Type 'number' is not assignable to type 'string'.


// 🔹 b) Interfaces
interface Employee {
  id: number;
  name: string;
  department: string;
}

const emp: Employee = {
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
function greet(name: string, age?: number): string {
  return age ? `Hello ${name}, Age: ${age}` : `Hello ${name}`;
}

function multiply(a: number, b: number = 2): number {
  return a * b;
}

console.log(greet("Amisha"));
console.log(greet("John", 25));
console.log(multiply(5)); // Default parameter used: 10
console.log(multiply(5, 3)); // 15


// 🔹 d) Classes + Access Modifiers
class Person {
  private name: string;
  protected age: number;

  constructor(name: string, age: number){
    this.name = name;
    this.age = age;
  }

  public introduce(): void {
    console.log(`Hi I am ${this.name}`);
  }
}

const person = new Person("Alice", 30);
person.introduce();
// person.name; // Error: Property 'name' is private


// 🔹 e) Inheritance
class CompanyEmployee extends Person {
  empId: number;

  constructor(name: string, age: number, empId: number){
    super(name, age);
    this.empId = empId;
  }

  public getDetails(): string {
    // age is protected, so it's accessible in the subclass
    return `ID: ${this.empId}, Age: ${this.age}`;
  }
}

const companyEmp = new CompanyEmployee("Bob", 28, 1001);
companyEmp.introduce();
console.log(companyEmp.getDetails());


// 🔹 f) Interface + Class Implementation
interface Shape {
  area(): number;
}

class Rectangle implements Shape {
  constructor(private w: number, private h: number){}

  area(): number {
    return this.w * this.h;
  }
}

const rect = new Rectangle(10, 5);
console.log(`Rectangle Area: ${rect.area()}`);
