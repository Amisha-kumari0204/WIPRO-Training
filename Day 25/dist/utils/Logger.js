"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Logger = void 0;
class Logger {
    static info(message) {
        console.log(`[INFO] [${new Date().toISOString()}]: ${message}`);
    }
    static error(message, error) {
        console.error(`[ERROR] [${new Date().toISOString()}]: ${message}`, error || "");
    }
    static warn(message) {
        console.warn(`[WARN] [${new Date().toISOString()}]: ${message}`);
    }
}
exports.Logger = Logger;
//# sourceMappingURL=Logger.js.map