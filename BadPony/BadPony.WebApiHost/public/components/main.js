var app = angular.module('badpony', ['ngRoute']);

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when("/player/:id", {
        controller: "PlayerController",
        templateUrl: "/components/player/playerView.html"
    })
    .when("/", {
        controller: "LocationController",
        templateUrl: "/components/location/locationView.html"
    });

    
});