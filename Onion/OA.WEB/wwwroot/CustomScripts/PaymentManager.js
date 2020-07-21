var PaymentManager = {

    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },


    CreateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("AccountNumber") === '') {
            AppUtil.ShowErrorOnControl("This field is required.", "AccountNumber", "top left");
            $(window).scrollTop(0);
            status = false;
        }

        if (AppUtil.GetIdValue("BankName") === '') {
            AppUtil.ShowErrorOnControl("This field is required.", "BankName", "top left");
            $(window).scrollTop(0);
            status = false;
        }

        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },

    AddPayment: function () {
        var url = "/Payment/PayInsert/";
        var LinkID = AppUtil.GetIdValue("LinkID");
        var Amount = AppUtil.GetIdValue("Amount");
        var BankName = AppUtil.GetIdValue("BankName");
        var AccountNumber = AppUtil.GetIdValue("AccountNumber");
        var Description = AppUtil.GetIdValue("Description");
    


        var model = { Description: Description, LinkID: LinkID, Amount: Amount, BankName: BankName, AccountNumber: AccountNumber};
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        var totalfiles = document.getElementById('browse').files.length;
        for (var index = 0; index < totalfiles; index++) {
            formData.append("files[]", document.getElementById('browse').files[index]);
        }
        formData.append('modelInfo', JSON.stringify(model));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, PaymentManager.AddPaymentSuccess, PaymentManager.AddPaymentFail);

    },
    AddPaymentSuccess: function (data) {
        if (data.success === true) {
            var templates = '<div><div><h6 style="color:green">Successfully recieved. After verify we will update the transaction status</h6></div></div> ';
            $("#confirmDiv").empty();
            $("#remve").empty();
            $("#confirmDiv").append(templates);
            $(window).scrollTop(0);
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    AddPaymentFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    
    //CalculateFee: function () {
    //    debugger;
    //    var url = "/MyTransaction/CalculateFee/";
        
    //    var Amount = $("#Amountvalue").val();
    //    var model = { Amount: Amount };
    //    var formData = new FormData();
    //    formData.append('modelInfo', JSON.stringify(model));
       
    //    var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    //    var header = {};
    //    header['__RequestVerificationToken'] = AntiForgeryToken; 
    //    AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, PaymentManager.CalculateFeeSuccess, PaymentManager.CalculateFeeFail);

    //},
    //CalculateFeeSuccess: function (data) {
    //    if (data.success === true) {
    //        //efp
    //        $("#replaceDIV").empty();
    //        $("#replaceDIV").append("<div><h5><b>Escrow fee : " + data.efp + "</b></h5></div>");      
    //    }
    //    else if (data.success === false) {
    //        AppUtil.ShowSuccess(data.message);
    //    }
    //},
    //CalculateFeeFail: function (data) {
    //    AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    //},

    //UpdateEmployee: function () {

    //    var url = "/Utility/UpdateEmployee";
    //    var Id = AppUtil.GetIdValue("UpddateID");
    //    var Title = AppUtil.GetIdValue("titleUpdate");
    //    var Description = AppUtil.GetIdValue("descriptionUpdate");

    //    var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    //    var header = {};
    //    header['__RequestVerificationToken'] = AntiForgeryToken;


    //    var data = { Id: Id, Title: Title, Description: Description };


    //    var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    //    var header = {};
    //    header['__RequestVerificationToken'] = AntiForgeryToken;
    //    var formData = new FormData();
    //    formData.append('EmployeeInfo', JSON.stringify(data));


    //    AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, PaymentManager.UpdateEmployeeSuccess, PaymentManager.UpdateEmployeeFail);


    //},
    //UpdateEmployeeSuccess: function (data) {

    //    if (data.success === true) {
    //        AppUtil.ShowSuccess("Successfully Updated. ");
    //        PaymentManager.clearUpdateInformation();
    //        $("#mdlUpdate").modal('hide');
    //        window.location.reload();
    //    }
    //},
    //UpdateEmployeeFail: function () {
    //    AppUtil.ShowSuccess("Error! Contact with Administator")
    //},


    //DeleteEmployee: function (_id) {

    //    var url = "/UserManage/DeleteEmployee/";
    //    var Id = _ID;
    //    var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    //    var header = {};
    //    header['__RequestVerificationToken'] = AntiForgeryToken;
    //    var data = ({ id: Id });
    //    data = PaymentManager.addRequestVerificationToken(data);
    //    AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, PaymentManager.DeleteEmployeeSuccess, PaymentManager.DeleteEmployeeFailed);
    //},
    //DeleteEmployeeSuccess: function (data) {
    //    if (data.success === true) {
    //        AppUtil.ShowSuccess("Successfully Deleted!");
    //        $("#mdlDelete").modal("hide");
    //        table.draw();
    //    }
    //    if (data.success === false) {
    //        AppUtil.ShowSuccess("Failed to delete!");
    //    }
    //    $("#mdlDelete").modal("hide");

    //},
    //DeleteEmployeeFailed: function (data) {
    //    AppUtil.ShowSuccess("Error! Contact with Administator")
    //},

    //clearSaveInformation: function () {
    //    $("#addFirstName").val("");
    //    $("#addLastName").val("");
    //    $("#addPhone").val("");
    //    $("#addEmail").val("");
    //    $("#addPassword").val("");
    //},
    //clearUpdateInformation: function () {
    //    $("#UpddateID").val("");
    //    $("#titleUpdate").val("");
    //    $("#descriptionUpdate").val("");
    //},

}