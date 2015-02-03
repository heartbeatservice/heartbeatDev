HeartbeatApp.controller("CalendarController", function AppController($scope, $location, HeartbeatService) {
   
    $scope.currentDate = "2014/11/20";
    $scope.CompanyId = 0;
    $scope.professionalId = 0;
    $scope.ProfessionalInfo = {ProfessionalId: 0, FirstName: '', LastName: '' };
    $scope.CustomerList = [];
    $scope.ProfessionalList = [];
    $scope.ProfessionalSchedule = {};
    $scope.CustomerId = 0;
    $scope.CustomerDropDown = [];
    $scope.year=2014;
    $scope.month = 11;
    $scope.itemCreated = false;
    $scope.openform = true;
   // $scope.ServiceURL = "http://localhost:3687/api/";
    $scope.ServiceURL="http://services.heartbeat-biz.com/api/";
    $scope.init = function () {
        //hide everything
        $("#scheduler").ajaxComplete(function () {
            if ($scope.itemCreated)
                $scope.RefreshPage();
            else
                console.log('Logging Now');
        });
        $scope.currentDate = $('#CurrentDate').val();
        $scope.professionalId = $('#DoctorId').val();
        $scope.CompanyId = $('#CompanyId').val();
        $scope.CustomerId = $('#CustomerId').val();
        $scope.setMonthYear();
        $scope.GetProfessionals();
        $scope.GetCustomers();
        
        
       
        //http://localhost:3687/api/Customer?companyId=1
        //http://localhost:3687/api/Professional?CompanyId=1
    };
    //Refreshing The Page
    $scope.setMonthYear = function () {
        var date=new Date($scope.currentDate);
        $scope.month = date.getMonth() + 1;
        $scope.year = date.getFullYear();
       

    };
    $scope.RefreshPage = function () {
        $scope.itemCreated = false;
        $('#scheduler').html('');
        $scope.findCurrentProvider();
        $scope.loadDailyCalendar();
    };

    $scope.checkReloadLogic = function () {
        var dateText = $('span[data-bind = "text: formattedDate"]').html();
        var arr = dateText.split(",");
    
        if (arr.length === 2) {
            dateText = arr[0] + " 01," + arr[1];
        }
        if (arr.length === 3) {
            dateText = arr[1] + arr[2];
        }
        else if (arr.length === 5) {
            dateText = arr[3] + arr[4];
        }
        var date = new Date(dateText);
        var currentmonth = new Date($scope.currentDate).getMonth() + 1;
        var year = date.getFullYear(), month = (date.getMonth() + 1), day = date.getDate();
        if (month < 10) month = "0" + month;
        if (day < 10) day = "0" + day;
        var properlyFormatted = "" + month + '/' + day + '/' + year;
        $scope.currentDate = properlyFormatted;
        console.log($scope.currentDate);
        if (month !== currentmonth) {
            $scope.setMonthYear();
            $scope.$apply();
            $('#scheduler').html('');
            $scope.loadDailyCalendar();
        }
    };

    $scope.GetProfessionals = function () {

        //Get the Provider Info And Update the Label for that Provider on Calendar Page
        var resource = 'Professional?CompanyId=' + $scope.CompanyId;
        HeartbeatService.GetData($scope.PopulateProviders, $scope.Error, resource);
        //Next Step in this process will be to load the drop down with Users and Select the Correct one. Thinking of using Ng-Model
    };
    $scope.PopulateProviders = function (response) {
        $scope.ProfessionalList = response;
        $scope.ProfessionalList.push({ ProfessionalId: 0, FirstName: 'Select Professional', LastName: 'to view schedule' });
        $scope.ProfessionalList = $scope.ProfessionalList.reverse();
        $scope.findCurrentProvider();
        $scope.$apply();
    };

    $scope.findCurrentProvider=function(){

        for(var i=0;i<$scope.ProfessionalList.length;i++)
        {
            if($scope.professionalId==$scope.ProfessionalList[i].ProfessionalId)
            {
                $scope.professionalId = $scope.ProfessionalList[i].ProfessionalId;
                $scope.ProfessionalInfo = $scope.ProfessionalList[i];

                break;
            }
        }
    };
    $scope.GetCustomers = function () {

        //Get the Provider Info And Update the Label for that Provider on Calendar Page
        var resource = 'Customer?companyId=' + $scope.CompanyId;
        HeartbeatService.GetData($scope.PopulateCustomers, $scope.Error, resource);
        //Next Step in this process will be to load the drop down with Users and Select the Correct one. Thinking of using Ng-Model
    };
    $scope.PopulateCustomers = function (response) {
        $scope.CustomerList = response;
        //Need to assign this to the Drop Down Object for Kendo.....
        $scope.assignCustomers($scope.CustomerId);
        $scope.$apply();
    };

    $scope.assignCustomers = function (customerId) {
         function customer() {

            this.text = "";
            this.value = "";
            this.color = "#f8a398";
        }
         for (var i = 0; i < $scope.CustomerList.length; i++) {
             cst = new customer();
            cst.text = $scope.CustomerList[i].FirstName + ' ' + $scope.CustomerList[i].LastName;
            cst.value = $scope.CustomerList[i].CustomerId;
            $scope.CustomerDropDown.push(cst);
           
        }
      //  console.log(JSON.stringify($scope.CustomerDropDown));
        $scope.$apply();
        $scope.loadDailyCalendar();
    };
    $scope.Error = function (result) {

        alert("FAILED : " + result.status + ' ' + result.statusText);
    };
    
    $scope.loadDailyCalendar = function () {

        $(function () {
            $("#scheduler").kendoScheduler({
                date: new Date($scope.currentDate),
                startTime: new Date($scope.currentDate+" 7:00 AM"),
                height:600,
                views: [
                   { type:"day",
                       group: {
                           orientation: "vertical"
                       }
                   },
                     //,
                    //{ type: "workWeek", selected: true },
                    "week",
                    { type: "month", selected: true },
                     "agenda"
                ],
                edit: function (e) {
                    if (e.container.find("[name=isAllDay]").attr("checked")) {
                        e.container.find("[name=isAllDay]").click();

                        e.container.find("[name=isAllDay]").attr("checked", false);
                    }
                        e.container.find("[name=isAllDay]").parent().prev().remove().end().remove();
                        e.container.find("[data-container-for=recurrenceRule]").prev().remove().end().remove();
                        e.container.find("[for=ownerId]").html("Customer");
                        $(".k-window-title").html('Schedule Appointment with Dr. ' + $scope.ProfessionalInfo.FirstName);
                    
                },
                change: function (e) {
        var start = e.start; //selection start date
            var end = e.end; //selection end date
            var slots = e.slots; //list of selected slots
            var events = e.events; //list of selected Scheduler events

            var message = "change:: selection from {0:g} till {1:g}";

            if (events.length) {
                message += ". The selected event is '" + events[events.length - 1].title + "'";
            }

           // console.log(message);
        },
                // timezone: "Etc/UTC",
                dataSource: {
                    batch: true,
                    transport: {
                        read: {

                            beforeSend: function (xhrObj) {
                                xhrObj.setRequestHeader("Content-Type", "application/json");
                                xhrObj.setRequestHeader("Accept", "application/json");

                            },
                            success:function(data){alert("Got It");},
                            url: $scope.ServiceURL + 'ProfessionalSchedule//'+ $scope.professionalId+"?month="+$scope.month+"&year="+$scope.year,
                            dataType: "json"
                        },
                        update: {
                            beforeSend: function (xhrObj, s) {
                                xhrObj.setRequestHeader("Content-Type", "application/json");
                                xhrObj.setRequestHeader("Accept", "application/json");
                                s.data = JSON.stringify($scope.ProfessionalSchedule);
                            },
                           
                            url: $scope.ServiceURL + 'ProfessionalSchedule/',
                            type: "put"

                        },
                        create: {
                            beforeSend: function (xhrObj, s) {
                                xhrObj.setRequestHeader("Content-Type", "application/json");
                                xhrObj.setRequestHeader("Accept", "application/json");
                                s.data = JSON.stringify($scope.ProfessionalSchedule);
                              
                                
                            },
                            
                            url: $scope.ServiceURL + 'ProfessionalSchedule/',
                            type: "post"
                        },
                        destroy: {
                          
                            url: $scope.ServiceURL,
                            dataType: "jsonp"
                        },
                        parameterMap: function (options, operation) {
                            var attribString = "";
                            if (operation !== "read" && options.models) {
                                var Items = $('.k-edit-form-container > .k-edit-field');
                                for (var i = 0; i < Items.length; i++)
                                {
                                    inputVal = Items[i];

                                   attribString+= inputVal.getAttribute("data-container-for");
                                }
                               
                                switch(operation)
                                {
                                    case "create":
                                        $scope.ProfessionalSchedule = options.models[options.models.length-1];
                                        $scope.ProfessionalSchedule.ProfessionalId = $scope.professionalId;
                                        $scope.ProfessionalSchedule.UserId = 1;
                                        console.log(JSON.stringify(options.models[0]));
                                        $scope.itemCreated = true;
                                        break;
                                    case "update":
                                        $scope.ProfessionalSchedule = options.models[0];
                                        //console.log(JSON.stringify(options.models[0]));
                                        break;
                                    case "destroy":
                                        alert("Destroyed");
                                        break;
                                    default:
                                        alert("No action Performed");
                                }
                            }
                        }
                    },
                    schema: {
                        model: {
                            id: "taskId",
                            fields: {
                                taskId: { from: "TaskID", type: "number" },
                                title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                                start: { type: "date", from: "Start" },
                                end: { type: "date", from: "End" },
                                startTimezone: { from: "StartTimezone" },
                                endTimezone: { from: "EndTimezone" },
                                description: { from: "Description" },
                                recurrenceId: { from: "RecurrenceID" },
                                recurrenceRule: { from: "RecurrenceRule" },
                                recurrenceException: { from: "RecurrenceException" },
                                ownerId: { from: "OwnerID", defaultValue: 1 },
                                isAllDay: { type: "boolean", from: "IsAllDay" },
                                ProfessionalId: { type: "number", from: "ProfessionalId" },
                                UserId: { type: "number", from: "UserId" }
                            }
                        }
                    },
                    //filter: {
                    //    logic: "or",
                    //    filters: [
                    //        { field: "ownerId", operator: "eq", value: 1 },
                    //        { field: "ownerId", operator: "eq", value: 2 },
                    //        { field: "ownerId", operator: "eq", value: 3}
                    //    ]
                    //}
                },
                resources: [
                    {
                        field: "ownerId",
                        title: "Owner",
                        dataSource: $scope.CustomerDropDown

                            /*[
                            { text: "Alex", value: 1, color: "#f8a398" },
                            { text: "Bob", value: 2, color: "#51a0ed" },
                            { text: "Charlie", value: 3 }
                           // , color: "#56ca85"
                           
                        ] **/
                        //Have to Assign Data Source To This.
                    }
                ]
            });

            //$("#people :checkbox").change(function (e) {
            //    var checked = $.map($("#people :checked"), function (checkbox) {
            //        return parseInt($(checkbox).val());
            //    });

                var scheduler = $("#scheduler").data("kendoScheduler");

                //scheduler.dataSource.filter({
                //    operator: function (task) {
                //        return true;
                //    }
                //});
            //});
        });


   
        //Show Everything Now
        $(".k-nav-next").mouseup(function () {
           
            setTimeout(function () {
                var ua = window.navigator.userAgent;
                var msie = ua.indexOf("MSIE ");

                //if (msie > 0)      // If Internet Explorer, return version number
             
                $scope.checkReloadLogic();

            }, 1000);
            

      
        });

        $(".k-scheduler-monthview").dblclick(function () {

            $scope.openform = false;


        });

        $(".k-nav-prev").mouseup(function () {

            setTimeout(function () {
                var ua = window.navigator.userAgent;
                var msie = ua.indexOf("MSIE ");

                //if (msie > 0)      // If Internet Explorer, return version number
              
                    $scope.checkReloadLogic();
            }, 1000);



        });


        $(".k-nav-current").click(function () {
          
            setTimeout(function () {
                
                $(".k-scheduler-calendar").mouseup(function () {
                    setTimeout(function () {
                        var ua = window.navigator.userAgent;
                        var msie = ua.indexOf("MSIE ");

                        //if (msie > 0)      // If Internet Explorer, return version number
                        //    alert(new Date($scope.currentDate).getMonth() + 1);
                        //else
                        $scope.checkReloadLogic();
                    }, 1000);

                });
            }, 1000);



        });
    }
});