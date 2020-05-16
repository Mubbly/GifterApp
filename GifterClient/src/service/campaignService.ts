import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { ICampaign, ICampaignCreate, ICampaignEdit } from 'domain/ICampaign';
import { BaseService } from './base/baseService';
import { AppState } from 'state/appState';

@autoinject
export class CampaignService extends BaseService<ICampaign, ICampaignCreate, ICampaignEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.CAMPAIGNS, httpClient, appState);
    }
}
