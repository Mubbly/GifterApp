import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { BaseService } from './base/baseService';
import { IGift, IGiftCreate, IGiftEdit } from 'domain/IGift';
import * as ApiEndpointUrls from 'utils/apiEndpointUrls';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import { IReservedGift, IReservedGiftEdit } from '../domain/IReservedGift';

@autoinject
export class GiftService extends BaseService<IGift, IGiftCreate, IGiftEdit> {
    constructor(protected httpClient: HttpClient, appState: AppState) {
        super(ApiEndpointUrls.GIFTS, httpClient, appState);
    }

    async getAllForUser(userId: string): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/User/${userId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    // -------------------------------------------- RESERVED GIFTS ------------------------------------------

    async getAllPersonalReserved(): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Reserved/${ApiEndpointUrls.PERSONAL}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getPersonalReserved(giftId: string): Promise<IFetchResponse<IGift>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Reserved/${ApiEndpointUrls.PERSONAL}/${giftId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    /** Reserve gift - updates Active status Gift to Reserved status */
    async updateToReservedStatus(entity: IGift, userReceiverId: string): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}

        const targetGiftId = entity.id;
        const targetReservedGift: IReservedGiftEdit = {
            giftId: targetGiftId,
            userReceiverId: userReceiverId,
            comment: null
        }
        try {
            const response = await this.httpClient.post(`${this.apiEndpointUrl}/Reserved`, 
                JSON.stringify(targetReservedGift),
                { 
                    cache: "no-store",
                    headers: AUTH_HEADERS
                }
            );

            if(response.ok) {
                console.log('response', response);
                return {
                    status: response.status
                    // no data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    /** Mark as Gifted - deletes reservation, updates Reserved status Gift to Archived status */
    async updateToGiftedStatus(entity: IGift, userReceiverId: string): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        
        const targetGiftId = entity.id;
        const targetReservedGift: IReservedGiftEdit = {
            giftId: targetGiftId,
            userReceiverId: userReceiverId,
            comment: null
        }
        try {
            const response = await this.httpClient.put(`${this.apiEndpointUrl}/Reserved/${targetGiftId}`, 
            JSON.stringify(targetReservedGift),
                { 
                    cache: "no-store",
                    headers: AUTH_HEADERS
                }
            );
        
            if(response.ok) {
                return {
                    status: response.status
                    // no data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    /** Cancel reservation - deletes reservation, changes Reserved status Gift back to Active status */
    async cancelReservation(entity: IGift, userReceiverId: string): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}

        const targetGiftId = entity.id;
        const targetReservedGift: IReservedGiftEdit = {
            giftId: targetGiftId,
            userReceiverId: userReceiverId,
            comment: null
        }
        try {
            const response = await this.httpClient.delete(`${this.apiEndpointUrl}/Reserved/${targetGiftId}`, 
                JSON.stringify(targetReservedGift),
                { 
                    cache: "no-store",
                    headers: AUTH_HEADERS
                }
            );

            if(response.ok) {
                console.log('response', response);
                return {
                    status: response.status
                    // no data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    // -------------------------------------------- ARCHIVED GIFTS ------------------------------------------

    
    async getAllGivenArchived(): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Archived/Given`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getAllReceivedArchived(): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Archived/Received`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getAllPendingReceivedArchived(): Promise<IFetchResponse<IGift[]>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Archived/Received/Pending`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getGivenArchived(giftId: string): Promise<IFetchResponse<IGift>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Archived/Given/${giftId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getReceivedArchived(giftId: string): Promise<IFetchResponse<IGift>> {
        const AUTH_HEADERS = { 'Authorization': 'Bearer ' + this.appState.jwt}
        try {
            const response = await this.httpClient
                .fetch(`${this.apiEndpointUrl}/Archived/Received/${giftId}`, {
                    cache: "no-store",
                    headers: AUTH_HEADERS
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as IGift;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                status: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
}
