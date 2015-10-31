(function () {
    'use strict';

    angular.module('bpLocationController', []).controller("LocationController", ["$scope", "$http",
        function ($scope, $http) {
            var loadLocation = function () {
                $http.get("api/Location/").success(function (data, status, headers, config) {
                    $scope.location = BadPony.Models.createLocationViewModel(data);
                });
            };

            $scope.$watch("Player", function (newValue, oldValue) {
                if (!oldValue && (newValue != oldValue)) {
                    loadLocation();
                }
            });

            var gotoLocation = function (destinationId) {
                $http.post("api/Move", {
                    objectId: $scope.Player.Id,
                    destinationId: destinationId
                }).success(function (data, status, headers, config) {
                    loadLocation();
                });
            };

            var doJob = function (jobId) {
                $http.post("api/Job", {
                    jobId: jobId,
                    playerId: $scope.Player.Id
                }).success(function (data, status, headers, config) {
                    if (data === false) {
                        alert(data + "You are out of action points.\nPlease wait a while before trying to work again.");
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
        }]
    );
})();
