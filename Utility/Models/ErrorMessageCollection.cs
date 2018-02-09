using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Utility.Models
{
    public class ErrorMessageCollection
    {
        public List<ErrorMessageCultureViewModel> setErrorMessage()
        {
            List<ErrorMessageCultureViewModel> errList = new List<ErrorMessageCultureViewModel>();
            ErrorMessageCultureViewModel requiredErrMsg = new ErrorMessageCultureViewModel() { KeyCulture = 0 , ErrorMessage="{0} field is {1}",ErrorType="required"};
            ErrorMessageCultureViewModel minlenghtErrMsg = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required minmum length : {2}", ErrorType = "minlength" };
            ErrorMessageCultureViewModel maxlengthErrMsg = new ErrorMessageCultureViewModel() {KeyCulture = 0 , ErrorMessage="{0} field has {1} error, required maximum length : {2}", ErrorType="maxlength" };
            ErrorMessageCultureViewModel minErrMsg= new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required minimum value : {2}", ErrorType = "min" };
            ErrorMessageCultureViewModel maxErrMsg = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required max value : {2}", ErrorType = "max" };
            ErrorMessageCultureViewModel emailErrMsg = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field required {1} format", ErrorType = "email" };
            ErrorMessageCultureViewModel regexErrMsg = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} patten error!", ErrorType = "pattern" };

            errList.Add(requiredErrMsg);
            errList.Add(minErrMsg);
            errList.Add(maxErrMsg);
            errList.Add(minlenghtErrMsg);
            errList.Add(maxlengthErrMsg);
            errList.Add(emailErrMsg);
            errList.Add(regexErrMsg);

            return errList;
        }
        
    }
}
