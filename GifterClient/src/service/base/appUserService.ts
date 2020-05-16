import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './baseService';
import { IAppUser } from 'domain/IAppUser';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import * as Utils from 'utils/utilFunctions';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class AppUserService extends BaseService<IAppUser, IAppUser, IAppUser> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.APP_USERS, httpClient, appState);
    }

    async create(entity: IAppUser): Promise<IFetchResponse<IAppUser>> {
        return Utils.actionNotAllowed();
    }

    async update(entity: IAppUser): Promise<IFetchResponse<IAppUser>> {
        return Utils.actionNotAllowed();
    }

    async delete(id: string): Promise<IFetchResponse<string>> {
        return Utils.actionNotAllowed();
    }


}
