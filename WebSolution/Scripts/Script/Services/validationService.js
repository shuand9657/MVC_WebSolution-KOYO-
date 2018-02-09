
    MVCApp.factory('validationFactory', validationFactory);

    function validationFactory(ValErrMsgService)
    {
        var factory = {
            ValidateForm: ValidateForm,
            ValidateElem: ValidateElem
        };
        return factory;

        function ValidateForm(formName)
        {
            var vm = this;
            if (!vm.validMsg)
            {
                vm.validMsg = ValErrMsgService.GetValidaitonErrorMessage();
            }

            var formCollection;
            var errorCollection = [], errorSummary = [];
            if (formName)
            {
                if (formName.$error)
                {
                    formCollection = formName.$error;

                    for (var i in formCollection)
                    {
                        for (var j in formCollection[i])
                        {
                            errorSummary.push({ name: formCollection[i][j].$name, validator: i });
                        }
                    }
                }
                //distinct duplicated element
                var flag = [], tempSummary = errorSummary;
                errorSummary = [];
                for (var i = 0 ; i < tempSummary.length ; i++)
                {
                    if (flag[tempSummary[i].name]) continue;
                    flag[tempSummary[i].name] = true;
                    errorSummary.push(tempSummary[i]);
                }

                if (errorSummary.length > 0)
                {
                    for (var j in vm.validMsg)
                    {
                        for (var i in errorSummary)
                        {
                            if (vm.validMsg[j].ErrorType == errorSummary[i].validator)
                            {
                                var name = errorSummary[i].name, validator = errorSummary[i].validator;
                                errorCollection.push({ name: name, validator: validator, message: String.format(vm.validMsg[j].ErrorMessage, name, validator, formName[name].attributes[validator]) });
                            }
                        }
                    }
                }
            }
            return errorCollection;
        }

        function ValidateElem(elemID)
        {
            var vm = this;

            if (!vm.validMsg)
            {
                vm.validMsg = ValErrMsgService.GetValidaitonErrorMessage();
            }
            if (elemID)
            {
                var elemName = elemID.$name,
                elemError = elemID.$error,
                elemAttri = elemID.attributes;

                var errors = [];
                if (elemID)
                {
                    for (var i = 0 ; i < vm.validMsg.length; i++)
                    {
                        if (elemError[vm.validMsg[i].ErrorType])
                        {
                            errors.push({ name: elemName, validator: elemError, message: String.format(vm.validMsg[i].ErrorMessage, elemName, vm.validMsg[i].ErrorType, elemAttri[vm.validMsg[i].ErrorType]) });
                        }
                    }
                }
                return errors;
            }
        }
    }

