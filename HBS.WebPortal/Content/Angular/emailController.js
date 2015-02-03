HeartbeatApp.controller("emailController", function AppController($scope, $location, HeartbeatService) {
    $scope.resource = "Email";
    $scope.Customer = { FullName: "", Question: "" };
    $scope.SendMessage = function () {
        //$scope.resource += $scope.Customer.FullName + "&Question=" + $scope.Customer.Question;
        HeartbeatService.PostData($scope.success, $scope.error, $scope.resource,$scope.Customer);
       
    };

    $scope.success = function (response) {

        if (response) {

            alert("Message Sent Successfully");

        }
        else {
            alert("Message Not Sent");
        }
    };

    $scope.error = function (err) {

        alert("err");
    };
});