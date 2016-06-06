mainApp
    .controller('userController', [
        '$scope', '$stateParams', '$resource', '$state', '$mdDialog', '$mdMedia', 'userService', function ($scope, $stateParams, $resource, $state, $mdDialog, $mdMedia, userService) {

            var userSave = $resource("/GroupBuyServer/api/user", {});
            $scope.user = {
                firstName: "",
                lastName: "",
                userName: "",
                password: ""
            };

            $scope.showSellerComment = function (ev) {
                $mdDialog.show({
                    controller: 'allCommentsController',
                    templateUrl: 'app/user/dialogs/allComments.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true
                });
            };

            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = user;
                $scope.user.sellerRate = [
                    {
                        commenter: "yulia",
                        content: "coolll products",
                        date: "22/2/16"
                    },
                    {
                        commenter: "amit",
                        content: "coolll products ,hkjhkjhk",
                        date: "22/2/16"
                    }
                ];
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