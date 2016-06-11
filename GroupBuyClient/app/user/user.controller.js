mainApp
    .controller('userController', [
        '$scope', '$stateParams', '$resource', '$state', '$mdDialog', '$mdMedia', 'userService',
        function ($scope, $stateParams, $resource, $state, $mdDialog, $mdMedia, userService) {

            var userApi = $resource("/GroupBuyServer/api/users/user");
            $scope.currentUser = {
                firstName: "",
                lastName: "",
                userName: "",
                password: "",
                email: "",
                registerDate: ""
        };

            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.loggedUser = user;
            }

            $scope.id = $stateParams.id;
            if ($scope.id) {
                userApi.get({ id: $scope.id }).$promise.then(function(result) {
                    $scope.currentUser = result;
                });
            } else {
                $scope.currentUser = $scope.loggedUser;
            }

            $scope.showSellerComment = function (ev) {
                $mdDialog.show({
                    controller: 'allCommentsController',
                    templateUrl: 'app/user/dialogs/allComments.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true
                });
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

            $scope.updateUser = function (user) {
                if (validateUser(user)) {
                    userApi.save($scope.currentUser).$promise.then(function (user) {
                        if (user) {
                            userService.setLoggedUSer($scope.currentUser);
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