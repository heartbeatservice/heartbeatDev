$(document).ready(function () {
    var crudServiceBaseUrl = "http://localhost:3687/api";
    var users = getUsers(crudServiceBaseUrl + "/User");
    var status = getStatus(crudServiceBaseUrl + "/WorkflowStatus");
    
    var element = $("#grid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    url: crudServiceBaseUrl + "/WorkflowCategory",
                    contentType: "application/json",
                    dataType: "json",
                    type: "GET"
                }
            },
            parameterMap: function (data, operation) {
                //Page methods always need values for their parameters
                data = $.extend({ sort: null, filter: null }, data);
                return JSON.stringify(data);
            },
            schema: {
                data: "Data",
                total: "Total",
                model: {
                    id: "WorkflowCategoryID",
                    fields: {
                        CategoryName: { type: "string" }
                    }
                }
            },
            pageSize: 20,
            serverPaging: true,
            serverSorting: true
        },
        height: 600,
        sortable: true,
        pageable: true,
        columnMenu: false,
        detailInit: detailInit,
        dataBound: function() {
            //this.expandRow(this.tbody.find("tr.k-master-row").first());
        },
        columns: [
            {
                field: "CategoryName",
                title: "Workflow Queue",
                width: "800px"
            }
        ]
    });

    function detailInit(e) {
        $("<div/>").appendTo(e.detailCell).kendoGrid({
            dataSource: {
                autoSync: false,
                transport: {
                    read: {
                        url: crudServiceBaseUrl + "/Workflow",
                        contentType: "application/json",
                        dataType: "json"
                    },
                    update: {
                        url: crudServiceBaseUrl + "/Workflow",
                        contentType: "application/json",
                        dataType: "json",
                        type: "PUT"
                    },
                    destroy: {
                        url: crudServiceBaseUrl + "/Workflow",
                        contentType: "application/json",
                        dataType: "json",
                        type: "DELETE"
                    },
                    create: {
                        url: crudServiceBaseUrl + "/Workflow",
                        contentType: "application/json",
                        dataType: "json",
                        type: "POST"
                    },
                    parameterMap: function (data, operation) {
                        if (operation !== "read" && data.models) {
                            data.models[0].CategoryID = e.data.id;
                            return JSON.stringify(data.models[0]);
                        }
                        return JSON.stringify(data);
                    }
                },
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                pageSize: 10,
                filter: { field: "CategoryID", operator: "eq", value: e.data.id },
                batch: true,
                pageSize: 20,
                schema: {
                    data: "Data",
                    total: "Total",
                    model: {
                        id: "WorkflowID",
                        fields: {
                            WorkflowID: { editable: false, nullable: true },
                            CategoryID: { field: "CategoryID", type: "number", editable: false },
                            OwnerID: { field: "OwnerID", type: "number", editable: true },
                            WorkerID: { field: "WorkerID", type: "number", editable: true },
                            WorkflowTitle: { type: "string", editable: true },
                            WorkflowNote: { type: "string", editable: true },
                            DueDate: { type: "date", editable: true },
                            StatusID: { field: "StatusID", type: "number", editable: true },
                            DateCreated: { type: "date", editable: false },
                            DateUpdated: { type: "date", editable: false }
                        }
                    }
                }
            },
            filterable: {
                extra: false,
                operators: {
                    string: {
                        startswith: "Starts with",
                        contains: "Contains",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            pageable: true,
            height: 250,
            toolbar: ["create"],
            columns: [
                { field: "WorkflowTitle", title: "Workflow Title" },
                { field: "OwnerID", values: users, title: "Owner", width: "100px" },
                { field: "WorkerID", values: users, title: "Worker", width: "100px" },
                { field: "WorkflowNote", title: "Note" },
                { field: "DueDate", title: "Due Date", format: "{0:MM/dd/yyyy}", width: "120px" },
                { field: "StatusID", values: status, title: "Status", width: "140px" },
                { command: ["edit", "destroy"], title: "&nbsp;", width: "200px" }],
            scrollable: false,
            editable: "popup",
            sortable: true
        });
    }

    // check for change event
    /*
    $('#grid').data().kendoGrid.dataSource.bind('change', function (e) {
        console.log(e.action);

        var workflowHistory = {};
        workflowHistory.WorkflowHistoryID = 1;
        workflowHistory.WorkflowID = 1;
        workflowHistory.Payload = "{'OwnerID':1}";
        workflowHistory.DateCreated = new Date().toString("yyyy-MM-dd");

        //var test = logWorkflowHistory(crudServiceBaseUrl + "/WorkflowHistory", workflowHistory);
    })
    */

    /**************************************************************************
    * Workflow Admin Section
    ***************************************************************************/
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: crudServiceBaseUrl + "/WorkflowCategory",
                contentType: "application/json",
                dataType: "json"
            },
            update: {
                url: crudServiceBaseUrl + "/WorkflowCategory",
                contentType: "application/json",
                dataType: "json",
                type: "PUT"
            },
            destroy: {
                url: crudServiceBaseUrl + "/WorkflowCategory",
                contentType: "application/json",
                dataType: "json",
                type: "DELETE"
            },
            create: {
                url: crudServiceBaseUrl + "/WorkflowCategory",
                contentType: "application/json",
                dataType: "json",
                type: "POST"
            },
            parameterMap: function (options, operation) {
                if (operation !== "read" && options.models) {
                    return JSON.stringify(options.models[0]);
                }
                return JSON.stringify(options);
            }
        },
        pageSize: 20,
        batch: true,
        schema: {
            data: "Data",
            total: "Total",
            model: {
                id: "WorkflowCategoryID",
                fields: {
                    WorkflowCategoryID: { editable: false, nullable: true },
                    CategoryName: { field: "CategoryName", type: "string", editable: true }
                }
            }
        }
    });

    $("#workflowAdminGrid").kendoGrid({
        dataSource: dataSource,
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    contains: "Contains",
                    eq: "Is equal to",
                    neq: "Is not equal to"
                }
            }
        },
        pageable: true,
        height: 550,
        toolbar: ["create"],
        columns: [
            { field: "CategoryName", title: "Category Name" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "200px" }],
        editable: "popup",
        sortable: true
    });
});

// ajax function to get list of users from the server for drop down list
function getUsers(url) {
    return JSON.parse($.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        global: false,
        async: false,
        success: function (data) {
            return data;
        }
    }).responseText);
}

function getStatus(url) {
    return JSON.parse($.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        global: false,
        async: false,
        success: function (data) {
            return data;
        }
    }).responseText);
}

function logWorkflowHistory(url, changeLog) {
    return JSON.parse($.ajax({
        type: 'POST',
        url: url,
        //contentType: "application/json; charset=utf-8",
        data: changeLog,
        dataType: 'json',
        global: false,
        async: false,
        success: function (data) {
            return data;
        }
    }).responseText);
}