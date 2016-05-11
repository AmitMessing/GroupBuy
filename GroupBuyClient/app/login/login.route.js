'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.login', {
            templateUrl: 'app/login/login.template.html',
            controller: 'loginController'
        });
    }
    ]);