HeartbeatApp.controller("CustomerController", function AppController($scope, $location, HeartbeatService) {
    $scope.Insurance = [{ InsuranceId: '1', InsuranceName: 'ABC Testing' }];
    $scope.CompanyId = $('#company').val();
    $scope.Customers = [];
    $scope.SearchParam = '';
    $scope.InsuranceEntry = {};
    $scope.myProviders = { ProfessionalId: 0, FirstName: '', LastName: '' };
    $scope.AllProviders = [{ ProfessionalId: 1, FirstName: 'Farzana', LastName: 'Aziz' }, { ProfessionalId: 2, FirstName: 'Abbas', LastName: 'Rizwi' }, { ProfessionalId: 3, FirstName: 'Fawzia', LastName: 'Kazmi' }];
    $scope.clearCustomer = function () {

        $scope.Customer = {};

    };

    $scope.init = function () {
        $scope.GetProviders();
    };

    
    $scope.GetProviders = function () {
        var resource = 'Professional/' + $scope.CompanyId;
        HeartbeatService.CustomGetData($scope.LoadProviders, $scope.Error, resource);
    };

    $scope.LoadProviders = function (response) {

        $scope.AllProviders = response;
    };
    $scope.GridOptions = {
        data: 'Customers',
        enableCellSelection: false,
        enableRowSelection: false,
        enableCellEdit: false,
        enableColumnResize: true,
        enableColumnReordering: true,
        columnDefs: [
                     { field: 'FirstName', displayName: 'First Name', enableCellEdit: true, width: 100 },
                     { field: 'LastName', displayName: 'Last Name', enableCellEdit: true, width: 100 },
                     { field: 'DateOfBirth', displayName: 'DOB', enableCellEdit: true, width: 100 },
               { field: 'HomePhone', displayName: 'Home Phone', enableCellEdit: true, width: 100 },

               { field: 'Address1', displayName: 'Address', enableCellEdit: true, width: 250 },
               { field: 'City', displayName: 'City', enableCellEdit: true, width: 125 },
        { field: 'CustomerId', displayName: 'Insurance', enableCellEdit: true, width: 80, cellTemplate: "<button style='margin-left:20px;'class='btn-small btn-warning' ng-click='OpenInsurance(row.entity[col.field]);' ><span  class='glyphicon glyphicon-folder-open'></span></button>" },
         { field: 'CustomerId', displayName: 'View/Edit', enableCellEdit: true, width: 100, cellTemplate: "<button style='margin-left:20px;' class='btn-small btn-danger' ng-click='EditCustomer(row.entity[col.field]);' ><span class='glyphicon glyphicon-pencil'></span></button>" },
          { field: 'CustomerId', displayName: 'Appointment', enableCellEdit: true, width: 120, cellTemplate: "<button style='margin-left:20px;' class='btn-small btn-primary' ng-click='AppointmentForm(row.entity[col.field]);' > <span class='glyphicon glyphicon-th-list'></span></button>" }
        ]



    };
    $scope.AddCustomer = function () {
        $scope.Customer.CompanyId = $('#company').val();
        $scope.Customer.CreatedBy = $('#user').val();
        $scope.Customer.Active = true;
        var resource = 'Customer';
        HeartbeatService.PostDataToApi($scope.AddSuccess, $scope.Error, resource, $scope.Customer);

    };

    $scope.AddSuccess = function (response) {
        $scope.Customer.CustomerId = response;
        $scope.Customers.push($scope.Customer);
        $scope.$apply();
        $('#dismiss').click();
        $scope.ShowInsuranceForm();
    };

    $scope.Error = function (result) {

        alert("FAILED : " + result.status + ' ' + result.statusText);
    };

    $scope.CustomerSearch = function () {

        var myParams = $scope.SearchParam.split(",");
        var dob = '';
        var name = '';
        if (myParams.length > 0) {
            for (i = 0; i < myParams.length; i++) {
                if (HeartbeatService.IsDate(myParams[i].trim())) {
                    dob = myParams.replace('/', '-').trim();
                    name = '-1';

                }
                else {
                    dob = '1-1-1900';
                    name = myParams[i].trim();
                }

            }
        }
        else {
            dob = '1-1-1900';
            name = '-1';
        }

        var resource = 'Customer?companyId=' + $scope.CompanyId + '&customerName=' + name + '&dob=' + dob;
        HeartbeatService.GetData($scope.SearchSuccess, $scope.Error, resource);

    };

    $scope.SearchSuccess = function (data) {
        $scope.Customers = data;
        $scope.$apply();
    };



    $scope.EditCustomer = function (CustomerId) {
        var resource = 'Customer?customerId=' + CustomerId;
        HeartbeatService.GetData($scope.GetSuccess, $scope.Error, resource);
    };
    $scope.GetSuccess = function (response) {
        $scope.applyCustomerToModel(response);
        $('#editbtn').click();
    };

    $scope.GetAppointmentSuccess = function (response) {
        $scope.applyCustomerToModel(response);
        $('#AddAppointmentbtn').click();
    };
    $scope.UpdateCustomer = function () {
        var resource = 'Customer/' + $scope.Customer.CustomerId;
        HeartbeatService.PutData($scope.EditSuccess, $scope.Error, resource, $scope.Customer);
    };
    $scope.EditSuccess = function (response) {
        alert("Updated successfully");
    };


    $scope.OpenInsurance = function (customerId) {

        
        var resource = "CustomerInsurance?customerInsuranceId=" + customerId;
        $scope.CustomerId = customerId;

        HeartbeatService.GetData($scope.InsuranceSuccess, $scope.Error, resource);



    }

    $scope.InsuranceSuccess = function (data) {

        $scope.Insurance = data;

        var resource = 'Customer?customerId=' + $scope.CustomerId;
        $('#Insurancebtn').click();
        HeartbeatService.GetData($scope.applyCustomerToModel, $scope.Error, resource);

    }

    $scope.ShowInsuranceForm = function () {
        var companyId = $('#companyInsurance').val();
        var resource = "Insurance?companyId=" + companyId + '&InsuranceName=-1';
        HeartbeatService.GetData($scope.InsuranceLookupSuccess, $scope.Error, resource);
    }

    $scope.InsuranceLookupSuccess = function (data) {
        $scope.AllInsurances = data;
        $scope.$apply();

        $('#insuranceClose').click();
        $('#AddInsurancebtn').click();
    }

    $scope.applyCustomerToModel = function (data) {
        $scope.Customer = data;
        $scope.$apply();
    }

    $scope.AddCustomerInsurance = function () {
        $scope.InsuranceEntry.InsuranceId = $('#InsuranceId').val();
        $scope.InsuranceEntry.CustomerId = $('#CustomerIdForInsurance').val();
        var resource = 'CustomerInsurance';
        HeartbeatService.PostDataToApi($scope.AddCustomerInsuranceSuccess, $scope.Error, resource, $scope.InsuranceEntry);
    }

    $scope.AddCustomerInsuranceSuccess = function (response) {
        $('#dismissAddInsurance').click();

        $scope.OpenInsurance($scope.InsuranceEntry.CustomerId)
    }

    $scope.AppointmentForm = function (CustomerId) {
        var resource = 'Customer?customerId=' + CustomerId;
        HeartbeatService.GetData($scope.GetAppointmentSuccess, $scope.Error, resource);
        $('#AddAppointmentbtn').click();
        //var companyId = $('#companyInsurance').val();
        //var resource = "Insurance?companyId=" + companyId + '&InsuranceName=-1';
        //HeartbeatService.GetData($scope.InsuranceLookupSuccess, $scope.Error, resource);
    }


    $scope.ProfessionalSearch = function () {

        var myParams = $scope.SearchParam.split(",");
        var idNumber = '';
        var name = '';
        if (myParams.length > 0) {
            for (i = 0; i < myParams.length; i++) {
               
                   
                    name = myParams[i].trim();
                }

            }
        
        else {
           
            name = '-1';
        }

        var resource = 'Customer?companyId=' + $scope.CompanyId + '&customerName=' + name + '&dob=' + dob;
        HeartbeatService.GetData($scope.SearchSuccess, $scope.Error, resource);

    };

    $scope.AddAppointment = function () {

      

    };

    $scope.ViewSchedule = function () {

        if($scope.myProviders.ProfessionalId===0)
        {
            $('#aptmsg').html("Please Select Provider First");
        }
        else {

            $('#frmschedule').submit();
        }

    };
});