//The service which communicates with the SignalR server.
var app = angular.module("MyApp");

app.factory("appservice", ["$rootScope", "signalrserverurl",
    function ($rootScope, signalrserverurl) {
        function backendserver(hubName) {
            //connection with the SignalR server. The URL is from the service registred to the app
            var connection = $.hubConnection(signalrserverurl);
            //creating an instance of the hub
            var proxy = connection.createHubProxy(hubName);

            //connecting to the hub. As soon as the connection is successfully established the method invoke (signalR in-built method)
            //calls the 'SendPerformance' method in the hub. This is to start the communication line.
            connection.start().done(function () {
                proxy.invoke("SendPerformance")
                    .done(function () {
                        console.log("Connected to the server");                        
                    })
                    .fail(function (error) {
                        console.log('Invocation of method failed. Error: ' + error);
                    });
            }).fail(function (a) {
                console.log('not connected' + a);
            });

            //The method which the hub will call from the server side. The IndexController will invoke this method by passing the 'AddMessage' function name and the callback function.
            //This method will then call the hub to get the data. This method is the one that keeps getting data from the hub, since the AddMessage is registered via the controller. 
            //Since this method is invoked from the IndexController, the callback will get executed and $rootScope will apply the chnges happeing from the callback to the root of the application.
            return {
                on: function (eventName, callBack) {
                    proxy.on(eventName, function (result) {
                        $rootScope.$apply(function () {
                            if (callBack) {
                                callBack(result);
                            }
                        });
                    });
                }                
            };
        }
        return backendserver;
    }]);