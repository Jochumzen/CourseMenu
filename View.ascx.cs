/*
' Copyright (c) 2014  Plugghest.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Web;
using System.Collections.Generic;
using Plugghest.Base2;
using DotNetNuke.Entities.Tabs;
using System.Web.Script.Serialization;
using DotNetNuke.Framework;

namespace Plugghest.Modules.CourseMenu
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CourseMenuModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CourseMenuModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (!IsPostBack)
                //    GetMenu();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public void GetMenu()
        {

            //Todo: Fix CourseMenu now that a course can have a hierarchy of items as well as headings.

            lbltest.Text = "Menu must be updated to deal with hierarchy";

            string courseIdStr = Page.Request.QueryString["c"];
            if (courseIdStr == null)    //This is a Plugg outside a course: no menu
                return;

            int courseId;
            bool isNum = int.TryParse(courseIdStr, out courseId);
            if (!isNum)
            {
                lbltest.Text = "Incorrect format for URL. Format should be http://plugghest.com/12/c/6 where the first number is the PluggID and the second number is the CourseID";
                return;
            }
            //BindTree(courseId);
            //BaseHandler bh = new BaseHandler();
            //Course c = bh.GetCourse(courseId);

            ////if course exist in the database...
            //if (c == null)
            //{
            //    lbltest.Text = "There is no course with ID " + courseId;
            //    return;
            //}

            //string pluggIdstr = DotNetNuke.Entities.Tabs.TabController.CurrentPage.Title;
            //int pluggId = Convert.ToInt32(pluggIdstr);

            //IEnumerable<CourseItem> cpExist = bh.GetCourseItems(courseId, pluggId);
            //if (!cpExist.Any())
            //{
            //    lbltest.Text = "Plugg " + pluggId + " is not in course " + courseId;
            //    return;
            //}

            //var tc = new TabController();
            //IEnumerable<CourseItem> cps = bh.GetItemsInCourse(courseId);
            //foreach (CourseItem cp in cps)
            //{
            //    Plugg p = bh.GetPlugg(cp.ItemId);
            //    TabInfo ti = tc.GetTabByName(p.PluggId.ToString() + ": " + p.Title , PortalId);
            //    string myUrl = DotNetNuke.Common.Globals.NavigateURL(ti.TabID, "", "", "&c=" + courseId);
            //    Menu_Pluggs.Items.Add(new MenuItem(p.PluggId.ToString() + ": " + p.Title, "", "", myUrl));

            //    if (pluggId == p.PluggId)
            //    {
            //        int index = cp.CIOrder - 1;
            //        Menu_Pluggs.Items[index].Selected = true; //active order in menu
            //    }
            //}

        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }



        //public void BindTree(int courseId)
        //{
        //    BaseHandler bh = new BaseHandler();
        //    List<CoursePlugg> tree = (List<CoursePlugg>)bh.GetCourseItemsAsTree(courseId);
        //    PopulateTreeNodes(tree, TreeViewMain.Nodes);
        //}

        //private void PopulateTreeNodes(List<CoursePlugg> LstCourseItem, TreeNodeCollection RootNodes)
        //{
        //    foreach (CoursePlugg ObjCourseItem in LstCourseItem)
        //    {
        //        TreeNode NodeToAdd = new TreeNode();
        //        if (ObjCourseItem.ItemType.ToString() == ECourseItemType.Plugg.ToString())
        //        {
        //            BaseHandler plugghandler = new BaseHandler();
        //            PluggContainer p = new PluggContainer();
        //            p.ThePlugg = plugghandler.GetPlugg(ObjCourseItem.ItemId);
        //            string curlan = (Page as PageBase).PageCulture.Name;
        //            p.CultureCode = curlan;
        //            p.LoadTitle();
        //            NodeToAdd.Text = "<a  style='text-decoration: underline;cursor: pointer; ' href='/" + ObjCourseItem.ItemId + "' >" + p.TheTitle.Text + "</a>";
        //        }
        //        else
        //        {
        //            NodeToAdd.Text = Convert.ToString(ObjCourseItem.label);
        //        }
        //        NodeToAdd.SelectAction = TreeNodeSelectAction.None;
        //        RootNodes.Add(NodeToAdd);
        //        if (ObjCourseItem.children != null)
        //            PopulateTreeNodes((List<CourseItem>)ObjCourseItem.children, NodeToAdd.ChildNodes);
        //    }
        //}
    }
}