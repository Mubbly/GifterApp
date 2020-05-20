import { autoinject } from 'aurelia-framework';
import { AppState } from "state/appState";
import { AppUserService } from 'service/base/appUserService';
import { NotificationService } from 'service/notificationService';
import { CampaignService } from 'service/campaignService';

@autoinject
export class HomeIndex {
    private _userFullName = '';
    constructor(private appState: AppState, 
        private appUserService: AppUserService,
        private notificationService: NotificationService,
        private campaignService: CampaignService) {
    }
}
