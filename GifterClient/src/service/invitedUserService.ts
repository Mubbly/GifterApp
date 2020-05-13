import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IInvitedUser, IInvitedUserCreate, IInvitedUserEdit } from 'domain/IInvitedUser';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class InvitedUserService extends BaseService<IInvitedUser, IInvitedUserCreate, IInvitedUserEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.INVITED_USERS, httpClient, appState);
    }
}
