(function () {
    'use strict';

    angular.module('bpInspectController', []).controller("InspectController", ["$scope", "$http", "$routeParams",
        function ($scope, $http, $routeParams) {
            var loadObject = function (objectId) {
                $http.get("api/Object/" + objectId).success(function (data, status, headers, config) {
                    $scope.object = data;
                });
            };

            loadObject($routeParams["id"]);
        }]
    );
})();
