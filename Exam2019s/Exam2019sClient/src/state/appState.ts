import { Optional } from "types/generalTypes";
import { ICurrentUser } from "domain/IAppUser";

export class AppState {
    constructor(){
    }
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

    // TODO: Generalize instead of repeating
    get userId(): Optional<string> {
        return localStorage.getItem('userId');
    }

    set userId(value: Optional<string>){
        if (value){
            localStorage.setItem('userId', value);
        } else {
            localStorage.removeItem('userId');
        }
    }

    get userFullName(): Optional<string> {
        return localStorage.getItem('userFullName');
    }

    set userFullName(value: Optional<string>){
        if (value){
            localStorage.setItem('userFullName', value);
        } else {
            localStorage.removeItem('userFullName');
        }
    }

    get isDarkTheme(): Optional<string> {
        return localStorage.getItem('isDarkTheme');
    }

    set isDarkTheme(value: Optional<string>){
        if (value){
            localStorage.setItem('isDarkTheme', value);
        } else {
            localStorage.removeItem('isDarkTheme');
        }
    }
}
