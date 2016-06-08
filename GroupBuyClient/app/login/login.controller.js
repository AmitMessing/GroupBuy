mainApp
    .controller('loginController', [
        '$scope', '$stateParams', '$state', '$resource', 'userService', function ($scope, $stateParams, $state, $resource, userService) {
            $scope.user = {};

            $scope.register = function() {
                $state.go("shell.register");
            }

            var loginUser = $resource("/GroupBuyServer/api/login", {});

            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = user;
            }
            else {
                $scope.user = undefined;
            }

            $scope.error = "";
            $scope.loginDetails = {
                userName: "",
                password: ""
            };

            var validateLoginFields = function () {
                if ($scope.loginDetails.userName === "" || $scope.loginDetails.userName === undefined ||
                    $scope.loginDetails.password === "" || $scope.loginDetails.password === undefined) {
                    $scope.error = "Please enter user name and password";
                    return false;
                }
                return true;
            };

            $scope.login = function () {
                if (validateLoginFields()) {
                    loginUser.save($scope.loginDetails)
                        .$promise.then(function(user) {
                            if (user.id) {
                                userService.setLoggedUSer(user);
                                $state.go('shell.home', {}, { reload: true });
                            }
                            else {
                                $scope.error = error.data.message;
                            }
                        }, function(error) {
                             $scope.error = error.data.message;
                        });
                }
            };
        }
    ]);