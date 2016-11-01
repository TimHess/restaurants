angular.module('app.Hippo')
    .component('restaurant', {
        bindings: { restaurant: '=', remove: '&', edit: '&' },
        controller: function ($rootScope, $scope) {
            $rootScope.$watch('authenticated', function () {
                $scope.authenticated = $rootScope.authenticated;
            });
        },
        template: '<div class="restaurant">' +
                    '<i class="fa fa-lg fa-trash-o" ng-click="$ctrl.remove({id:$ctrl.restaurant.id})" ng-if="authenticated"></i>' +
                    '<i class="fa fa-lg fa-pencil" ng-click="$ctrl.edit({toEdit:$ctrl.restaurant})" ng-if="authenticated"></i>' +
                    '<h3>{{ $ctrl.restaurant.name }}</h3>' +
                    '<h4>Featured Cuisine: {{ $ctrl.restaurant.cuisineType }}</h4>' +
                    '<rating average="{{ $ctrl.restaurant.averageRating }}" total="{{ $ctrl.restaurant.totalRatings }}" restaurant-id="{{ $ctrl.restaurant.id }}"></rating>' +
                   '</div>'
    });
