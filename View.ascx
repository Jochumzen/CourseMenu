<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.CourseMenu.View" %>

<asp:Panel ID="pnlTitle" Visible ="false" runat ="server" >
    <h2><asp:Label ID="lblTitle" runat="server" resourcekey="Title"></asp:Label>&nbsp;<asp:HyperLink ID="hlBackToCourse" runat="server" resourcekey="BackToCourse"/></h2>
    <asp:HyperLink ID="lnkNextPlugg" runat="server" CssClass="Button_default" style="float:right" resourcekey="NextPlugg"></asp:HyperLink>
</asp:Panel>

<asp:TreeView ID="TreeViewMain" ExpandDepth="3" ShowExpandCollapse="true" PopulateNodesFromClient="true" 
    EnableClientScript="false" SelectedNodeStyle-CssClass="link-1" SelectedNodeStyle-BackColor="yellow" 
    runat="server" >
</asp:TreeView>
<hr />
