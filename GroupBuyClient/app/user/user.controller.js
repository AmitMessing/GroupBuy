mainApp
    .controller('userController', [
        '$scope', '$stateParams', '$resource', '$state', 'userService', function ($scope, $stateParams, $resource,$state, userService) {

            var userSave = $resource("/GroupBuyServer/api/register", {});
            $scope.user = {
                firstName: "",
                lastName: "",
                userName: "",
                password: ""
            };

            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = JSON.parse(user);
            }
            else {
                $scope.user = undefined;
            }


            var validateUser = function(user) {
                if (user.firstName === "" || user.firstName === undefined) {
                    return false;
                }
                else if (user.lastName === "" || user.lastName === undefined) {
                    return false;
                }
                else if (user.userName === "" || user.userName === undefined) {
                    return false;
                }
                else if (user.password === "" || user.password === undefined) {
                    return false;
                }
                return true;
            }

            $scope.updateUser = function (user) {
                if (validateUser(user)) {
                    userSave.save($scope.user).$promise.then(function(user) {
                        if (user) {
                            userService.setLoggedUSer($scope.user);
                            $state.go("shell.user", {}, { reload: true });
                        }
                    }, function(error) {
                        $scope.errorMessage = error.data.Message;
                    });

                } else {
                    $scope.errorMessage = "Missing details.";
                }
            }
        }
    ]);