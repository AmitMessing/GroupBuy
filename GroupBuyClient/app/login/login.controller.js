mainApp
    .controller('loginController', [
        '$scope', '$stateParams', '$state', function ($scope, $stateParams, $state) {
            $scope.user = {};

            $scope.register = function() {
                $state.go("shell.register");
            }
            
        }
    ]);