(function () {
    'use strict';

    var badponyApp = angular.module('badpony', [
        'ngRoute',
        'ui.ace',
        'bpLocationController',
        'bpInspectController'
    ]);

    badponyApp.config(["$routeProvider", "$locationProvider", function($routeProvider, $locationProvider) {
        $routeProvider.when("/", {
            controller: "LocationController",
            templateUrl: "/components/views/locationView.html"
        });
        $routeProvider.when("/inspect/:id", {
            controller: "InspectController",
            templateUrl: "/components/views/inspectView.html"
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
