mainApp.controller('userController', ['$scope', '$state', '$http', 'userService', function ($scope, $state, $http, userService) {
    var user = userService.getLoggedUser();
    if(user != null){
        $scope.user = JSON.parse(user);
    }
    else {
        $scope.user = undefined;
    }

    $scope.error = "";
    $scope.loginDetails = {
        userName: "",
        password: ""
    };
    $scope.registerDetails = {
        userName: "",
        password: "",
        email: ""
    };

    var validateLoginFields = function(){
        if($scope.loginDetails.userName === "" || $scope.loginDetails.userName === undefined ||
            $scope.loginDetails.password === "" || $scope.loginDetails.password === undefined)
        {
            $scope.error = "נא למלא את כל השדות";
            return false;
        }
        return true;
    };

    var validateRegisterationFields = function(){
        if ($scope.registerDetails.userName === "" || $scope.registerDetails.userName === undefined ||
            $scope.registerDetails.email === "" || $scope.registerDetails.email === undefined ||
            $scope.registerDetails.password === "" || $scope.registerDetails.password === undefined)
        {
            $scope.error = "נא למלא את כל השדות";
            return false;
        }
        return true;
    };

    $scope.login = function (){
        if(validateLoginFields())
        {
            $http.post("/GroupBuyServer/api/login/", $scope.loginDetails)
                .success (function(user) {
                    if(user)
                    {
                        userService.setLoggedUSer(user);
                        $state.go('home');
                    }
                    else {
                        $scope.error = "שם משתמש או סיסמא שגויים, נסה שנית";
                        $scope.$apply();
                    }
                }
            )
            .error(function(data) {
                console.log(data);
            });
        }
    };

    $scope.register = function(){
        if(validateFields())
        {
            $.ajax({
                method   : 'POST',
                url      : '/register',
                data     : $scope.registerDetails,
                dataType : 'json',
                success: function(result) {
                    if(result.error != null || result.error != undefined)
                    {
                        $scope.error = "שם משתמש כבר קיים";
                        $scope.$apply();
                    }else {
                        userService.setLoggedUSer(result);
                        $state.go('home');
                    }
                }
            });
        }
    }  
}]);