import { Optional } from "types/generalTypes";

export class AppState {
    constructor(){
    }

    //public readonly baseUrl = 'https://localhost:5001/api/';

    // JavaScript Object Notation Web Token 
    // to keep track of logged in status
    // https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage
    get jwt(): Optional<string> {
        return localStorage.getItem('jwt');
    }

    set jwt(value: Optional<string>){
        if (value){
            localStorage.setItem('jwt', value);
        } else {
            localStorage.removeItem('jwt');
        }
    }
}
