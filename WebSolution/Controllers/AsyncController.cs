using BzModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebSolution.Controllers
{
    public class AsyncController : Controller
    {
        // GET: Async
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> TreeViewNode(int? node, int ? Level, int? rootID)
        {
            List<DBTreeDataProfile> root = new List<DBTreeDataProfile>();
            DBTreeDataProfile treeProfile = new DBTreeDataProfile();

            var rootItem = await Task.Run(()=> treeProfile.getAccDepView()).ConfigureAwait(false);
            int pid = 0,
                rootid = 0;
            if(node == null && Level == null)
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
                    x.ParentName
                }).ToList();
                var data = new { item = items };
                return await Task.Run(()=>Json(data, JsonRequestBehavior.AllowGet)).ConfigureAwait(false);
            }
            else
            {
                if (node.HasValue)
                {
                    if(Level.Value.Equals(0))
                    {
                        pid = node.Value;
                        var subItems = rootItem.Where(x => x.MasterID.Equals(pid)).Select(x => new
                        {
                            nodeID = x.DepartID,
                            text = x.DepartName,
                            Level = 1,
                            hasChildren = true,
                            ParentName = "Department",
                            rootID = pid
                        }).Distinct().OrderBy(x => x.text).Select(x => new
                        {
                            ID = Guid.NewGuid().ToString().Replace("-", ""),
                            x.nodeID,
                            x.Level,
                            x.text,
                            x.ParentName,
                            x.hasChildren,
                            x.rootID
                        }).ToList();
                        var data = new { item = subItems };
                        return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet)).ConfigureAwait(false);
                    }
                    if (Level.Value.Equals(1))
                    {
                        pid = node.Value;
                        rootid = rootID.Value;
                        var subItems = rootItem.Where(x => x.DepartID.Equals(pid) && x.MasterID.Equals(rootid)).Select(x => new
                        {
                            nodeID = x.UserID,
                            text = x.FirstName + " " + x.LastName,
                            Level = 2,
                            hasChildren = false,
                            ParentName = "User"
                        }).Distinct().OrderBy(q => q.text).Select(x => new
                        {
                            ID = Guid.NewGuid().ToString().Replace("-", ""),
                            x.nodeID,
                            x.Level,
                            x.text,
                            x.hasChildren,
                            x.ParentName
                        }).ToList();
                        var data = new { items = subItems };
                        return await Task.Run(() => Json(data, JsonRequestBehavior.AllowGet)).ConfigureAwait(false);
                    }
                    else return null;
                }
                return null;
            }
            
        }


    }
}