mainApp
    .controller('searchController', ['$scope', '$resource', '$state', '$stateParams', '$sce', function ($scope, $resource, $state, $stateParams, $sce) {
        var search = $resource("/GroupBuyServer/api/search/search", {}, { 'post': { method: 'POST', isArray: true } });

        search.post({ searchText: $stateParams.searchQuery }).$promise.then(function (searchResult) {
            $scope.products = searchResult;
        }, function (error) {

        });

        $scope.trustStringAsHtml = function (title) {
            return $sce.trustAsHtml(title.replace("<em>","<strong>").replace("</em>","</strong>"));
        }

        $scope.gotoProduct = function (id) {
            $state.go('shell.product', { id: id });
        };
    }]);