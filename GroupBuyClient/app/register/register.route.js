'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.register', {
            templateUrl: 'app/register/register.template.html',
            controller: 'registerController'
        });
    }
    ]);