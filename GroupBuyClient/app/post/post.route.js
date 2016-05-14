'use strict';

angular.module('mainApp')
    .config(['$stateProvider', function($stateProvider) {
        $stateProvider.state('shell.post', {
            url: '/post',
            templateUrl: 'app/post/post.template.html',
            controller: 'postController'
        });
    }
    ]);