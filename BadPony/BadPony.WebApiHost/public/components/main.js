(function () {
    'use strict';

    var badponyApp = angular.module('badpony', [
        'ngRoute',
        'bpLocationController'
    ]);

    badponyApp.config(["$routeProvider", "$locationProvider", function($routeProvider, $locationProvider) {
        $routeProvider.when("/", {
            controller: "LocationController",
            templateUrl: "/components/views/LocationView.html"
        });
    }]);

    window._registerNamespace = function(namespace) {
        var parts = namespace.split(".");
        var parent = window;
        for (var i = 0; i < parts.length; i++) {
            parent[parts[i]] = parent[parts[i]] || {};
            parent = parent[parts[i]];
        }
    }
})();
