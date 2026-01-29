/* ---------- Utility Logger (US-10) ---------- */
function log(message) {
    console.log(`[LOG]: ${message}`);
}

/* US-02 & US-08*/
function validateItem(item) {
    if (typeof item.price !== "number" || item.price <= 0) {
        throw new Error(`Invalid price for item: ${item.name}`);
    }

    if (typeof item.quantity !== "number" || item.quantity <= 0) {
        throw new Error(`Invalid quantity for item: ${item.name}`);
    }
}

/* US-03: Inventory Validation */
function validateStock(item) {
    if (item.quantity > item.stock) {
        throw new Error(`Insufficient stock for item: ${item.name}`);
    }
}

/* ---------- Billing Logic ---------- */

/* US-04: Calculate Subtotal */
function calculateSubtotal(item) {
    return item.price * item.quantity;
}

/* US-05: Calculate Total */
function calculateTotal(subtotals) {
    return subtotals.reduce((sum, value) => sum + value, 0);
}

/* ---------- Discount Logic (US-06) ---------- */
function applyDiscount(total) {
    let discountRate = 0;

    if (total > 10000) {
        discountRate = 0.10;
    } else if (total >= 5000) {
        discountRate = 0.05;
    }

    const discountAmount = total * discountRate;
    return {
        discountRate,
        discountAmount,
        discountedTotal: total - discountAmount
    };
}

/* ---------- Tax Logic (US-07) ---------- */
function calculateGST(amount) {
    const GST_RATE = 0.18; // 18% GST
    return amount * GST_RATE;
}

/* ---------- Order Processing (US-01) ---------- */
function processOrder(items) {
    log("Order processing started...");

    let subtotals = [];

    items.forEach(item => {
        validateItem(item);
        validateStock(item);

        const subtotal = calculateSubtotal(item);
        subtotals.push(subtotal);

        log(`Item: ${item.name}, Subtotal: ₹${subtotal}`);
    });

    const total = calculateTotal(subtotals);
    log(`Total before discount: ₹${total}`);

    const discountDetails = applyDiscount(total);
    log(`Discount Applied: ${discountDetails.discountRate * 100}%`);
    log(`Discount Amount: ₹${discountDetails.discountAmount}`);

    const gst = calculateGST(discountDetails.discountedTotal);
    log(`GST (18%): ₹${gst}`);

    const finalPayable = discountDetails.discountedTotal + gst;

    /* US-09: Order Summary */
    console.log("========= ORDER SUMMARY =========");
    console.table(items.map(item => ({
        Item: item.name,
        Price: item.price,
        Quantity: item.quantity,
        Subtotal: item.price * item.quantity
    })));

    console.log(`Total: ₹${total}`);
    console.log(`Discount: -₹${discountDetails.discountAmount}`);
    console.log(`GST: ₹${gst}`);
    console.log(`Final Payable Amount: ₹${finalPayable}`);
    console.log("=================================");

    log("Order processing completed successfully.");
}

/* ---------- Sample Order Data ---------- */
const orderItems = [
    { name: "Laptop", price: 50000, quantity: 1, stock: 5 },
    { name: "Mouse", price: 500, quantity: 2, stock: 10 },
    { name: "Keyboard", price: 1500, quantity: 1, stock: 4 }
];

/* ---------- Execute Order ---------- */
try {
    processOrder(orderItems);
} catch (error) {
    console.error(`[ERROR]: ${error.message}`);
}
