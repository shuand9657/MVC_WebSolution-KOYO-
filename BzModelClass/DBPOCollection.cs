using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModelClass.DBModel;
using System.Web.Mvc;

namespace BzModelClass
{
    public class DBPOCollection
    {
        private POEntity _currentpoentitiyItem;
        private List<POEntity> _poentityItems;
        private POEntity_View _currentpoentityviewItem;
        private List<POEntity_View> _poentityviewItems;
        private PODetail_View _currentpodetailviewItem;
        private List<PODetail_View> _podetailviewItems;
        private PODetail _currentpodetailItem;
        private List<PODetail> _podetailItems;
        private OrgMaster _currentmasterItem;
        private List<OrgMaster> _masterItemsList;
        private PartsMaster _currentpartsmasterItem;
        private List<PartsMaster> _partsmasterItems;
        private Account _currentaccountItem;
        private List<Account> _accountItems;
        private Department _currentdepartmentItem;
        private List<Department> _departmentItems;
        private List<SelectListItem> _partsItems;
        private List<SelectListItem> _pono;
        private List<SelectListItem> _supplierItems;
        private List<SelectListItem> _masterItems;
        private List<SelectListItem> _projectItems;

        public DBPOCollection()
        {
            _currentpoentitiyItem = new POEntity();
            _poentityItems = new List<POEntity>();
            _currentpoentityviewItem = new POEntity_View();
            _poentityviewItems = new List<POEntity_View>();
            _currentpodetailviewItem = new PODetail_View();
            _podetailviewItems = new List<PODetail_View>();
            _currentpodetailItem = new PODetail();
            _podetailItems = new List<PODetail>();
            _currentaccountItem = new Account();
            _accountItems = new List<Account>();
            _currentdepartmentItem = new Department();
            _departmentItems = new List<Department>();
            _currentpartsmasterItem = new PartsMaster();
            _partsmasterItems = new List<PartsMaster>();
            _currentmasterItem = new OrgMaster();
            _masterItemsList = new List<OrgMaster>();
        }

        public List<POEntity> POEntityItems
        {
            get
            {
                if (_poentityItems == null)
                    _poentityItems = new List<POEntity>();
                return _poentityItems;
            }
            set
            {
                if (value != _poentityItems)
                    _poentityItems = value;
            }
        }

        public List<OrgMaster> MasterListItems
        {
            get
            {
                if (_masterItemsList == null)
                    _masterItemsList = new List<OrgMaster>();
                return _masterItemsList;
            }
            set
            {
                if (value != _masterItemsList)
                    _masterItemsList = value;
            }
        }
        public List<POEntity_View> POEntityViewItems
        {
            get
            {
                if (_poentityviewItems == null)
                    _poentityviewItems = new List<POEntity_View>();
                return _poentityviewItems;
            }
            set
            {
                if (value != _poentityviewItems)
                    _poentityviewItems = value;
            }
        }
        public List<PODetail_View>PODetailViewItems
        {
            get
            {
                if (_podetailviewItems == null)
                    _podetailviewItems = new List<PODetail_View>();
                return _podetailviewItems;
            }
            set
            {
                if (value != _podetailviewItems)
                    _podetailviewItems = value;
            }
        }
        public List<PODetail> PODetailItems
        {
            get
            {
                if (_podetailItems == null)
                    _podetailItems = new List<PODetail>();
                return _podetailItems;
            }
            set
            {
                if (value != _podetailItems)
                    _podetailItems = value;
            }
        }
        public List<PartsMaster> PartsMasterItems
        {
            get
            {
                if (_partsmasterItems == null)
                    _partsmasterItems = new List<PartsMaster>();
                return _partsmasterItems;
            }
            set
            {
                if (value != _partsmasterItems)
                    _partsmasterItems = value;
            }
        }
        public List<Account> AccountItems
        {
            get
            {
                if (_accountItems == null)
                    _accountItems = new List<Account>();
                return _accountItems;
            }
            set
            {
                if (value != _accountItems)
                    _accountItems = value;
            }
        }
        public List<Department> DepartmentItems
        {
            get
            {
                if (_departmentItems == null)
                    _departmentItems = new List<Department>();
                return _departmentItems;
            }
            set
            {
                if (value != _departmentItems)
                    _departmentItems = value;
            }
        }
        public List<SelectListItem> PartsItems
        {
            get
            {
                if (_partsItems == null)
                    _partsItems = new List<SelectListItem>();
                return _partsItems;
            }
            set
            {
                if (value != _partsItems)
                    _partsItems = value;
            }
        }
        public List<SelectListItem> ProjectItems
        {
            get
            {
                if (_projectItems == null)
                    _projectItems = new List<SelectListItem>();
                return _projectItems;
            }
            set
            {
                if (value != _projectItems)
                    _projectItems = value;
            }
        }
        public List<SelectListItem> SupplierItems
        {
            get
            {
                if (_supplierItems == null)
                    _supplierItems = new List<SelectListItem>();
                return _supplierItems;
            }
            set
            {
                if (value != _supplierItems)
                    _supplierItems = value;
            }
        }
        public List<SelectListItem> MasterItems
        {
            get
            {
                if (_masterItems == null)
                    _masterItems = new List<SelectListItem>();
                return _masterItems;
            }
            set
            {
                if (value != _masterItems)
                    _masterItems = value;
            }
        }
        public List<SelectListItem> PONo
        {
            get
            {
                if (_pono == null)
                    _pono = new List<SelectListItem>();
                return _pono;
            }
            set
            {
                if (value != _pono)
                    _pono = value;
            }
        }

    }
}
