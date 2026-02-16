import { TaskService } from "../src/services/TaskService";
import { Logger } from "../src/utils/Logger";

/**
 * Simple validation script for TaskService
 */
function runTests() {
    Logger.info("Starting TaskFlowPro Tests...");
    
    const service = new TaskService();
    const initialCount = service.getAllTasks().length;
    
    service.addTask("Test Task");
    const newCount = service.getAllTasks().length;

    if (newCount === initialCount + 1) {
        Logger.info("TEST PASSED: Task added successfully.");
    } else {
        Logger.error("TEST FAILED: Task was not added.");
        process.exit(1);
    }

    Logger.info("All tests completed successfully.");
}

runTests();
