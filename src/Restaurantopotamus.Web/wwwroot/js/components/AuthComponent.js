angular.module('app.Hippo')
    .component('auth', {
        bindings: { restaurant: '=', remove: '&' },
        controller: AuthController,
        template: '<div class="login">' +
                    '<div class="authenticated" ng-show="authenticated">Hiya {{ user.name }}! <button class ="btn btn-primary" ng-click="logout()">Sign Out</button></div>' +
                    '<div class="anonymous" ng-show="!authenticated && !showForm">' +
                        '<a href ng-click="register()">Register</a>' +
                        '<button class ="btn btn-primary" ng-click="login()">Sign In</button>' +
                    '</div>' +
                    '<div ng-show="!authenticated && showForm">' +
                        '<a href ng-click="reset()">Cancel</a>' +
                        '<form novalidate>' +
                            'Username: <input id="username" type="text" ng-model="user.name" />' +
                            'Password: <input id="password" type="password" ng-model="user.password" />' +
                            '<input class ="btn btn-primary" type="submit" ng-click="formSubmit(user)" value="{{ buttonText }}" />' +
                        '</form>' +
                    '</div>' +
                  '</div>'
    });