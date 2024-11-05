const localhost = "https://localhost:44321/api";

class ApiService {
    constructor(baseURL = localhost) {
        this.baseURL = baseURL;
    }

    async login(userInfo) {
        const response = await fetch(`${localhost}/User/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login: userInfo.login,
                password: userInfo.password
            })
        });

        const result = await response.json();

        if (response.ok) {
            localStorage.setItem('token', result.token);
        } else {
            console.log(result.statusText);
        }

        return result;
    }

    async register(userInfo) {
        const response = await fetch(`${localhost}/User/Register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login: userInfo.login,
                password: userInfo.password
            })
        });

        const result = await response.json();

        if (response.ok) {
            localStorage.setItem('token', result.token);
        } else {
            console.log(result.statusText);
        }

        return result;
    }

    async getUsers() {
        try {
            const response = await fetch(`${this.baseURL}/User`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            return data;
        } catch (error) {
            console.error('Error:', error);
            return [];
        }
    }

    async getEventInformation() {
        try {
            const response = await fetch(`${this.baseURL}/Event/GetEventInfo`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();

            return data;
        } catch (error) {

            console.error('Error:', error);
        }
    }

    async getEvents() {
        try {
            const response = await fetch(`${this.baseURL}/Event/GetEvents`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();

            return data;
        } catch (error) {
            console.error('Error fetching events:', error);

            return [];
        }
    }

    async fetchEventById(id) {
        try {
            const response = await fetch(`${this.baseURL}events/${id}`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();

            return data;
        } catch (error) {
            console.error(`Error fetching event by ID ${id}:`, error);

            return null;
        }
    }

    async createEvent(eventData) {
        const response = await fetch(`${this.baseURL}/Event/AddEvent`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(eventData)
        });

        if (response.ok) {
            document.getElementById('message').innerHTML = '<div class="alert alert-success">Мероприятие добавлено успешно!</div>';
        } else {
            const result = await response.json();
            document.getElementById('message').innerHTML = `<div class="alert alert-danger">${result.message}</div>`;
        }

        return response;
    }

    async addTickets(tickets) {
        const response = await fetch(`${this.baseURL}/Ticket/AddTickets`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(tickets)
        });
        const result = await response.json();

        if (response.ok) {
            document.getElementById('message').innerHTML = '<div class="alert alert-success">Билеты добавлены успешно!</div>';
        } else {
            document.getElementById('message').innerHTML = `<div class="alert alert-danger">${result.message}</div>`;
        }

        return response;
    }

    async buyTickets(tickets) {
        const response = await fetch(`${this.baseURL}/Ticket/BuyTickets`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': localStorage.getItem("token")
            },
            body: JSON.stringify(tickets)
        });
        const result = await response.json();

        if (response.ok) {
            return result;
        } else {
            document.getElementById('message').innerHTML = `<div class="alert alert-danger">${result.message}</div>`;
        }
        
        return result;
    }
}

export const apiService = new ApiService(localhost);