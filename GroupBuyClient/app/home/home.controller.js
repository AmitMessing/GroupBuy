mainApp
    .controller('homeController', ['$scope', '$state','$resource', 'userService', function ($scope, $state,$resource, userService) {

        var homeApi = $resource("/GroupBuyServer/api/home/home", {}, { 'get': { method: 'GET', isArray: true } });
        $scope.user = {};
        var user = userService.getLoggedUser();
        if (user != null) {
            $scope.user = user;
        }
        else {
            $scope.user = undefined;
        }

    $scope.gotoProduct = function(id) {
        $state.go('shell.product', { id: id });
    };

    $scope.sortContent = function(content) {
        return content.slice(0, 76) + '...';
    };

    $scope.sortTitle = function (title) {
        return title.slice(0, 15) + '...';
    };

    var onload = function () {
        $scope.loading = true;
            homeApi.get().$promise.then(function(result) {
                $scope.products = result;
                $scope.loading = false;
            }, function(error) {
                var i = error;
                $scope.loading = false;
            });
        };

        
    

        onload();
    }]);