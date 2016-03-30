mainApp.service('userService', ['$cookies', '$state', function ($cookies, $state) {
    var setLoggedUSer = function (usr) {
        $cookies.put("loggedUser", JSON.stringify(usr));
    };

    var getLoggedUser = function () {
        return $cookies.get("loggedUser");
    };

    var logout = function () {
        $cookies.remove("loggedUser");
        $state.go('home');
    };

    return {
        setLoggedUSer: setLoggedUSer,
        getLoggedUser: getLoggedUser,
        logout: logout,
    };
}]
);