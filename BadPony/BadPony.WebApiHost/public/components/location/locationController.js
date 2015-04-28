app.controller('LocationController', function ($scope, $http) {
    $http({
        url: "api/Player/Default"
    }).success(function (data, status, headers, config) {
        var player = data;
        $scope.player = player;

        if (data) {
            $http({
                url: "api/Location/" + data.ContainerId
            }).success(function(data, status, headers, config) {
                $scope.location = data;
            });

            $scope.go = function (id) {

                $http.post("api/Move", { objectId: player.Id, destinationId: id })
                .success(function (data, status, headers, config) {
                    // TODO: Replace this with the Angular way of reloading the controller.
                    location.reload();
                });
            }

            $scope.job = function (id) {
                $http.post("api/Job", { JobId: id, PlayerId: player.Id })
                .success(function (data, status, headers, config) {
                    location.reload();
                    if (data == false) {
                        alert("You are out of action points.\nPlease wait a while before trying to work again.");
                    }
                });
            }
        }
    });
    
});