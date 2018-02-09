using DBModelClass.DBModel;
using KoyoSMS.WCF.Common;
using KoyoSMS.WCF.Common.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BzModelClass
{
    public class DBTreeDataProfile 
    {
        #region property
        EFDBModelEntities db = new EFDBModelEntities();
        public string Name { get; set; }
        public int ID { get; set; }   
        public string ParentName { get; set; }  
        public string icon { get; set; }  
        public string TaskID { get; set; }    
        public DBTreeDataProfile Parent { get; set; }
        public List<DBTreeDataProfile> Items { get; set; }
        public bool HasChildren { get; set; }
        public bool expanded { get; set; }
        public List<DBtreeSubItems> subItems { get; set; }
        #endregion

        public DBTreeDataProfile() { }

        public List<DBTreeDataProfile> _treeProfile = new List<DBTreeDataProfile>();
         
        public DBTreeDataProfile(string name,int id) {
            Name = name;
            ID = id;
            
        }
        public DBTreeDataProfile(string name, int id, DBTreeDataProfile parent)
        {
            Name = name;
            ID = id;
            Parent = parent;                        
        }
        public class DBtreeSubItems
        {
            public int ParentID { get; set; }
            public string Name { get; set; }
            public int ID { get; set; }
        }

        public DBTreeDataProfile GetTreeViewItems()
        {
            List<DBTreeDataProfile> items = new List<DBTreeDataProfile>();
            DBTreeDataProfile treeprofile = new DBTreeDataProfile();
            DBTreeDataProfile PONode = new DBTreeDataProfile();
            var AccDepItems = treeprofile.getAccDepView();
            var level1 = AccDepItems.Select(x => new { x.OrgSN, x.MasterID }).Distinct();
            
            DBTreeDataProfile rootNode = new DBTreeDataProfile();
            PONode.Name = "POProfile";
            PONode.ID = 0;
            PONode.HasChildren = false;
            PONode.ParentName = "POProfile";
            PONode.Items = new List<DBTreeDataProfile>();

            rootNode.Name = "Root";
            rootNode.ID = 0;
            rootNode.HasChildren = true;
            rootNode.expanded = true;
            rootNode.icon = "root";
            rootNode.Items = new List<DBTreeDataProfile>();
            foreach (var p in level1)
            {
                var level2 = AccDepItems.Where(x=>x.MasterID ==p.MasterID ).Select(x => new { x.DepartID, x.DepartName}).Distinct();
                DBTreeDataProfile branchNode = new DBTreeDataProfile();
                branchNode.Name = p.OrgSN;
                branchNode.ID = p.MasterID;
                branchNode.ParentName = "Company";
                branchNode.icon = "root";
                branchNode.HasChildren = true;
                branchNode.Items = new List<DBTreeDataProfile>();

                
                

                foreach (var q in level2)
                {
                    var level3 = AccDepItems.Where(x => x.MasterID == p.MasterID && x.DepartID == q.DepartID).Select(x => new { x.UserID, x.Username }).Distinct();
                    DBTreeDataProfile leafNode = new DBTreeDataProfile();
                    leafNode.Name = q.DepartName;
                    leafNode.ID = q.DepartID;
                    leafNode.ParentName = "Department";
                    leafNode.icon = "depart";
                    leafNode.expanded = false;
                    leafNode.HasChildren = true;
                    leafNode.Items = new List<DBTreeDataProfile>();
                    
                    foreach (var w in level3)
                    {
                        DBTreeDataProfile endNode = new DBTreeDataProfile();
                        endNode.Name = w.Username;
                        endNode.ID = w.UserID;
                        endNode.ParentName = "User";
                        endNode.icon = "user";
                        endNode.HasChildren = false;                 
                        endNode.Items = new List<DBTreeDataProfile>();
                        leafNode.Items.Add(endNode);
                                                            
                    }
                    branchNode.Items.Add(leafNode);
                }
                rootNode.Items.Add(branchNode);
               

            }
            DBTreeDataProfile FinalRootNode = new DBTreeDataProfile();
            FinalRootNode.Name = "RootRoot";
            FinalRootNode.ID = 0;
            FinalRootNode.HasChildren = true;
            FinalRootNode.expanded = true;
            FinalRootNode.icon = "root";
            FinalRootNode.Items = new List<DBTreeDataProfile>();
            FinalRootNode.Items.Add(rootNode);              
            return rootNode;
        }

        public List<SelectListItem> GetUserAccItems(int MasterID, int DepartID)
        {
            List<SelectListItem> items = (from a in db.UserDepartment_View
                                          orderby a.FirstName
                                          where a.MasterID.Equals(MasterID) && a.DepartID.Equals(DepartID)
                                          select new SelectListItem()
                                          {
                                              Text = a.FirstName + " " + a.LastName,
                                              Value = a.UserID.ToString()
                                          }).Distinct().ToList();
            return items;
        }

        public List<Users> GetUserAccDetail(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<Users> query = from a in db.Users
                                      orderby a.FirstName
                                      select a;
            //System.Threading.Thread.Sleep(3000);
            return queryBuilder.BindQueryToContext(ref query,ref pageCondition);
        }
                    

        public List<UserDepartment_View> getAccDepView()
        {
            var query = (from a in db.UserDepartment_View
                         orderby a.OrgSN
                         select a).ToList();
            
            return query;
        }

        public List<OrgMaster> getMasterList(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<OrgMaster> query = (from a in db.OrgMaster
                                           select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);

        }

        public List<Department> getDepartList(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<Department> query = (from a in db.Department
                                            select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }

        public List<UserDepartment_View> getUserList(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<UserDepartment_View> query = (from a in db.UserDepartment_View
                                                     select a);
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }                                                                        
        
        public List<Department> getDepartmentList()
        {
            var query = (from a in db.Department
                         orderby a.DepartName
                         select a);
            query.Count();
            return query.ToList();
        }

        public List<Department> GetDepartmentItems(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();
            IQueryable<Department> query = (from a in db.Department
                                            orderby a.DepartSN
                                            select a
                                            );

            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }

        public List<Account> getAccountList()
        {
            var query = (from a in db.Account
                         orderby a.UserID
                         select a);
            query.Count();
            return query.ToList();
        }        
        public List<Account> GetAccountItems(SQLQueryBuilder queryBuilder)
        {
            PageBase pageCondition = new PageBase();

            IQueryable<Account> query = from a in db.Account
                                        orderby a.UserID
                                        select a;
            query.Count();
            return queryBuilder.BindQueryToContext(ref query, ref pageCondition);
        }      
        private List<OrgMaster> getMasterList()
        {
            var query = (from a in db.OrgMaster
                         orderby a.OrgSN
                         select a).ToList();
            return query;
        }
        public List<SelectListItem> GetMasterItems()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var MasterItems = treeProfile.getMasterList();
            List<SelectListItem> items = (from a in MasterItems
                                          orderby a.OrgSN
                                          select new SelectListItem()
                                          {
                                              Text = a.OrgSN,
                                              Value = a.MasterID.ToString()
                                          }).Distinct().ToList();
            
            //System.Threading.Thread.Sleep(3000);
            return items;
        }
        public List<SelectListItem> GetDepartItems()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var DepartItems = treeProfile.getDepartmentList();
            List<SelectListItem> items = (from a in DepartItems
                                          orderby a.DepartSN
                                          select new SelectListItem()
                                          {
                                              Text = a.DepartName,
                                              Value = a.DepartID.ToString()
                                          }).Distinct().ToList();
           
            return items;
        }

        public List<OrgMaster> GetMasterDetail ()
        {

            var query = from a in db.OrgMaster
                        orderby a.MasterID
                        select a;
            return query.ToList();
        }
        public List<Department> GetDepartDetail ()
        {

            var query = from a in db.Department
                        orderby a.DepartID
                        select a;

            return query.ToList();
        }
       
    }
}
