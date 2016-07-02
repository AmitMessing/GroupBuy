mainApp
    .controller('changePasswordDialogController', [
        '$scope', '$mdDialog', '$resource', '$state', 'user', 'userService', function ($scope, $mdDialog, $resource, $state, user, userService) {
            var api = $resource("/GroupBuyServer/api/users/changePassword");

            var currentUser = user;
            $scope.cancel = function() {
                $mdDialog.cancel();
            };

            $scope.save = function() {
                if (!$scope.changePassword || !$scope.changePassword.old || !$scope.changePassword.new || !$scope.changePassword.reNew) {
                    $scope.errorMessage = 'please enter all the fileds!';
                    $scope.error = true;
                } else if (currentUser.password !== $scope.changePassword.old) {
                    $scope.errorMessage = 'old passord is incorrect!';
                    $scope.error = true;
                } else if ($scope.changePassword.new !== $scope.changePassword.reNew) {
                    $scope.errorMessage = 'you must enter the same password!';
                    $scope.error = true;
                } else {
                    $scope.error = false;
                    currentUser.password = $scope.changePassword.new;
                    api.save(currentUser).$promise.then(function (result) {
                        userService.setLoggedUSer(result);
                        $scope.cancel();
                    });
                }
            };
        }
    ]);