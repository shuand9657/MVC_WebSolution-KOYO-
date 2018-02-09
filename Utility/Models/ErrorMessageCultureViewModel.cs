using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.EnumData;

namespace Utility.Models
{
    public class ErrorMessageCultureViewModel
    {
        #region property
        public int KeyCulture { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
        public List<ErrorMessageCultureViewModel> ErrorList { get; set; }
        #endregion
        public ErrorMessageCultureViewModel() { }
        
        public List<ErrorMessageCultureViewModel> _errCulture = new List<ErrorMessageCultureViewModel>();

        public ErrorMessageCultureViewModel(int key,string errMsg, string errType)
        {
            KeyCulture = key;
            ErrorMessage = errMsg;
            ErrorType = errType;
        }

        public List<ErrorMessageCultureViewModel> setErrorMessage()
        {
            List<ErrorMessageCultureViewModel> errList = new List<ErrorMessageCultureViewModel>();
            //key culture 0 = en-en
            ErrorMessageCultureViewModel requiredErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field is {1}", ErrorType = "required" };
            ErrorMessageCultureViewModel minlenghtErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required minmum length : {2}", ErrorType = "minlength" };
            ErrorMessageCultureViewModel maxlengthErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required maximum length : {2}", ErrorType = "maxlength" };
            ErrorMessageCultureViewModel minErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required minimum value : {2}", ErrorType = "min" };
            ErrorMessageCultureViewModel maxErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field has {1} error, required max value : {2}", ErrorType = "max" };
            ErrorMessageCultureViewModel emailErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field required {1} format", ErrorType = "email" };
            ErrorMessageCultureViewModel regexErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} patten error", ErrorType = "pattern" };
            ErrorMessageCultureViewModel dateErrMsgEn = new ErrorMessageCultureViewModel() { KeyCulture = 0, ErrorMessage = "{0} field required proper {1} format", ErrorType = "date" };

            //key culture 1 = zh-tw
            ErrorMessageCultureViewModel requiredErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位不能為空", ErrorType = "required" };
            ErrorMessageCultureViewModel minlenghtErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位最短長度 : {2}", ErrorType = "minlength" };
            ErrorMessageCultureViewModel maxlengthErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位最長長度 : {2}", ErrorType = "maxlength" };
            ErrorMessageCultureViewModel minErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位最小值為 : {2}", ErrorType = "min" };
            ErrorMessageCultureViewModel maxErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位最大值為 : {2}", ErrorType = "max" };
            ErrorMessageCultureViewModel emailErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位需要正確的電郵格式", ErrorType = "email" };
            ErrorMessageCultureViewModel regexErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位格式不符", ErrorType = "pattern" };
            ErrorMessageCultureViewModel dateErrMsgZh = new ErrorMessageCultureViewModel() { KeyCulture = 1, ErrorMessage = "{0} 欄位需要正確的日期格式", ErrorType = "date" };

            errList.Add(requiredErrMsgEn);
            errList.Add(minErrMsgEn);
            errList.Add(maxErrMsgEn);
            errList.Add(minlenghtErrMsgEn);
            errList.Add(maxlengthErrMsgEn);
            errList.Add(emailErrMsgEn);
            errList.Add(regexErrMsgEn);
            errList.Add(dateErrMsgEn);

            errList.Add(requiredErrMsgZh);
            errList.Add(minErrMsgZh);
            errList.Add(maxErrMsgZh);
            errList.Add(minlenghtErrMsgZh);
            errList.Add(maxlengthErrMsgZh);
            errList.Add(emailErrMsgZh);
            errList.Add(regexErrMsgZh);
            errList.Add(dateErrMsgZh);

            return errList;
        }
    }
}
