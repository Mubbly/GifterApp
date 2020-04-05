import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IPermission } from 'domain/IPermission';

@autoinject
export class PermissionService {
    private readonly _baseUrl = 'https://localhost:5001/api/Permissions';

    constructor(private httpClient: HttpClient) {

    }

    getPermissions(): Promise<IPermission[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IPermission[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getPermission(id: string): Promise<Optional<IPermission>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IPermission) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
