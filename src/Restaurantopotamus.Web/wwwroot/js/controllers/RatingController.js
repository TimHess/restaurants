angular.module('app.Hippo')
    .controller('RatingController', RatingController);

RatingController.$inject = ['$scope', '$http', 'toastr'];

function RatingController($scope, $http, toastr) {
    this.rate = function (rating) {
        // TODO: prevent abuse
        $http.post('/api/rating', { "value": rating, "restaurantId": this.restaurantId }).then(function successCallback(response) {
            toastr.success('Rating received!');
            $scope.$ctrl.total++;
        }, function errorCallback(response) {
            toastr.error('Sorry, there was an error processing your rating!');
        });
    };
}