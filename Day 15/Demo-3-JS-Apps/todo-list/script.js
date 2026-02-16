const todoInput = document.getElementById('todoInput');
const addBtn = document.getElementById('addBtn');
const todoList = document.getElementById('todoList');
const taskCount = document.getElementById('taskCount');
const clearCompletedBtn = document.getElementById('clearCompleted');

let tasks = [];

addBtn.addEventListener('click', addTask);
todoInput.addEventListener('keypress', (e) => {
    if (e.key === 'Enter') addTask();
});

function addTask() {
    const text = todoInput.value.trim();
    if (text === '') return;

    const task = {
        id: Date.now(),
        text: text,
        completed: false
    };

    tasks.push(task);
    todoInput.value = '';
    render();
}

function deleteTask(id) {
    tasks = tasks.filter(task => task.id !== id);
    render();
}

function toggleComplete(id) {
    tasks = tasks.map(task => {
        if (task.id === id) {
            return { ...task, completed: !task.completed };
        }
        return task;
    });
    render();
}

clearCompletedBtn.addEventListener('click', () => {
    tasks = tasks.filter(task => !task.completed);
    render();
});

function render() {
    todoList.innerHTML = '';
    
    tasks.forEach(task => {
        const li = document.createElement('li');
        if (task.completed) li.classList.add('completed');
        
        li.innerHTML = `
            <span class="task-text">${task.text}</span>
            <i class="fas fa-trash delete-btn"></i>
        `;
        
        li.querySelector('.task-text').addEventListener('click', () => toggleComplete(task.id));
        li.querySelector('.delete-btn').addEventListener('click', (e) => {
            e.stopPropagation();
            deleteTask(task.id);
        });
        
        todoList.appendChild(li);
    });
    
    const remainingCount = tasks.filter(t => !t.completed).length;
    taskCount.innerText = `${remainingCount} task${remainingCount !== 1 ? 's' : ''} left`;
}
