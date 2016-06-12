'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.search', {
            url: '/search/:searchQuery',
            templateUrl: 'app/search/search.template.html',
            controller: 'searchController'
        });
    }
    ]);