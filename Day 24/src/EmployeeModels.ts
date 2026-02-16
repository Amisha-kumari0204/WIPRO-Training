// 1. Interface
export interface IEmployee {
    id: number;
    name: string;
    department: string;
    salary: number;
    getDetails(): string;
}

// 2. Base Class
export class Person {
    constructor(public name: string, protected age: number) {}
}

// 3. Inheritance
export class Employee extends Person implements IEmployee {
    constructor(
        public id: number,
        name: string,
        age: number,
        public department: string,
        public salary: number
    ) {
        super(name, age);
    }

    // Typed Function (Method)
    getDetails(): string {
        return `[ID: ${this.id}] Name: ${this.name}, Dept: ${this.department}, Salary: ${this.salary}`;
    }
}
