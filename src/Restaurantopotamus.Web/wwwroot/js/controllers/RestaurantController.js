angular.module('app.Hippo')
    .controller('RestaurantController', RestaurantController);

RestaurantController.$inject = ['$scope', '$http', 'toastr'];

function RestaurantController($scope, $http, toastr) {
    $scope.loading = true;
    $scope.addOrEdit = false;
    $http.get('/api/restaurant').then(function successCallback(response) {
        $scope.Restaurants = response.data;
        $scope.loading = false;
    }, function errorCallback(response) {
        $scope.loading = false;
    });

    $scope.add = function () {
        $scope.addOrEdit = true;
        $scope.addEditButtonText = "Add Restaurant";
        $scope.restaurant = { name: null, cuisineType: null, id: null };
    }

    $scope.tryadd = function (restaurant) {
        $http.post('/api/restaurant/',
                    { "name": restaurant.name, "cuisineType": restaurant.cuisineType },
                    { headers: { "Authorization": "Bearer " + (JSON.parse(sessionStorage.getItem('user'))).token } })
            .then(function successCallback(response) {
                toastr.success(('R has been updated').replace("R", restaurant.name), 'Restaurant added!');
                $scope.Restaurants.push(response.data);
                $scope.addOrEdit = false;
            }, function errorCallback(response) {
                if (response.status == 401) {
                    alert("Sorry, you're not authorized to do that");
                } else {
                    alert("Sorry, something went wrong");
                }
            });
    }

    $scope.edit = function (toEdit) {
        $scope.addOrEdit = true;
        $scope.addEditButtonText = "Edit Restaurant";
        $scope.restaurant = { name: toEdit.name, cuisineType: toEdit.cuisineType, id: toEdit.id };
        $scope.saved = toEdit;
    }

    $scope.tryedit = function (restaurant) {
        $http.put('/api/restaurant/',
                    { "id":restaurant.id, "name": restaurant.name, "cuisineType": restaurant.cuisineType },
                    { headers: { "Authorization": "Bearer " + (JSON.parse(sessionStorage.getItem('user'))).token } })
            .then(function successCallback(response) {
                toastr.success(('R has been updated').replace("R", restaurant.name), 'Restaurant updated!');
                $scope.saved.name = $scope.restaurant.name;
                $scope.saved.cuisineType = $scope.restaurant.cuisineType;
                $scope.addOrEdit = false;
            }, function errorCallback(response) {
                if (response.status == 401) {
                    alert("Sorry, you're not authorized to do that");
                } else {
                    alert("Sorry, something went wrong");
                }
            });
    }

    $scope.submitForm = function (restaurant) {
        if (restaurant.id) {
            $scope.tryedit(restaurant)
        } else {
            $scope.tryadd(restaurant);
        }
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

    $scope.cancel = function () {
        $scope.addOrEdit = false;
    }
}