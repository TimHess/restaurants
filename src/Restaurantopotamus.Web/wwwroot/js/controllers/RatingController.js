angular.module('app')
    .controller('RatingController', RatingController);

RatingController.$inject = ['$scope', '$http'];

function RatingController($scope, $http) {
    $http.get('/api/rating/' + this.restaurantid).then(function successCallback(response) {
        $scope.ratingSummary = response.data;
    }, function errorCallback(response) {
        debugger;
    });

}