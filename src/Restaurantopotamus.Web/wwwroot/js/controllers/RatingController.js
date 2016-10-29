angular.module('app.Hippo')
    .controller('RatingController', RatingController);

RatingController.$inject = ['$scope', '$http'];

function RatingController($scope, $http) {
    this.rate = function (rating) {
        $http.post('/api/rating', { "value": rating, "restaurantId": this.restaurantId }).then(function successCallback(response) {
            console.log('rating received');
        }, function errorCallback(response) {
        });
    };
}