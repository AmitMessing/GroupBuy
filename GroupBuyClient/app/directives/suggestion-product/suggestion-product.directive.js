mainApp
    .directive('suggestionProduct', function () {
    return {
        restrict: 'EA',
        templateUrl: 'app/directives/suggestion-product/suggestion-product.template.html',
        scope: {
            product: '='   
        },
        controller: function ($scope, $state) {
            $scope.gotoProduct = function (id) {
                $state.go('shell.product', { id: id });
            };

            $scope.sortTitle = function (title) {
                if (title) {
                    return title.slice(0, 26) + ' ...';
                };

                return '';
            };
        }
    };
});