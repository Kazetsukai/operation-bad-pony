/// <reference path="../_app.ts" />

module BadPony.WebClient {
    'use strict';

    export class GameObjectModel {
        Id: number;
        Name: string;
        Description: string;
        Properties: { [Key: string]: string; }
        Actions: GameObjectModel[];

        constructor(data) {
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
            }
            else if(typeof (data.DestinationId) !== "undefined") {
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
    }
}
