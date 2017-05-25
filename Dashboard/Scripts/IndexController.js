var app = angular.module("MyApp");

app.controller("IndexController", ["$scope", "appservice",
    function ($scope, appservice) {
        console.log("Connecting to the server....");
        var signarRHub = appservice('echo');
        console.log("Connected to the server");
        $scope.currentHour = 00;
        $scope.currentMinute = 00;
        $scope.currentSecond = 00;
        $scope.currentMilisecond = 00;
        
        signarRHub.on("AddMessage", function (data) {
            for (i = 0; i < data.length; i++) {
                switch (data[i].categoryName) {
                    case 'Hour':
                        $scope.currentHour = data[i].value;
                        break;
                    case 'Minute':
                        $scope.currentMinute = data[i].value;
                        break;
                    case 'Second':
                        $scope.currentSecond = data[i].value;
                        break;
                    case 'Milisecond':
                        $scope.currentMilisecond = data[i].value;
                        break;
                    default:
                        break;
                }
            }
        });
    }]);