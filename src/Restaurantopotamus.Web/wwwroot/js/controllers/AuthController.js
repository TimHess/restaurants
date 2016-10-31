angular.module('app.Hippo')
    .controller('AuthController', AuthController);

AuthController.$inject = ['$scope', '$http', 'authenticated'];

function AuthController($scope, $http, authenticated) {


    $scope.login = function () {
        console.log('login');
    };

    $scope.trylogin = function () {
        console.log('getting a token');
        $http.post('/token', { "value": rating, "restaurantId": this.restaurantId })
            .then(function successCallback(response) {
                console.log('got a token');
                authenticated = true;
                }, function errorCallback(response) {
            });
    };
}