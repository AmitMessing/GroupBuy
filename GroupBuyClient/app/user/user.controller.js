mainApp
    .controller('userController', [
        '$scope', '$stateParams', '$resource', '$state', '$mdDialog', '$mdMedia', 'userService',
        function ($scope, $stateParams, $resource, $state, $mdDialog, $mdMedia, userService) {

            var userApi = $resource("/GroupBuyServer/api/users/user");
            var reviewsApi = $resource("/GroupBuyServer/api/users/reviews");
            var saveReviewsApi = $resource("/GroupBuyServer/api/onBuyerReviews/save");

            $scope.newReview = { rating: 3 };
            $scope.currentUser = {
                firstName: "",
                lastName: "",
                userName: "",
                password: "",
                email: "",
                image: "",
                registerDate: ""
            };
            $scope.reviews = [];


            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.loggedUser = user;
            }

            var loadReviews = function () {
                reviewsApi.get({ id: $scope.currentUser.id }).$promise.then(function (result) {
                   return $scope.reviews = result;
                });
            }

            $scope.id = $stateParams.id;
            if ($scope.id) {
                userApi.get({ id: $scope.id }).$promise.then(function(result) {
                    $scope.currentUser = result;
                    loadReviews();
                });

            } else {
                $scope.currentUser = $scope.loggedUser;
                loadReviews();
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

            $scope.dateFormat = function (date) {
                if (date && typeof date === 'string') {
                    var format = date.split("T")[0].split("-");
                    return format[2] + "." + format[1] + "." + format[0];
                }
            };

            $scope.getRatingArray = function (rating) {
                return new Array(rating);
            };

            $scope.saveReview = function () {
                $scope.newReview.publishDate = new Date();
                $scope.newReview.onUserId = $scope.currentUser.id;
                $scope.newReview.reviewerId = $scope.loggedUser.id;
                return saveReviewsApi.save($scope.newReview).$promise
                    .then(function (newRate) {
                        $scope.user.sellerRate = newRate.newRating;
                        $scope.newReview.content = "";
                        loadReviews();
                    }, function (error) {
                        $scope.errorMessage = error.data.Message;
                    });
            };

        }
    ]);