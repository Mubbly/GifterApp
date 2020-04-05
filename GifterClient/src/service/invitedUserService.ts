import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IInvitedUser } from 'domain/IInvitedUser';

@autoinject
export class InvitedUserService {
    private readonly _baseUrl = 'https://localhost:5001/api/InvitedUsers';

    constructor(private httpClient: HttpClient) {

    }

    getInvitedUsers(): Promise<IInvitedUser[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IInvitedUser[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getInvitedUser(id: string): Promise<Optional<IInvitedUser>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IInvitedUser) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
