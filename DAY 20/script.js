const API_URLS = {
    todos: 'https://jsonplaceholder.typicode.com/todos/',
    comments: 'https://jsonplaceholder.typicode.com/comments',
    users: 'https://jsonplaceholder.typicode.com/users'
};

function showLoading() {
    document.getElementById('loading').style.display = 'block';
    document.getElementById('error').style.display = 'none';
    document.getElementById('content').innerHTML = '';
}

function hideLoading() {
    document.getElementById('loading').style.display = 'none';
}

function showError(message) {
    document.getElementById('error').style.display = 'block';
    document.getElementById('error').innerText = 'Error: ' + message;
    hideLoading();
}

async function fetchTodos() {
    showLoading();
    try {
        const response = await fetch(API_URLS.todos);
        if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
        
        const todos = await response.json();
        displayTodos(todos);
        hideLoading();
    } catch (error) {
        showError('Failed to fetch todos: ' + error.message);
    }
}

async function fetchComments() {
    showLoading();
    try {
        const response = await fetch(API_URLS.comments);
        if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
        
        const comments = await response.json();
        displayComments(comments);
        hideLoading();
    } catch (error) {
        showError('Failed to fetch comments: ' + error.message);
    }
}

async function fetchUsers() {
    showLoading();
    try {
        const response = await fetch(API_URLS.users);
        if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
        
        const users = await response.json();
        displayUsers(users);
        hideLoading();
    } catch (error) {
        showError('Failed to fetch users: ' + error.message);
    }
}

function displayTodos(todos) {
    let html = `<h2 style="margin-bottom: 20px; color: #333;">Todos (${todos.length} total)</h2>`;
    html += '<div class="data-grid">';
    
    todos.forEach(todo => {
        const statusClass = todo.completed ? 'status-completed' : 'status-pending';
        const statusText = todo.completed ? 'Completed' : 'Pending';
        
        html += `
            <div class="card">
                <div class="card-title">${escapeHtml(todo.title)}</div>
                <div class="card-content">
                    <p><span class="label">ID:</span><span class="value">${todo.id}</span></p>
                    <p><span class="label">User ID:</span><span class="value">${todo.userId}</span></p>
                    <span class="status-badge ${statusClass}">${statusText}</span>
                </div>
            </div>
        `;
    });
    
    html += '</div>';
    document.getElementById('content').innerHTML = html;
}

function displayComments(comments) {
    let html = `<h2 style="margin-bottom: 20px; color: #333;">Comments (${comments.length} total)</h2>`;
    html += '<div class="data-grid">';
    
    comments.forEach(comment => {
        html += `
            <div class="card">
                <div class="card-title">${escapeHtml(comment.name)}</div>
                <div class="card-content">
                    <p><span class="label">Email:</span><span class="value">${escapeHtml(comment.email)}</span></p>
                    <p><span class="label">Post ID:</span><span class="value">${comment.postId}</span></p>
                    <p><span class="label">Comment ID:</span><span class="value">${comment.id}</span></p>
                    <p><strong>Comment:</strong></p>
                    <p style="font-style: italic; color: #777;">${escapeHtml(comment.body)}</p>
                </div>
            </div>
        `;
    });
    
    html += '</div>';
    document.getElementById('content').innerHTML = html;
}

function displayUsers(users) {
    let html = `<h2 style="margin-bottom: 20px; color: #333;">Users (${users.length} total)</h2>`;
    html += '<div class="table-container">';
    html += `
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>Website</th>
                    <th>Company</th>
                </tr>
            </thead>
            <tbody>
    `;
    
    users.forEach(user => {
        html += `
            <tr>
                <td>${user.id}</td>
                <td>${escapeHtml(user.name)}</td>
                <td>${escapeHtml(user.email)}</td>
                <td>${escapeHtml(user.phone)}</td>
                <td>${escapeHtml(user.website)}</td>
                <td>${escapeHtml(user.company.name)}</td>
            </tr>
        `;
    });
    
    html += `
            </tbody>
        </table>
    </div>
    `;
    document.getElementById('content').innerHTML = html;
}

function escapeHtml(text) {
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };
    return text.replace(/[&<>"']/g, m => map[m]);
}
