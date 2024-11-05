import { apiService } from "./api.js";

document.getElementById("loginForm")?.addEventListener("submit", async (event) => {
    event.preventDefault();
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    let userInfo = {
        login: username,
        password: password
    }

    await apiService.login(userInfo).then(response => {
        if (response.token) {
            window.location.replace("/")
        }
        else {
            const errorDiv = document.getElementById('error-message');
            errorDiv.textContent = response.statusText;
            errorDiv.style.display = 'block';
        }
    })
})

document.getElementById("registerForm")?.addEventListener("submit", async (event) => {
    event.preventDefault();
    const username = document.getElementById("regUsername").value;
    const password = document.getElementById("regPassword").value;

    let userInfo = {
        login: username,
        password: password
    }

    await apiService.register(userInfo).then(response => {
        if (response) {
            window.location.replace("/")
        }
        else {
            const errorDiv = document.getElementById('error-message');
            errorDiv.textContent = response.statusText;
            errorDiv.style.display = 'block';
        }
    })
})