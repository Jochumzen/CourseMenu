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
    public partial class View : PortalModuleBase, IActionable
    {
        public string CultureCode;
        public int CoursePluggId;
        public CoursePluggEntity currentCPE;
        public int CourseId;
        public CourseContainer cc;
        public int PluggId;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BaseHandler bh = new BaseHandler();
                    int lastSubject;
                    var ss = bh.GetSubjectsAsTree("en-US",out lastSubject);
                    Subject s = ss[0];
                    while (s != null)
                        s = bh.NextSubject(s, lastSubject);


                    CultureCode = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
                    PluggId = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

                    string coursePluggIdStr = Page.Request.QueryString["cp"];
                    if (coursePluggIdStr == null)    //This is a Plugg outside a course: no menu
                        return;

                    bool isNum = int.TryParse(coursePluggIdStr, out CoursePluggId);
                    if (!isNum)
                        return;

                    currentCPE = bh.GetCPEntity(CoursePluggId);
                    if (currentCPE == null)
                        return;
                    CourseId = currentCPE.CourseId;
                    cc = new CourseContainer(CultureCode, CourseId);
                    if (cc == null)
                        return;
                    pnlTitle.Visible = true;
                    cc.LoadPluggs();
                    CoursePlugg currentCP = bh.FindCoursePlugg(cc.ThePluggs, currentCPE.CoursePluggId);



                    PopulateTreeNodes((List<CoursePlugg>)cc.ThePluggs, TreeViewMain.Nodes);
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void PopulateTreeNodes(List<CoursePlugg> cps, TreeNodeCollection RootNodes)
        {
            foreach (CoursePlugg cp in cps)
            {
                TreeNode NodeToAdd = new TreeNode();
                PluggContainer p = new PluggContainer(CultureCode, cp.PluggId);
                p.LoadTitle();
                NodeToAdd.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(p.ThePlugg.TabId, "", "c=" + cc.ThePluggs[0].PluggId);
                NodeToAdd.Text = p.TheTitle.Text;
                NodeToAdd.SelectAction = TreeNodeSelectAction.Select;
                if (cp.PluggId == PluggId)
                    NodeToAdd.Text = "<strong>" + p.TheTitle.Text + "</strong>";
                RootNodes.Add(NodeToAdd);
                if (cp.children != null)
                    PopulateTreeNodes((List<CoursePlugg>)cp.children, NodeToAdd.ChildNodes);
            }
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
    }
}