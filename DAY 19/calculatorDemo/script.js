// Calculator Demo - Advanced Calculator with History
const display = document.getElementById('display');
const historyContainer = document.getElementById('history');
let calculations = [];
let currentInput = '0';
let previousInput = '';
let operator = null;
let shouldResetDisplay = false;

// Initialize display
function initDisplay() {
    display.value = currentInput;
}

// Append number to display
function appendNumber(num) {
    if (shouldResetDisplay) {
        currentInput = num;
        shouldResetDisplay = false;
    } else {
        if (currentInput === '0' && num !== '.') {
            currentInput = num;
        } else if (num === '.' && currentInput.includes('.')) {
            return; // Prevent multiple decimal points
        } else {
            currentInput += num;
        }
    }
    updateDisplay();
}

// Append operator
function appendOperator(op) {
    if (currentInput === '') return;

    if (operator !== null && !shouldResetDisplay) {
        calculate(); // Calculate if there's a pending operation
    }

    previousInput = currentInput;
    operator = op;
    shouldResetDisplay = true;
}

// Calculate result
function calculate() {
    if (operator === null || previousInput === '' || currentInput === '') {
        return;
    }

    let result;
    const prev = parseFloat(previousInput);
    const current = parseFloat(currentInput);

    switch (operator) {
        case '+':
            result = prev + current;
            break;
        case '-':
            result = prev - current;
            break;
        case '*':
            result = prev * current;
            break;
        case '/':
            if (current === 0) {
                alert('Cannot divide by zero!');
                clearDisplay();
                return;
            }
            result = prev / current;
            break;
        default:
            return;
    }

    // Handle floating point precision
    result = Math.round(result * 100000000) / 100000000;

    // Add to history
    const calculation = `${prev} ${operator} ${current} = ${result}`;
    addToHistory(prev, operator, current, result);

    // Reset for next calculation
    currentInput = result.toString();
    operator = null;
    previousInput = '';
    shouldResetDisplay = true;
    updateDisplay();
}

// Clear display
function clearDisplay() {
    currentInput = '0';
    previousInput = '';
    operator = null;
    shouldResetDisplay = false;
    updateDisplay();
}

// Delete last character
function deleteLastChar() {
    if (currentInput.length === 1) {
        currentInput = '0';
    } else {
        currentInput = currentInput.slice(0, -1);
    }
    updateDisplay();
}

// Toggle sign
function toggleSign() {
    if (currentInput !== '0' && currentInput !== '') {
        currentInput = (parseFloat(currentInput) * -1).toString();
        updateDisplay();
    }
}

// Update display
function updateDisplay() {
    display.value = currentInput;
}

// Add to history
function addToHistory(num1, op, num2, result) {
    calculations.unshift({
        num1: num1,
        operator: op,
        num2: num2,
        result: result,
        timestamp: new Date().toLocaleTimeString()
    });

    // Keep only last 20 calculations
    if (calculations.length > 20) {
        calculations.pop();
    }

    updateHistoryDisplay();
}

// Update history display
function updateHistoryDisplay() {
    if (calculations.length === 0) {
        historyContainer.innerHTML = '<div class="empty-history">No calculations yet</div>';
        return;
    }

    historyContainer.innerHTML = calculations
        .map((calc, index) => `
            <div class="history-item" onclick="useFromHistory(${index})">
                <div class="calculation">${calc.num1} ${calc.operator} ${calc.num2}</div>
                <div class="result">${calc.result}</div>
                <div style="font-size: 11px; color: #999; margin-top: 4px;">${calc.timestamp}</div>
            </div>
        `)
        .join('');
}

// Use calculation from history
function useFromHistory(index) {
    const calc = calculations[index];
    currentInput = calc.result.toString();
    operator = null;
    previousInput = '';
    shouldResetDisplay = true;
    updateDisplay();
}

// Clear history
function clearHistory() {
    if (calculations.length === 0) {
        alert('History is already empty!');
        return;
    }
    if (confirm('Are you sure you want to clear history?')) {
        calculations = [];
        updateHistoryDisplay();
    }
}

// Keyboard support
document.addEventListener('keydown', (event) => {
    event.preventDefault();
    
    const key = event.key;

    if (key >= '0' && key <= '9') appendNumber(key);
    else if (key === '.') appendNumber('.');
    else if (key === '+' || key === '-') appendOperator(key);
    else if (key === '*') appendOperator('*');
    else if (key === '/') appendOperator('/');
    else if (key === 'Enter' || key === '=') calculate();
    else if (key === 'Backspace') deleteLastChar();
    else if (key === 'Escape') clearDisplay();
});

// Initialize on page load
window.addEventListener('DOMContentLoaded', () => {
    initDisplay();
    updateHistoryDisplay();
});
