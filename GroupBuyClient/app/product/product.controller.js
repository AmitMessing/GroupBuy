mainApp
    .controller('productController', [
        '$scope', '$stateParams', '$resource', function($scope, $stateParams, $resource) {

            var api = $resource("/GroupBuyServer/api/products/:id", { id: '@id' });
            var productId = $stateParams.id;

            var calcProduct = function(discounts) {
                var orderdDiscounts = discounts.sort(function (a, b) {
                    return b.usersAmount - a.usersAmount;
                });

                var currentDiscountPresent = 0;
                var isFound = false;

                angular.forEach(orderdDiscounts, function(discout) {
                    if (!isFound && $scope.product.buyers.length >= discout.usersAmount) {
                        currentDiscountPresent = discout.present;
                        discout.isCurrent = true;
                        isFound = true;
                    }
                });

                $scope.currentPrice = $scope.calcPrice(currentDiscountPresent);
            };

            var sortDiscountAascending = function() {
                return $scope.product.discounts.sort(function(a, b) {
                    return a.usersAmount - b.usersAmount;
                });
            };

            var getProduct = function(id) {
                api.get({ id: id }).$promise.then(function(product) {
                    if (product) {
                        $scope.product = product;
                        calcProduct($scope.product.discounts);
                        sortDiscountAascending();
                    }
                }, function(error) {
                    $scope.errorMessage = error.data.Message;
                });
            };

            getProduct(productId);

            $scope.calcPrice = function(discount) {
                var price = $scope.product.basicPrice;
                return (price - ((price * discount) / 100)).toFixed(2);
            };
        }
    ]);