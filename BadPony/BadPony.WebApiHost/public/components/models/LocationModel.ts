/// <reference path="../_app.ts" />

module BadPony.WebClient {
    'use strict';

    export class LocationModel extends GameObjectModel {
        LocationObjects: IGameObjectGroup[];
        SelectedItem: GameObjectModel;

        constructor(data: any) {
            super(data);
            this.LocationObjects = [];

            /* More temporary hacks, I'm thinking we'll change LocationView on the server side
             * to just return one collection - may even negate the need for a GameObjectModel
             * class w/ constructor */
            if (data.Jobs.length > 0) {
                var jobs = {
                    "Key": "Jobs",
                    "Value": []
                };

                for (var i: number = 0; i < data.Jobs.length; i++) {
                    var thisModel = new GameObjectModel(data.Jobs[i]);
                    jobs.Value.push(thisModel);
                }

                this.LocationObjects.push(jobs);
            }

            if (data.Contents.length > 0) {
                var items = {
                    "Key": "Items",
                    "Value": []
                };

                for (var i: number = 0; i < data.Contents.length; i++) {
                    var thisModel = new GameObjectModel(data.Contents[i]);
                    items.Value.push(thisModel);
                }

                this.LocationObjects.push(items);
            }

            if (data.Exits.length > 0) {
                var exits = {
                    "Key": "Exits",
                    "Value": []
                };

                for (var i: number = 0; i < data.Exits.length; i++) {
                    var thisModel = new GameObjectModel(data.Exits[i]);
                    exits.Value.push(thisModel);
                }

                this.LocationObjects.push(exits);
            }

            if (data.Players.length > 0) {
                var players = {
                    "Key": "Players",
                    "Value": []
                };

                for (var i: number = 0; i < data.Players.length; i++) {
                    var thisModel = new GameObjectModel(data.Players[i]);
                    players.Value.push(thisModel);
                }

                this.LocationObjects.push(players);
            }
        }

        SelectObject(toSelect: GameObjectModel): void {
            this.SelectedItem = toSelect !== this.SelectedItem ? toSelect : null;
        }
    }
}
