/// <reference path="_app.ts" />
var BadPony;
(function (BadPony) {
    (function (WebClient) {
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
    })(BadPony.WebClient || (BadPony.WebClient = {}));
    var WebClient = BadPony.WebClient;
})(BadPony || (BadPony = {}));
//# sourceMappingURL=main.js.map
