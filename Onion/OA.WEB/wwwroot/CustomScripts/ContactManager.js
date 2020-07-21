var ContactManager = {

    UpdateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("Title") === '') {
            AppUtil.ShowErrorOnControl("Please add title.", "Title", "top left");
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

    
    UpdateTerms: function () {

        var url = "/Utility/ResponseContact";
        var ID = AppUtil.GetIdValue("UpddateID");
        var Title = AppUtil.GetIdValue("Title");
        var Description = AppUtil.GetIdValue("descriptionUpdate");

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;


        var data = { ID: ID, Title: Title, Description: Description };


        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('TermsInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, ContactManager.UpdateTermsSuccess, ContactManager.UpdateTermsFail);


    },
    UpdateTermsSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Email Sent. ");
            $("#mdlresponse").modal('hide');
        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator")

        }
    },
    UpdateTermsFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    DeleteTerms: function (_id) {

        var url = "/Utility/DeleteContact/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = ContactManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, ContactManager.DeleteTermsSuccess, ContactManager.DeleteTermsFailed);
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
  
}