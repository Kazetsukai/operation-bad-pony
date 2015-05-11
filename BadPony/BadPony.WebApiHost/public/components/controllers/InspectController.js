(function () {
    'use strict';

    angular.module('bpInspectController', []).controller("InspectController", ["$scope", "$http",
        function ($scope, $http) {
            var loadObject = function (objectId) {
                $http.get("api/Object/" + objectId).success(function (data, status, headers, config) {
                    $scope.object = data;
                });
            };

            loadObject(6);
        }]
    );
})();
