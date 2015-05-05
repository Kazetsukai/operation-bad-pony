/// <reference path="../_app.ts" />

module BadPony.WebClient {
    'use strict';

    export interface ILocationScope extends ng.IScope {
        player: IPlayerInfo;
        location: any;

        doAction(actionObject: GameObjectModel): void;
    }
}