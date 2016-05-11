mainApp
    .controller('registerController', [
        '$scope', '$stateParams','$resource', function($scope, $stateParams,$resource) {

            var register = $resource("/GroupBuyServer/api/register/:id", { id: '@id' });
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
                    register.save($scope.user);

                } else {
                    $scope.errorMessage = "Missing details.";
                }
            }
        }
    ]);