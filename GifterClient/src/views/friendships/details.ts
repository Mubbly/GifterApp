import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IFriendship } from 'domain/IFriendship';
import { FriendshipService } from 'service/friendshipService';

@autoinject
export class FriendshipDetails {
    private _friendships: IFriendship[] = [];
    private _friendship: Optional<IFriendship> = null;

    constructor(private friendshipService: FriendshipService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.friendshipService.getFriendship(params.id).then(
                data => this._friendship = data
            )
        }
    }
}
