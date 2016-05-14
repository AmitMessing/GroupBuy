'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.login', {
            url: '/login',
            templateUrl: 'app/login/login.template.html',
            controller: 'loginController'
        });
    }
    ]);