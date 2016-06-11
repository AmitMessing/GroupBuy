mainApp
    .controller('loginController', [
        '$scope', '$stateParams', '$state', '$resource', 'userService', function ($scope, $stateParams, $state, $resource, userService) {
            $scope.user = {};

            $scope.register = function() {
                $state.go("shell.register");
            }

            var loginUser = $resource("/GroupBuyServer/api/login/login", {});

            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = JSON.parse(user);
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
                    $scope.error = "נא למלא את כל השדות";
                    return false;
                }
                return true;
            };

            $scope.login = function () {
                if (validateLoginFields()) {
                    loginUser.save($scope.loginDetails)
                        .$promise.then(function(user) {
                            if (user) {
                                userService.setLoggedUSer(user);
                                $state.go('shell.home', {}, { reload: true });
                            }
                        }, function(error) {
                             $scope.error = error.data.message;
                        });
                }
            };
        }
    ]);