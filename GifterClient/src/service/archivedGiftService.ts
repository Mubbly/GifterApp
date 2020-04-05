import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IArchivedGift } from 'domain/IArchivedGift';

@autoinject
export class ArchivedGiftService {
    private readonly _baseUrl = 'https://localhost:5001/api/ArchivedGifts';

    constructor(private httpClient: HttpClient) {

    }

    getArchivedGifts(): Promise<IArchivedGift[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IArchivedGift[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getArchivedGift(id: string): Promise<Optional<IArchivedGift>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IArchivedGift) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
