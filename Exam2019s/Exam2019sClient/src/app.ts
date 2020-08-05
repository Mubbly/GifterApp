import { autoinject } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';
import routes from 'router';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class App {
    public readonly APP_NAME = 'ExamApp';
    public readonly HOME_ROUTE = 'homeIndex';
    public readonly LOGIN_ROUTE = 'accountLogin';

    router?: Router;

    constructor(private appState: AppState) {
    }

    activate() {
        this.initAppTheme();
    }

    attached() {}

    configureRouter(config: RouterConfiguration, router: Router): void {
        const DEFAULT_VIEW = routes[0].moduleId; // First route should always be home view

        this.router = router;
        config.title = this.APP_NAME;
        config.map(routes);
        config.mapUnknownRoutes(DEFAULT_VIEW);
    }

    onLogOut() {
        this.appState.jwt = null;
        this.router!.navigateToRoute(this.HOME_ROUTE);
    }

    onToggleTheme(event: Event) {
        event.preventDefault();

        let body = document.getElementsByTagName('body')[0];
        let isDarkTheme = this.appState.isDarkTheme && body.classList.contains(Utils.DARK_THEME_CLASS);

        if(isDarkTheme) {
            // Set light theme
            body.classList.remove(Utils.DARK_THEME_CLASS);
            this.appState.isDarkTheme = null;
        } else {
            // Set dark theme
            body.classList.add(Utils.DARK_THEME_CLASS);
            this.appState.isDarkTheme = "true";
        }
    }

    private initAppTheme(): void {
        if(this.appState.isDarkTheme) {
            let body = document.getElementsByTagName('body')[0];
            body.classList.add(Utils.DARK_THEME_CLASS);
        }
    }
}
