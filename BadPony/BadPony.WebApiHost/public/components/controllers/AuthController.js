(function () {
    'use strict';

    angular.module('bpAuthController', []).controller("AuthController", ["$scope", "$http",
        function ($scope, $http, $location) {
           $http.get("/api/Account")
                .success(function (data, status, headers, config) {
                    $scope.AuthMethods = data;
                })
                .error(function (data, status, headers, config) {
                    /*TO DO*/
                });
        }]);
})();