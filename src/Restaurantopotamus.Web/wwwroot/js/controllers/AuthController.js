angular.module('app.Hippo')
    .controller('AuthController', AuthController);

AuthController.$inject = ['$scope', '$http', '$rootScope'];

function AuthController($scope, $http, $rootScope) {
    var user = sessionStorage.getItem('user');
    if (user == null) {
        $scope.authenticated = false;
    }
    else {
        $scope.authenticated = true;
        $scope.user = JSON.parse(user);
    }

    $scope.showForm = false;

    $scope.$watch('authenticated', function () {
        $rootScope.authenticated = $scope.authenticated;
    });

    $scope.login = function () {
        $scope.showForm = "login";
        $scope.buttonText = "Sign In";
        focusOnUsername();
    };

    $scope.trylogin = function (user) {
        console.log('getting a token');
        $http.post('/token', "username=" + encodeURIComponent(user.name) + "&password=" + encodeURIComponent(user.password), { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
            .success(function (response) {
                user.token = response.access_token;
                sessionStorage.setItem('user', JSON.stringify(user));
                $scope.authenticated = true;
            }).error(function(response) {
                alert(response);
            });
    };

    $scope.formSubmit = function (user) {
        if (user == null || user.name == null || user.password == null) {
            alert('Please enter a username and password');
            return;
        }
        if ($scope.showForm === 'login') {
            $scope.trylogin(user);
        } else if ($scope.showForm === 'register') {
            $scope.tryregister(user);
        }
    }

    $scope.register = function () {
        $scope.showForm = "register";
        $scope.buttonText = "Register";
        focusOnUsername();
    }

    $scope.tryregister = function (user) {
        console.log('registering');
        $http.post('/api/auth/register', { "username": user.name, "password": user.password })
            .success(function (response) {
                user.token = response.access_token;
                sessionStorage.setItem('user', JSON.stringify(user));
                $scope.authenticated = true;
            }).error(function (response) {
                alert(response);
            });
    };

    $scope.reset = function () {
        $scope.user = null;
        $scope.showForm = false;
    }

    $scope.logout = function () {
        console.log('logged out');
        $scope.authenticated = false;
        sessionStorage.removeItem('token');
    }

    function focusOnUsername () {
        window.setTimeout(function () {
            document.getElementById('username').focus();
        }, 10);
    }
}