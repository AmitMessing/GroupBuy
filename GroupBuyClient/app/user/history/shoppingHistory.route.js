'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function ($stateProvider) {
        $stateProvider.state('shell.history', {
            url: '/shoppingCart',
            templateUrl: 'app/user/history/shoppingHistory.template.html',
            controller: 'historyController'
        });
    }
    ]);