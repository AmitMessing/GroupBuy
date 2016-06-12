mainApp
    .controller('productController', [
        '$scope', '$stateParams', '$resource', '$state', '$mdDialog', 'userService', function ($scope, $stateParams, $resource, $state, $mdDialog, userService) {

            var productDateApi = $resource('/GroupBuyServer/api/products/UpdateEndDate');
            var api = $resource('/GroupBuyServer/api/products/product');
            var buyersApi = $resource("/GroupBuyServer/api/buyers/save");
            var getReviewsApi = $resource("/GroupBuyServer/api/onSellerReviews/reviews");
            var saveReviewsApi = $resource("/GroupBuyServer/api/onSellerReviews/save");

            var productId = $stateParams.id;
            $scope.isSeller = false;
            $scope.isBuyer = false;
            $scope.isExpierd = false;
            $scope.newReview = { rating: 3 };

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

                $scope.product.descriptionDisplay = $scope.product.description;
            if ($scope.product.description.length > 600) {
                $scope.product.descriptionDisplay = $scope.product.description.substring(0, 600) + '...';
            }

                // Update current user status
            if ($scope.currentUser && $scope.product.seller.userName === $scope.currentUser.userName) {
                    $scope.isSeller = true;
                }

                var buyersUserName = $scope.product.buyers.map(function(buyer) {
                    return buyer.userName;
                });
                if ($scope.currentUser && buyersUserName.indexOf($scope.currentUser.userName) !== -1) {
                    $scope.isBuyer = true;
                }

            
                var today = new Date();
                $scope.minDate = today;
                $scope.endDate = new Date($scope.product.endDate.replace("T", " ").replace(/-/g, "/"));

                if ($scope.endDate < today) {
                    $scope.isExpierd = true;
                }
        };

        $scope.openDescription = function() {
            $mdDialog.show(
                        $mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title('Full Description')
                        .textContent($scope.product.description)
                        .ok('Got it!')
                    );
        };

        $scope.onEndDateChanged = function () {
            $scope.product.endDate = $scope.endDate;
            return productDateApi.save($scope.product).$promise
                .then(function() {
                }, function(error) {
                    $scope.errorMessage = error.data.Message;
                });

        };

            var sortDiscountAascending = function() {
                return $scope.product.discounts.sort(function(a, b) {
                    return a.usersAmount - b.usersAmount;
                });
            };

            var loadReviews = function () {

                $scope.newReview = {
                    rating: 3,
                    onUserId: $scope.product.seller.id,
                    productId: $scope.product.id
                };

                if ($scope.currentUser) {
                    $scope.newReview.reviewerId = $scope.currentUser.id;
                }

                return getReviewsApi.query({ id: $scope.product.seller.id }).$promise
                    .then(function(reviews) {
                        if (reviews) {
                            $scope.reviews = _.filter(reviews, function(review) { return review.productId === $scope.product.id; });
                        }
                    }, function(error) {
                        $scope.errorMessage = error.data.Message;
                    });
            };

            $scope.saveReview = function () {
                if (!$scope.currentUser) {
                    return $mdDialog.show(
                        $mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title('Pay Attention')
                        .textContent('You must log in to review a product!')
                        .ok('Got it!')
                    );
                };

                $scope.newReview.publishDate = new Date();
                return saveReviewsApi.save($scope.newReview).$promise
                    .then(function (newRate) {
                        $scope.product.seller.rating = newRate.newRating;
                        loadReviews();
                }, function(error) {
                        $scope.errorMessage = error.data.Message;
                });
            };

            $scope.showUser = function(user) {
                $state.go('shell.user', { id: user });
            };

            var initData = function (id) {
                $scope.loading = true;
                $scope.currentUser = userService.getLoggedUser();

                api.get({ id: id }).$promise.then(function(product) {
                    if (product) {
                        var products = $resource("/GroupBuyServer/api/products/suggestions");
                        products.query({ id: product.id }).$promise.then(function (response) {
                            console.log(response);
                        });
                        $scope.product = product;
                        loadReviews();
                        calcProduct($scope.product.discounts);
                        sortDiscountAascending();                        

                        $scope.loading = false;
                    }
                }, function(error) {
                    $scope.errorMessage = error.data.Message;
                    $scope.loading = false;
                });
            };

            $scope.calcPrice = function(discount) {
                var price = $scope.product.basicPrice;
                return (price - ((price * discount) / 100)).toFixed(2);
            };

            $scope.joinGroup = function () {
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
                        buyers: $scope.product.buyers,
                        product: $scope.product.id,
                        isSeller: $scope.isSeller
                    },
                    controller: 'buyersDialogController'
                });
            };

            $scope.dateFormat = function(date) {
                if (date && typeof date === 'string') {
                    var format = date.split("T")[0].split("-");
                    return format[2] + "." + format[1] + "." + format[0];
                }
            };

        $scope.getRatingArray = function(rating) {
            return new Array(rating);
        };

            initData(productId);
        }
    ]);