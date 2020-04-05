import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IUserProfile } from 'domain/IUserProfile';

@autoinject
export class UserProfileService {
    private readonly _baseUrl = 'https://localhost:5001/api/UserProfiles';

    constructor(private httpClient: HttpClient) {

    }

    getUserProfiles(): Promise<IUserProfile[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IUserProfile[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getUserProfile(id: string): Promise<Optional<IUserProfile>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IUserProfile) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
