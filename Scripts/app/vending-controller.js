angular.module('VendingApp', [])
    .controller('VendingCtrl', function ($scope, $http) {
        $scope.title = "loading snacks..."
        $scope.wallet = 0;
        $scope.change = 0;
        $scope.working = $('button, input').disabled;
        $scope.working = false
        $scope.selection = '';

        $scope.loadSlots = function () {
            $scope.working = true;
            $scope.title = "loading snacks...";
            $scope.slots = [];
            //URL for post??
            $http.get("/api/vending").success(function (data, status, headers, config) {
                $scope.slots = data;
                $scope.title = "Pick a snack";
                $scope.working = false;
            }).error(function (data, status, headers, config) {
                $scope.title = "Something went wrong..."
                $scope.working = false;
            });
        }

        $scope.addMoney = function (amount) {
            $scope.wallet += amount;
            //because randomly added very small decimal values
            $scope.wallet = Number($scope.wallet.toFixed(2))
        }

        $scope.keypadPress = function (inputKey) {
            $scope.selection += inputKey;
        }

        $scope.submit = function () {
            $scope.working = true;
            var slot = $scope.slots.filter(function (x) {
                return $scope.selection.substr(0, 1).toUpperCase() == x.alpha && $scope.selection.substr(1, 2) == x.numeric
            });
            if (slot == undefined) {
                var snack = slot[0].snack;
                if (slot[0].price <= $scope.wallet) {
                    $http.post('/api/vending', { 'snackId': snack.id, 'title': snack.title }).success(function (data, status, headers, config) {
                        var change = $scope.wallet - slot[0].price;
                        $scope.title = "Enjoy your " + snack.title + "! Your change is $" + change.toFixed(2);
                        $scope.wallet = 0;
                        snack = data;
                        $scope.selection = '';
                        $scope.working = false;
                    }).error(function (data, status, headers, config) {
                        $scope.title = "Something went wrong..."
                        $scope.working = false;
                    });
                }
                else {
                    $scope.title = "Insufficent funds";
                    $scope.working = false;
                }
            }
            else {
                $scope.title = "Invalid code";
                $scope.working = false;
            }
        }
    });

$(function () {
    $('body').tooltip({ selector: '[data-toggle="tooltip"]' });
});