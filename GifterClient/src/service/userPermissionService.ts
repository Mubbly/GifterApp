import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IUserPermission } from 'domain/IUserPermission';

@autoinject
export class UserPermissionService {
    private readonly _baseUrl = 'https://localhost:5001/api/UserPermissions';

    constructor(private httpClient: HttpClient) {

    }

    getUserPermissions(): Promise<IUserPermission[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IUserPermission[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getUserPermission(id: string): Promise<Optional<IUserPermission>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IUserPermission) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
