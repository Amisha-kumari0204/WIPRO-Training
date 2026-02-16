"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const TaskService_1 = require("./services/TaskService");
const taskService = new TaskService_1.TaskService();
function listTasks(taskArray) {
    console.log("--- TaskFlowPro: Professional Workflow Active ---");
    taskArray.forEach(task => {
        const status = task.completed ? "[x]" : "[ ]";
        console.log(`${status} ${task.id}: ${task.title}`);
    });
}
taskService.addTask("Ensure CI/CD readiness");
listTasks(taskService.getAllTasks());
//# sourceMappingURL=index.js.map