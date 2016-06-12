mainApp
    .controller('historyController', [
        '$scope', '$resource', '$state', 'userService', function ($scope, $resource, $state, userService) {

            var purchases = $resource("/GroupBuyServer/api/purchases/userPurchases", {});
            $scope.loading = true;

            purchases.query({ id: userService.getLoggedUser().id }).$promise.then(function (result) {
                $scope.userPurchases = result;
                $scope.loading = false;
            });

            $scope.gotoProduct = function (id) {
                $state.go('shell.product', { id: id });
            };
        }
    ]);