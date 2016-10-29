angular.module('app.Hippo')
    .component('restaurant', {
        bindings: { restaurant: '=', remove: '&' },
        controller: function () {
        },
        template: '<div class="restaurant">' +
                    '<i class="fa fa-lg fa-trash-o" ng-click="$ctrl.remove({id:$ctrl.restaurant.id})"></i>' +
                    '<i class="fa fa-lg fa-pencil" ng-click="$ctrl.edit({id:$ctrl.restaurant.id})"></i>' +
                    '<h3>{{ $ctrl.restaurant.name }}</h3>' +
                    '<h4>Featured Cuisine: {{ $ctrl.restaurant.cuisineType }}</h4>' +
                    '<rating average="{{ $ctrl.restaurant.averageRating }}" total="{{ $ctrl.restaurant.totalRatings }}" restaurant-id="{{ $ctrl.restaurant.id }}"></rating>' +
                   '</div>'
    });
