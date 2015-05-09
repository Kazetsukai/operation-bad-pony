///#source 1 1 /public/components/main.js
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

///#source 1 1 /public/components/models/LocationViewModel.js
(function () {
    'use strict';

    _registerNamespace("BadPony.Models");

    BadPony.Models.createLocationViewModel = function (data) {
        data.SelectedItem = null;
        data.SelectObject = function (toSelect) {
            data.SelectedItem = data.SelectedItem !== toSelect ? toSelect : null;
        }


        /* More temporary hacks, I'm thinking we'll change LocationView on the server side
         * to just return one dictionary */

        data.LocationObjects = [];
        if (data.Jobs.length > 0) {
            var jobs = {
                "Key": "Jobs",
                "Value": data.Jobs
            };

            for (var i = 0; i < jobs.Value.length; i++) {
                var thisJob = jobs.Value[i];
                thisJob.Actions = [{
                    "Id": thisJob.Id,
                    "Name": "Work",
                    "Description": "AP Cost = " + thisJob.APCost + "; Pay = $" + thisJob.Pay,
                    "Properties": {
                        "Pay": thisJob.Pay,
                        "APCost": thisJob.APCost
                    }
                }];
            }

            data.LocationObjects.push(jobs);
        }

        if (data.Contents.length > 0) {
            data.LocationObjects.push({
                "Key": "Items",
                "Value": data.Contents
            });
        }

        if (data.Exits.length > 0) {
            var exits = {
                "Key": "Exits",
                "Value": data.Exits
            };

            for (var i = 0; i < exits.Value.length; i++) {
                var thisExit = exits.Value[i];
                thisExit.Actions = [{
                    "Id": thisExit.Id,
                    "Name": "Go through door",
                    "Properties": {
                        "DestinationId": thisExit.DestinationId
                    }
                }];
            }

            data.LocationObjects.push(exits);
        }

        if (data.Players.length > 0) {
            data.LocationObjects.push({
                "Key": "Players",
                "Value": data.Players
            });
        }

        return data;
    }
})();
///#source 1 1 /public/components/controllers/LocationController.js
(function () {
    'use strict';

    angular.module('bpLocationController', []).controller("LocationController", ["$scope", "$http",
        function ($scope, $http) {
            var loadLocation = function (containerId) {
                $http.get("api/Location/" + containerId).success(function (data, status, headers, config) {
                    $scope.location = BadPony.Models.createLocationViewModel(data);
                });
            };

            $http.get("api/Player/Default").success(function (data, status, headers, config) {
                $scope.player = data;

                /* temporary hack so that I could handle moving to another location generically in the view and model */
                var gotoLocation = function (destinationId) {
                    $http.post("api/Move", {
                        objectId: $scope.player.Id,
                        destinationId: destinationId
                    }).success(function (data, status, headers, config) {
                        loadLocation(destinationId);
                    });
                };

                /* temporary hack so that I could handle doing a job generically in the view and model */
                var doJob = function (jobId) {
                    $http.post("api/Job", {
                        jobId: jobId,
                        playerId: $scope.player.Id
                    }).success(function (data, status, headers, config) {
                        if (data === false) {
                            alert("You are out of action points.\nPlease wait a while before trying to work again.");
                        }
                    });
                };

                /* temporary super duper hack so that I could handle actions generically
                *
                *
                * hopefully we can change this to just be a really simple POST request,
                * with the logic of what the action does being handled server side and
                * changes being returned */
                $scope.doAction = function (actionObject) {
                    var destId = parseInt(actionObject.Properties["DestinationId"]);
                    if (destId && !isNaN(destId)) {
                        gotoLocation(destId);
                        return;
                    }

                    var pay = parseInt(actionObject.Properties["Pay"]);
                    if (pay && !isNaN(pay)) {
                        doJob(actionObject.Id);
                        return;
                    }
                };

                if (data) {
                    loadLocation($scope.player.ContainerId);
                }
            });
        }]
    );
})();

