mainApp
    .controller('postController', [
        '$scope', '$stateParams', function($scope, $stateParams) {
            $scope.discounts = [
            {
                numberOfBuyers: 5,
                value: 10
            },
            {
                numberOfBuyers: 15,
                value: 20
            }];

        }
    ]);