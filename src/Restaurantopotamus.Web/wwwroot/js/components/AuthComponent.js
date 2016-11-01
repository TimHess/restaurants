angular.module('app.Hippo')
    .component('auth', {
        bindings: { restaurant: '=', remove: '&' },
        controller: AuthController,
        template: '<div class="login">' +
                    '<div class="authenticated" ng-show="authenticated">Hi {{ $ctrl.username }} <button class="btn" ng-click="logout()">Sign Out</button></div>' +
                    '<div class="anonymous" ng-show="!authenticated">' +
                        '<button class="btn" ng-click="login()">Sign In</button>' +
                    '</div>' +
                  '</div>'
    });