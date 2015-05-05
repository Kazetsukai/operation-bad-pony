/// <reference path="../_app.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var BadPony;
(function (BadPony) {
    (function (WebClient) {
        'use strict';

        var LocationModel = (function (_super) {
            __extends(LocationModel, _super);
            function LocationModel(data) {
                _super.call(this, data);
                this.LocationObjects = [];

                /* More temporary hacks, I'm thinking we'll change LocationView on the server side
                * to just return one collection - may even negate the need for a GameObjectModel
                * class w/ constructor */
                if (data.Jobs.length > 0) {
                    var jobs = {
                        "Key": "Jobs",
                        "Value": []
                    };

                    for (var i = 0; i < data.Jobs.length; i++) {
                        var thisModel = new WebClient.GameObjectModel(data.Jobs[i]);
                        jobs.Value.push(thisModel);
                    }

                    this.LocationObjects.push(jobs);
                }

                if (data.Contents.length > 0) {
                    var items = {
                        "Key": "Items",
                        "Value": []
                    };

                    for (var i = 0; i < data.Contents.length; i++) {
                        var thisModel = new WebClient.GameObjectModel(data.Contents[i]);
                        items.Value.push(thisModel);
                    }

                    this.LocationObjects.push(items);
                }

                if (data.Exits.length > 0) {
                    var exits = {
                        "Key": "Exits",
                        "Value": []
                    };

                    for (var i = 0; i < data.Exits.length; i++) {
                        var thisModel = new WebClient.GameObjectModel(data.Exits[i]);
                        exits.Value.push(thisModel);
                    }

                    this.LocationObjects.push(exits);
                }

                if (data.Players.length > 0) {
                    var players = {
                        "Key": "Players",
                        "Value": []
                    };

                    for (var i = 0; i < data.Players.length; i++) {
                        var thisModel = new WebClient.GameObjectModel(data.Players[i]);
                        players.Value.push(thisModel);
                    }

                    this.LocationObjects.push(players);
                }
            }
            LocationModel.prototype.SelectObject = function (toSelect) {
                this.SelectedItem = toSelect !== this.SelectedItem ? toSelect : null;
            };
            return LocationModel;
        })(WebClient.GameObjectModel);
        WebClient.LocationModel = LocationModel;
    })(BadPony.WebClient || (BadPony.WebClient = {}));
    var WebClient = BadPony.WebClient;
})(BadPony || (BadPony = {}));
//# sourceMappingURL=LocationModel.js.map
