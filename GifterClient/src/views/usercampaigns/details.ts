import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IUserCampaign } from 'domain/IUserCampaign';
import { UserCampaignService } from 'service/userCampaignService';

@autoinject
export class UserCampaignDetails {
    private _userCampaigns: IUserCampaign[] = [];
    private _userCampaign: Optional<IUserCampaign> = null;

    constructor(private userCampaignService: UserCampaignService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.userCampaignService.getUserCampaign(params.id).then(
                data => this._userCampaign = data
            )
        }
    }
}
