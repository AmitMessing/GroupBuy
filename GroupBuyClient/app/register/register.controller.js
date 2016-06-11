mainApp
    .controller('registerController', [
        '$scope', '$stateParams', '$resource', '$state', 'userService', function ($scope, $stateParams, $resource,$state, userService) {

            var register = $resource("/GroupBuyServer/api/register/register", {});
            $scope.user = {
                firstName: "",
                lastName: "",
                userName: "",
                password: "",
                email: "",
                image: ""
            };

            document.getElementById("uploadBtn").onchange = function () {
                document.getElementById("uploadFile").value = this.value;
                $scope.user.image = this.value;
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
                            userService.setLoggedUSer(user);
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