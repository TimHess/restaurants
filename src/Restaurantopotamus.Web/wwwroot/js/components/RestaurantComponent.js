angular.module('app.Hippo')
    .component('restaurant', {
        bindings: { restaurant: '=', remove: '&' },
        controller: function () {
        },
        template: '<div class="restaurant">' +
                    '<h3>{{ $ctrl.restaurant.name }}</h3>' +
                    '<h4>Featured Cuisine: {{ $ctrl.restaurant.cuisineType }}</h4>' +
                    '<rating average="{{ $ctrl.restaurant.averageRating }}" total="{{ $ctrl.restaurant.totalRatings }}"></rating>' +
                    '<button ng-click="$ctrl.remove({id:$ctrl.restaurant.id})">remove me</button>' +
                   '</div>'
    });
