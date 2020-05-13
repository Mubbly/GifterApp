import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IReservedGift, IReservedGiftCreate, IReservedGiftEdit } from 'domain/IReservedGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';

@autoinject
export class ReservedGiftService extends BaseService<IReservedGift, IReservedGiftCreate, IReservedGiftEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.RESERVED_GIFTS, httpClient, appState);
    }
}
