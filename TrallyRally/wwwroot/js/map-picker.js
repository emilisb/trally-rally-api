// Initialize and add the map
function initMap() {
    const latitudeInput = document.getElementsByName('Latitude')[0];
    const longitudeInput = document.getElementsByName('Longitude')[0];
    const maxDistanceInput = document.getElementsByName('MaxDistance')[0];

    const initLatitude = parseFloat(latitudeInput.value) || 54.6872;
    const initLongitude = parseFloat(longitudeInput.value) || 25.2797;
    const initMaxDistance = parseFloat(maxDistanceInput.value) || 50;

    const initLocation = { lat: initLatitude, lng: initLongitude };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 18,
        center: initLocation,
        disableDoubleClickZoom: true,
    });

    const distanceCircle = new google.maps.Circle({
        strokeColor: "#FF0000",
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: "#FF0000",
        fillOpacity: 0.35,
        map,
        center: initLocation,
        radius: initMaxDistance,
        editable: true
    });

    const marker = new google.maps.Marker({
        position: initLocation,
        map: map,
        draggable: true
    });

    google.maps.event.addListener(map, 'dblclick', function (event) {
        marker.setPosition(event.latLng);
        distanceCircle.setCenter(event.latLng);
        latitudeInput.value = event.latLng.lat();
        longitudeInput.value = event.latLng.lng();
    });

    google.maps.event.addListener(marker, 'dragend', function () {
        latitudeInput.value = marker.getPosition().lat();
        longitudeInput.value = marker.getPosition().lng();
        distanceCircle.setCenter(marker.getPosition());
    });

    google.maps.event.addListener(distanceCircle, 'radius_changed', function () {
        maxDistanceInput.value = Math.round(distanceCircle.getRadius());
    });

    maxDistanceInput.addEventListener('change', function (e) {
        distanceCircle.setRadius(parseFloat(e.target.value));
    });
}