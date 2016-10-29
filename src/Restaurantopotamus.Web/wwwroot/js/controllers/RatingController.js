angular.module('app.Hippo')
    .controller('RatingController', RatingController);

RatingController.$inject = ['$scope', '$http'];

function RatingController($scope, $http) {
    $scope.rate = function () {
        debugger;
    }
}