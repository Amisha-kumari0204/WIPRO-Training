document.getElementById('contactForm').addEventListener('submit', function(event) {
    event.preventDefault();
    
    // Clear previous errors
    clearErrors();
    
    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const message = document.getElementById('message').value.trim();
    
    let isValid = true;
    
    if (name === '') {
        showError('nameError', 'Name is required');
        isValid = false;
    }
    
    if (email === '') {
        showError('emailError', 'Email is required');
        isValid = false;
    } else if (!isValidEmail(email)) {
        showError('emailError', 'Please enter a valid email address');
        isValid = false;
    }
    
    if (message === '') {
        showError('messageError', 'Message is required');
        isValid = false;
    }
    
    if (isValid) {
        alert('Thank you, ' + name + '! Your message has been sent (demo).');
        document.getElementById('contactForm').reset();
    }
});

function showError(id, message) {
    document.getElementById(id).textContent = message;
}

function clearErrors() {
    const errors = document.querySelectorAll('.error');
    errors.forEach(error => error.textContent = '');
}

function isValidEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}
