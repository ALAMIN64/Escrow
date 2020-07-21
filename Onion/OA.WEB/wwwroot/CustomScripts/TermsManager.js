
var TermsManager = {

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

    InsertTerms: function () {
        var url = "/Utility/AddTerms/";
        var Title = AppUtil.GetIdValue("titleAdd");
        var Description = AppUtil.GetIdValue("descriptionAdd");
        var Terms = { Title: Title, Description: Description };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('TermsInfo', JSON.stringify(Terms));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TermsManager.InsertTermsSuccess, TermsManager.InsertTermsFail);

    },
    InsertTermsSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted"); TermsManager.clearSaveInformation();
            $("#mdlAddNew").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
        }

    },
    InsertTermsFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Utility/EditTerms/";
        var data = ({ id: _id });
        data = TermsManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TermsManager.GetForUpdatesuccess, TermsManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.terms.id);
        $("#titleUpdate").val(data.terms.title);
        $("#descriptionUpdate").val(data.terms.description);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    UpdateTerms: function () {

        var url = "/Utility/UpdateTerms";
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
        formData.append('TermsInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TermsManager.UpdateTermsSuccess, TermsManager.UpdateTermsFail);


    },
    UpdateTermsSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            TermsManager.clearUpdateInformation();
            $("#mdlUpdate").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator")

        }
    },
    UpdateTermsFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    DeleteTerms: function (_id) {

        var url = "/Utility/DeleteTerms/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = TermsManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TermsManager.DeleteTermsSuccess, TermsManager.DeleteTermsFailed);
    },
    DeleteTermsSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            window.location.reload();
            $("#mdlDelete").modal("hide");

        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }

    },
    DeleteTermsFailed: function (data) {
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