mainApp
    .controller('searchController', ['$scope', '$resource', '$state', '$stateParams', '$sce', function ($scope, $resource, $state, $stateParams, $sce) {
        $scope.currentPage = 1;

        var search = $resource("/GroupBuyServer/api/search/search", {}, { 'post': { method: 'POST', isArray: false } });
        search.post({ searchText: $stateParams.searchQuery, page: $scope.currentPage }).$promise.then(function (result) {
            $scope.products = result.searchResult;
            $scope.pagesNeeded = result.pagesNeeded;
            $scope.isShowMoreDisabled = $scope.pagesNeeded <= $scope.currentPage;
        }, function (error) {

        });

        

        $scope.trustStringAsHtml = function (title) {
            return $sce.trustAsHtml(title);
        }

        $scope.gotoProduct = function (id) {
            $state.go('shell.product', { id: id });
        };

        $scope.showMore = function () {
            $scope.currentPage++;

            search.post({ searchText: $stateParams.searchQuery, page: $scope.currentPage }).$promise.then(function (result) {
                $scope.products = $scope.products.concat(result.searchResult);
                $scope.isShowMoreDisabled = $scope.pagesNeeded <= $scope.currentPage;
            }, function (error) {

            });
        }
    }]);