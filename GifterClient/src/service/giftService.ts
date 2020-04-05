import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IGift } from 'domain/IGift';
import { IFetchResponse } from 'types/IFetchResponse';
import { IGiftCreate } from 'domain/IGiftCreate';
import { IGiftEdit } from 'domain/IGiftEdit';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class GiftService {
    private readonly _baseUrl = 'https://localhost:5001/api/Gifts';

    constructor(private httpClient: HttpClient) {

    }

    async getGifts(): Promise<IFetchResponse<IGift[]>> {
        try {
            const response = await this.httpClient.fetch(this._baseUrl);

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IGift[];
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch(reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getGift(id: string): Promise<IFetchResponse<IGift>> {
        try {
            const response = await this.httpClient.fetch(`${this._baseUrl}/${id}`);

            if(UtilFunctions.isSuccessful(response)) {
                const data = (await response.json()) as IGift;
                console.log(data);
                return {
                    status: response.status,
                    data: data
                }
            }
            return {
                status: response.status,
                errorMessage: response.statusText
            }
        } catch(reason) {
            return {
                status: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async createGift(gift: IGiftCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(gift));

            if(UtilFunctions.isSuccessful(response)) {
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

    async updateGift(gift: IGiftEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.put(`${this._baseUrl}/${gift.id}`, JSON.stringify(gift));

            if(UtilFunctions.isSuccessful(response)) {
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

    async deleteGift(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient.delete(`${this._baseUrl}/${id}`, null);

            if(UtilFunctions.isSuccessful(response)) {
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
}
