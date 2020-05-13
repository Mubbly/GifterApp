import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IStatus } from 'domain/IStatus';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { IStatusEdit, IStatusCreate } from '../domain/IStatus';
import { AppState } from 'state/appState';

@autoinject
export class StatusService extends BaseService<IStatus, IStatusCreate, IStatusEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.STATUSES, httpClient, appState);
    }
}
