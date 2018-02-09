using DBModelClass.DBModel;
using KoyoSMS.WCF.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BZModelClass
{
    public  class DatabaseClass<T> where T : class
    {
        public EFDBModelEntities ef = new EFDBModelEntities();


        public void DoDBAction(T item, enumActionItem action, params object[] keyValues)
        {

            var entry = ef.Entry<T>(item);
            IDbSet<T> DbSet;
            DbSet = ef.Set<T>();
            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                if (keyValues.Count() > 0)
                {
                    T attachedEntity = DbSet.Find(keyValues);

                    if (attachedEntity != null)
                    {
                        var attachedEntry = ef.Entry(attachedEntity);
                        switch (action)
                        {
                            case enumActionItem.Delete:
                                attachedEntry.State = EntityState.Deleted;
                                break;
                            case enumActionItem.Edit:
                                attachedEntry.CurrentValues.SetValues(item);
                                break;
                        }

                    }
                    else
                    {
                        switch (action)
                        {
                            case enumActionItem.Insert :
                                entry.State = EntityState.Added;
                                break;
                            case enumActionItem.Delete:
                                entry.State = EntityState.Deleted;
                                break;
                            case enumActionItem.Edit:
                                entry.State = EntityState.Modified;
                                break;
                        }

                    }
                }
                else
                {
                    switch (action)
                    {
                        case enumActionItem.Insert:
                            entry.State = EntityState.Added;
                            break;
                        case enumActionItem.Delete:
                            entry.State = EntityState.Deleted;
                            break;
                        case enumActionItem.Edit:
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
            }
            ef.SaveChanges();

        }
    }
}