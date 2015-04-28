app.controller('PlayerController', function ($scope, $routeParams, $http) {
    $http({
        url: "api/Player/" + $routeParams.id
    }).success(function (data, status, headers, config) {
        var player = data;
        $scope.player = player;

        if (data) {
            $http({
                url: "api/Location/" + data.ContainerId
            }).success(function (data, status, headers, config) {
                $scope.location = data;
            });
        }
    });

});