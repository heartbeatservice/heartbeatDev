HeartbeatApp.controller("SchedulingController", function AppController($scope, $location, HeartbeatService) {
    $scope.page = "Heartbeat Service Portal";
    $scope.ProviderDashboard = [{ doctor: 'Farzana Aziz', patients: 23 }, { doctor: 'Abbas Rizvi', patients: 12 }];
    $scope.ReminderDashboard = ['23 Patients have to be reminded of an upcoming appointment', "Dr. Farzana's Schedule has to be updated"]
});
   