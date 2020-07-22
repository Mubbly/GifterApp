import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IArchivedGift, IArchivedGiftCreate, IArchivedGiftEdit } from 'domain/IArchivedGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IArchivedGiftEditTemp } from '../domain/IArchivedGift';

// TODO: Remove temporary workaround to satisfy ReservedGifts service
@autoinject
export class ArchivedGiftService extends BaseService<IArchivedGift, IArchivedGiftCreate, IArchivedGiftEditTemp> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.ARCHIVED_GIFTS, httpClient, appState);
    }
}
