mainApp
    .controller('shellController', [
        '$scope', '$element', '$state', '$window', 'userService', function ($scope, $element, $state, $window, userService) {
            $scope.isOpen = false;
            $scope.categories = ['Fashion', 'Gifts', 'Home', 'Man Fashion'];
           
            $scope.user = {};
            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = JSON.parse(user);
            }
            else {
                $scope.user = undefined;
            }


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
            };
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
