import { Task } from "../models/Task";
import { Logger } from "../utils/Logger";

export class TaskService {
    private tasks: Task[] = [
        { id: 1, title: "Set up project structure", completed: true },
        { id: 2, title: "Configure tsconfig.json", completed: true },
        { id: 3, title: "Implement npm scripts", completed: true },
        { id: 4, title: "Verify project structure", completed: true },
    ];

    public getAllTasks(): Task[] {
        Logger.info(`Fetching all tasks. Count: ${this.tasks.length}`);
        return this.tasks;
    }

    public addTask(title: string): void {
        const newTask: Task = {
            id: this.tasks.length + 1,
            title,
            completed: false
        };
        this.tasks.push(newTask);
        Logger.info(`Added new task: "${title}" with ID: ${newTask.id}`);
    }
}

