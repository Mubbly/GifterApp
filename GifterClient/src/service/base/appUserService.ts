import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IAppUser } from 'domain/IAppUser';

@autoinject
export class AppUserService {
    private readonly _baseUrl = 'https://localhost:5001/api/AppUsers';

    constructor(private httpClient: HttpClient) {

    }

    getAppUsers(): Promise<IAppUser[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IAppUser[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getAppUser(id: string): Promise<Optional<IAppUser>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IAppUser) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
