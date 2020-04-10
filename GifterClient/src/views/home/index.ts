import { autoinject } from 'aurelia-framework';
import { AppState } from "state/appState";

@autoinject
export class HomeIndex {
    constructor(private appState: AppState) {

    }
}
