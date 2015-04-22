app.controller('LocationController', function ($scope, $http) {
    $http({
        url: "api/Location/1"
    }).success(function(data, status, headers, config) {
        $scope.location = data;
    });
});