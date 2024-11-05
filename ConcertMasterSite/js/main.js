import { apiService } from './api.js';

document.addEventListener('DOMContentLoaded', () => {
    loadEventInfo();
});

async function loadEventInfo() {
    let events = await apiService.getEvents();
    let eventInfoElement = document.getElementById("eventInfo");

    events.forEach(element => {
        addEventInfoElement(element, eventInfoElement);
    });
}

function addEventInfoElement(element, eventInfoElement) {
    const h5 = document.createElement('h5');
    h5.className = "mb-1";
    h5.textContent = element.title;

    const small = document.createElement('div');
    small.textContent = new Date(element.created).toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        timeZoneName: 'short',
    });

    const div = document.createElement('div');
    div.className = "d-flex w-100 justify-content-between";
    div.appendChild(h5);
    div.appendChild(small);

    const p = document.createElement('p');
    p.className = "mb-1";
    p.textContent = element.description;

    const a = document.createElement('a');
    a.className = "list-group-item list-group-item-action";
    a.ariaCurrent = "true";
    a.id = element.id;

    a.addEventListener('click', () => {
        const allItems = eventInfoElement.getElementsByClassName('list-group-item');
        for (let item of allItems) {
            item.classList.remove('active');
        }

        a.classList.add('active');
        displayTickets(element.tickets);
    });

    a.appendChild(div);
    a.appendChild(p);
    eventInfoElement.appendChild(a);
}

function displayTickets(tickets) {
    const ticketList = document.getElementById("ticketList");
    ticketList.innerHTML = "";

    if (!tickets || tickets.length === 0) {
        const noTicketsItem = document.createElement("li");
        noTicketsItem.textContent = "Билетов нет";
        ticketList.appendChild(noTicketsItem);
        return;
    }

    const ticketTypeMap = {
        0: "Студенческий билет",
        1: "VIP",
        2: "Обычный"
    };

    tickets.forEach(ticket => {
        const listItem = document.createElement("li");

        const ticketTypeText = ticketTypeMap[ticket.type] || "Неизвестный тип";
        listItem.textContent = `${ticketTypeText}: ${ticket.price} руб. (Доступно: ${ticket.count})`;

        const inputContainer = document.createElement("div");
        inputContainer.style.marginTop = "5px";

        const input = document.createElement("input");
        input.type = "number";
        input.min = 1;
        input.max = ticket.count;
        input.placeholder = "Количество";
        input.style.marginRight = "10px";
        input.style.width = "250px";

        const buyButton = document.createElement("button");
        buyButton.textContent = "Купить";
        buyButton.className = "btn btn-primary";

        buyButton.addEventListener("click", () => {
            const quantity = parseInt(input.value);

            if (isNaN(quantity) || quantity < 1 || quantity > ticket.count) {
                alert("Пожалуйста, введите корректное количество билетов.");
                return;
            }

            const ticketResponse = {
                id: ticket.id,
                type: ticket.type,
                status: ticket.status,
                price: ticket.price,
                count: quantity,
                eventId: ticket.eventId
            }

            apiService.buyTickets(ticketResponse)
                .then(() => location.reload())
                .catch(error => {
                    console.log(error);
                    alert("Ошибка при покупке билетов: " + error)
                });
        });

        inputContainer.appendChild(input);
        inputContainer.appendChild(buyButton);
        listItem.appendChild(inputContainer);

        ticketList.appendChild(listItem);
    });
}
