angular.module('app.Hippo')
    .controller('RestaurantController', RestaurantController);

RestaurantController.$inject = ['$scope', '$http'];

function RestaurantController($scope, $http) {
    $http.get('/api/restaurant').then(function successCallback(response) {
        $scope.Restaurants = response.data;
    }, function errorCallback(response) {
        debugger;
    });

    $scope.add = function () {

    }

    $scope.addSubmit = function () {

    }

    $scope.edit = function () {

    }

    $scope.editSubmit = function () {

    }

    $scope.remove = function (id) {
        if (!confirm("Are you sure? You will not be able to undo this action!")) {
            return;
        }

        $http.delete('/api/restaurant/' + id).then(function successCallback(response) {
            var element = document.getElementById("r" + id);
            element.parentNode.removeChild(element);
        }, function errorCallback(response) {
            debugger;
        });
    }
}