using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BzModelClass;
using KoyoSMS.WCF.Common;
using DBModelClass.DBModel;
using Kendo.Mvc.UI;
using System.Net.Http;
using System.Web.Http.Validation;
using System.Web.Http.Controllers;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using Utility.Models;
using Utility.EnumData;
using System.Globalization;
using System.Threading;
using System.Net;
using Microsoft.Owin.Security.OAuth;
using System.Web.Security;

namespace WebSolution.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        DBPOCollection poCollection = new DBPOCollection();

        public static CancellationTokenSource cts;
        private string _taskID;
        public string TaskID {
            get
            {
                if (String.IsNullOrEmpty(_taskID))
                    _taskID = Guid.NewGuid().ToString();
                return _taskID;
            }
            set {
                _taskID = value;
            }
        }

        [HttpGet]
        public async Task<ActionResult> POProfile()
        {
            DBModelClass.DBModel.POEntityViewModel listModel = new DBModelClass.DBModel.POEntityViewModel();
            await Task.Run(async () =>
            {
                listModel.MasterList = await getMasterItemDDLFor();
                listModel.MasterID = Int32.Parse(listModel.MasterList.FirstOrDefault().Value);
            });
            

            return View(listModel);
        }
        [HttpPost]
        public ActionResult POProfile(POEntityViewModel model)
        {
            try
            {
                ModelState.Clear();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }

            return View();
        }

        public async Task<ActionResult> valid(POEntityViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var data = new
                    {
                        success = false,
                        errors = ModelState.Keys.SelectMany(x => ModelState[x].Errors)
                        .Select(y => y.ErrorMessage).ToArray()
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
            return View();
        }

        public ActionResult tryDynamicLoad()
        {
            return View();
        }
        public ActionResult TreeView()
        {
            return View();
        }

        public ActionResult iFrame()
        {
            return View();
        }


        public ActionResult leftMenu()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult getTreeViewSource()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            DBTreeDataProfile treedata = treeProfile.GetTreeViewItems();

            return Json(treedata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFormLoadData()
        {
            DBPOProfile poProfile = new DBPOProfile();
            poCollection.MasterItems = poProfile.GetMasterItems();
            poCollection.ProjectItems = poProfile.GetProjectItems();
            poCollection.PONo = poProfile.GetPONoItems();

            var jsonData = new
            {
                MasterItems = poCollection.MasterItems,
                ProjectItems = poCollection.ProjectItems,
                PONoitems = poCollection.PONo
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #region cascade item here     
        [HttpPost]
        public JsonResult GetMasterItemsList()
        {
            DBPOProfile poProfile = new DBPOProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            var MasterItems = poProfile.GetMasterDDLItem(queryBuilder);
            List<SelectListItem> item = (from a in MasterItems
                                         select new SelectListItem()
                                         {
                                             Text = a.OrgSN,
                                             Value = a.MasterID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0", Selected = true });
            var data = new
            {
                data = item
            };
            return Json(item);
        }
        [HttpPost]
        public JsonResult GetProjectItemsList(int MasterID)
        {
            //string strMasterID;// = collection["MasterID"];//== "" ? collection["MasterID"] : "0";
            //if (collection["MasterID"] == "")
            //    strMasterID = "0";
            //else strMasterID = collection["MasterID"];

            //int MasterID = Convert.ToInt32(strMasterID);
            int masterID = MasterID > 0 ? MasterID : 0;
            DBPOProfile poProfile = new DBPOProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            queryBuilder.QueryParameters.Add(new SQLQueryParameter("MasterID", masterID));
            var ProjectID = poProfile.GetProjectDDLItem(queryBuilder);
            List<SelectListItem> item = (from a in ProjectID
                                         select new SelectListItem()
                                         {
                                             Text = a.ProjectName,
                                             Value = a.ProjectID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0", Selected = true });

            return Json(item);
        }
        [HttpPost]
        public JsonResult GetSupplierItemsList(int ProjectID)
        {
            string strProjectID;//= string.IsNullOrEmpty(collection["ProjectID"]) ? collection["ProjectID"] : "0";
            //if (collection["ProjectID"] == "")
            //    strProjectID = "0";
            //else strProjectID = collection["ProjectID"];

            //int projectID = Convert.ToInt32(strProjectID);
            int projectID = ProjectID > 0 ? ProjectID : 0;
            DBPOProfile poProfile = new DBPOProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            queryBuilder.QueryParameters.Add(new SQLQueryParameter("ProjectID", projectID));
            var SupplierID = poProfile.GetSupplierDDLItem(queryBuilder);
            List<SelectListItem> item = (from a in SupplierID
                                         select new SelectListItem()
                                         {
                                             Text = a.SupplierOrgSN,
                                             Value = a.SupplierID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0" });
            return Json(item);
        }

        [HttpPost]
        public JsonResult GetSupplieItemsListNoLimits(FormCollection collection)
        {
            DBPOProfile poProfile = new DBPOProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            var ProjectItems = poProfile.GetMasterDDLItem(queryBuilder);
            List<SelectListItem> item = (from a in ProjectItems
                                         select new SelectListItem()
                                         {
                                             Text = a.OrgSN,
                                             Value = a.MasterID.ToString()
                                         }).Distinct().ToList();
            item.Insert(0, new SelectListItem() { Text = "Select All", Value = "0", Selected = true });
            return Json(item);
        }
        #endregion

        [HttpPost]
        public JsonResult GetPOViewItems(int MasterID, int ProjectID, int SupplierID, string PONo, DataSourceRequest request)
        {
            int masterID = MasterID > 0 ? MasterID : 0;
            int projectID = ProjectID > 0 ? ProjectID : 0;
            string pono = !string.IsNullOrEmpty(PONo) ? PONo : "";
            int page = request.Page;
            int pageSize = request.PageSize;


            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            KoyoSMS.WCF.Common.Model.PageBase pageCondition = new KoyoSMS.WCF.Common.Model.PageBase();
            DBPOProfile poProfile = new DBPOProfile();

            if (masterID > 0)
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("MasterID", masterID));
            if (projectID > 0)
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("ProjectID", projectID));
            if (!string.IsNullOrEmpty(pono))
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("PONo", pono));

            pageCondition.PageIndex = page - 1;
            pageCondition.PageSize = pageSize > 0 ? pageSize : 5;

            poCollection.POEntityViewItems = poProfile.GetPOViews(queryBuilder, pageCondition);

            int total = pageCondition.TotalRecord;

            var jsonData = new
            {
                total = total,
                data = poCollection.POEntityViewItems
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoPOProfileAction(POEntity item)
        {
            DateTime today = DateTime.Now;

            POEntity currentItem = UtilityModelClass.Models.Utility.CopyShadow<POEntity>(item);
            currentItem.CreateDate = (item.CreateDate);
            currentItem.UpdateDate = today;
            currentItem.UserID = 1;

            DBPOProfile poProfile = new DBPOProfile();
            if (currentItem.POID > 0)
                poProfile.DoPOProfileAction(enumActionItem.Edit, currentItem);
            else
                poProfile.DoPOProfileAction(enumActionItem.Insert, currentItem);

            var jsonData = item;

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPODetailItem(int POID, DataSourceRequest request)
        {
            KoyoSMS.WCF.Common.Model.PageBase pageCondition = new KoyoSMS.WCF.Common.Model.PageBase();
            int page = request.Page;
            int pageSize = request.PageSize;
            int iPOID = POID > 0 ? POID : 0;
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            DBPOProfile poProfile = new DBPOProfile();
            if (iPOID > 0)
            {
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("POID", iPOID));
            }


            pageCondition.PageIndex = page - 1;
            pageCondition.PageSize = pageSize > 0 ? pageSize : 5;

            poCollection.PODetailViewItems = poProfile.GetPODetailViews(queryBuilder, pageCondition);

            int total = pageCondition.TotalRecord;
            

            var jsonData = new
            {
                total = total,
                data = poCollection.PODetailViewItems
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> getCompanyProfileData(int masterID)
        {
            
            cts = new CancellationTokenSource();
            //CancellationToken token = cts.Token;
            CancellationToken token = Response.ClientDisconnectedToken;
            await Task.Delay(5000);
            return await Task.Run(async () =>
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }
                else
                {
                    return await getAsyncCompanyProfileData(masterID, token);
                }
            }, token);

        }
        protected async Task<JsonResult> getAsyncCompanyProfileData(int masterID, CancellationToken ct)
        {
            Task.Delay(5000);
            int MasterID = masterID;
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            if (MasterID > 0)
            {
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("MasterID", MasterID));
            }
            var data = treeProfile.getMasterList(queryBuilder);
            if (ct.IsCancellationRequested)
            {
                ct.ThrowIfCancellationRequested();
            }

            return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public async Task<JsonResult> getDepartProfileData(int departID)
        {
            CancellationToken token = Response.ClientDisconnectedToken;

            Thread.Sleep(3500);
            return await Task.Run(async () =>
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                    throw new OperationCanceledException(token);
                }
                else
                {
                    return await getAsyncDepartProfileData(departID, token);
                }
            }, token);
            
        }
        protected async Task<JsonResult> getAsyncDepartProfileData(int departID, CancellationToken ct)
        {
            int DepartID = departID;
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            if (DepartID > 0)
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("DepartID", DepartID));
            var data = treeProfile.getDepartList(queryBuilder);
            if (ct.IsCancellationRequested)
            {
                ct.ThrowIfCancellationRequested();
            }
            return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public async Task<JsonResult> getUserProfileData(int userID)
        {
            CancellationToken token = Response.ClientDisconnectedToken;

            await Task.Delay(3400);
            return await Task.Run(async () =>
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }
                else
                {
                    return await getAsyncUserProfileData(userID, token);
                }
            }, token);
        }
        protected async Task<JsonResult> getAsyncUserProfileData(int userID, CancellationToken ct)
        {
            int UserID = userID;
            await Task.Delay(3500);
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            if (UserID > 0)
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("UserID", UserID));
            var data = treeProfile.getUserList(queryBuilder);
            if (ct.IsCancellationRequested)
            {
                ct.ThrowIfCancellationRequested();
            }
            return await Task.Run(()=> Json(data, JsonRequestBehavior.AllowGet));
        }


        [HttpPost]
        public async Task<ActionResult> asyncTVonDemand(int? nodeID, int? level, int? rootID)
        {
            CancellationToken token = Response.ClientDisconnectedToken;

            var isAuthenticater = User.Identity.IsAuthenticated;

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            
            //await Task.Delay(3000);
            return
            await Task.Run(async () =>
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException(token);
                }
                else
                {
                     return await tvonDemand(nodeID, level, rootID, token);
                }

            }, token);
            //return null;
        }

        [HttpPost]
        [AsyncTimeout(3000)]
        public async Task<ActionResult> tvonDemand(int? nodeID, int? Level, int? rootID, CancellationToken ct)
        {
            List<DBTreeDataProfile> root = new List<DBTreeDataProfile>();
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var taskID = TaskID;
            await Task.Delay(3000);
            //cts = new CancellationTokenSource();
            var rootItem = await Task.Run(() => treeProfile.getAccDepView());
            if (ct.IsCancellationRequested)
            {
                ct.ThrowIfCancellationRequested();
                throw new OperationCanceledException(cts.Token);
            }
            int pid = 0;
            int rootid = 0;
            if (nodeID == null && Level == null)
            {
                var items = rootItem.Select(x => new
                {
                    nodeID = x.MasterID,
                    text = x.OrgSN,
                    Level = 0,
                    hasChildren = true,
                    ParentName = "Company"
                }).OrderBy(q => q.text).Distinct().Select(x => new
                {
                    ID = Guid.NewGuid().ToString().Replace("-", ""),
                    x.nodeID,
                    x.text,
                    x.Level,
                    x.hasChildren,
                    x.ParentName,
                    TaskID = taskID
                }
                    ).ToList();
                var data = new { items = items };
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    throw new OperationCanceledException(cts.Token);
                }
                return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
            }
            else
            {
                if (nodeID.HasValue)
                {
                    if (Level.Value.Equals(0))
                    {
                        pid = nodeID.Value;
                        var subItems = rootItem.Where(x => x.MasterID.Equals(pid)).Select(x => new
                        {
                            nodeID = x.DepartID,
                            text = x.DepartName,
                            Level = 1,
                            hasChildren = true,
                            ParentName = "Department",
                            rootID = pid
                        }).Distinct()
                            .OrderBy(a => a.text).Select(x => new {
                                ID = Guid.NewGuid().ToString().Replace("-", ""),
                                x.nodeID,
                                x.Level,
                                x.text,
                                x.hasChildren,
                                x.rootID,
                                x.ParentName,
                                TaskID = taskID
                            }).ToList();
                        var data = new { items = subItems };
                        if (ct.IsCancellationRequested)
                        {
                            ct.ThrowIfCancellationRequested();
                            throw new OperationCanceledException(cts.Token);
                        }
                        return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
                    }
                    if (Level.Value.Equals(1))
                    {
                        pid = nodeID.Value;
                        rootid = rootID.Value;
                        var subItems = rootItem.Where(x => x.DepartID.Equals(pid) && x.MasterID.Equals(rootid)).Select(x => new
                        {
                            nodeID = x.UserID,
                            text = x.FirstName + " " + x.LastName,
                            Level = 2,
                            hasChildren = false,
                            ParentName = "User"
                        }).Distinct()
                            .OrderBy(a => a.text).Select(x => new {
                                ID = Guid.NewGuid().ToString().Replace("-", ""),
                                x.nodeID,
                                x.Level,
                                x.text,
                                x.hasChildren,
                                x.ParentName,
                                TaskID = taskID
                            }).ToList();
                        var data = new { items = subItems };
                        if (ct.IsCancellationRequested)
                        {
                            ct.ThrowIfCancellationRequested();
                            throw new OperationCanceledException(cts.Token);
                        }
                        return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
                    }
                    else return null;
                    

                }
                return null;
            }
        }

        [AllowAnonymous]
        public ActionResult ngDialog()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult CultureChange()
        {
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if(cultureCookie != null)
            {
                ViewBag.CookieCulture = cultureCookie.Value as string;
            }
            else
            {
                ViewBag.CookieCulture = "None";
            }

            ViewBag.LangSelectList = CultureHelper.GetAllCultures().ToSelectList(x => x.Value, x => x.Value) as IList<SelectListItem>;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CultureChange(CultureViewModel vm)
        {
            try
            {
                var culture = CultureHelper.GetCulture(vm.Name);
                HttpCookie cultureCookie = Request.Cookies["_culture"];
                if(cultureCookie != null)
                {
                    cultureCookie.Value = culture;
                }
                else
                {
                    cultureCookie = new HttpCookie("_culture");
                    cultureCookie.Value = culture;
                    cultureCookie.Expires = DateTime.Now.AddMonths(2);
                }
                Response.Cookies.Add(cultureCookie);
                var ci = new CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                System.Threading.Thread.CurrentThread.CurrentCulture = ci;

                return RedirectToAction("POProfile");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult _edit()
        {
            return View();
        }
        public ActionResult _read()
        {
            return View();
        }

        public async Task<ActionResult> templatePOProfile()
        {
            return PartialView();
        }

        public async Task<ActionResult> Companyread()
        {
            
            DBModelClass.DBModel.POOrgMasterViewModel listModel = new DBModelClass.DBModel.POOrgMasterViewModel();
            await Task.Run(async () =>
            {
                listModel.MasterList = await getMasterItemDDLFor();
                listModel.DepartList = await getDepartItemDDLFor();
                listModel.MasterID = Int32.Parse(listModel.MasterList.FirstOrDefault().Value);
                listModel.DepartID = Int32.Parse(listModel.DepartList.FirstOrDefault().Value);
            });

            return PartialView(listModel);
        }
        private async Task<SelectList> getMasterItemDDLFor()
        {
            var item =await Task.Run(()=>getMasterItemsOnLoad());

            return new SelectList(item, "Value", "Text");
        }
        private async Task<SelectList> getDepartItemDDLFor()
        {
            var item = await Task.Run(() => getDepartItemsOnLoad()) ;
            
            return new SelectList(item, "Value", "Text");
        }
        private async Task<List<SelectListItem>> getMasterItemsOnLoad()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var orgItems =  treeProfile.GetMasterItems();
            List<SelectListItem> list = (from a in orgItems
                                         select new SelectListItem()
                                         {
                                             Text = a.Text,
                                             Value = a.Value.ToString()
                                         }).ToList();
            return list;
        }
        private async Task<List<SelectListItem>> getDepartItemsOnLoad()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var departItems = treeProfile.GetDepartItems();
            List<SelectListItem> list = (from a in departItems
                                         select new SelectListItem()
                                         {
                                             Text = a.Text,
                                             Value = a.Value.ToString()
                                         }).ToList();
            return list;
        }
        [HttpPost]
        public async Task<JsonResult> getUserAccItems(int MasterID, int DepartID)
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            List<SelectListItem> data = new List<SelectListItem>();
            if (MasterID > 0 && DepartID >0 )
            {
                data = await Task.Run(()=> treeProfile.GetUserAccItems(MasterID,DepartID));
            }
            //System.Threading.Thread.Sleep(3000);
            return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
        }
        [HttpPost]
        public async Task<JsonResult> getUserAccDetail(int UserID)
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            SQLQueryBuilder queryBuilder = new SQLQueryBuilder();
            if (UserID > 0)
                queryBuilder.QueryParameters.Add(new SQLQueryParameter("UserID", UserID));
            var data = treeProfile.GetUserAccDetail(queryBuilder);
            
            return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public async Task<JsonResult> getOrgDetail()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var OrgItems = treeProfile.GetMasterDetail();
            //var res = await Task.Factory.StartNew<JsonResult>(() => Json(OrgItems, JsonRequestBehavior.AllowGet));
            var res = await Task.Run(() =>  Json(OrgItems, JsonRequestBehavior.AllowGet));
            return res;// Json(OrgItems, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> getDepartDetail()
        {
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();
            var departItems = treeProfile.GetDepartDetail();

            var res = await Task.Factory.StartNew<JsonResult>(() => Json(departItems, JsonRequestBehavior.AllowGet));
            return res;// Json(departItems,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getValErrMsg(string key)
        {
            int iKey = int.Parse(key);
            iKey = iKey >= 1 ? 1 : iKey;
            ErrorMessageCultureViewModel errList = new ErrorMessageCultureViewModel();
            var errListItem = errList.setErrorMessage().Where(x=>x.KeyCulture.Equals(iKey));

            return Json(errListItem, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult testCancelToken()
        {   
            return View();
        }

        public JsonResult getHttpModuleAction()
        {
            customeHttpModule.objArrayList.Add("httpModule: Event");
            var data = customeHttpModule.objArrayList;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> testSomeLongTask (string a)
        {
            CancellationToken clientToken = Response.ClientDisconnectedToken;
            
            await Task.Delay(5000);
            if (clientToken.IsCancellationRequested)
            {
                Console.WriteLine(a);
                clientToken.ThrowIfCancellationRequested();
            }
            return 
            await Task.Run(async () =>
            {
                if (clientToken.IsCancellationRequested)
                {
                    Console.WriteLine(a);
                    clientToken.ThrowIfCancellationRequested();
                    return null;
                }
                else
                {
                    await Task.Delay(3000);
                    var data = "aaaa";
                    if (clientToken.IsCancellationRequested)
                    {
                        Console.WriteLine(a);
                        clientToken.ThrowIfCancellationRequested();
                    }
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }, clientToken);
        }

        public async Task<JsonResult> LongTaskAgain (string a)
        { 
            
            CancellationToken clientToken = Response.ClientDisconnectedToken;
            await Task.Delay(5000);
            if (clientToken.IsCancellationRequested)
            {
                Console.WriteLine(a);
                clientToken.ThrowIfCancellationRequested();
            }
            return await Task.Run(async () =>
            {
                if (clientToken.IsCancellationRequested)
                {
                    clientToken.ThrowIfCancellationRequested();
                    Console.WriteLine(a);
                    return null;
                }
                else
                {
                    await Task.Delay(3000);
                    var data = "bbbbb";
                    if (clientToken.IsCancellationRequested) {
                        Console.WriteLine(a);
                        clientToken.ThrowIfCancellationRequested();
                    }
                    
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }, clientToken);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult getKeyCultureSource()
        {
            List<SelectListItem> keyCulture = new List<SelectListItem>();
            var item = CultureHelper.GetAllCultures().ToSelectList(x => x.Value, x => x.Key) as IList<SelectListItem>;
            keyCulture = (from a in item
                          select new SelectListItem()
                          {
                              Text = a.Text,
                              Value = a.Value
                          }).ToList();
            var jsonData = keyCulture;

            return Json(jsonData,JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult serachDialog()
        {
            return View();
        }
        public ActionResult test()
        {
            return PartialView();
        }
    }
}