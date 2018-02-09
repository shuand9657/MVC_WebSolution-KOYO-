MVCApp.controller('poValidationCtrl', poValidationCtrl);

function poValidationCtrl($http, $scope, $rootScope, ValErrMsgService, ToolTipFactory, UserProfileService ,localStorageService)
{
    var vm = this;
    vm.formValidat = {};
    vm.thisForm=this.validateForm;
    vm.validClick = validClick;

    vm.SetTooltip = SetTooltip;

    vm.onLoadMasterDetail = onLoadMasterDetail;
    vm.OrgItemsChange = OrgItemsChange;
    vm.FormErrSummary = FormErrSummary;

    vm.errorTooltip = {};
    
    var user = localStorageService.get("UserProfile");
    ValErrMsgService.LoadValidationMessage(user.keyCulture);

    function SetTooltip(fieldName)
    {
        if (fieldName.$invalid)
        {
            var errors = ValErrMsgService.ValidateElem(fieldName);
            if (errors.length != 0)
            {
                vm.errorTooltip[fieldName.$name] = {
                    Message: errors[0].message
                };
            }
            else
                if (vm.errorTooltip[fieldName.$name])
                {
                    delete vm.errorTooltip[fieldName.$name];
                }
        }
        else
        {
            if (vm.errorTooltip[fieldName.$name])
            {
                delete vm.errorTooltip[fieldName.$name];
            }
        }
    }
    function validClick(formName)
    {
        vm.formToolTip = ValErrMsgService.ValidateForm(formName);
        //vm.formToolTip = validationFactory.ValidateForm(formName);
        vm.formToolTipContent = ToolTipFactory.ToolTipFor("vm.formToolTip");
    };

    function FormErrSummary(formName)
    {
        var a = formName;
        //vm.formToolTip = validationFactory.ValidateForm(formName);
        vm.formToolTip = ValErrMsgService.ValidateForm(formName);
        if (vm.formToolTip.length > 0)
        {
            vm.showHint = true;
            a = ToolTipFactory.ToolTipFor("vm.formToolTip");
            vm.errorTooltip.FormValid = a;
        }
        else
        {
            vm.showHint = false;
            delete vm.errorTooltip.FormValid;
        }
        
    }

    function onLoadMasterDetail() {
        $http({
            method: 'POST',
            url: '/Home/getOrgDetail'
        }).success(function (data, status, headers, config) {
            vm.OrgDetail = data;
        }).error(function (data, status, headers, config) {
            console.log(data);
        })
    }
    vm.onLoadMasterDetail();
    function OrgItemsChange(MasterID)
    {
        if (MasterID > 0)
        {
            vm.testMasterList = vm.OrgDetail[MasterID - 1].OrgSN;
        }
        else
        {
            vm.testMasterList = "";
        }
    };

    $(function () {
        $("Form").kendoValidator();
    });

    vm.errorCollection = [];
    vm.errorSummary = [];
}