angular.module('app.Hippo')
    .controller('RatingController', RatingController);

RatingController.$inject = ['$scope', '$http', 'toastr'];

function RatingController($scope, $http, toastr) {
    this.rate = function (rating) {
        $http.post('/api/rating', { "value": rating, "restaurantId": this.restaurantId }).then(function successCallback(response) {
            toastr.success('Rating received!');
        }, function errorCallback(response) {
        });
    };
}