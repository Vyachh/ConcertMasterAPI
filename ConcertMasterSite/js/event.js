import { apiService } from "./api.js";

document.getElementById('eventForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    let organizers = [];
    let artists = [];

    document.querySelectorAll('#organizers input[type="checkbox"]:checked').forEach(function (checkbox) {
        organizers.push(checkbox.id);
    });

    document.querySelectorAll('#artists input[type="checkbox"]:checked').forEach(function (checkbox) {
        artists.push(checkbox.id);
    });

    let params = []
    params.organizers = organizers
    params.artists = artists

    checkRequiredParams(params)

    let eventData = {
        title: document.getElementById('title')?.value,
        description: document.getElementById('description')?.value,
        genre: document.getElementById('eventGenre')?.value,
        type: document.getElementById('eventType')?.value,
        eventStart: document.getElementById('eventStart')?.value,
        addressVenue: document.getElementById('address')?.value
    };

    eventData.organizers = organizers
    eventData.artists = artists

    await apiService.createEvent(eventData).then((response) => {
        if (response.ok) {
            window.location.replace("/")
        }
    });
});

function checkRequiredParams(params) {
    if (params.organizers === undefined || params.artists === undefined) {
        document.getElementById('message').innerHTML = '<div class="alert alert-danger">Обязательные поля "Организаторы" или "Артисты" не заполнены!</div>';
    }
}

loadEventInfo()

async function loadEventInfo() {
    let eventInfo = await apiService.getEventInformation();

    let genres = document.getElementById("eventGenre");
    let types = document.getElementById("eventType");
    let organizers = document.getElementById("organizers");
    let artists = document.getElementById("artists");

    eventInfo.eventGenres?.forEach(genre => {
        const option = document.createElement('option');
        option.value = genre;
        option.textContent = genre.replace(/([A-Z])/g, ' $1').trim();
        genres.appendChild(option);
    });

    eventInfo.eventTypes?.forEach(type => {
        const option = document.createElement('option');
        option.value = type;
        option.textContent = type.replace(/([A-Z])/g, ' $1').trim();
        types.appendChild(option);
    });

    if (eventInfo.organizers.length == 0) {
        const noOrganizers = "Организаторов нет";
        const option = document.createElement('option');
        option.value = noOrganizers;
        option.textContent = noOrganizers.replace(/([A-Z])/g, ' $1').trim();
        organizers.appendChild(option);
    }
    else {
        eventInfo.organizers?.forEach(organizer => {
            createOrganizerRow(organizer, organizers);
        });
    }

    if (eventInfo.artists.length == 0) {
        const noArtists = "Артистов нет";
        const option = document.createElement('option');
        option.value = noArtists;
        option.textContent = noArtists.replace(/([A-Z])/g, ' $1').trim();
        artists.appendChild(option);
    } else {
        eventInfo.artists?.forEach(artist => {
            createArtistRow(artist, artists);
        });
    }
}

function createArtistRow(artist, artists) {
    const li = document.createElement('li');
    li.className = 'list-group-item ';
    li.id = `${artist.userId}`;

    const checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.value = artist.userName;
    checkbox.id = `${artist.userId}`;
    checkbox.name = 'artists';
    checkbox.className = 'form-check-input me-1';

    const label = document.createElement('label');
    label.htmlFor = checkbox.id;
    label.className = 'form-check-label';
    label.textContent = artist.userName;

    li.appendChild(checkbox);
    li.appendChild(label);
    artists.appendChild(li);
}

function createOrganizerRow(organizer, organizers) {
    const li = document.createElement('li');
    li.className = 'list-group-item m-6';
    li.id = `${organizer.userId}`;

    const checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.value = organizer.userName;
    checkbox.id = `${organizer.userId}`;
    checkbox.name = 'organizers';
    checkbox.className = 'form-check-input me-1';

    const label = document.createElement('label');
    label.htmlFor = checkbox.id;
    label.className = 'form-check-label';
    label.textContent = organizer.userName;

    li.appendChild(checkbox);
    li.appendChild(label);
    organizers.appendChild(li);
}
