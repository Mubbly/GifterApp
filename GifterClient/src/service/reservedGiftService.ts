import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IReservedGift, IReservedGiftCreate, IReservedGiftEdit, IReservedGiftEditTemp } from 'domain/IReservedGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import * as Utils from 'utils/utilFunctions';

// TODO: Remove temporary workaround to satisfy ReservedGifts service
@autoinject
export class ReservedGiftService extends BaseService<IReservedGift, IReservedGiftCreate, IReservedGiftEditTemp> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.RESERVED_GIFTS, httpClient, appState);
    }
}
