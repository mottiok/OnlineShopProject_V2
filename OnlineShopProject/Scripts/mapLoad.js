//$(document).ready(function () {
//    var map = new GMaps({
//        div: '#map',
//        lat: 51.5073346,
//        lng: -0.1276831,
//        width: '500px',
//        height: '500px',
//        zoom: 12,
//        zoomControl: true,
//        zoomControlOpt: {
//            style: 'SMALL',
//            position: 'TOP_LEFT'
//        },
//        panControl: false
//    });

//    //map.addMarker({
//    //    lat: 51.5073346,
//    //    lng: -0.1276831,
//    //    //title: 'Lima',
//    //    //click: function (e) {
//    //    //    alert('You clicked in this marker');
//    //    //}
//    //});

//    //var markers_data = [];

//    //GMaps.geocode({
//    //    address: 'London',
//    //    callback: function (results, status) {
//    //        if (status == 'OK') {
//    //            var latlng = results[0].geometry.location;
//    //            markers_data.push({
//    //                lat: latlng.lat,
//    //                lng: latlng.lng,
//    //                //title : item.name,
//    //                //icon : {
//    //                //   size : new google.maps.Size(32, 32),
//    //                //  url : icon
//    //                //  }
//    //            });
//    //            //map.setCenter(latlng.lat(), latlng.lng());
//    //            //map.addMarker({
//    //            //    lat: latlng.lat(),
//    //            //    lng: latlng.lng()
//    //            //});
//    //        }
//    //    }
//    //});


//    //map.addMarkers(markers_data);
//});
$(document).ready(function initialize() {
    var addresses = $("#mapAddresses").data("value");
    var mapCanvas = document.getElementById('gmap');
    var mapOptions = {
        center: new google.maps.LatLng(44.5403, -78.5463),
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    var map = new google.maps.Map(mapCanvas, mapOptions);
    var geocoder = new google.maps.Geocoder();

    var addressesArray = addresses.split('~');

    for (i = 0; i < addressesArray.length; i++) {

        geocoder.geocode({ 'address': addressesArray[i] }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location
                });
            }
            // else {
            //    alert('Geocode was not successful for the following reason: ' + status);
            // }
        })
    }

    
    //var marker = new google.maps.Marker({
    //    position: new google.maps.LatLng(44.5403, -78.5463),
    //    map: map,
    //    title: 'Hello World!'
    //});
}
);