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