import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IDonatee, IDonateeCreate, IDonateeEdit } from 'domain/IDonatee';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class DonateeService extends BaseService<IDonatee, IDonateeCreate, IDonateeEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.DONATEES, httpClient, appState);
    }
}
