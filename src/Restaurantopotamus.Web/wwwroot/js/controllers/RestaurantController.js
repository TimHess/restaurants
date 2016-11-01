angular.module('app.Hippo')
    .controller('RestaurantController', RestaurantController);

RestaurantController.$inject = ['$scope', '$http', '$uibModal'];

function RestaurantController($scope, $http, $uibModal) {
    $scope.loading = true;
    $http.get('/api/restaurant').then(function successCallback(response) {
        $scope.Restaurants = response.data;
        $scope.loading = false;
    }, function errorCallback(response) {
        $scope.loading = false;
    });
    $scope.status = {
        isopen: false
    };

    var $ctrl = this;
    $ctrl.items = ['item1', 'item2', 'item3'];
    $ctrl.openComponentModal = function () {
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            component: 'modalComponent',
            resolve: {
                items: function () {
                    return $ctrl.items;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $ctrl.selected = selectedItem;
        }, function () {
            $log.info('modal-component dismissed at: ' + new Date());
        });
    };

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

        $http.delete('/api/restaurant/' + id, { headers: { "Authorization": "Bearer " + (JSON.parse(sessionStorage.getItem('user'))).token } })
            .then(function successCallback(response) {
                var element = document.getElementById("r" + id);
                element.parentNode.removeChild(element);
            }, function errorCallback(response) {
                if (response.status == 401) {
                    alert("Sorry, you're not authorized to do that");
                } else {
                    alert("Sorry, something went wrong");
                }
            });
    }
}