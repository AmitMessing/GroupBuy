
angular.module('uiRouterApp', [])
    .config(['$stateProvider', '$urlRouterProvider',
        function ($stateProvider,   $urlRouterProvider) {
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
                }).
                state('productDetails',
                {
                    url: '/productDetails',
                    templateUrl: 'html/product-details.html',
                    controller: ''
                }).
                state('contactUs',
                {
                    url: '/contactUs',
                    templateUrl: 'html/contact-us.html',
                    controller:''
                })
}]);