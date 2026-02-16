/**
 * TaskFlowPro Entry Point
 * 
 * This file serves as the main entry point for the TaskFlowPro application.
 * It demonstrates the basic setup and confirms that the TypeScript tooling is working correctly.
 */

import { Task } from "./models/Task";
import { TaskService } from "./services/TaskService";

const taskService = new TaskService();

function listTasks(taskArray: Task[]): void {
    console.log("--- TaskFlowPro: Professional Workflow Active ---");
    taskArray.forEach(task => {
        const status = task.completed ? "[x]" : "[ ]";
        console.log(`${status} ${task.id}: ${task.title}`);
    });
}

// Add a new task to demonstrate functionality
taskService.addTask("Ensure CI/CD readiness");

listTasks(taskService.getAllTasks());

