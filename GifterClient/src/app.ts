import { autoinject } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';
import routes from 'router';
// import 'bootstrap/dist/css/bootstrap.min.css';
import { AppState } from 'state/appState';
import { AppUserService } from './service/base/appUserService';
import { IAppUserEdit } from 'domain/IAppUser';
import * as Utils from 'utils/utilFunctions';
import { INotification, IUserNotificationEdit } from 'domain/INotification';
import { Optional } from 'types/generalTypes';
import { NotificationService } from './service/notificationService';
import { IFetchResponse } from './types/IFetchResponse';
import { IUserNotification } from './domain/IUserNotification';
import { NotificationTypes } from 'domain/PredefinedData';
import { HttpClient } from 'aurelia-fetch-client';

@autoinject
export class App {
    public readonly APP_NAME = 'GifterApp';
    public readonly HOME_ROUTE = 'homeIndex';
    public readonly LOGIN_ROUTE = 'accountLogin';
    public readonly FRIENDS_LIST_ROUTE = 'friendshipsIndex';
    public readonly FRIEND_REQUESTS_ROUTE = 'friendshipsPending';
    public readonly NO_NEW_NOTIFICATIONS_MESSAGE = 'No new notifications';
    public readonly ERROR_NOT_MARKED_AS_SEEN_MESSAGE = 'Error: Could not mark as seen';

    router?: Router;

    private _notifications: IUserNotification[] = [];
    private _noNotificationMessage: Optional<string> = null;

    constructor(private appState: AppState,
        private notificationService: NotificationService) {
    }

    activate() {
        this.initAppTheme();
        this.getAllActiveNotifications();
        
        // this.initNavTabs();
        // setInterval(this.getAllActiveNotifications, 5000); // Check for new notifications after every 10 minutes (600000ms) or 5mins (300000ms). TODO: For testing, 5sek (5000).
    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        const DEFAULT_VIEW = routes[0].moduleId; // First route should always be home view

        this.router = router;
        config.title = this.APP_NAME;
        config.map(routes);
        config.mapUnknownRoutes(DEFAULT_VIEW);
    }

    onNotificationClick(event: Event, notification: IUserNotification) {
        event.preventDefault();
        if(notification && notification.id) {
            this.markAsInactive(notification);

            switch(notification.notification.notificationTypeId) {
                case NotificationTypes.NEW_FRIEND_REQUEST:
                    return this.router!.navigateToRoute(this.FRIEND_REQUESTS_ROUTE);
                    case NotificationTypes.ACCEPTED_FRIEND_REQUEST:
                    return this.router!.navigateToRoute(this.FRIENDS_LIST_ROUTE);
                default:
                    Utils.refreshPage();
            }
        }
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

    private getAllActiveNotifications(): void {
        this.notificationService
        .getAllPersonalActive()
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this._noNotificationMessage = this.NO_NEW_NOTIFICATIONS_MESSAGE;
            } else {
                if(!response.data || response.data.length <= 0) {
                    this._noNotificationMessage = this.NO_NEW_NOTIFICATIONS_MESSAGE;
                    return;
                }
                this._noNotificationMessage = null;
                this._notifications = response.data;
                console.log('Notifications: ', this._notifications);
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private markAsInactive(userNotification: IUserNotification): Promise<void> {
        return this.notificationService
        .markAsInactive(userNotification)
        .then((response) => {
            if(!Utils.isSuccessful(response)) {
                this._noNotificationMessage = this.ERROR_NOT_MARKED_AS_SEEN_MESSAGE;
            } else {
                this._noNotificationMessage = null;
                console.log('Notifications: ', response.data);
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    // private initNavTabs(): void {
    //     let activeNavLink = document.getElementsByClassName('nav-link.active')[0];
    //     if(!activeNavLink) 
    //     {
    //         // TODO
    //     }
    // }
}
