/// <reference path="../_app.ts" />

module BadPony.WebClient {
    'use strict';

    class LocationController {
        constructor(
            $scope: ILocationScope,
            $http: ng.IHttpService
            ) {

            var loadLocation = function (containerId: number): void {
                $http.get<any>("api/Location/" + containerId)
                    .success(function (data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig) {
                        $scope.location = new LocationModel(data);
                    });
            }

            $http.get<IPlayerInfo>("api/Player/Default")
                .success(function (data: IPlayerInfo, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig) {
                    $scope.player = data;

                    /* temporary hack so that I could handle moving to another location generically in the view and model */
                    var gotoLocation = function (destinationId: number): void {
                        $http.post<any>("api/Move", {
                            objectId: $scope.player.Id,
                            destinationId: destinationId
                        })
                            .success(function (data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig) {
                                loadLocation(destinationId);
                            })
                    };

                    /* temporary hack so that I could handle doing a job generically in the view and model */
                    var doJob = function (jobId: number): void {
                        $http.post<any>("api/Job", {
                            jobId: jobId,
                            playerId: $scope.player.Id
                        })
                            .success(function (data: any, status: number, headers: ng.IHttpHeadersGetter, config: ng.IRequestConfig) {
                                if (data === false) {
                                    alert("You are out of action points.\nPlease wait a while before trying to work again.");
                                }
                            })
                    };

                    /* temporary super duper hack so that I could handle actions generically
                     *
                     * 
                     * hopefully we can change this to just be a really simple POST request, 
                     * with the logic of what the action does being handled server side and 
                     * changes being returned */
                    $scope.doAction = function (actionObject: GameObjectModel) {
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
        }
    }

    angular.module('bpLocationController', [])
        .controller("LocationController", LocationController);
}