import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IStatus } from '../domain/IStatus';

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
            })
    }

    getStatus(id: string) {

    }

    // TODO: .. update, delete etc
}
