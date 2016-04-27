mainApp
    .controller('shellController', [
        '$scope', '$element', '$state', function ($scope, $element, $state) {
            $scope.isOpen = false;
            $scope.categories = ['Fashion', 'Gifts', 'Home', 'Man Fashion'];
            $scope.goHome = function() {
                $state.go('shell.home');
            };
        }
    ]);