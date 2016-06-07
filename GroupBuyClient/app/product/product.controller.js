mainApp
    .controller('productController', [
        '$scope', '$stateParams', '$resource', '$mdDialog', 'userService', function($scope, $stateParams, $resource, $mdDialog, userService) {

            var api = $resource("/GroupBuyServer/api/products");
            var buyersApi = $resource("/GroupBuyServer/api/buyers");
            var reviewsApi = $resource("/GroupBuyServer/api/onSellerReviews");

            var productId = $stateParams.id;
            $scope.isSeller = false;
            $scope.isBuyer = false;
            $scope.isExpierd = false;
            $scope.newReview = {};
            $scope.newReview.rating = 3;

            var calcProduct = function(discounts) {
                // Check current discount
                var orderdDiscounts = discounts.sort(function(a, b) {
                    return b.usersAmount - a.usersAmount;
                });

                var currentDiscountPrecent = 0;
                var isFound = false;

                angular.forEach(orderdDiscounts, function(discout) {
                    if (!isFound && $scope.product.buyers.length >= discout.usersAmount) {
                        currentDiscountPrecent = discout.precent;
                        discout.isCurrent = true;
                        isFound = true;
                    }
                });

                $scope.currentPrice = $scope.calcPrice(currentDiscountPrecent);

                // Update current user status
                if ($scope.product.seller.userName === $scope.currentUser.userName) {
                    $scope.isSeller = true;
                }

                if ($scope.product.buyers.indexOf($scope.currentUser.userName) !== -1) {
                    $scope.isBuyer = true;
                }

                var today = new Date();
                var endDate = new Date($scope.product.endDate.replace("T", " ").replace(/-/g, "/"));

                if (endDate < today) {
                    $scope.isExpierd = true;
                }
            };

            var sortDiscountAascending = function() {
                return $scope.product.discounts.sort(function(a, b) {
                    return a.usersAmount - b.usersAmount;
                });
            };

        var bla = function(sellerId) {
            return reviewsApi.query({ id: sellerId }).$promise.then(function (sherker) {
                if (sherker) {
                    var f = sherker;
                }
            }, function (error) {
                $scope.errorMessage = error.data.Message;
            });
        };

            var initData = function(id) {
                $scope.currentUser = userService.getLoggedUser();

                return api.get({ id: id }).$promise.then(function(product) {
                    if (product) {
                        $scope.product = product;
                        bla(product.seller.id);
                        calcProduct($scope.product.discounts);
                        sortDiscountAascending();
                    }
                }, function(error) {
                    $scope.errorMessage = error.data.Message;
                });
            };

            $scope.calcPrice = function(discount) {
                var price = $scope.product.basicPrice;
                return (price - ((price * discount) / 100)).toFixed(2);
            };

            $scope.joinGroup = function() {
                if (!$scope.currentUser) {
                    $mdDialog.show(
                        $mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title('Pay Attention')
                        .textContent('You must log in to join this group!')
                        .ok('Got it!')
                    );
                } else {
                    buyersApi.save({ productId: $scope.product.id, buyerId: $scope.currentUser.id })
                        .$promise.then(function(result) {
                            if (result) {
                                initData(result.productId);
                                $mdDialog.show(
                                    $mdDialog.alert()
                                    .clickOutsideToClose(true)
                                    .title('Congratulations!')
                                    .textContent('You joined the group!')
                                    .ok('Got it!')
                                );
                            }
                        }, function(error) {
                            $scope.errorMessage = error.data.Message;
                        });
                }
            };

            $scope.showBuyers = function() {
                $mdDialog.show({
                    templateUrl: 'app/product/buyers-dialog/buyers-dialog.template.html',
                    parent: angular.element(document.body),
                    clickOutsideToClose: true,
                    locals: {
                        buyers: $scope.product.buyers
                    },
                    controller: function(scope, buyers) {
                        scope.buyers = buyers;
                        scope.cancel = function() {
                            $mdDialog.cancel();
                        };
                    }
                });
            };

            $scope.dateFormat = function(date) {
                if (date) {
                    var format = date.split("T")[0].split("-");
                    return format[2] + "." + format[1] + "." + format[0];
                }
            };

            initData(productId);
        }
    ]);