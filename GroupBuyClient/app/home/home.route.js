'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.home', {
            url: '/home',
            templateUrl: 'app/home/home.template.html',
            controller: 'homeController'
        });
    }
    ]);