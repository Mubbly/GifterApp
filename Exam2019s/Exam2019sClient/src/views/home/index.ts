import { autoinject } from 'aurelia-framework';
import { AppState } from "state/appState";
import { Optional } from 'types/generalTypes';

@autoinject
export class HomeIndex {
    private readonly DEFAULT_NAME = 'friend';
    private _userFullName: Optional<string> = this.DEFAULT_NAME;

    constructor(private appState: AppState) {
    }

    activate(props: any) {
        if(this.appState.jwt && this.appState.userFullName) {
            this._userFullName = this.appState.userFullName;
        }
    }
}
