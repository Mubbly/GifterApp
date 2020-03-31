import { EventAggregator, Subscription } from 'aurelia-event-aggregator';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { IGifterApp } from "domain/IGifterApp";
import { EventTypes } from 'types/EventTypes';
import { RouterConfiguration, Router } from 'aurelia-router';

@autoinject
export class App {
    router?: Router;

    constructor() {

    }

    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;
        config.title = "Donatees";

        config.map([
            {
                route: ['', 'home', 'home/index'],
                name: 'home',
                moduleId: PLATFORM.moduleName('views/home/index'),
                nav: true,
                title: 'Home'
            },
            {
                route: ['statuses', 'statuses/index'],
                name: 'statuses',
                moduleId: PLATFORM.moduleName('views/statuses/index'),
                nav: true,
                title: 'Statuses'
            },
            {
                route: ['actiontypes', 'actiontypes/index'],
                name: 'actiontypes',
                moduleId: PLATFORM.moduleName('views/actiontypes/index'),
                nav: true,
                title: 'ActionTypes'
            },
            {
                route: ['campaigns', 'campaigns/index'],
                name: 'campaigns',
                moduleId: PLATFORM.moduleName('views/campaigns/index'),
                nav: true,
                title: 'Campaigns'
            },
            {
                route: ['donatees', 'donatees/index'],
                name: 'donatees',
                moduleId: PLATFORM.moduleName('views/donatees/index'),
                nav: true,
                title: 'Donatees'
            },
            {
                route: ['campaigndonatees', 'campaigndonatees/index'],
                name: 'campaigndonatees',
                moduleId: PLATFORM.moduleName('views/campaigndonatees/index'),
                nav: true,
                title: 'CampaignDonatees'
            }
        ]);

        config.mapUnknownRoutes('views/home/index');
    }

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

