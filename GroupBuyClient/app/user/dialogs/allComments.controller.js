mainApp
    .controller('allCommentsController', [
        '$scope', '$stateParams', '$resource', '$state', '$mdDialog', 'userService',
        function ($scope, $stateParams, $resource, $state, $mdDialog, userService) {

            var user = userService.getLoggedUser();
            if (user != null) {
                $scope.user = user;
            }
            else {
                $scope.user = undefined;
            }

            $scope.user.sellerRate = [
                   {
                       commenter: "yulia",
                       content: "coolll products",
                       date: "22/2/16"
                   },
                   {
                       commenter: "amit",
                       content: "coolll products ,hkjhkjhk",
                       date: "22/2/16"
                   },
                   {
                       commenter: "amit",
                       content: "coolll products ,hkjhkjhk",
                       date: "22/2/16"
                   },
                   {
                       commenter: "amit",
                       content: "coolll products ,hkjhkjhk",
                       date: "22/2/16"
                   },
                   {
                       commenter: "amit",
                       content: "coolll products ,hkjhkjhk",
                       date: "22/2/16"
                   }
            ];

        }]);

