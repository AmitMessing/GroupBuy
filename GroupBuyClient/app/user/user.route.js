﻿'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.user', {
            url: '/user/:id',
            templateUrl: 'app/user/user.template.html',
            controller: 'userController'
        });
    }
    ]);