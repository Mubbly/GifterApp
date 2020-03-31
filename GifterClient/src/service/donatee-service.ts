import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IDonatee } from '../domain/IDonatee';

@autoinject
export class DonateeService {
    private readonly _baseUrl = 'https://localhost:5001/api/Donatees';

    constructor(private httpClient: HttpClient) {

    }

    getDonatees(): Promise<IDonatee[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IDonatee[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return []; 
            })
    }

    getDonatee(id: string) {

    }

    // TODO: .. update, delete etc
}
