
var pinfoManager = {

    CreateVerndorTypeValidation: function () {

    },
    CreateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("step1") === '') {
            AppUtil.ShowErrorOnControl("Please add something.", "step1", "top left");
            status = false;
        }

        if (AppUtil.GetIdValue("step2") === '') {
            AppUtil.ShowErrorOnControl("Please add something.", "step2", "top left");
            status = false;
        }

        if (AppUtil.GetIdValue("step3") === '') {
            AppUtil.ShowErrorOnControl("Please add something.", "step3", "top left");
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
        if (AppUtil.GetIdValue("upstep1") === '') {
            AppUtil.ShowErrorOnControl("Please add something.", "upstep1", "top left");
            status = false;
        }
       
        if (AppUtil.GetIdValue("upstep2") === '') {
            AppUtil.ShowErrorOnControl("Please add something.", "upstep2", "top left");
            status = false;
        }
       
        if (AppUtil.GetIdValue("upstep3") === '') {
            AppUtil.ShowErrorOnControl("Please add something.", "upstep3", "top left");
            status = false;
        }
       
        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },

    Insertpinfo: function () {
        var url = "/Process/Addinfor/";
        var Step1 = AppUtil.GetIdValue("step1");
        var Step2 = AppUtil.GetIdValue("step2");
        var Step3 = AppUtil.GetIdValue("step3");
        var Step4 = AppUtil.GetIdValue("step4");
        var Step5 = AppUtil.GetIdValue("step5");
        var Step6 = AppUtil.GetIdValue("step6");
        var pinfo = { Step1: Step1, Step2: Step2, Step3: Step3,Step4: Step4, Step5: Step5, Step6: Step6 };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('inforInfo', JSON.stringify(pinfo));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, pinfoManager.InsertpinfoSuccess, pinfoManager.InsertpinfoFail);

    },
    InsertpinfoSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted");
            $("#mdlAddNew").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
        }

    },
    InsertpinfoFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Process/Editinfor/";
        var data = ({ id: _id });
        data = pinfoManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, pinfoManager.GetForUpdatesuccess, pinfoManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.infor.id);
        $("#upstep1").val(data.infor.step1);
        $("#upstep2").val(data.infor.step2);
        $("#upstep3").val(data.infor.step3);
        $("#upstep4").val(data.infor.step4);
        $("#upstep5").val(data.infor.step5);
        $("#upstep6").val(data.infor.step6);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    Updatepinfo: function () {

        var url = "/Process/Updateinfor";
        var Id = AppUtil.GetIdValue("UpddateID");
        var Step1 = AppUtil.GetIdValue("upstep1");
        var Step2 = AppUtil.GetIdValue("upstep2");
        var Step3 = AppUtil.GetIdValue("upstep3");
        var Step4 = AppUtil.GetIdValue("upstep4");
        var Step5 = AppUtil.GetIdValue("upstep5");
        var Step6 = AppUtil.GetIdValue("upstep6");
        var pinfo = { Id: Id, Step1: Step1, Step2: Step2, Step3: Step3, Step4: Step4, Step5: Step5, Step6: Step6 };

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;



        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('pinfo', JSON.stringify(pinfo));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, pinfoManager.UpdatepinfoSuccess, pinfoManager.UpdatepinfoFail);


    },
    UpdatepinfoSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            $("#mdlUpdate").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator");
                
        }
    },
    UpdatepinfoFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    Deletepinfo: function (_id) {

        var url = "/Process/Deleteinfor/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = pinfoManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, pinfoManager.DeletepinfoSuccess, pinfoManager.DeletepinfoFailed);
    },
    DeletepinfoSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            window.location.reload();
            $("#mdlDelete").modal("hide");
        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }

    },
    DeletepinfoFailed: function (data) {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },
}