HeartbeatApp.factory('HeartbeatService', function () {

    return {

        serviceAuthor: { 'Name': 'Umais Siddiqui' },

        GetData: function (callSuccess, wrong, resource) {
            request = $.ajax({
                beforeSend: function (xhrObj) {
                    xhrObj.setRequestHeader("Content-Type", "application/json");
                    xhrObj.setRequestHeader("Accept", "application/json");
                },

               
               //url: "http://localhost:3687/api/" + resource,
                url: "http://services.heartbeat-biz.com/api/"+ resource,

                type: "get",

                success: function (response) { callSuccess(response); },
                error: function (result) { wrong(result); }
            });
        },

        PostData: function (callSuccess, wrong, resource, params) {
            request = $.ajax({
                beforeSend: function (xhrObj) {
                    xhrObj.setRequestHeader("Content-Type", "application/json");
                    xhrObj.setRequestHeader("Accept", "application/json");
                },
                url: "/api/" + resource,
                type: "post",
                data: JSON.stringify(params),
                success: function (response) { callSuccess(response); },
                error: function (result) { wrong(result); }
            });
        },
        PostDataToApi: function (callSuccess, wrong, resource, params) {
            request = $.ajax({
                beforeSend: function (xhrObj) {
                    xhrObj.setRequestHeader("Content-Type", "application/json");
                    xhrObj.setRequestHeader("Accept", "application/json");
                },
               // url: "http://localhost:3687/api/" + resource,
                url: "http://services.heartbeat-biz.com/api/"+ resource,
                type: "post",
                data: JSON.stringify(params),
                success: function (response) { callSuccess(response); },
                error: function (result) { wrong(result); }
            });
        },
        

            PutData: function (callSuccess, wrong, resource, params) {
                request = $.ajax({
                    beforeSend: function (xhrObj) {
                        xhrObj.setRequestHeader("Content-Type", "application/json");  
                    },
                  //  url: "http://localhost:3687/api/" + resource,
                   url: "http://services.heartbeat-biz.com/api/"+ resource,
                    type: "PUT",
                    data: JSON.stringify(params),
                    success: function (response) { callSuccess(response); },
                    error: function (result) { wrong(result); }
                });
            },
        
            CustomGetData: function (callSuccess, wrong, resource) {
                request = $.ajax({
                    beforeSend: function (xhrObj) {
                        xhrObj.setRequestHeader("Content-Type", "application/json");
                        xhrObj.setRequestHeader("Accept", "application/json");
                    },


                   // url: "http://localhost:3687/CustomApi/" + resource,
                    url: "http://services.heartbeat-biz.com/CustomApi/" + resource,

                    type: "get",

                    success: function (response) { callSuccess(response); },
                    error: function (result) { wrong(result); }
                });
            },
        IsDate: function (dateStr) {

            var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
            var matchArray = dateStr.match(datePat); // is the format ok?

            if (matchArray == null) {
                
                return false;
            }

            month = matchArray[1]; // p@rse date into variables
            day = matchArray[3];
            year = matchArray[5];

            if (month < 1 || month > 12) { // check month range
                alert("Month must be between 1 and 12.");
                return false;
            }

            if (day < 1 || day > 31) {
                alert("Day must be between 1 and 31.");
                return false;
            }

            if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
                alert("Month " + month + " doesn`t have 31 days!")
                return false;
            }

            if (month == 2) { // check for february 29th
                var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
                if (day > 29 || (day == 29 && !isleap)) {
                    alert("February " + year + " doesn`t have " + day + " days!");
                    return false;
                }
            }
            return true; // date is valid
        }



    }

});