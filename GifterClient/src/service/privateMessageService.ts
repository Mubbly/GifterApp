import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IPrivateMessage, IPrivateMessageCreate, IPrivateMessageEdit } from 'domain/IPrivateMessage';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class PrivateMessageService extends BaseService<IPrivateMessage, IPrivateMessageCreate, IPrivateMessageEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.PRIVATE_MESSAGES, httpClient, appState);
    }
}
