
var unansweredApp = angular.module('serviceUnanswered', ['ngRoute']);

unansweredApp.config(function ($httpProvider) {
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
});

unansweredApp.controller('mainController', ['$scope', '$http', function ($scope, $http) {
    
    $scope.redirectLocation = "";
    $scope.isProcessing = false;
    $scope.loggingOut = "";
    $scope.loggingOutBtn = "";
    
    $http({
        url: 'services/ConfirmSubmit.asmx/GetRedirectLocation',
        method: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ resultsId: sessionResultsId }),
        dataType: 'json'
    })
    .then(function (resp) {
        var respData = resp.data;
        $scope.redirectLocation = respData.d;
        //console.log($scope.redirectLocation);
    }, function () {
        console.log('ERROR');
    });

    $scope.logout = function () {
        //ajax loader doesn't work on ie8
        var notIe8 = navigator.appVersion.indexOf("MSIE 8.0") > -1;
        if (!notIe8) {
            $scope.isProcessing = true;
            $scope.loggingOut = "disableLogoutPop";
            $scope.loggingOutBtn = "casLogoutBtnDisabled";
        }
    };
   
}]);
