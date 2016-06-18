angular.module('vidSwitch').controller('VidSwitchCtrl', ['$scope', '$http', '$location', '$mdDialog', function ($scope, $http, $location, $mdDialog) {

    $scope.accessCode = $location.path().substring(1);
    $scope.presets = [];
    $scope.selectedPreset = 0;

    function loadPresets() {
        $http.get('api/presets')
        .then(function (res) {
            $scope.presets = res.data.presets;
            $scope.selectedPreset = res.data.selectedPreset;
        });
    };

    $scope.userChangedPreset = function (ev) {
        var uri = '/api/presets/select/' + $scope.selectedPreset;
        var accessTimestamp = Math.floor(Date.now() / 1000);
        var hashString = uri + ' ' + accessTimestamp + ' ' + $scope.accessCode;
        var accessHash = sha256_digest(hashString);


        var req = {
            method: 'GET',
            url: uri,
            params: {
                timestamp: accessTimestamp,
                hash: accessHash
            }
        };
        $http(req)
        .then(
            function successCallback(res) {
                $scope.presets = res.data.presets;
                $scope.selectedPreset = res.data.selectedPreset;
            },
            function errorCallback(res) {
                $mdDialog.show(
                  $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title('Authorization Error')
                    .textContent('You are not authorized to perform this action.  The authorization code may have changed. See the Remote Access help on the switch to refresh your bookmark.')
                    .ariaLabel('Error')
                    .ok('Got it!')
                    .targetEvent(ev)
                );
            });
    };

    function generateAccessHeaders(uri) {
        var accessCode = "XYZ";
        var accessTimestamp = Math.floor(Date.now() / 1000);

        var hashString = uri + ' ' + accessTimestamp + ' ' + accessCode;
        var accessHash = sha256_digest(hashString);

        return {
            XAccessHash: accessHash,
            XAccessTimestamp: accessTimestamp
        };
    }

    loadPresets();



}]);


