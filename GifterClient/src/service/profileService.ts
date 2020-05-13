import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IProfile, IProfileCreate, IProfileEdit } from 'domain/IProfile';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class ProfileService extends BaseService<IProfile, IProfileCreate, IProfileEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.PROFILES, httpClient, appState);
    }
}
