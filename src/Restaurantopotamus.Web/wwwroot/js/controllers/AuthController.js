angular.module('app.Hippo')
    .controller('AuthController', AuthController);

AuthController.$inject = ['$scope', '$http', '$rootScope'];

function AuthController($scope, $http, $rootScope) {
    $scope.authenticated = false;

    $scope.$watch('authenticated', function () {
        $rootScope.authenticated = $scope.authenticated;
    });

    $scope.login = function () {
        console.log('login');
        $scope.authenticated = true;
    };

    $scope.trylogin = function () {
        console.log('getting a token');
        $http.post('/token', { "value": rating, "restaurantId": this.restaurantId })
            .then(function successCallback(response) {
                console.log('got a token');
                $scope.authenticated = true;
                }, function errorCallback(response) {
            });
    };

    $scope.logout = function () {
        console.log('logged out');
        $scope.authenticated = false;
    }
}