import { autoinject } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';
import routes from 'router';
import { AppState } from 'state/appState';

@autoinject
export class App {
    public readonly APP_NAME = 'GifterApp';
    public readonly HOME_ROUTE = 'homeIndex';
    router?: Router;

    constructor(private appState: AppState) {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        const DEFAULT_VIEW = routes[0].moduleId; // First route should always be home view

        this.router = router;
        config.title = this.APP_NAME;
        config.map(routes);
        config.mapUnknownRoutes(DEFAULT_VIEW);
    }

    logoutOnClick() {
        this.appState.jwt = null;
        this.router!.navigateToRoute(this.HOME_ROUTE);
    }

    //const routeConfigs = getRouteConfigs();
    //console.log(routeConfigs);
    //config.map(routeConfigs);

    // /**
    //  * Gets all RouteConfigs as list
    //  * based on names present in GifterEntities enum
    //  * and view types present in ViewTypes enum
    //  */
    // function getRouteConfigs(): RouteConfig[] {
    //     let routerConfigs: RouteConfig[] = [];

    //     for (let entity in GifterEntities) {
    //         const viewName: GifterEntities = GifterEntities[entity as keyof typeof GifterEntities];
    //         for(let type in ViewTypes) {
    //             const viewType: ViewTypes = ViewTypes[type as keyof typeof ViewTypes];
    //             const config: RouteConfig = getRouteConfig(viewName, viewType);
    //             routerConfigs.push(config);
    //         }
    //     }
    //     return routerConfigs;
    // }

    // /**
    //  * Gets one config based on view name & view type.
    //  * Example routeConfig:
    //  *      {
    //  *          route: ['campaigns', 'campaigns/index']
    //  *          name: "campaignsIndex"
    //  *          moduleId: PLATFORM.moduleName('views/campaigns/index');
    //  *          nav: true
    //  *          title: "Campaigns"
    //  *      }
    //  * @param viewName ex. User, Campaign etc
    //  * @param viewType ex. Index, Edit etc
    //  */
    // function getRouteConfig(viewName: GifterEntity, viewType: ViewTypes): RouteConfig {
    //     const VIEW_NAME = viewName.endsWith('s') ? `${viewName}es` : `${viewName}s`;
    //     const VIEW_NAME_WITH_TYPE_PATH = (`${VIEW_NAME}/${viewType}`).toLowerCase();

    //     let routePaths = [''];
    //     let routeName = `${VIEW_NAME.toLowerCase()}${viewType}`;;
    //     let routeModuleId = `views/${VIEW_NAME_WITH_TYPE_PATH}`;  
    //     let routeNav = false;
    //     let routeTitle = `${VIEW_NAME}${viewType}`;

    //     switch(viewType) {
    //         case ViewTypes.Index:
    //             routePaths = [VIEW_NAME.toLowerCase(), VIEW_NAME_WITH_TYPE_PATH];
    //             routeNav = true;
    //             routeTitle = `${VIEW_NAME}`
    //             break;
    //         case ViewTypes.Details:
    //         case ViewTypes.Edit:
    //         case ViewTypes.Delete:
    //             routePaths = [`${VIEW_NAME_WITH_TYPE_PATH}/:id`];
    //             break;
    //         case ViewTypes.Create:
    //             routePaths = [VIEW_NAME_WITH_TYPE_PATH];
    //             break;
    //         default:
    //             console.warn(`Not supported viewType was passed into getRouter(): ${viewType}`);
    //     }

    //     const routeConfig: RouteConfig = { 
    //         route: routePaths, 
    //         name: routeName, 
    //         moduleId: PLATFORM.moduleName(routeModuleId), 
    //         nav: routeNav, 
    //         title: routeTitle
    //     };

    //     return routeConfig;
    // }

//   private _subscriptions: Subscription[] = [];
//   private _todos: IGifterApp[] = [];

//   private _placeholder = "Gift description";
//   private _appTitle = "Add Gifts";
//   private _submitButtonTitle = "Add";
//   private _input = ""; // binded in html

//   constructor(private eventAggregator: EventAggregator) {
//     this._subscriptions.push(
//       this.eventAggregator.subscribe(
//         EventTypes.NewGiftAddition,
//         (desc: string) => this.eventNewGiftAddition(desc)
//       )
//     );
//   }

//   detach() {
//     this._subscriptions.forEach(subscription => subscription.dispose());
//     this._subscriptions = [];
//   }

//   eventNewGiftAddition(description: string) {
//     this._todos.push({ description: description, done: false });
//   }

//   removeTodo(index: number) {
//     this._todos.splice(index, 1);
//   }
}
