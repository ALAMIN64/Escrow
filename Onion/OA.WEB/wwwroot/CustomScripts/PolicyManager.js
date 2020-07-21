
var PolicyManager = {

    
    CreateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("titleAdd") === '') {
            AppUtil.ShowErrorOnControl("Please add title.", "titleAdd", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("descriptionAdd") === '') {
            AppUtil.ShowErrorOnControl("Please write something.", "descriptionAdd", "top left");
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

    InsertPolicy: function () {
        var url = "/Utility/AddPolicy/";
        var Title = AppUtil.GetIdValue("titleAdd");
        var Description = AppUtil.GetIdValue("descriptionAdd");
        var Policy = { Title: Title, Description: Description };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('PolicyInfo', JSON.stringify(Policy));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, PolicyManager.InsertPolicySuccess, PolicyManager.InsertPolicyFail);

    },
    InsertPolicySuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted");
            PolicyManager.clearSaveInformation();
        $("#mdlAddNew").modal('hide');
        window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
        }
        
    },
    InsertPolicyFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Utility/EditPolicy/";
        var data = ({ id: _id });
        data = PolicyManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, PolicyManager.GetForUpdatesuccess, PolicyManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.policy.id);
        $("#titleUpdate").val(data.policy.title);
        $("#descriptionUpdate").val(data.policy.description);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    UpdatePolicy: function () {

        var url = "/Utility/UpdatePolicy";
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
        formData.append('PolicyInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, PolicyManager.UpdatePolicySuccess, PolicyManager.UpdatePolicyFail);


    },
    UpdatePolicySuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            PolicyManager.clearUpdateInformation();
            $("#mdlUpdate").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator")

        }
    },
    UpdatePolicyFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    DeletePolicy: function (_id) {

        var url = "/Utility/DeletePolicy/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = PolicyManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, PolicyManager.DeletePolicySuccess, PolicyManager.DeletePolicyFailed);
    },
    DeletePolicySuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            $("#mdlDelete").modal("hide");

            window.location.reload();
        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }

    },
    DeletePolicyFailed: function (data) {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },
    clearSaveInformation: function () {
        $("#titleAdd").val("");
        $("#descriptionAdd").val("");
    },
    clearUpdateInformation: function () {
        $("#UpddateID").val("");
        $("#titleUpdate").val("");
        $("#descriptionUpdate").val("");
    },
}