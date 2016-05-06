mainApp
    .controller('shellController', [
        '$scope', '$element', '$state', '$window', function ($scope, $element, $state, $window) {
            $scope.isOpen = false;
            $scope.categories = ['Fashion', 'Gifts', 'Home', 'Man Fashion'];
            $scope.goHome = function() {
                $state.go('shell.home');
            };
        }
    ]);

mainApp.directive("scroll", function ($window) {
    return function (scope, element, attrs) {

        angular.element($window).bind("scroll", function () {
            if (this.pageYOffset >= 100) {
                scope.boolChangeClass = true;
                console.log('Scrolled below header.');
            } else {
                scope.boolChangeClass = false;
                console.log('Header is in view.');
            }
            scope.$apply();
        });
    };
});
