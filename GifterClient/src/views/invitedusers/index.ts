import { autoinject } from 'aurelia-framework';
import { IInvitedUser } from 'domain/IInvitedUser';
import { InvitedUserService } from 'service/invitedUserService';

@autoinject
export class InvitedUsersIndex {
    private _invitedUsers: IInvitedUser[] = [];

    constructor(private invitedUserService: InvitedUserService) {

    }

    attached() {
        this.invitedUserService.getInvitedUsers().then(
            data => this._invitedUsers = data
        );
    }
}
