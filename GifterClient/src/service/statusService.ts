import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { Optional } from 'types/generalTypes';
import { IStatus } from 'domain/IStatus';

@autoinject
export class StatusService {
    private readonly _baseUrl = 'https://localhost:5001/api/Statuses';

    constructor(private httpClient: HttpClient) {

    }

    getStatuses(): Promise<IStatus[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IStatus[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return [];
            });
    }

    getStatus(id: string): Promise<Optional<IStatus>> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id)
            .then(response => response.json())
            .then((data: IStatus) => data)
            .catch(reason => { 
                console.log(reason);
                return null;
            });
    }

    // TODO: .. update, delete etc
}
