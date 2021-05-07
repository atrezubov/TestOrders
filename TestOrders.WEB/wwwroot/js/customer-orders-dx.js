var getCustomersApi = murl + 'api/getcustomers';
var insertCustomerApi = murl + 'api/addcustomer';
var updateCustomerApi = murl + 'api/updatecustomer';
var getOrdersApi = murl + 'api/getorders';
var insertOrderApi = murl + 'api/addorder';
var deleteOrderApi = murl + 'api/deleteorder';

$("#customersGridContainer").dxDataGrid({
    dataSource: DevExpress.data.AspNet.createStore({
        key: "id",
        loadUrl: getCustomersApi,
        insertUrl: insertCustomerApi,
        updateUrl: updateCustomerApi
    }),
    remoteOperations: true,
    allowColumnReordering: true,
    allowColumnResizing: true,
    columnAutoWidth: true,
    showBorders: false,
    sorting: {
        mode: "multiple"
    },
    filterRow: {
        visible: true,
        applyFilter: "auto"
    },
    searchPanel: {
        visible: true,
        width: 240,
        placeholder: "Search..."
    },
    headerFilter: {
        visible: true
    },
    columnChooser: {
        enabled: true
    },
    columnFixing: {
        enabled: true
    },
    columns: [
        {
            dataField: "id",
            visible: false,
            formItem: {
                editorOptions: { readOnly: true }
            }
        },
        {
            dataField: "name",
            caption: "Name",
            validationRules: [{ type: "required" }]
        },
        {
            dataField: "address",
            caption: "Address",
            validationRules: [{ type: "required" }]
        },
        {
            dataField: "deleted",
            caption: "Deleted"
        }
    ],
    paging: {
        pageSize: 25
    },
    editing: {
        mode: "form",
        allowAdding: true,
        allowUpdating: true
    },
    masterDetail: {
        enabled: true,
        template: function (container, options) {
            var currentEmployeeData = options.data;
            $("<div>")
                .addClass("master-detail-caption")
                .addClass("dx-master-detail-caption-custom-italic")
                .text(currentEmployeeData.name + "'s orders:")
                .appendTo(container);

            $("<div>")
                .dxDataGrid({
                    dataSource: DevExpress.data.AspNet.createStore({
                        key: "id",
                        loadUrl: getOrdersApi + '?customerId=' + options.key,
                        insertUrl: insertOrderApi,
                        deleteUrl: deleteOrderApi
                    }),
                    remoteOperations: true,
                    allowColumnReordering: true,
                    allowColumnResizing: true,
                    columnAutoWidth: true,
                    showBorders: false,
                    sorting: {
                        mode: "multiple"
                    },
                    filterRow: {
                        visible: true,
                        applyFilter: "auto"
                    },
                    searchPanel: {
                        visible: true,
                        width: 240,
                        placeholder: "Search..."
                    },
                    headerFilter: {
                        visible: true
                    },
                    columnFixing: {
                        enabled: true
                    },
                    paging: {
                        pageSize: 25
                    },
                    editing: {
                        mode: "form",
                        allowAdding: true,
                        allowDeleting: true
                    },
                    onRowInserting: function (e) {
                        e.data.customerid = options.key;
                    },
                    onRowUpdating: function (e) {
                        e.newData = $.extend({}, e.oldData, e.newData);
                    },
                    columns: [
                        {
                            dataField: "id",
                            visible: false,
                            formItem: {
                                editorOptions: { readOnly: true }
                            }
                        },
                        {
                            dataField: "customerid",
                            visible: false,
                            value: options.key,
                            formItem: {
                                editorOptions: { readOnly: true }
                            }
                        },
                        {
                            dataField: "date",
                            caption: "Date",
                            dataType: "date",
                            validationRules: [{ type: "required" }]
                        },
                        {
                            dataField: "number",
                            caption: "Number",
                            dataType: "number",
                            validationRules: [{ type: "required" }]
                        },
                        {
                            dataField: "amount",
                            caption: "Amount",
                            dataType: "number",
                            validationRules: [{ type: "required" }]
                        },
                        {
                            dataField: "description",
                            caption: "Description",
                            visible: false,
                            formItem: {
                                colSpan: 2,
                                editorType: "dxTextArea",
                                editorOptions: {
                                    height: 100
                                }
                            }
                        },
                        {
                            dataField: "deleted",
                            caption: "Deleted",
                            dataType: "boolean"
                        }
                    ],
                    masterDetail: {
                        enabled: true,
                        template: function (containerInside, optionsInside) {
                            var currentEmployeeDataInside = optionsInside.data;
                            $("<div>")
                                .addClass("master-detail-caption")
                                .addClass("dx-master-detail-caption-custom")
                                .text("Description: " + currentEmployeeDataInside.description)
                                .appendTo(containerInside);
                        }
                    }
                }).appendTo(container);

        }
    }
});