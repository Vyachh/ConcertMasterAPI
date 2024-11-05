import { apiService } from "./api.js";

document.addEventListener('DOMContentLoaded', () => {
    loadEvents();
});

document.getElementById('eventForm').addEventListener('submit', async function (event) {
    event.preventDefault();
    const tickets = [];
    const selectElement = document.getElementById('eventTitles');
    const selectedOption = selectElement.options[selectElement.selectedIndex];
    const eventId = selectedOption.id; // Получаем eventId сразу при отправке формы

    const vipCount = document.querySelector('input[name="vipNumber"]').value;
    const vipPrice = document.querySelector('input[name="vipPrice"]').value;
    tickets.push({
        type: 1,
        price: parseInt(vipPrice, 10),
        count: parseInt(vipCount, 10),
        eventId: eventId
    });

    const standardCount = document.querySelector('input[name="standardNumber"]').value;
    const standardPrice = document.querySelector('input[name="standardPrice"]').value;
    tickets.push({
        type: 0,
        price: parseInt(standardPrice, 10),
        count: parseInt(standardCount, 10),
        eventId: eventId
    });

    const studentCount = document.querySelector('input[name="studentNumber"]').value;
    const studentPrice = document.querySelector('input[name="studentPrice"]').value;
    tickets.push({
        type: 2,
        price: parseInt(studentPrice, 10),
        count: parseInt(studentCount, 10),
        eventId: eventId
    });

    await apiService.addTickets(tickets);
});

async function loadEvents() {
    let events = await apiService.getEvents();
    let eventTitles = document.getElementById("eventTitles");

    events?.forEach(event => {
        const option = document.createElement('option');
        option.value = event.title;
        option.id = event.id;
        option.textContent = event.title.replace(/([A-Z])/g, ' $1').trim();
        eventTitles.appendChild(option)
    });

    eventTitles.addEventListener('change', function () {
        let selectedEvent = events.find(e => e.title == eventTitles.value);
        changeEventInfo(selectedEvent)
    })
}

function changeEventInfo(selectedEvent) {
    document.getElementById("title").textContent = selectedEvent.title
    document.getElementById("description").textContent = selectedEvent.description
    document.getElementById("eventType").textContent = selectedEvent.typeName
    document.getElementById("eventGenre").textContent = selectedEvent.genreName
    document.getElementById("eventStart").textContent = selectedEvent.eventStart
    document.getElementById("organizers").textContent = selectedEvent.organizers.map(organizer => organizer.name).join(', ');
    document.getElementById("artists").textContent = selectedEvent.artists.map(artist => artist.name).join(', ');
    document.getElementById("eventInfo").classList.remove('d-none')
}

