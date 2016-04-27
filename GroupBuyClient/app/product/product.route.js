'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.product', {
            url: '/product/{id}',
            templateUrl: 'app/product/product.template.html',
            controller: 'productController'
        });
    }
    ]);