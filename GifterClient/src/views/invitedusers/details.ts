import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IInvitedUser } from 'domain/IInvitedUser';
import { InvitedUserService } from 'service/invitedUserService';

@autoinject
export class InvitedUserDetails {
    private _invitedUsers: IInvitedUser[] = [];
    private _invitedUser: Optional<IInvitedUser> = null;

    constructor(private invitedUserService: InvitedUserService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.invitedUserService.getInvitedUser(params.id).then(
                data => this._invitedUser = data
            )
        }
    }
}
