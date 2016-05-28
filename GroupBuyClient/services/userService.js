mainApp.service('userService', ['$cookies', '$state', function ($cookies, $state) {
    var setLoggedUSer = function (usr) {
        $cookies.put("loggedUser", JSON.stringify(usr));
    };

    var getLoggedUser = function () {
        var user = $cookies.get("loggedUser");
        if (user) {
            return JSON.parse(user);
        }
        return null;
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