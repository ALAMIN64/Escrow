
var AboutManager = {

    CreateVerndorTypeValidation: function () {

    },
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

    InsertAbout: function () {
        var url = "/Utility/AddAbout/";
        var Title = AppUtil.GetIdValue("titleAdd");
        var Description = AppUtil.GetIdValue("descriptionAdd");
        var about = { Title: Title, Description: Description };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('aboutInfo', JSON.stringify(about));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, AboutManager.InsertAboutSuccess, AboutManager.InsertAboutFail);

    },
    InsertAboutSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted");
            AboutManager.clearSaveInformation();
            $("#mdlAddNew").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
        }

    },
    InsertAboutFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Utility/EditAbout/";
        var data = ({ id: _id });
        data = AboutManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, AboutManager.GetForUpdatesuccess, AboutManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.about.id);
        $("#titleUpdate").val(data.about.title);
        $("#descriptionUpdate").val(data.about.description);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    UpdateAbout: function () {

        var url = "/Utility/UpdateAbout";
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
        formData.append('aboutInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, AboutManager.UpdateAboutSuccess, AboutManager.UpdateAboutFail);


    },
    UpdateAboutSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            AboutManager.clearUpdateInformation();
            $("#mdlUpdate").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator");
                
        }
    },
    UpdateAboutFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    DeleteAbout: function (_id) {

        var url = "/Utility/DeleteAbout/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = AboutManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, AboutManager.DeleteAboutSuccess, AboutManager.DeleteAboutFailed);
    },
    DeleteAboutSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            window.location.reload();
            $("#mdlDelete").modal("hide");
        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }

    },
    DeleteAboutFailed: function (data) {
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



    UpdateStatus: function () {

        var url = "/MyTransaction/UpdateStatus";
        var ID = AppUtil.GetIdValue("TransactionID");
        var Status = AppUtil.GetIdValue("statusID");

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;


        var data = { ID: ID, Status: Status };


        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('aboutInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, AboutManager.UpdateStatusSuccess, AboutManager.UpdateStatusFail);


    },
    UpdateStatusSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            $("#divReplace").empty();
            $("#divReplace").append("<div class=\"text-center\"><h5 style=\"color:red\">Status Successfully Changed.</h5></div>");

        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator");

        }
    },
    UpdateStatusFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },

}