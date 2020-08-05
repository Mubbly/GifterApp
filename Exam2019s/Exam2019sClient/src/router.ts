import { PLATFORM } from 'aurelia-framework';

// First route should always be home view
export default [
        { route: ['', 'home', 'home/index'], name: 'homeIndex', moduleId: PLATFORM.moduleName('views/home/index'), nav: false, title: 'Home' },
        
        { route: ['account/login'], name: 'accountLogin', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
        { route: ['account/register'], name: 'accountRegister', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

        // { route: ['about', 'about/index'], name: 'aboutIndex', moduleId: PLATFORM.moduleName('views/about/index'), nav: false, title: 'About' },
        // { route: ['about/contacts'], name: 'aboutContacts', moduleId: PLATFORM.moduleName('views/about/contacts'), nav: false, title: 'Contacts' },
        // { route: ['about/help'], name: 'aboutHelp', moduleId: PLATFORM.moduleName('views/about/help'), nav: false, title: 'Help' },
        // { route: ['search/index', 'search', 'about/search', 'about/search/index'], name: 'search', moduleId: PLATFORM.moduleName('views/about/search'), nav: false, title: 'Search' },
        // { route: ['friendships/pending'], name: 'friendshipsPending', moduleId: PLATFORM.moduleName('views/friendships/pending'), nav: false, title: 'Pending friendships' },

        { route: ['examples', 'examples/index'], name: 'examplesIndex', moduleId: PLATFORM.moduleName('views/examples/index'), nav: true, title: 'Example' },
        { route: ['examples/details/:id'], name: 'examplesDetails', moduleId: PLATFORM.moduleName('views/examples/details'), nav: false, title: 'examplesDetails' },
        { route: ['examples/edit/:id?'], name: 'examplesEdit', moduleId: PLATFORM.moduleName('views/examples/edit'), nav: false, title: 'examplesEdit' },
        { route: ['examples/delete/:id?'], name: 'examplesDelete', moduleId: PLATFORM.moduleName('views/examples/delete'), nav: false, title: 'examplesDelete' },
        { route: ['examples/create'], name: 'examplesCreate', moduleId: PLATFORM.moduleName('views/examples/create'), nav: false, title: 'examplesCreate' },
]
