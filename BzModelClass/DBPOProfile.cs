using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBModelClass.DBModel;
using KoyoSMS.WCF.Common;
using KoyoSMS.WCF.Common.Model;
using System.Web.Mvc;
using BZModelClass;

namespace BzModelClass
{   
    public class DBPOProfile
    {
        EFDBModelEntities db = new EFDBModelEntities();

        public List<POEntity_View> GetPOs (SQLQueryBuilder queryBuilder, PageBase pageCondition)
        {
            IQueryable<POEntity_View> query = (from a in db.POEntity_View
                                          orderby a.POID
                                          select a);

            return queryBuilder.BindQueryToContext<POEntity_View>(ref query, ref pageCondition);
        }

        public List<POEntity_View> GetPOViews (SQLQueryBuilder queryBuiler, PageBase pagecondition)
        {
            IQueryable<POEntity_View> query = (from a in db.POEntity_View
                                               orderby a.POID
                                               select a);
            return queryBuiler.BindQueryToContext<POEntity_View>(ref query, ref pagecondition);
        }

        public List<PODetail_View> GetPODetailViews(SQLQueryBuilder queryBuilder,PageBase pageCondition)
        {
            IQueryable<PODetail_View> query = (from a in db.PODetail_View
                                               orderby a.PODetailID
                                               select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }

        public POEntity GetPOEntityItems(int poid)
        {
            var query = (from a in db.POEntity
                         where a.POID == poid
                         select a).FirstOrDefault();
            return query;
        }
        
        private List<OrgMaster> GetMasterName()
        {
            var query = (from a in db.OrgMaster
                         orderby a.MasterID
                         select a).ToList();
            return query;
        }               
        public List<SelectListItem> GetMasterItems()
        {
            DBPOProfile poProfile = new DBPOProfile();
            var MasterName = poProfile.GetMasterName();
            List<SelectListItem> item = (from a in MasterName
                                         orderby a.MasterID
                                         select new SelectListItem()
                                         {
                                             Text = a.OrgSN,
                                             Value = a.MasterID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0" });
            return item;
        }
        private List<ProjectOrg_View> GetProjectNames()
        {
            var query = (from a in db.ProjectOrg_View
                         orderby a.ProjectID
                         select a).ToList();
            return query;
        }
        public List<SelectListItem> GetProjectItems()
        {
            DBPOProfile poProfile = new DBPOProfile();
            var ProjectName = poProfile.GetProjectNames();
            List<SelectListItem> item = (from a in ProjectName
                                         orderby a.ProjectID
                                         select new SelectListItem()
                                         {
                                             Text = a.ProjectName,
                                             Value = a.ProjectID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0" });
            return item;
        }
        private List<POEntity> GetPONo()
        {
            var query = (from a in db.POEntity
                         orderby a.POID
                         select a).ToList();
            return query;
        }
        public List<SelectListItem> GetPONoItems()
        {
            DBPOProfile poProfile = new DBPOProfile();
            var PONoItems = poProfile.GetPONo();
            List<SelectListItem> item = (from a in PONoItems
                                         orderby a.POID
                                         select new SelectListItem()
                                         {
                                             Text = a.PONo,
                                             Value = a.PONo
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0" });
            return item;
        }

        private List<PartsMaster> GetPartsMaster()
        {
            var query = (from a in db.PartsMaster
                         orderby a.PartsName
                         select a).ToList();
            return query;
        }
        public List<SelectListItem> GetPartsMasterItems()
        {
            DBPOProfile poProfile = new DBPOProfile();
            var PartsItems = poProfile.GetPartsMaster();
            List<SelectListItem> item = (from a in PartsItems
                                         orderby a.PartsName
                                         select new SelectListItem()
                                         {
                                             Text = a.PartsName,
                                             Value = a.PartsID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select An Item", Value = "0" });
            return item;
        }


        public void DoPOProfileAction(enumActionItem action, POEntity item)
        {
            DatabaseClass<POEntity> dbc = new DatabaseClass<POEntity>();
            switch(action)
            {
                case enumActionItem.Edit:
                    dbc.DoDBAction(item, enumActionItem.Edit);
                    break;
                case enumActionItem.Insert:
                    dbc.DoDBAction(item, enumActionItem.Insert);
                    break;
                case enumActionItem.Delete:
                    dbc.DoDBAction(item, enumActionItem.Delete, item.POID);
                    break;
            }
        }


        #region cascade items here
        public List<OrgMaster> GetMasterDDLItem(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<OrgMaster> query = (from a in db.OrgMaster
                                           orderby a.OrgSN
                                           select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }
        public List<ProjectOrg_View> GetProjectDDLItem(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondtion = new PageBase();
            IQueryable<ProjectOrg_View> query = (from a in db.ProjectOrg_View
                                         orderby a.ProjectName
                                         select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondtion);
        }
        public List<POEntity_View> GetSupplierDDLItem(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<POEntity_View> query = (from a in db.POEntity_View
                                               orderby a.POID
                                               select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }

        #endregion
    }
}
