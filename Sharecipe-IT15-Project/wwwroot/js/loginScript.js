const container = document.getElementById('containers');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
    setTimeout(() => {
        window.location.href = 'Register';
    }, 580); // Delay of 600 milliseconds (adjust as needed)
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
    setTimeout(() => {
        window.location.href = 'Login';
    }, 550); // Delay of 600 milliseconds (adjust as needed)
});




