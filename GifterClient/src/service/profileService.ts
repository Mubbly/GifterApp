import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IProfile } from 'domain/IProfile';

@autoinject
export class ProfileService {
    private readonly _baseUrl = 'https://localhost:5001/api/Profiles';

    constructor(private httpClient: HttpClient) {

    }

    getProfiles(): Promise<IProfile[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IProfile[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getProfile(id: string): Promise<Optional<IProfile>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IProfile) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
