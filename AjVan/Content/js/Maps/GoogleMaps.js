function GoogleMaps(latElement, lngElement, defaultLat = null, defaultLng = null) {

    // In the following example, markers appear when the user clicks on the map.
    // The markers are stored in an array.
    // The user can then click an option to hide, show or delete the markers.
    var _this = this;
    this.map;
    this.isDisabled = false;
    this.markers = [];
    if (defaultLat != null && defaultLng != null) {
        this.defaultLat = defaultLat.replace(/,/, '.');
        this.defaultLng = defaultLng.replace(/,/, '.');

    }
   
    this.initMap = function () {
        var defaultLocation = null;
        if (_this.defaultLat != null && _this.defaultLng != null) {
            defaultLocation = { lat: parseFloat(_this.defaultLat), lng: parseFloat(_this.defaultLng)};
        }

        var zagreb = { lat: 45.8150108, lng: 15.981919000000062 };

        _this.map = new google.maps.Map(document.getElementById('map'), {
            zoom: 12,
            center: defaultLocation != null ? defaultLocation : zagreb,
            mapTypeId: 'hybrid'
        });

        console.log(defaultLocation)
        if (defaultLocation != null) {
            _this.addMarker(defaultLocation);
        }

        // This event listener will call addMarker() when the map is clicked.
        _this.map.addListener('click', function (event) {
            _this.addMarker(event.latLng);
        });
    }

    // Adds a marker to the map and push to the array.
    this.addMarker = function (location) {
        if (!_this.isDisabled) {
            _this.clearMarkers();
            var marker = new google.maps.Marker({
                position: location,
                map: _this.map
            });
            _this.markers.push(marker);


            // Update form elements
            lngElement.val(location.lng);
            latElement.val(location.lat);
            // Call defined callback
            //onMarketCreateCallback();
        }
        
    }

    // Sets the map on all markers in the array.
    this.setMapOnAll = function(map) {
        for (var i = 0; i < _this.markers.length; i++) {
            _this.markers[i].setMap(map);
        }
    }

    // Removes the markers from the map, but keeps them in the array.
   this.clearMarkers = function() {
       _this.setMapOnAll(null);
    }

    // Shows any markers currently in the array.
    this.showMarkers = function() {
        _this.setMapOnAll(map);
    }

    // Deletes all markers in the array by removing references to them.
    this.deleteMarkers = function() {
        _this.clearMarkers();
        _this.markers = [];
    }
    this.disableInputs = function () {
        _this.isDisabled = true;
    }
}
