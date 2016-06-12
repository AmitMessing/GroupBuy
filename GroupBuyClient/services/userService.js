mainApp.service('userService', ['$cookies', '$window', '$state', function ($cookies,$window, $state) {
    var setLoggedUSer = function (usr) {
        $window.sessionStorage.setItem("loggedUser", JSON.stringify(usr));
    };

    var getLoggedUser = function () {
        var user = $window.sessionStorage.getItem("loggedUser");
        if (user && user !== "undefined") {
            return JSON.parse(user);
        }
        return null;
    };

    var logout = function () {
        $window.sessionStorage.removeItem("loggedUser");
        $state.go('home');
    };

    return {
        setLoggedUSer: setLoggedUSer,
        getLoggedUser: getLoggedUser,
        logout: logout,
    };
}]
);