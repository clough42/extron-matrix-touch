angular.module('vidSwitch').controller('VidSwitchCtrl', ['$scope', '$http', function ($scope, $http) {

    $scope.presets = [];
    $scope.selectedPreset = 0;
    
    function loadPresets() {
        $http.get('api/presets.json')
        .then(function (res) {
            $scope.presets = res.data.presets;
            $scope.selectedPreset = res.data.selectedPreset;
        });
    }

    $scope.submit = function () {
        //if( $scope.command ) {
        //$scope.gameOver = tooSmallEngine.execute($scope.command, $scope.messageStream);
        //updateRoomDisplay();
        //}
        //$scope.command = '';
    }

    loadPresets();

}]);


