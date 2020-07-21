var UserManager = {
    DeleteSeller: function (_ID) {

        var url = "/UserManage/DeleteSeller/";
        var Id = _ID;
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = UserManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, UserManager.DeleteSellerSuccess, UserManager.DeleteSellerFailed);
    },
    DeleteSellerSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            $("#mdlDelete").modal("hide");
            table.draw();
        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }

    },
    DeleteSellerFailed: function (data) {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },

    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },


    CreateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("addFirstName") === '') {
            AppUtil.ShowErrorOnControl("Please add first name.", "addFirstName", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("addEmail") === '') {
            AppUtil.ShowErrorOnControl("Please write email address.", "addEmail", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("addPhone") === '') {
            AppUtil.ShowErrorOnControl("Please write phone number.", "addPhone", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("addPassword") === '') {
            AppUtil.ShowErrorOnControl("Password is required.", "addPassword", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue($("#addPassword").val().length <= 5)) {
            AppUtil.ShowErrorOnControl("Short password.", "addPassword", "top left");
            status = false;
        }
        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },
    UpdateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("titleUpdate") === '') {
            AppUtil.ShowErrorOnControl("Please add title.", "titleUpdate", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("descriptionUpdate") === '') {
            AppUtil.ShowErrorOnControl("Please write something.", "descriptionUpdate", "top left");
            status = false;
        }
        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },

    InsertEmployee: function () {
        var url = "/UserManage/InsertEmployee/";
        var FirstName = AppUtil.GetIdValue("addFirstName");
        var LastName = AppUtil.GetIdValue("addLastName");
        var Email = AppUtil.GetIdValue("addEmail");
        var Phone = AppUtil.GetIdValue("addPhone");
        var Password = AppUtil.GetIdValue("addPassword");


        var Employee = { FirstName: FirstName, LastName: LastName, Email: Email, Phone: Phone, Password: Password };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('EmployeeInfo', JSON.stringify(Employee));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, UserManager.InsertEmployeeSuccess, UserManager.InsertEmployeeFail);

    },
    InsertEmployeeSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted");
            UserManager.clearSaveInformation();
            table.draw();
            $("#mdlAddNew").modal('hide');
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    InsertEmployeeFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Utility/EditEmployee/";
        var data = ({ id: _id });
        data = UserManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, UserManager.GetForUpdatesuccess, UserManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.Employee.id);
        $("#titleUpdate").val(data.Employee.title);
        $("#descriptionUpdate").val(data.Employee.description);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    UpdateEmployee: function () {

        var url = "/Utility/UpdateEmployee";
        var Id = AppUtil.GetIdValue("UpddateID");
        var Title = AppUtil.GetIdValue("titleUpdate");
        var Description = AppUtil.GetIdValue("descriptionUpdate");

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;


        var data = { Id: Id, Title: Title, Description: Description };


        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('EmployeeInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, UserManager.UpdateEmployeeSuccess, UserManager.UpdateEmployeeFail);


    },
    UpdateEmployeeSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            UserManager.clearUpdateInformation();
            $("#mdlUpdate").modal('hide');
            window.location.reload();
        }
    },
    UpdateEmployeeFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    DeleteEmployee: function (_id) {

        var url = "/UserManage/DeleteEmployee/";
        var Id = _ID;
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = UserManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, UserManager.DeleteEmployeeSuccess, UserManager.DeleteEmployeeFailed);
    },
    DeleteEmployeeSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            $("#mdlDelete").modal("hide");
            table.draw();
        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }
        $("#mdlDelete").modal("hide");

    },
    DeleteEmployeeFailed: function (data) {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },

    clearSaveInformation: function () {
        $("#addFirstName").val("");
        $("#addLastName").val("");
        $("#addPhone").val("");
        $("#addEmail").val("");
        $("#addPassword").val("");
    },
    clearUpdateInformation: function () {
        $("#UpddateID").val("");
        $("#titleUpdate").val("");
        $("#descriptionUpdate").val("");
    },

}