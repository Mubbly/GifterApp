import { autoinject } from 'aurelia-framework';
import { AppState } from "state/appState";
import * as Utils from 'utils/utilFunctions';

@autoinject
export class AboutIndex {
    constructor(private appState: AppState) {}

    activate() {}

    attached() {}

    // onToggleTheme(event: Event) {
    //     event.preventDefault();

    //     let body = document.getElementsByTagName('body')[0];
    //     let isDarkTheme = this.appState.isDarkTheme && body.classList.contains(Utils.DARK_THEME_CLASS);

    //     if(isDarkTheme) {
    //         // Set light theme
    //         body.classList.remove(Utils.DARK_THEME_CLASS);
    //         this.appState.isDarkTheme = null;
    //     } else {
    //         // Set dark theme
    //         body.classList.add(Utils.DARK_THEME_CLASS);
    //         this.appState.isDarkTheme = "true";
    //     }
    // }
}
