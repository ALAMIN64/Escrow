var TransactionManager = {

    addRequestVerificationToken: function (data) {
        data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        return data;
    },


    CreateValidation: function () {
        var status = true;
        if (AppUtil.GetIdValue("RoleID") === '') {
            $("#roleerror").append("<p><b>Please specify your role.</b></p>");
            //AppUtil.ShowErrorOnControl("Please select this your role.", "RoleID", "top left");
            $(window).scrollTop(0);
            status = false;
        }
        if (AppUtil.GetIdValue("RoleID") == '1') {
            if (AppUtil.GetIdValue("firstName") === '') {
                $("#nameerror").append("<p><b>First name is required</b></p>");
                //AppUtil.ShowErrorOnControl("Please add buyer name.", "firstName", "top left");
                $(window).scrollTop(0);
                status = false;
            }
            if (AppUtil.GetIdValue("buyeremail") === '') {
                $("#bemailerror").append("<p><b>Email is required</b></p>");
                //AppUtil.ShowErrorOnControl("Please write buyer email address.", "buyeremail", "top left");
                $(window).scrollTop(0);
                status = false;
            }
        }
        if (AppUtil.GetIdValue("RoleID") == '2') {
            if (AppUtil.GetIdValue("selleremail") === '') {
                $("#semailerror").append("<p><b>Email is required</b></p>");
                //AppUtil.ShowErrorOnControl("Please write seller email address.", "selleremail", "top left");
                $(window).scrollTop(0);
                status = false;
            }
        }

        if (AppUtil.GetIdValue("itemName") === '') {
            $("#itemerror").append("<p><b>Item name is required</b></p>");
            //AppUtil.ShowErrorOnControl("Please inert item name.", "itemName", "top left");
            $(window).scrollTop(0);
            status = false;
        }
        if (AppUtil.GetIdValue("amount") === '') {
            $("#amounterror").append("<p><b>Amount is required</b></p>");
            //AppUtil.ShowErrorOnControl("Please add amount.", "amount", "top left");
            $(window).scrollTop(0);

            status = false;
        }
        if (AppUtil.GetIdValue("descriptionOfItem") === '') {
            $("#deserror").append("<p><b>Item description is required</b></p>");
            //AppUtil.ShowErrorOnControl("Please write item description.", "descriptionOfItem", "top left");
            $(window).scrollTop(0);

            status = false;
        }
        if (AppUtil.GetIdValue("DeliveryTypeID") === '') {
            $("#dtypeerror").append("<p><b>This field is required</b></p>");
            //AppUtil.ShowErrorOnControl("Please select delivery type.", "DeliveryTypeID", "top left");
            $(window).scrollTop(0);
            status = false;
        }
        if (AppUtil.GetIdValue("DeliveryWindowID") === '') {
            $("#windowerror").append("<p><b>This field is required</b></p>");
            //AppUtil.ShowErrorOnControl("This field is required.", "DeliveryWindowID", "top left");
            $(window).scrollTop(0);
            status = false;
        }
        if (AppUtil.GetIdValue("DeliveryWindowID") === '15') {
            if (AppUtil.GetIdValue("deliverytime") === '') {
                $("#timeerror").append("<p><b>Delivery window is required</b></p>");
                //AppUtil.ShowErrorOnControl("This field is required.", "deliverytime", "top left");
                $(window).scrollTop(0);
                status = false;
            }
        }
        if (AppUtil.GetIdValue("DeliveryTypeID") === '2') {
            if (AppUtil.GetIdValue("deliveryfee") === '') {
                $("#feeerror").append("<p><b>Delivery charge is required</b></p>");
                //AppUtil.ShowErrorOnControl("Delivery fee is required.", "deliveryfee", "top left");
                $(window).scrollTop(0);
                status = false;
            }
        }
        if (AppUtil.GetIdValue("TermsOfReturns") === '') {
            $("#termserror").append("<p><b>Terms of return is required</b></p>");
            //AppUtil.ShowErrorOnControl("Please write  terms of return.", "TermsOfReturns", "top left");
            $(window).scrollTop(0);

            status = false;
        }
        if (AppUtil.GetIdValue("amount") === '') {
            if ($('#summeryID').not(":checked")) {
            }
            else {
                $("#summeryerror").append("<p><b>Summery is required</b></p>");
                //AppUtil.ShowErrorOnControl("Summery is required.", "summeryID", "top left");
                status = false;
            }
        }

        if (status == false) {
            return false;
        }
        else {
            return true;
        }
    },
    //UpdateValidation: function () {
    //    var status = true;
    //    if (AppUtil.GetIdValue("titleUpdate") === '') {
    //        AppUtil.ShowErrorOnControl("Please add title.", "titleUpdate", "top left");
    //        status = false;
    //    }
    //    if (AppUtil.GetIdValue("descriptionUpdate") === '') {
    //        AppUtil.ShowErrorOnControl("Please write something.", "descriptionUpdate", "top left");
    //        status = false;
    //    }
    //    if (status == false) {
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //},

    InsertTransaction: function () {
        debugger;
        var url = "/Transaction/GenerateLink/";
        var UserRoleId = AppUtil.GetIdValue("RoleID");
        var FirstName = AppUtil.GetIdValue("firstName");
        var LastName = AppUtil.GetIdValue("LastName");
        var Email = AppUtil.GetIdValue("buyeremail");
        var SellerEmail = AppUtil.GetIdValue("selleremail");
        var ItemName = AppUtil.GetIdValue("itemName");
        var Amount = AppUtil.GetIdValue("amount");
        var DescriptionOfItem = AppUtil.GetIdValue("descriptionOfItem");
        var DeliveryTypeID = AppUtil.GetIdValue("DeliveryTypeID");
        var DeliveryTime = AppUtil.GetIdValue("deliverytime");
        var DeliveryWindowID = AppUtil.GetIdValue("DeliveryWindowID");
        var TermsOfReturns = AppUtil.GetIdValue("TermsOfReturns");
        var Deliveryfee = AppUtil.GetIdValue("deliveryfee");


        var Employee = {
            UserRoleId: UserRoleId, FirstName: FirstName, LastName: LastName, Email: Email, SellerEmail: SellerEmail, ItemName: ItemName, Amount: Amount,
            DescriptionOfItem: DescriptionOfItem, DeliveryTypeID: DeliveryTypeID, DeliveryTime: DeliveryTime, TermsOfReturns: TermsOfReturns, Deliveryfee: Deliveryfee,
            DeliveryWindowID: DeliveryWindowID
        };
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken;
        var formData = new FormData();
        var totalfiles = document.getElementById('browse').files.length;
        for (var index = 0; index < totalfiles; index++) {
            formData.append("files[]", document.getElementById('browse').files[index]);
        }
        formData.append('EmployeeInfo', JSON.stringify(Employee));
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TransactionManager.InsertTransactionSuccess, TransactionManager.InsertTransactionFail);

    },
    InsertTransactionSuccess: function (data) {
        if (data.success === true) {
            var templates = '<div><div><h6 style="color:green">Successfully started Transaction & link generated.</h6><h4><b>Transaction</b></h4></div><div><h5><b>Status : Pending </b></h5></div><div><p>Send this links to buyer for access to this trasaction</p></div><div><p>Transaction Link: <b>' + data.link + '</b></p></div><div><p>An email with the links will be sent to buyer and seller mail</p></div></div> ';
            $("#divSuccess").empty();
            $("#divSuccess").append(templates);
            $(window).scrollTop(0);
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    InsertTransactionFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

    ViewSummery: function () {
        debugger;
        var url = "/Transaction/CalculateFee/";
        var buyerName = $("#firstName").val() + " " + $("#LastName").val();
        var buyerEmail = $("#email").val();
        var selerEmail = $("#selleremail").val();
        var items = $("#itemName").val();

        var Amount = $("#amount").val();
        var DeliveryTypeID = $("#DeliveryTypeID").val()
        var DeliveryFee = $("#deliveryfee").val();
        var model = { DeliveryFee: DeliveryFee, Amount: Amount };
        var formData = new FormData();
        formData.append('modelInfo', JSON.stringify(model));
       
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken; 
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TransactionManager.ViewSummerySuccess, TransactionManager.ViewSummeryFail);

    },
    ViewSummerySuccess: function (data) {
        if (data.success === true) {
            var buyerName = $("#firstName").val() + " " + $("#LastName").val();
            var buyerEmail = $("#buyeremail").val();
            var selerEmail = $("#selleremail").val();
            var DeliveryFee = $("#deliveryfee").val() != 0 ? $("#deliveryfee").val():0;
            var DeliveryTime = $("#deliverytime").val();
            var deliveryName = $("#DeliveryTypeID").val() == Number(1) ? "Free Delivery" : $("#DeliveryTypeID").val() == Number(2) ? "Paid Delivery" : "Free Delivery";
            var items = $("#itemName").val();
            if ($("#RoleID").val() === "2") {
                $("#replaceDiv").empty();
                $("#replaceDiv").append('<div><label class="label-control">Email(Buyer) : ' + selerEmail + '</label></div><div><label class="label-control">Item : ' + items + '</label></div><div><label class="label-control">Amount : ' + data.calculate.amount + '</label></div><div><label class="label-control">Delivery : ' + deliveryName + '</label></div><div><label class="label-control">Delivery Charge : ' + DeliveryFee + '</label></div><div><label class="label-control">Escrow fee : ' + data.calculate.escrowfee + '</label></div><div><label class="label-control">Total : ' + data.calculate.total + '</label></div>');
            }
            if ($("#RoleID").val() === "1") {
                $("#replaceDiv").empty();
                $("#replaceDiv").append('<div><label class="label-control">Name of Buyer : ' + buyerName + '</label></div><div><label class="label-control">Email(Buyer) : ' + buyerEmail + '</label></div><div><label class="label-control">Item : ' + items + '</label></div><div><label class="label-control">Amount : ' + data.calculate.amount + '</label></div><div><label class="label-control">Delivery : ' + deliveryName + '</label></div><div><label class="label-control">Delivery Charge : ' + DeliveryFee + '</label></div><div><label class="label-control">Escrow fee : ' + data.calculate.escrowfee + '</label></div><div><label class="label-control">Total : ' + data.calculate.total + '</label></div>');
            }
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    ViewSummeryFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },
    
    CalculateFee: function () {
        debugger;
        var url = "/MyTransaction/CalculateFee/";
        
        var Amount = $("#Amountvalue").val();
        var model = { Amount: Amount };
        var formData = new FormData();
        formData.append('modelInfo', JSON.stringify(model));
       
        var AntiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var header = {};
        header['__RequestVerificationToken'] = AntiForgeryToken; 
        AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TransactionManager.CalculateFeeSuccess, TransactionManager.CalculateFeeFail);

    },
    CalculateFeeSuccess: function (data) {
        if (data.success === true) {
            //efp
            $("#replaceDIV").empty();
            $("#replaceDIV").append("<div><h5><b>Escrow fee : " + data.efp + "</b></h5></div>");      
        }
        else if (data.success === false) {
            AppUtil.ShowSuccess(data.message);
        }
    },
    CalculateFeeFail: function (data) {
        AppUtil.ShowSuccess("Error Occoured. Contact with Administrator.");
    },

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


    //    AppUtil.MakeAjaxCallJSONAntifergery(url, "POST", formData, header, TransactionManager.UpdateEmployeeSuccess, TransactionManager.UpdateEmployeeFail);


    //},
    //UpdateEmployeeSuccess: function (data) {

    //    if (data.success === true) {
    //        AppUtil.ShowSuccess("Successfully Updated. ");
    //        TransactionManager.clearUpdateInformation();
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
    //    data = TransactionManager.addRequestVerificationToken(data);
    //    AppUtil.MakeAjaxCallsForAntiForgery(url, "POST", data, TransactionManager.DeleteEmployeeSuccess, TransactionManager.DeleteEmployeeFailed);
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