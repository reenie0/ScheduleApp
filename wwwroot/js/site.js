function navigateTo(section) {
    // Function to navigate to different sections of the dashboard
    alert(`Navigating to ${section}...`);
    // You can implement the logic to navigate to the respective section
}

// Form validation example
document.getElementById('login-form').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent form submission for demo
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    if (email === '' || password === '') {
        alert('Please fill in all fields.');
    } else {
        // Simulate a login action
        alert('Logging in...');
    }
});