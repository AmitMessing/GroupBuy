'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell', {
            abstract: true,
            controller: 'shellController',
            templateUrl: 'app/shell/shell.template.html'
        });
    }
    ]);