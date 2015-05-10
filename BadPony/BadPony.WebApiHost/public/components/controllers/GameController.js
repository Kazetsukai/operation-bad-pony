(function () {
    'use strict';

    angular.module('bpGameController', []).controller("GameController", ["$scope", "$http", "$location",
        function ($scope, $http, $location) {
            $scope.Player = null;

            $http.get("/api/Player")
                .success(function (data, status, headers, config) {
                    $scope.Player = data;
                })
                .error(function (data, status, headers, config) {
                    if (status === 401) {
                        $location.url("/Authenticate");
                    }
                });
        }]);
})();