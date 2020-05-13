import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IPermission, IPermissionCreate, IPermissionEdit } from 'domain/IPermission';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class PermissionService extends BaseService<IPermission, IPermissionCreate, IPermissionEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.PERMISSIONS, httpClient, appState);
    }
}
