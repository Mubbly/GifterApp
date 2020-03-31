import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { IActionType } from '../domain/IActionType';

@autoinject
export class ActionTypeService {
    private readonly _baseUrl = 'https://localhost:5001/api/ActionTypes';

    constructor(private httpClient: HttpClient) {

    }

    getActionTypes(): Promise<IActionType[]> {
        return this.httpClient
            .fetch(this._baseUrl)
            .then(response => response.json())
            .then((data: IActionType[]) => data)
            .catch(reason => { 
                console.log(reason); 
                return []; 
            })
    }

    getActionType(id: string) {

    }

    // TODO: .. update, delete etc
}
