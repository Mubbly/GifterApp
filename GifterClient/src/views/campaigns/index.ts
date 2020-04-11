import { autoinject } from 'aurelia-framework';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaignService';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';

@autoinject
export class CampaignsIndex {
    private _campaigns: ICampaign[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private campaignService: CampaignService) {

    }

    attached() {
        this.getAllCampaigns();
    }

    private getAllCampaigns(): void {
        this.campaignService.getCampaigns().then(
            response => {
                if(UtilFunctions.isSuccessful(response)) {
                    this._campaigns = response.data!;
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                }
            }
        );
    }
}
