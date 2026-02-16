/**
 * app.js - Main SPA Controller (US-SPA-01, US-SPA-02, US-DOM-01, US-DOM-02)
 */

document.addEventListener('DOMContentLoaded', () => {
    // Selectors
    const appContent = document.getElementById('app-content');
    const navLinks = document.querySelectorAll('[data-link]');

    /**
     * Router function to handle navigation (US-SPA-02)
     */
    const router = async () => {
        const routes = {
            '#home': renderHome,
            '#services': renderServices,
            '#booking': renderBooking,
            '#about': renderAbout
        };

        const path = window.location.hash || '#home';
        const renderer = routes[path] || renderHome;

        // Update active link
        navLinks.forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('href') === path) {
                link.classList.add('active');
            }
        });

        // Add fade-out effect
        appContent.classList.add('fade-in');
        
        // Render the content
        appContent.innerHTML = await renderer();
        
        // Attach event listeners for the new content (US-JQ-01)
        attachDynamicEventListeners();
        
        // Remove fade effect after short delay
        setTimeout(() => appContent.classList.remove('fade-in'), 500);
    };

    /**
     * Render Home Page (US-HTML-01, US-HTML-04)
     */
    const renderHome = async () => {
        return `
            <section class="hero-section">
                <div class="container text-center">
                    <h1 class="display-3 fw-bold mb-4">Your Home, Our Priority</h1>
                    <p class="lead mb-5">Book trusted local professionals for all your home needs.</p>
                    <div class="d-flex justify-content-center gap-3">
                        <a href="#services" class="btn btn-primary btn-lg rounded-pill px-4">Browse Services</a>
                        <a href="#booking" class="btn btn-outline-light btn-lg rounded-pill px-4">Book Now</a>
                    </div>
                </div>
            </section>
            
            <section class="container py-5">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <h2 class="mb-4">Why Choose SmartServe?</h2>
                        <ul class="list-group list-group-flush mb-4">
                            <li class="list-group-item bg-transparent border-0 d-flex align-items-start">
                                <i class="fas fa-check-circle text-primary me-3 mt-1"></i>
                                <div><strong>Verified Professionals:</strong> All our experts are background checked.</div>
                            </li>
                            <li class="list-group-item bg-transparent border-0 d-flex align-items-start">
                                <i class="fas fa-clock text-primary me-3 mt-1"></i>
                                <div><strong>On-Time Service:</strong> We value your time as much as you do.</div>
                            </li>
                            <li class="list-group-item bg-transparent border-0 d-flex align-items-start">
                                <i class="fas fa-shield-alt text-primary me-3 mt-1"></i>
                                <div><strong>Safe & Secure:</strong> Modern SPA architecture ensures performance.</div>
                            </li>
                        </ul>
                        <!-- US-HTML-04: Video Feature -->
                        <div class="ratio ratio-16x9 rounded-custom shadow-sm mt-4">
                            <iframe src="https://www.youtube.com/embed/dQw4w9WgXcQ" title="Service Demo" allowfullscreen></iframe>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <img src="https://images.unsplash.com/photo-1581092918056-0c4c3acd3789?auto=format&fit=crop&w=800&q=80" alt="Professional at work" class="img-fluid rounded-custom shadow">
                    </div>
                </div>
            </section>
        `;
    };

    /**
     * Render Services Page (US-BS-01, US-JS-04, US-AJS-01, US-AJS-02)
     */
    const renderServices = async () => {
        const services = await ServiceModule.fetchServices();
        
        // Using Map/Reduce or Filter for dynamic listings (US-JS-04)
        const categories = ['All', ...new Set(services.map(s => s.category))];
        
        const categoryFilterHtml = categories.map(cat => `
            <button class="btn btn-outline-primary btn-sm rounded-pill px-3 filter-btn" data-category="${cat}">
                ${cat}
            </button>
        `).join('');

        const servicesHtml = services.map(({ name, category, description, price, image, rating }) => `
            <div class="col-md-4 mb-4 service-item" data-category="${category}">
                <div class="card h-100 service-card shadow-sm">
                    <img src="${image}" class="card-img-top" alt="${name}">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center mb-2">
                            <span class="badge bg-info text-dark">${category}</span>
                            <small class="text-warning"><i class="fas fa-star me-1"></i>${rating}</small>
                        </div>
                        <h5 class="card-title">${name}</h5>
                        <p class="card-text text-muted">${description}</p>
                        <div class="d-flex justify-content-between align-items-center mt-auto">
                            <span class="h5 mb-0">₹${price}</span>
                            <a href="#booking" class="btn btn-primary btn-sm rounded-pill">Book Now</a>
                        </div>
                    </div>
                </div>
            </div>
        `).join('');

        return `
            <div class="container py-5">
                <div class="text-center mb-5">
                    <h2 class="fw-bold">Our Services</h2>
                    <p class="text-muted">Explore our wide range of professional local services.</p>
                </div>
                
                <div class="d-flex justify-content-center gap-2 mb-5 flex-wrap">
                    ${categoryFilterHtml}
                </div>

                <div class="row" id="services-container">
                    ${servicesHtml}
                </div>
            </div>
        `;
    };

    /**
     * Render Booking Page (US-HTML-03, US-BS-03, US-AJS-05)
     */
    const renderBooking = async () => {
        // US-AJS-05: Using Date objects for default values
        const today = new Date().toISOString().split('T')[0];
        
        return `
            <div class="container py-5">
                <div class="row justify-content-center">
                    <div class="col-md-8">
                        <div class="booking-form shadow">
                            <h2 class="text-center mb-4">Book a Service</h2>
                            <form id="bookingForm" class="needs-validation" novalidate>
                                <div class="row g-3">
                                    <div class="col-md-6">
                                        <label for="fullName" class="form-label">Full Name</label>
                                        <input type="text" class="form-control" id="fullName" required>
                                        <div class="invalid-feedback">Please provide your name.</div>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="email" class="form-label">Email</label>
                                        <input type="email" class="form-control" id="email" required>
                                        <div class="invalid-feedback">Valid email is required.</div>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="serviceType" class="form-label">Service Required</label>
                                        <select class="form-select" id="serviceType" required>
                                            <option value="" selected disabled>Choose...</option>
                                            <option>Appliances</option>
                                            <option>Plumbing</option>
                                            <option>Beauty</option>
                                            <option>Cleaning</option>
                                            <option>Fitness</option>
                                            <option>Maintenance</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="bookingDate" class="form-label">Preferred Date</label>
                                        <input type="date" class="form-control" id="bookingDate" min="${today}" value="${today}" required>
                                    </div>
                                    <div class="col-12">
                                        <label for="address" class="form-label">Service Address</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="address" placeholder="1234 Main St" required>
                                            <button class="btn btn-outline-secondary" type="button" id="detectLocation">
                                                <i class="fas fa-location-crosshairs"></i>
                                            </button>
                                        </div>
                                        <small class="text-muted" id="locationStatus"></small>
                                    </div>
                                    <div class="col-12 mt-4">
                                        <button class="btn btn-primary w-100 btn-lg rounded-pill" type="submit">Submit Booking</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        `;
    };

    /**
     * Render About Page (US-HTML-01)
     */
    const renderAbout = async () => {
        return `
            <div class="container py-5">
                <div class="row align-items-center">
                    <div class="col-md-6">
                        <h2 class="fw-bold mb-4">About SmartServe</h2>
                        <p class="lead">SmartServe is a modern SPA dedicated to making local service discovery and booking seamless.</p>
                        <p>Our platform leverages the latest web technologies including HTML5, CSS3, and Advanced JavaScript to provide a fast, responsive, and secure experience.</p>
                        <div class="mt-4">
                            <div class="d-flex align-items-center mb-3">
                                <div class="bg-primary text-white rounded-circle p-3 me-3">
                                    <i class="fas fa-rocket fa-lg"></i>
                                </div>
                                <div>
                                    <h5 class="mb-0">Fast Performance</h5>
                                    <p class="mb-0 text-muted">Optimized SPA architecture with zero page reloads.</p>
                                </div>
                            </div>
                            <div class="d-flex align-items-center mb-3">
                                <div class="bg-success text-white rounded-circle p-3 me-3">
                                    <i class="fas fa-universal-access fa-lg"></i>
                                </div>
                                <div>
                                    <h5 class="mb-0">Accessible Design</h5>
                                    <p class="mb-0 text-muted">Following A11Y best practices for everyone.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="p-5">
                             <img src="https://images.unsplash.com/photo-1521737711867-e3b97375f902?auto=format&fit=crop&w=800&q=80" alt="Team working" class="img-fluid rounded-circle shadow-lg">
                        </div>
                    </div>
                </div>
            </div>
        `;
    };

    /**
     * Attach Event Listeners to dynamic content (US-JQ-01, US-JS-02, US-HTML-06)
     */
    const attachDynamicEventListeners = () => {
        // Service Filtering Logic (US-JS-02, US-JS-04)
        $('.filter-btn').on('click', function() {
            const category = $(this).data('category');
            $('.filter-btn').removeClass('btn-primary').addClass('btn-outline-primary');
            $(this).removeClass('btn-outline-primary').addClass('btn-primary');

            if (category === 'All') {
                $('.service-item').fadeIn();
            } else {
                $('.service-item').hide();
                $(`.service-item[data-category="${category}"]`).fadeIn();
            }
        });

        // Form Validation Logic (US-BS-03)
        $('#bookingForm').on('submit', function(event) {
            if (!this.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            } else {
                event.preventDefault();
                alert('Success! Your booking has been submitted. (In a real app, this would be an AJAX call to a server)');
            }
            $(this).addClass('was-validated');
        });

        // Geolocation Feature (US-HTML-06)
        $('#detectLocation').on('click', function() {
            const status = $('#locationStatus');
            if (navigator.geolocation) {
                status.text('Locating...');
                navigator.geolocation.getCurrentPosition(
                    (position) => {
                        const { latitude, longitude } = position.coords;
                        status.text(`Detected: Lat ${latitude.toFixed(2)}, Lng ${longitude.toFixed(2)}`);
                        $('#address').val(`Latitude: ${latitude}, Longitude: ${longitude}`);
                    },
                    () => {
                        status.text('Unable to retrieve location.');
                    }
                );
            } else {
                status.text('Geolocation is not supported by your browser.');
            }
        });
    };

    // Navigation Events
    window.addEventListener('hashchange', router);
    
    // Initial Route
    router();
});
