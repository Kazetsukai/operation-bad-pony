/// <reference path="../_app.ts" />
var BadPony;
(function (BadPony) {
    (function (WebClient) {
        'use strict';

        var GameObjectModel = (function () {
            function GameObjectModel(data) {
                this.Id = data.Id;
                this.Name = data.Name;
                this.Description = data.Description;
                this.Properties = data.Properties || {};

                this.Actions = [];

                /* lololol more temporary hacks to handle actions, see comments on LocationController.ts for reasons */
                if (typeof (data.Pay) !== "undefined") {
                    var action = new GameObjectModel({
                        "Id": this.Id,
                        "Name": "Work",
                        "Description": "AP Cost = " + data.APCost + "; Pay = $" + data.Pay,
                        "Properties": {
                            "Pay": data.Pay,
                            "APCost": data.APCost
                        }
                    });
                    this.Actions.push(action);
                } else if (typeof (data.DestinationId) !== "undefined") {
                    var action = new GameObjectModel({
                        "Id": this.Id,
                        "Name": "Go through door",
                        "Properties": {
                            "DestinationId": data.DestinationId
                        }
                    });
                    this.Actions.push(action);
                }
            }
            return GameObjectModel;
        })();
        WebClient.GameObjectModel = GameObjectModel;
    })(BadPony.WebClient || (BadPony.WebClient = {}));
    var WebClient = BadPony.WebClient;
})(BadPony || (BadPony = {}));
//# sourceMappingURL=GameObjectModel.js.map
