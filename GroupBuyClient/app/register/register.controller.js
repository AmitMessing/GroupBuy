mainApp
    .controller('registerController', [
        '$scope', '$stateParams', '$resource', '$state', 'userService', function ($scope, $stateParams, $resource,$state, userService) {

            var register = $resource("/GroupBuyServer/api/register/register", {});
            $scope.user = {
                firstName: "",
                lastName: "",
                userName: "",
                password: ""
            };


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

            $scope.register = function (user) {
                if (validateUser(user)) {
                    register.save($scope.user).$promise.then(function(user) {
                        if (user) {
                            userService.setLoggedUSer($scope.user);
                            $state.go("shell.home", {}, { reload: true });
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