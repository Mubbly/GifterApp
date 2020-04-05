import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IPrivateMessage } from 'domain/IPrivateMessage';

@autoinject
export class PrivateMessageService {
    private readonly _baseUrl = 'https://localhost:5001/api/PrivateMessages';

    constructor(private httpClient: HttpClient) {

    }

    getPrivateMessages(): Promise<IPrivateMessage[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IPrivateMessage[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getPrivateMessage(id: string): Promise<Optional<IPrivateMessage>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IPrivateMessage) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
