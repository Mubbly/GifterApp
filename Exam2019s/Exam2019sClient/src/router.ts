import { PLATFORM } from 'aurelia-framework';

// First route should always be home view
export default [
        { route: ['', 'home', 'home/index'], name: 'homeIndex', moduleId: PLATFORM.moduleName('views/home/index'), nav: false, title: 'Home' },

        { route: ['account/login'], name: 'accountLogin', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
        { route: ['account/register'], name: 'accountRegister', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

        // Admin

        { route: ['admin', 'admin/index'], name: 'adminIndex', moduleId: PLATFORM.moduleName('views/admin/index'), nav: false, title: 'Admin' },

        { route: ['admin/quizzes/create'], name: 'adminQuizzesCreate', moduleId: PLATFORM.moduleName('views/admin/quizzes/create'), nav: false, title: 'Quiz create' },
        // { route: ['admin/quizzes/edit/:id?'], name: 'adminQuizzesEdit', moduleId: PLATFORM.moduleName('views/admin/quizzes/edit'), nav: false, title: 'Quiz edit' },
        // { route: ['admin/quizzes/delete/:id?'], name: 'adminQuizzesDelete', moduleId: PLATFORM.moduleName('views/admin/quizzes/delete'), nav: false, title: 'Quiz delete' },
        
        { route: ['admin/questions/create'], name: 'adminQuestionsCreate', moduleId: PLATFORM.moduleName('views/admin/questions/create'), nav: false, title: 'Question create' },
        // { route: ['admin/questions/edit/:id?'], name: 'adminQuestionsEdit', moduleId: PLATFORM.moduleName('views/admin/questions/edit'), nav: false, title: 'Question edit' },
        // { route: ['admin/questions/delete/:id?'], name: 'adminQuestionsDelete', moduleId: PLATFORM.moduleName('views/admin/questions/delete'), nav: false, title: 'Question delete' },
        
        { route: ['admin/answers/create'], name: 'adminAnswersCreate', moduleId: PLATFORM.moduleName('views/admin/answers/create'), nav: false, title: 'Answer create' },
        // { route: ['admin/answers/edit/:id?'], name: 'adminAnswersEdit', moduleId: PLATFORM.moduleName('views/admin/answers/edit'), nav: false, title: 'Answer edit' },
        // { route: ['admin/answers/delete/:id?'], name: 'adminAnswersDelete', moduleId: PLATFORM.moduleName('views/admin/answers/delete'), nav: false, title: 'Answer delete' },

        // General

        { route: ['examples', 'examples/index'], name: 'examplesIndex', moduleId: PLATFORM.moduleName('views/examples/index'), nav: false, title: 'Example' },
        { route: ['examples/details/:id'], name: 'examplesDetails', moduleId: PLATFORM.moduleName('views/examples/details'), nav: false, title: 'examplesDetails' },
        { route: ['examples/edit/:id?'], name: 'examplesEdit', moduleId: PLATFORM.moduleName('views/examples/edit'), nav: false, title: 'examplesEdit' },
        { route: ['examples/delete/:id?'], name: 'examplesDelete', moduleId: PLATFORM.moduleName('views/examples/delete'), nav: false, title: 'examplesDelete' },
        { route: ['examples/create'], name: 'examplesCreate', moduleId: PLATFORM.moduleName('views/examples/create'), nav: false, title: 'examplesCreate' },

        { route: ['quizzes', 'quizzes/index'], name: 'quizzesIndex', moduleId: PLATFORM.moduleName('views/quizzes/index'), nav: false, title: 'Quizzes' },
        { route: ['quizzes/details/:id'], name: 'quizzesDetails', moduleId: PLATFORM.moduleName('views/quizzes/details'), nav: false, title: 'Quiz questions' },
    ]
