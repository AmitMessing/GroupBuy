mainApp
    .controller('buyersDialogController', [
        '$scope', '$mdDialog', '$resource', 'buyers', 'product', 'isSeller', function ($scope, $mdDialog, $resource, buyers, product, isSeller) {
            var buyersApi = $resource("/GroupBuyServer/api/buyers");
            $scope.buyers = buyers;
            $scope.productId = product;
            $scope.isSeller = isSeller;

            $scope.cancel = function () {
                $mdDialog.cancel();
            };

            $scope.deleteBuyer = function (buyer) {
                buyersApi.delete({ productId: $scope.productId, buyer: buyer.userName }).$promise
                .then(function (result) {
                    _.remove($scope.buyers, function(item) {
                        return item.userName === result.buyer;
                    });
                });
            };
        }
    ]);