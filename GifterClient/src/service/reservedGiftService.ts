import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IReservedGift } from 'domain/IReservedGift';

@autoinject
export class ReservedGiftService {
    private readonly _baseUrl = 'https://localhost:5001/api/ReservedGifts';

    constructor(private httpClient: HttpClient) {

    }

    getReservedGifts(): Promise<IReservedGift[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IReservedGift[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getReservedGift(id: string): Promise<Optional<IReservedGift>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IReservedGift) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
