# TaskFlowPro

Professional TypeScript application for task management.

## Project Overview

TaskFlowPro is a scalable TypeScript application built with a focus on professional tooling, consistent build processes, and easy debugging.

## Features

- **Strict TypeScript Configuration**: Enforces type safety across the codebase.
- **Modular Architecture**: Organized into models, services, and utilities.
- **Source Maps**: Enabled for seamless debugging of TypeScript code.
- **Pre-configured VS Code Debugging**: Ready-to-use launch configurations.

## Getting Started

### Prerequisites

- Node.js (v16+)
- npm

### Installation

```bash
npm install
```

### Available Scripts

- `npm run dev`: Runs the application in development mode using `ts-node`.
- `npm run build`: Transpiles TypeScript to JavaScript in the `dist` folder.
- `npm start`: Runs the transpiled JavaScript application.
- `npm test`: Runs the basic verification test suite.
- `npm run clean`: Removes the `dist` folder.
- `npm run rebuild`: Cleans and builds the project.

## Debugging

This project includes a `.vscode/launch.json` file with two configurations:
1. **Debug TaskFlowPro (ts-node)**: Debug the source TypeScript code directly.
2. **Debug Compiled TaskFlowPro**: Debug the transpiled JavaScript with source maps.

## Folder Structure

- `src/`: Source code.
  - `models/`: Interface and type definitions.
  - `services/`: Business logic and data management.
  - `utils/`: Shared utilities and helper functions.
  - `index.ts`: Application entry point.
- `dist/`: Transpiled JavaScript output (generated after build).
- `tests/`: Basic validation and testing scripts.
- `.vscode/`: IDE specific configurations.
