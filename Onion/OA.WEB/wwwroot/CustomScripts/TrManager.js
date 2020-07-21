var TrManager = {
    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },

    UpdateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("Status") === '') {
            AppUtil.ShowErrorOnControl("This field is required.", "Status", "top left");
            status = false;
        }
      
        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },
    UpdateStatus: function () {
        var url = "/ManagePayment/UpdateStatus/";
        var data = ({ id: _id });
        data = TrManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TrManager.UpdateStatusSuccess, TrManager.UpdateStatusFail);

    },
    UpdateStatusSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated");
            table.draw();
            $("#mdlUpdateStaus").modal('hide');
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    UpdateStatusFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    ViewDetails: function (_id) {
        var url = "/ManagePayment/ViewDetails/";
        var data = ({ id: _id });
        data = TrManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TrManager.ViewDetailsSuccess, TrManager.ViewDetailsFail);

    },
    ViewDetailsSuccess: function (data) {
        var payement = data.payement;
        if (data.success === true) {
            $("#divdetails").empty();
            $("#divdetails").append("<div class=\"row\"><div class= \"col-4\">Tranasaction Link ID : " + payement.linkID + "</div><div class=\"col-4\"> Amount : " + payement.amount + "</div><div class=\"col-4\">Account Number : " + payement.accountNumber + "</div><div class=\"col-6\">Bank Name : " + payement.bankName + "</div><div class=\"col-6\">Description : " + payement.description + "</div><div class=\"col-12\"><img src=\"" + payement.documentsUrl + "\" height=\"100\" width=\"160\" /><img src=\"" + payement.documentsUrl2 + "\" height=\"100\" width=\"160\" /><img src=\"" + payement.documentsUrl3 +"\" height=\"100\" width=\"160\" /></div></div >");
            $("#mdlDetails").modal('show');
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    ViewDetailsFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Utility/EditEmployee/";
        var data = ({ id: _id });
        data = TrManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TrManager.GetForUpdatesuccess, TrManager.GetForUpdateFail);
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


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TrManager.UpdateEmployeeSuccess, TrManager.UpdateEmployeeFail);


    },
    UpdateEmployeeSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            TrManager.clearUpdateInformation();
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
        data = TrManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TrManager.DeleteEmployeeSuccess, TrManager.DeleteEmployeeFailed);
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