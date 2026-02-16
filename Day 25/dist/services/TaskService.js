"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TaskService = void 0;
const Logger_1 = require("../utils/Logger");
class TaskService {
    tasks = [
        { id: 1, title: "Set up project structure", completed: true },
        { id: 2, title: "Configure tsconfig.json", completed: true },
        { id: 3, title: "Implement npm scripts", completed: true },
        { id: 4, title: "Verify project structure", completed: true },
    ];
    getAllTasks() {
        Logger_1.Logger.info(`Fetching all tasks. Count: ${this.tasks.length}`);
        return this.tasks;
    }
    addTask(title) {
        const newTask = {
            id: this.tasks.length + 1,
            title,
            completed: false
        };
        this.tasks.push(newTask);
        Logger_1.Logger.info(`Added new task: "${title}" with ID: ${newTask.id}`);
    }
}
exports.TaskService = TaskService;
//# sourceMappingURL=TaskService.js.map