import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IFriendship } from 'domain/IFriendship';

@autoinject
export class FriendshipService {
    private readonly _baseUrl = 'https://localhost:5001/api/Friendships';

    constructor(private httpClient: HttpClient) {

    }

    getFriendships(): Promise<IFriendship[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IFriendship[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getFriendship(id: string): Promise<Optional<IFriendship>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IFriendship) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
