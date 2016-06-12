mainApp
    .controller('registerController', [
        '$scope', '$stateParams', '$resource', '$state', 'userService', function ($scope, $stateParams, $resource,$state, userService) {

            var register = $resource("/GroupBuyServer/api/register/register", {});
            var register = $resource("/GroupBuyServer/api/users/register");
            $scope.user = {
                firstName: "",
                lastName: "",
                userName: "",
                password: "",
                email: "",
                image: "",
                registerDate: ""
            };

            $scope.uploadMessage = "Click to select picture";
            var imageLoader = document.getElementById("uploadBtn");

            function handleImage(e) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    var img = new Image();
                    img.onload = function() {
                        $scope.product.image = event.srcElement.result;
                    }
                }
                reader.readAsDataURL(e.target.files[0]);
            }
            imageLoader.addEventListener('change', handleImage, false);
           
            imageLoader.onchange = function () {
                $scope.user.image = this.value;
                $scope.uploadMessage = this.value;
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