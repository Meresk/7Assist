// When running OpenVidu locally, leave these variables empty
// For other deployment type, configure them with correct URLs depending on your deployment
var APPLICATION_SERVER_URL = "http://localhost:6080/";
var LIVEKIT_URL = "ws://localhost:7880";//"wss://haha-1b7hsnu1.livekit.cloud";
configureUrls();

const LivekitClient = window.LivekitClient;
var room;

function configureUrls() {
    // If APPLICATION_SERVER_URL is not configured, use default value from OpenVidu Local deployment
    if (!APPLICATION_SERVER_URL) {
        if (window.location.hostname === "localhost") {
            APPLICATION_SERVER_URL = "http://localhost:6080/";
        } else {
            APPLICATION_SERVER_URL = "http://localhost:6080/";
        }
    }

    // If LIVEKIT_URL is not configured, use default value from OpenVidu Local deployment
    if (!LIVEKIT_URL) {
        if (window.location.hostname === "localhost") {
            LIVEKIT_URL = "ws://localhost:7880/";
        } else {
            LIVEKIT_URL = "wss://" + window.location.hostname + ":7443/";
        }
    }
}
async function joinRoomHidden() {

    room = new LivekitClient.Room();
    try {
        const roomName = "all_users";
        const userName = (await getUserClaims()).toString();

        const token = await getToken(roomName, userName);

        await room.connect(LIVEKIT_URL, token);
        const p = room.localParticipant;
        await p.setCameraEnabled(true);
        await p.setMicrophoneEnabled(false);
    } catch (error) {
        console.log("There was an error connecting to the room:", error.message);
    }
}
async function joinRoom(name = null) {
    document.getElementById("join-button").disabled = true;
    document.getElementById("join-button").innerText = "Joining...";

    room = new LivekitClient.Room();

    room.on(LivekitClient.RoomEvent.TrackSubscribed, (track, _publication, participant) => {
        addTrack(track, participant.identity);
    });

    room.on(LivekitClient.RoomEvent.TrackUnsubscribed, (track, _publication, participant) => {
        track.detach();
        document.getElementById(track.sid)?.remove();

        if (track.kind === "video") {
            removeVideoContainer(participant.identity);
        }
    });

    try {
        const roomName = name === null ? (await getUserClaims()).toString() : name.toString();
        const userName = (await getUserClaims()).toString();

        const token = await getToken(roomName, userName);

        await room.connect(LIVEKIT_URL, token);
        document.getElementById("room-title").innerText = roomName;
        document.getElementById("join").hidden = true;
        document.getElementById("room").hidden = false;
        await room.localParticipant.enableCameraAndMicrophone();
        const localVideoTrack = this.room.localParticipant.videoTrackPublications.values().next().value.track;
        addTrack(localVideoTrack, userName, true);
    } catch (error) {
        console.log("There was an error connecting to the room:", error.message);
        //await leaveRoom();
    }
}
async function joinRoomWithAll() {
    room = new LivekitClient.Room();

    room.on(LivekitClient.RoomEvent.TrackSubscribed, (track, _publication, participant) => {
        addTrack(track, participant.identity);
    });

    room.on(LivekitClient.RoomEvent.TrackUnsubscribed, (track, _publication, participant) => {
        track.detach();
        document.getElementById(track.sid)?.remove();

        if (track.kind === "video") {
            removeVideoContainer(participant.identity);
        }
    });

    try {
        const roomName = "all_users";
        const userName = (await getUserClaims()).toString();

        const token = await getToken(roomName, userName);

        await room.connect(LIVEKIT_URL, token);
        document.getElementById("room-title").innerText = roomName;
        document.getElementById("join").hidden = true;
        document.getElementById("room").hidden = false;
        const localVideoTrack = this.room.localParticipant.videoTrackPublications.values().next().value.track;
        addTrack(localVideoTrack, userName, true);
    } catch (error) {
        console.log("There was an error connecting to the room:", error.message);
        //await leaveRoom();
    }
}

function addTrack(track, participantIdentity, local = false) {
    const element = track.attach();
    element.id = track.sid;
    if (track.kind === "video") {
        const videoContainer = createVideoContainer(participantIdentity, local);
        videoContainer.append(element);
        appendParticipantData(videoContainer, participantIdentity); 
        //+ (local ? " (You)" : "")
    } else {
        document.getElementById("layout-container").append(element);
    }
}

async function leaveRoom() {
    await room.disconnect();
    removeAllLayoutElements();
    document.getElementById("join").hidden = false;
    document.getElementById("room").hidden = true;
    document.getElementById("join-button").disabled = false;
    document.getElementById("join-button").innerText = "Join!";
}

function generateFormValues() {
    document.getElementById("room-name").value = "Test Room";
    document.getElementById("participant-name").value = "Participant" + Math.floor(Math.random() * 100);
}
async function getUserClaims() {
    const response = await fetch('/claims', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    });

    if (!response.ok) {
        throw new Error('Failed to fetch claims');
    }

    const claims = await response.json();
    return claims
}
function createVideoContainer(participantIdentity, local = false) {
    const videoContainer = document.createElement("div");
    videoContainer.id = `camera-${participantIdentity}`;
    videoContainer.className = "video-container" + (local ? "-you" : "");
    const layoutContainer = document.getElementById("card-" + participantIdentity) == null ? document.getElementById("layout-container") : document.getElementById("card-" + participantIdentity);
    layoutContainer.prepend(videoContainer);

    return videoContainer;
}

function appendParticipantData(videoContainer, participantIdentity) {
    const dataElement = document.createElement("div");
    dataElement.className = "participant-data";
    dataElement.innerHTML = `<a href="/room2/?roomname=${participantIdentity}">${participantIdentity}</a>`;
    videoContainer.prepend(dataElement);
}

function removeVideoContainer(participantIdentity) {
    const videoContainer = document.getElementById(`camera-${participantIdentity}`);
    videoContainer?.remove();
}

function removeAllLayoutElements() {
    const layoutElements = document.getElementById("layout-container").children;
    Array.from(layoutElements).forEach((element) => {
        element.remove();
    });
}

/**
 * --------------------------------------------
 * GETTING A TOKEN FROM YOUR APPLICATION SERVER
 * --------------------------------------------
 * The method below request the creation of a token to
 * your application server. This prevents the need to expose
 * your LiveKit API key and secret to the client side.
 *
 * In this sample code, there is no user control at all. Anybody could
 * access your application server endpoints. In a real production
 * environment, your application server must identify the user to allow
 * access to the endpoints.
 */
async function getToken(roomName, participantName) {
    const response = await fetch(APPLICATION_SERVER_URL + "api/Token", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            roomName,
            participantName
        }),
        credentials: 'include' // Включаем отправку куков
    });

    if (!response.ok) {
        throw new Error('getToken error');
    }

    const token = await response.json();
    return token.token;
}
