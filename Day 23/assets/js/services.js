/**
 * services.js - Handles data fetching and service logic (US-AJS-03, US-AJAX-01, US-JSON-01)
 */

const ServiceModule = (() => {
    // Private state (US-AJS-04: Closures)
    let services = [];

    /**
     * Fetch all services from JSON (US-AJAX-01, US-JSON-01)
     */
    const fetchServices = async () => {
        try {
            const response = await fetch('data/services.json');
            if (!response.ok) throw new Error('Failed to fetch services');
            services = await response.json();
            return services;
        } catch (error) {
            console.error('Error fetching services:', error);
            return [];
        }
    };

    /**
     * Filter services by category (US-JS-04, US-AJS-01)
     */
    const filterByCategory = (category) => {
        if (category === 'All') return services;
        return services.filter(service => service.category === category);
    };

    /**
     * Get service by ID (US-JS-04)
     */
    const getServiceById = (id) => {
        return services.find(service => service.id === parseInt(id));
    };

    // Public API
    return {
        fetchServices,
        filterByCategory,
        getServiceById
    };
})();
