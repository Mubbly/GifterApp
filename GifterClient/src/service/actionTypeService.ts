import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IActionType, IActionTypeCreate, IActionTypeEdit } from 'domain/IActionType';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class ActionTypeService extends BaseService<IActionType, IActionTypeCreate, IActionTypeEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.ACTION_TYPES, httpClient, appState);
    }
}
