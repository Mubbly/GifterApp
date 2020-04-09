import { Optional } from "types/generalTypes";

export class AppState {
    constructor() {

    }

    public readonly _baseUrl = 'https://localhost:5001/api/';

    // Json Web Token to keep track of logged in status
    public jwt: Optional<string> = null;
}
