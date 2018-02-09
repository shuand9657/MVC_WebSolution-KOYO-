'use strict';
MVCApp.service('ValErrMsgService', valMsgService);


function valMsgService(httpFactory, VariableService,localStorageService)
{
    var vm = this;
    
    //function
    vm.LoadValidationMessage = loadValidErrMsg;

    //validation function
    vm.ValidateForm = validateForm;
    vm.ValidateElem = validateElem;

    function loadValidErrMsg(userCulture)
    {
        var keyCulture = { key: userCulture };
        var promise = httpFactory.HttpPostAsync('/Home/getValErrMsg', keyCulture);
        promise.success(function (data, status, headers, config) {
            VariableService.setErrorMessageCollection(data);
        }).error(function (data, status, headers, config) {
            console.log(status);
        });
    };

    function validateForm(formName)
    {
        var vn = this;
        if (!vn.errMsgColle)
        {
            vn.errMsgColle = VariableService.getErrorMessageCollection();
        }
        var errColle = [], errSummary = [], errMsgSummary = [];
        if (formName.$error)
        {
            errColle = formName.$error;

            for (var i in errColle)
            {
                for (var j in errColle[i])
                {
                    errSummary.push({
                        name: errColle[i][j].$name,
                        validator: i
                    });
                }
            }
            //distinct duplicate element
            var flag = [], tempSummary = errSummary;
            errSummary = [];
            for (var i = 0; i < tempSummary.length; i++)
            {
                if (flag[tempSummary[i].name]) continue;
                flag[tempSummary[i].name] = true;
                errSummary.push(tempSummary[i]);
            }

            if (errSummary.length > 0)
            {
                for (var i in vn.errMsgColle)
                {
                    for (var j in errSummary)
                    {
                        if (vn.errMsgColle[i].ErrorType == errSummary[j].validator)
                        {
                            var name = errSummary[j].name,
                                validator = errSummary[j].validator;
                            errMsgSummary.push({
                                name: name,
                                validator: validator,
                                message: String.format(vn.errMsgColle[i].ErrorMessage, name, validator, formName[name].attributes[validator])
                            });
                        }
                    }
                }
            }
        }
        return errMsgSummary;
    };

    function validateElem(elemID)
    {
        var vn = this;
        if (!vn.errMsgColle)
        {
            vn.errMsgColle = VariableService.getErrorMessageCollection();
        }
        var error = [];
        if (elemID)
        {
            var elemName = elemID.$name,
                elemError = elemID.$error,
                elemAttri = elemID.attributes;

            for (var i = 0 ; i < vn.errMsgColle.length; i++)
            {
                if (elemError[vn.errMsgColle[i].ErrorType])
                {
                    error.push({
                        name: elemName,
                        validator: elemError,
                        message: String.format(vn.errMsgColle[i].ErrorMessage, elemName, vn.errMsgColle[i].ErrorType, elemAttri[vn.errMsgColle[i].ErrorType])
                    });
                }
            }
            return error;
        }
    }
}