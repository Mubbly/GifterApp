import { PLATFORM } from 'aurelia-framework';

// First route should always be home view
export default [
        { route: ['', 'home', 'home/index'], name: 'homeIndex', moduleId: PLATFORM.moduleName('views/home/index'), nav: false, title: 'Home' },

        { route: ['account/login'], name: 'accountLogin', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
        { route: ['account/register'], name: 'accountRegister', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

        { route: ['examples', 'examples/index'], name: 'examplesIndex', moduleId: PLATFORM.moduleName('views/examples/index'), nav: false, title: 'Example' },
        { route: ['examples/details/:id'], name: 'examplesDetails', moduleId: PLATFORM.moduleName('views/examples/details'), nav: false, title: 'examplesDetails' },
        { route: ['examples/edit/:id?'], name: 'examplesEdit', moduleId: PLATFORM.moduleName('views/examples/edit'), nav: false, title: 'examplesEdit' },
        { route: ['examples/delete/:id?'], name: 'examplesDelete', moduleId: PLATFORM.moduleName('views/examples/delete'), nav: false, title: 'examplesDelete' },
        { route: ['examples/create'], name: 'examplesCreate', moduleId: PLATFORM.moduleName('views/examples/create'), nav: false, title: 'examplesCreate' },

        { route: ['quizzes', 'quizzes/index'], name: 'quizzesIndex', moduleId: PLATFORM.moduleName('views/quizzes/index'), nav: true, title: 'Quizzes' },
        { route: ['quizzes/details/:id'], name: 'quizzesDetails', moduleId: PLATFORM.moduleName('views/quizzes/details'), nav: false, title: 'Quiz questions' },
]
