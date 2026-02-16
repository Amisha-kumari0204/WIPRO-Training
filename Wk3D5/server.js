const http = require('http');
const fs = require('fs');
const path = require('path');

const PORT = 8000;
const HOST = 'localhost';

const server = http.createServer((req, res) => {
    // Set default to index.html or StudentManagement_Sprint2.html
    let filePath = req.url === '/' ? '/StudentManagement_Sprint2.html' : req.url;
    filePath = path.join(__dirname, filePath);

    // Read the file
    fs.readFile(filePath, (err, data) => {
        if (err) {
            if (err.code === 'ENOENT') {
                res.writeHead(404, { 'Content-Type': 'text/html' });
                res.write('<h1>404 - File Not Found</h1>');
            } else {
                res.writeHead(500);
                res.write('Server Error');
            }
            res.end();
            return;
        }

        // Determine content type
        const ext = path.extname(filePath).toLowerCase();
        let contentType = 'text/html';
        if (ext === '.css') contentType = 'text/css';
        else if (ext === '.js') contentType = 'text/javascript';
        else if (ext === '.json') contentType = 'application/json';
        else if (ext === '.png') contentType = 'image/png';
        else if (ext === '.jpg' || ext === '.jpeg') contentType = 'image/jpeg';
        else if (ext === '.gif') contentType = 'image/gif';
        else if (ext === '.svg') contentType = 'image/svg+xml';

        res.writeHead(200, { 'Content-Type': contentType });
        res.end(data);
    });
});

server.listen(PORT, HOST, () => {
    console.log(`
╔════════════════════════════════════════════════════════════╗
║  Spring Flowers School - Student Management Portal        ║
║  Server running at http://${HOST}:${PORT}                    ║
║  File: StudentManagement_Sprint2.html                     ║
║  Press Ctrl+C to stop the server                          ║
╚════════════════════════════════════════════════════════════╝
    `);
});

server.on('error', (err) => {
    if (err.code === 'EADDRINUSE') {
        console.error(`Port ${PORT} is already in use. Try another port.`);
    } else {
        console.error('Server error:', err);
    }
    process.exit(1);
});
