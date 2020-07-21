
var HomeManager = {

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
        var url = "/Utility/AddHomeContent/";
        var Title = AppUtil.GetIdValue("titleAdd");
        var Description = AppUtil.GetIdValue("descriptionAdd");
        var about = { Title: Title, Description: Description };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('aboutInfo', JSON.stringify(about));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, HomeManager.InsertAboutSuccess, HomeManager.InsertAboutFail);

    },
    InsertAboutSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted");
            HomeManager.clearSaveInformation();
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
        var url = "/Utility/EditHomeContent/";
        var data = ({ id: _id });
        data = HomeManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, HomeManager.GetForUpdatesuccess, HomeManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.about.id);
        $("#titleUpdate").val(data.about.title);
        $("#descriptionUpdate").val(data.about.desciption);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    UpdateAbout: function () {

        var url = "/Utility/UpdateHomeContent";
        var Id = AppUtil.GetIdValue("UpddateID");
        var Title = AppUtil.GetIdValue("titleUpdate");
        var Desciption = AppUtil.GetIdValue("descriptionUpdate");

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;


        var data = { Id: Id, Title: Title, Desciption: Desciption };


        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('aboutInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, HomeManager.UpdateAboutSuccess, HomeManager.UpdateAboutFail);


    },
    UpdateAboutSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            HomeManager.clearUpdateInformation();
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

        var url = "/Utility/DeleteHomeContent/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = HomeManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, HomeManager.DeleteAboutSuccess, HomeManager.DeleteAboutFailed);
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
}