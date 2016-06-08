$(function () {

    $("#grid").jqGrid({
        url: "/toDoList/GetTodoLists",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Id', 'Task Name', 'Task Description', 'Submission Date', 'Due Date', 'Severity', 'Status', 'Owner'],
        colModel: [
            { key: true, hidden: true, name: 'item_id', index: 'item_id', editable: false },
            { key: false, name: 'Title', index: 'Title', editable: true },
            { key: false, name: 'Content', index: 'Content', editable: true },
            { key: false, name: 'SubmissionDate', index: 'SubmissionDate', editable: false },
            { key: false, name: 'DueDate', index: 'DueDate', editable: true },
            { key: false, name: 'Severity', index: 'Severity', editable: true },
            { key: false, name: 'taskStatus', index: 'taskStatus', editable: true },
            { key: false, name: 'Owner', index: 'Owner', editable: false }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Todo List',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: '/toDoList/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/toDoList/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/toDoList/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});