import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IGift, IGiftCreate, IGiftEdit } from 'domain/IGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class GiftService extends BaseService<IGift, IGiftCreate, IGiftEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.GIFTS, httpClient, appState);
    }
}
