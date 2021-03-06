﻿mainApp
    .controller('shellController', [
        '$scope', '$element', '$state', '$window', '$resource', 'userService', function ($scope, $element, $state, $window, $resource, userService) {

            $scope.isOpen = false;
            $scope.categories = ['Fashion', 'Gifts', 'Home', 'Man Fashion'];
           
            $scope.user = {};
            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = user;
            }
            else {
                $scope.user = undefined;
            }

            $scope.userZone = function() {
                $state.go('shell.user', { id: user .id});
            };

            $scope.addPost = function () {
                $state.go('shell.post');
            };

            $scope.logout = function () {
                $scope.user = undefined;
                userService.setLoggedUSer($scope.user);
                $state.go('shell.home',{}, {reload: true});
            };

            $scope.goHome = function() {
                $state.go('shell.home');
            };

            $scope.gotoLogin = function () {
                $state.go('shell.login');
            };

            $scope.isSearching = false;

            $scope.searchClicked = function() {
                if (!$scope.isSearching) {
                    $scope.isSearching = true;
                } else {
                    $scope.isSearching = false;
                }

                $state.go('shell.search', { searchQuery: $scope.searchText });
            };

            $scope.viewShoppingCart = function() {
                $state.go('shell.history');
            }
    }
    ]);

mainApp.directive("scroll", function ($window) {
    return function (scope, element, attrs) {

        angular.element($window).bind("scroll", function () {
            if (this.pageYOffset >= 30) {
                scope.boolChangeClass = true;
            } else {
                scope.boolChangeClass = false;
            }
            scope.$apply();
        });
    };
});
