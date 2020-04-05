import { autoinject } from 'aurelia-framework';
import { IFriendship } from 'domain/IFriendship';
import { FriendshipService } from 'service/friendshipService';

@autoinject
export class FriendshipsIndex {
    private _friendships: IFriendship[] = [];

    constructor(private friendshipService: FriendshipService) {

    }

    attached() {
        this.friendshipService.getFriendships().then(
            data => this._friendships = data
        );
    }
}
