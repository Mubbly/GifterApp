import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IFriendship, IFriendshipCreate, IFriendshipEdit } from 'domain/IFriendship';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class FriendshipService extends BaseService<IFriendship, IFriendshipCreate, IFriendshipEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.FRIENDSHIPS, httpClient, appState);
    }
}
