/// <reference path="_app.ts" />

module BadPony.WebClient {
    'use strict';

    var badponyApp = angular.module('badpony', [
        'ngRoute',
        'bpLocationController'
    ]);

    badponyApp.config(function ($routeProvider, $locationProvider) {
        $routeProvider.when("/", {
            controller: "LocationController",
            templateUrl: "/components/views/LocationView.html"
        }); 
    });
}