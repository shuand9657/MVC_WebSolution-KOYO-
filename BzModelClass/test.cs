using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace test
{
    public class UITreeViewEntity : INotifyPropertyChanged
    {
        public UITreeViewEntity() { }
        public UITreeViewEntity(string name, string image, int ID, int objectID)
        {
            TaskName = name;
            Image = image;
            TaskID = ID;
            ObjectID = objectID;
            TaskCollection = new ObservableCollection<UITreeViewEntity>();
        }

        public UITreeViewEntity(string name, string image, int ID, int objectID, UITreeViewEntity parent)
        {
            TaskName = name;
            Image = image;
            TaskID = ID;
            ObjectID = objectID;
            Parent = parent;
            TaskCollection = new ObservableCollection<UITreeViewEntity>();
        }
        private string _taskName;
        private ObservableCollection<UITreeViewEntity> _taskCollection;

        #region Property

        public UITreeViewEntity Parent { get; set; }
        public string Image { get; set; }
        public int TaskID { get; set; }
        public int DataTypeID { get; set; }
        public string Key { get; set; }
        public int ObjectID { get; set; }
        public int TreeNodeLevel { get; set; }

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                if (_taskName != value)
                {
                    _taskName = value;
                    OnPropertyChanged("TaskName");
                }
            }
        }

        public ObservableCollection<UITreeViewEntity> TaskCollection
        {
            get { return _taskCollection; }
            set
            {
                if (value != _taskCollection)
                {
                    _taskCollection = value;
                    OnPropertyChanged("TaskCollection");
                }
            }
        }

        public bool HasChildNodes
        {
            get
            {
                if (TaskCollection == null)
                    return false;
                else if (TaskCollection.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public string GetPath()
        {
            var pathComponents = new List<string>();
            var currentItem = this;

            while (currentItem != null)
            {
                pathComponents.Add(currentItem.ToString());
                currentItem = currentItem.Parent;
            }

            return (string.Join(Path.DirectorySeparatorChar.ToString(), pathComponents.Reverse<string>().ToArray()));
        }
        #endregion

        #region Function
        public int GetParentObjectID(int taskID)
        {
            int objectID = 0;
            if (TaskID == (int)taskID)
                objectID = ObjectID;
            else
            {
                if (Parent != null)
                    objectID = Parent.GetParentObjectID(taskID);
            }
            return objectID;
        }

        private int GetParentObjectID(int taskID, UITreeViewEntity item)
        {
            int objectID = 0;
            if (item.TaskID == (int)taskID)
                objectID = item.ObjectID;
            else
            {
                if (item.Parent != null)
                    objectID = GetParentObjectID(taskID, item.Parent);
            }
            return objectID;
        }

        public UITreeViewEntity GetParentItem(int taskID)
        {
            UITreeViewEntity returnObject = null;
            if (TaskID == (int)taskID)
                returnObject = this;
            else
            {
                if (Parent != null)
                    returnObject = Parent.GetParentItem(taskID);
            }
            return returnObject;
        }

        private UITreeViewEntity GetParentItem(int taskID, UITreeViewEntity item)
        {
            UITreeViewEntity returnObject = null;
            if (item.TaskID == (int)taskID)
                returnObject = item;
            else
            {
                if (item.Parent != null)
                    returnObject = GetParentItem(taskID, item.Parent);
            }
            return returnObject;
        }

        public UITreeViewEntity GetParentItemByLevel(int levelID)
        {
            UITreeViewEntity returnObject = null;
            if (TreeNodeLevel == (int)levelID)
                returnObject = this;
            else
            {
                if (Parent != null)
                    returnObject = GetParentItemByLevel(levelID, Parent);
            }
            return returnObject;
        }

        private UITreeViewEntity GetParentItemByLevel(int levelID, UITreeViewEntity item)
        {
            UITreeViewEntity returnObject = null;
            if (item.TreeNodeLevel == (int)levelID)
                returnObject = item;
            else
            {
                if (item.Parent != null)
                    returnObject = GetParentItem(levelID, item.Parent);
            }
            return returnObject;
        }

        public UITreeViewEntity GetParentItem(int taskID, int objectID)
        {
            UITreeViewEntity returnObject = GetParentItem(taskID);
            if (returnObject == null)
                return null;
            if (returnObject.ObjectID == objectID)
                return returnObject;
            if (returnObject.Parent == null)
                return null;
            return returnObject.Parent.TaskCollection.Where(p => p.ObjectID == objectID).FirstOrDefault();
        }

        public UITreeViewEntity GetParentChildItem(int taskID, int objectID)
        {
            UITreeViewEntity returnObject = GetParentItem(taskID);
            if (returnObject == null)
                return null;
            return returnObject.TaskCollection.Where(p => p.ObjectID == objectID).FirstOrDefault();
        }

        public UITreeViewEntity GetNetParentItem(int taskID, int objectID)
        {
            UITreeViewEntity returnObject = GetParentItemByLevel(0);
            if (returnObject.TaskID == taskID && returnObject.ObjectID == objectID)
                return returnObject;
            else
            {
                foreach (var p in returnObject.TaskCollection)
                {
                    returnObject = p.FindChildNode(taskID, objectID);
                    if (returnObject != null)
                        return returnObject;
                }
            }
            return null;
        }

        public UITreeViewEntity FindChildNode(int taskID, int objectID)
        {
            if (TaskID == taskID && ObjectID == objectID)
            {
                return this;
            }
            else
            {
                foreach (var p in TaskCollection)
                {
                    if (p.TaskID == taskID && p.ObjectID == objectID)
                        return p;
                    {
                        foreach (var x in p.TaskCollection)
                        {
                            UITreeViewEntity returnObject = p.FindChildNode(taskID, objectID);
                            if (returnObject != null)
                                return returnObject;
                        }
                    }
                }
            }
            return null;
        }

        public void AddNode(UITreeViewEntity item)
        {
            List<string> paths = new List<string>(Regex.Split(@item.GetPath(), @"\\"));
            if (TreeNodeLevel > paths.Count - 1)
                return;
            string id = paths[TreeNodeLevel];
            bool found = false;
            foreach (var p in TaskCollection)
            {
                if (p.ToString() == id)
                {
                    found = true;
                    p.AddNode(item);
                    return;
                }
            }
            if (!found)
            {
                UITreeViewEntity addItem = item.GetParentItemByLevel(TreeNodeLevel + 1);
                //item.Parent = addItem ;
                TaskCollection.Add(addItem);
                this.OnPropertyChanged("HasChildNodes");
            }
        }

        public void RemoveNodeByPath(string path)
        {
            List<string> paths = new List<string>(Regex.Split(@path, @"\\"));
            if (TreeNodeLevel > paths.Count - 1)
                return;
            string id = paths[TreeNodeLevel + 1];
            foreach (var p in TaskCollection)
            {
                if (p.ToString() == id)
                {
                    if (TreeNodeLevel == paths.Count - 2)
                    {
                        TaskCollection.Remove(p);
                        this.OnPropertyChanged("HasChildNodes");
                        return;
                    }
                    else
                    {
                        p.RemoveNodeByPath(path);
                        return;
                    }
                }
            }
        }

        public UITreeViewEntity FindChildNodeByPath(string path)
        {
            List<string> paths = new List<string>(Regex.Split(@path, @"\\"));
            if (TreeNodeLevel >= paths.Count - 1)
                return null;
            string id = paths[TreeNodeLevel + 1];
            foreach (var p in TaskCollection)
            {
                if (p.ToString() == id)
                {
                    if (TreeNodeLevel == paths.Count - 2)
                    {
                        return p;
                    }
                    else
                    {
                        return p.FindChildNodeByPath(path);
                    }
                }
            }
            return null;
        }

        public void AddSelfToParent()
        {
            if (Parent != null)
            {
                if (Parent.TaskCollection == null)
                    Parent.TaskCollection = new ObservableCollection<UITreeViewEntity>();
                Parent.TaskCollection.Add(this);
                Parent.OnPropertyChanged("HasChildNodes");
            }
        }

        public void RemoveSelfFromParent()
        {
            if (Parent != null && Parent.TaskCollection != null)
            {
                Parent.TaskCollection.Remove(this);
                Parent.OnPropertyChanged("HasChildNodes");
                if (Parent.TaskCollection.Count == 0)
                    Parent.RemoveSelfFromParent();
            }
        }

        public override string ToString()
        {
            //return TaskName;
            return TaskID.ToString() + "_" + ObjectID.ToString();
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
