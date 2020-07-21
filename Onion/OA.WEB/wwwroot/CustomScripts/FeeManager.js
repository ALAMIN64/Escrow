
var FeeManager = {

    CreateVerndorTypeValidation: function () {

    },
    CreateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("amount1Add") === '') {
            AppUtil.ShowErrorOnControl("Please add start Amount.", "amount1Add", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("amount2Add") === '') {
            AppUtil.ShowErrorOnControl("Please add to Amount.", "amount2Add", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("percentageAdd") === '') {
            AppUtil.ShowErrorOnControl("Please add Percentage.", "percentageAdd", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("totalAdd") === '') {
            AppUtil.ShowErrorOnControl("Please add Total fees.", "totalAdd", "top left");
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
        if (AppUtil.GetIdValue("amount1Update") === '') {
            AppUtil.ShowErrorOnControl("Please add start Amount.", "amount1Update", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("amount2Update") === '') {
            AppUtil.ShowErrorOnControl("Please add start Amount.", "amount2Update", "top left");
            status = false;
        }
        if (AppUtil.GetIdValue("percentageUpdate") === '') {
            AppUtil.ShowErrorOnControl("Please write percentage.", "percentageUpdate", "top left");
            status = false;
        }

        if (AppUtil.GetIdValue("totalUpdate") === '') {
            AppUtil.ShowErrorOnControl("Please add Total fees.", "totalUpdate", "top left");
            status = false;
        }
        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },

    InsertFee: function () {
        var url = "/Utility/AddFeeCalcucate/";
        var Amount1 = AppUtil.GetIdValue("amount1Add");
        var Amount2 = AppUtil.GetIdValue("amount2Add");
        var Percentage = AppUtil.GetIdValue("percentageAdd");
        var TotalFees = AppUtil.GetIdValue("totalAdd");
        var Fee = { Amount1: Amount1, Amount2: Amount2, Percentage: Percentage, TotalFees: TotalFees };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('feeInfo', JSON.stringify(Fee));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, FeeManager.InsertFeeSuccess, FeeManager.InsertFeeFail);

    },
    InsertFeeSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Inserted");
            FeeManager.clearSaveInformation();
            $("#mdlAddNew").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
        }

    },
    InsertFeeFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    GetForUpdate: function (_id) {
        var url = "/Utility/EditFeeCalcucate/";
        var data = ({ id: _id });
        data = FeeManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, FeeManager.GetForUpdatesuccess, FeeManager.GetForUpdateFail);
    },
    GetForUpdatesuccess: function (data) {
        $("#UpddateID").val(data.fee.id);
        $("#amount1Update").val(data.fee.amount1);
        $("#amount2Update").val(data.fee.amount2);
        $("#percentageUpdate").val(data.fee.percentage);
        $("#totalUpdate").val(data.fee.totalFees);
        $("#mdlUpdate").modal("show");

    },
    GetForUpdateFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");

    },

    UpdateFee: function () {

        var url = "/Utility/UpdateFeeCalcucate";
        var Id = AppUtil.GetIdValue("UpddateID");
        var Amount1 = AppUtil.GetIdValue("amount1Update");
        var Amount2 = AppUtil.GetIdValue("amount2Update");
        var Percentage = AppUtil.GetIdValue("percentageUpdate");
        var TotalFees = AppUtil.GetIdValue("totalUpdate");

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = { Id: Id, Amount1: Amount1, Amount2: Amount2, Percentage: Percentage, TotalFees: TotalFees };

        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        formData.append('feeInfo', JSON.stringify(data));


        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, FeeManager.UpdateFeeSuccess, FeeManager.UpdateFeeFail);


    },
    UpdateFeeSuccess: function (data) {

        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Updated. ");
            FeeManager.clearUpdateInformation();
            $("#mdlUpdate").modal('hide');
            window.location.reload();
        }
        else {
            AppUtil.ShowSuccess("Error! Contact with Administator")

        }
    },
    UpdateFeeFail: function () {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    DeleteFee: function (_id) {

        var url = "/Utility/DeleteFeeCalcucate/";
        var Id = AppUtil.GetIdValue("deleteID");
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var data = ({ id: Id });
        data = FeeManager.addRequestVerificationToken(data);
        AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, FeeManager.DeleteFeeSuccess, FeeManager.DeleteFeeFailed);
    },
    DeleteFeeSuccess: function (data) {
        if (data.success === true) {
            AppUtil.ShowSuccess("Successfully Deleted!");
            window.location.reload();
            $("#mdlDelete").modal("hide");

        }
        if (data.success === false) {
            AppUtil.ShowSuccess("Failed to delete!");
        }

    },
    DeleteFeeFailed: function (data) {
        AppUtil.ShowSuccess("Error! Contact with Administator")
    },


    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },
    clearSaveInformation: function () {
        $("#amount1Add").val("");
        $("#amount2Add").val("");
        $("#percentageAdd").val("");
        $("#totalAdd").val("");
    },
    clearUpdateInformation: function () {
        $("#UpddateID").val("");
        $("#amount1Update").val("");
        $("#amount2Update").val("");
        $("#percentageUpdate").val("");
        $("#totalUpdate").val("");
    },
}