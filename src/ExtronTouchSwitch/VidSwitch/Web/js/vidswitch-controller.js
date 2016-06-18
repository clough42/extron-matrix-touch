angular.module('vidSwitch').controller('VidSwitchCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.presets = [];
    $scope.selectedPreset = 0;
    
    function loadPresets() {
        $http.get('api/presets')
        .then(function (res) {
            $scope.presets = res.data.presets;
            $scope.selectedPreset = res.data.selectedPreset;
        });
    };

    $scope.userChangedPreset = function () {
        var uri = '/api/presets/select/' + $scope.selectedPreset;
        $http.get(uri)
        .then(function (res) {
            $scope.presets = res.data.presets;
            $scope.selectedPreset = res.data.selectedPreset;
        });
    };

    loadPresets();

}]);


