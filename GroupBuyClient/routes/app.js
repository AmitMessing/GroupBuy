angular.module('uiRouterApp', [])
    .config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider,   $urlRouterProvider, $locationProvider) {
            $urlRouterProvider
                .otherwise('/');

            $stateProvider
                // home page
                .state('home', {
                    url: '/',
                    templateUrl: 'html/home.html',
                    controller: 'homeController'
                }).
                state('login',
                {
                    url: '/login',
                    templateUrl: 'html/login.html',
                    controller: 'userController'
                })
}]);