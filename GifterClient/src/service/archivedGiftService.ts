import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IArchivedGift, IArchivedGiftCreate, IArchivedGiftEdit } from 'domain/IArchivedGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class ArchivedGiftService extends BaseService<IArchivedGift, IArchivedGiftCreate, IArchivedGiftEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.ARCHIVED_GIFTS, httpClient, appState);
    }
}
