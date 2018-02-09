using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Utility
{
    [Serializable]
    public class FunctionResult : ActionResult //actionresult
    {
        private bool _result = true;
        private List<string> _errorList = new List<string>();
        private string _returnValue = string.Empty;
        private string _referValue = string.Empty;
        private string _message = string.Empty;
        private int _referID;
        private List<object> _referList;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public List<string> ErrorList
        {
            get { return _errorList; }
            set { _errorList = value; }
        }

        public string ReturnValue
        {
            get { return _returnValue; }
            set { _returnValue = value; }
        }

        public string ReferValue
        {
            get { return _referValue; }
            set { _referValue = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public int ReferID
        {
            get { return _referID; }
            set { _referID = value; }
        }

        public List<object> ReferList
        {
            get
            {
                if (_referList == null)
                    _referList = new List<object>();
                return _referList;
            }
            set
            {
                _referList = value;
            }
        }

        

        public override void ExecuteResult(ControllerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
