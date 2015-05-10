(function () {
    'use strict';

    var badponyApp = angular.module('badpony', [
        'ngRoute',
        'bpGameController',
        'bpAuthController',
        'bpLocationController'
    ]);

    badponyApp.config(["$routeProvider", "$locationProvider", function($routeProvider, $locationProvider) {
        $routeProvider.when("/", {
            controller: "GameController",
            templateUrl: "/components/views/gameView.html"
        }).when("/Authenticate", {
            controller: "AuthController",
            templateUrl: "/components/views/authView.html"
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
