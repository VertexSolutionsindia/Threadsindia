﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer_po.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="en">
    <head id="Head1" runat="server">
         <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
        <title></title>
              
          <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

      <script type = "text/javascript">
          function Confirm() {
              var confirm_value = document.createElement("INPUT");
              confirm_value.type = "hidden";
              confirm_value.name = "confirm_value";
              if (confirm("Do you want to add new product?")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
          }
    </script>        


  
   
<style>
.ajax__combobox_itemlist
{
position:absolute!important; 

top: auto !important;
left: auto !important;
}

.cbox
{
    
  
   
   font-size:15px;
}
</style>
  <style>
                 .tablestyles table tr td
                 {
                   height:30px;
                 }
                 </style>
                 <style>
.tablestyle table
{
    text-align:center;
}
.tablestyle table  th
{
    padding:8px;
    font-size:15px;
   
}

</style>
        <!-- Bootstrap -->
          <script src="bootstrap/js/jquery-3.1.1.min.js"></script>

          <script src="bootstrap/js/bootstrap-select.js"></script>
           <link href="bootstrap/css/bootstrap-select.css" rel="stylesheet" />
           <link rel="stylesheet" type="text/css" media="screen" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.7.5/css/bootstrap-select.min.css">
         <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
        <link href="css/waves.min.css" type="text/css" rel="stylesheet">
        <!--        <link rel="stylesheet" href="css/nanoscroller.css">-->
        <link href="css/menu.css" type="text/css" rel="stylesheet">
        <link href="css/style.css" type="text/css" rel="stylesheet">
         <link href="css1/Product_entrycss.css" type="text/css" rel="stylesheet">
        <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
        <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->

        <style>
        .item
        {
            text-align:center;
            padding:5px;
        }
        
        
        </style>
    </head>
    <body>
        <!-- Static navbar -->
 <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>

    <div>
         <nav class="navbar navbar-inverse yamm navbar-fixed-top">
            <div class="container-fluid">
                <button type="button" class="navbar-minimalize minimalize-styl-2  pull-left "><i class="fa fa-bars"></i></button>
                <span class="search-icon"><i class="fa fa-search"></i></span>
                <div class="search" style="display: none;">
                    <form role="form">
                        <input type="text" class="form-control" autocomplete="off" placeholder="Write something and press enter">
                        <span class="search-close"><i class="fa fa-times"></i></span>
                    </form>
                </div>
                  <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#"><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
                   <%-- <ul class="nav navbar-nav">
                        <li class="dropdown">
                           
                          <li class="dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
<asp:Button ID="Button4" runat="server"  Text="ADD" class="btn btn-primary"></asp:Button> <span aria-hidden="true" class="glyphicon glyphicon-plus"></span> </a>
                            <ul class="dropdown-menu">
                               
                            </ul>
                        </li>
                    </ul>
                          
                    <ul class="nav navbar-nav navbar-right navbar-top-drops">
                        <li class="dropdown"><a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown">

</a>

                            
                        <li class="dropdown profile-dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" ><img src="../default-profile-pic.png" alt="" width="25px"><%=User.Identity.Name%></b></span>  <span class="fa fa-caret-down" aria-hidden="true" style=""></a>
                            <ul class="dropdown-menu">
                                <li><a href="Profile_main.aspx"><i class="fa fa-user"></i>My Profile</a></li>
                                <li><a href="Seetings.aspx"><i class="fa fa-calendar"></i>Settings</a></li>                         
                                <li><a href="Advanced_Settings.aspx"><i class="fa fa-envelope"></i>Advanced Settings</a></li>
                                <li><a href="#"><i class="fa fa-barcode"></i>Custom Field</a></li>
                                <li class="divider"></li>
                               
                                 <li ><a href="#" ><asp:LinkButton id="LoginLink" Text="Log Out"  class="fa fa-sign-out" aria-hidden="true"
                      OnClick="LoginLink_OnClick" runat="server" /></a></li>
                            </ul>
                        </li>
                    </ul>--%>
                </div><!--/.nav-collapse -->
            </div><!--/.container-fluid -->
        </nav>
        <section class="page">


  <nav class="navbar-aside navbar-static-side" role="navigation">
                <div class="sidebar-collapse nano">
                    <div class="nano-content">
                        <ul class="nav metismenu" id="side-menu">

                            <li class="active">
                                <a href="Dashboard.aspx"><i class="fa fa-home fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Home </span><span class="fa arrow"></span></a>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Dashboard.aspx">Dashboard </a></li>
                           </ul>
                            </li>


                             <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Admin Setup </span><span class="fa arrow"></span></a>
                         
                          <ul class="nav nav-second-level collapse">
                          
                             <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;General Master </span><span class="fa arrow"></span></a>
                                 <ul class="nav nav-second-level collapse">
                                    <li><a href="Country.aspx">Country</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="State.aspx">State</a></li>
                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="City.aspx">City</a></li>

                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Currrency.aspx">Currency</a></li>

                           </ul>
                           
                                    
                             </li>




                              <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;User managemnet </span><span class="fa arrow"></span></a>
                               
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Employee_master.aspx">Salesman master</a></li>

                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Representative_entry.aspx">Representative master</a></li>

                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Designation.aspx">Designation</a></li>

                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Department.aspx">Department</a></li>

                           </ul>

                                    
                             </li>


                              <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Company setup </span><span class="fa arrow"></span></a>
                                 <ul class="nav nav-second-level collapse">
                                    <li><a href="">Company</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="location.aspx">Location</a></li>
                           </ul>
                            
                           

                                    
                             </li>



                           </ul>
                          
                             </li>
                               
                         




                            <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Master </span><span class="fa arrow"></span></a>
                          
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Item_Group_master.aspx">Item Group master</a></li>
                           </ul>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Item_Master.aspx">Item Master</a></li>
                           </ul>
                          
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Shade_No_Master.aspx">Shade No Master</a></li>
                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Party_master.aspx">Party master</a></li>

                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Party_wise_rate.aspx">Party wise rate</a></li>

                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Cone_type_master.aspx">Cone type master</a></li>

                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Unit.aspx">unit master</a></li>

                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Tax_Entry.aspx">Tax Entry</a></li>

                           </ul>
                                <ul class="nav nav-second-level collapse">
                                    <li><a href="Category.aspx">Category Entry</a></li>

                           </ul>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Order_half_ID.aspx">Order half ID</a></li>

                           </ul>
                            </li>
                           

                             <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Purchase </span><span class="fa arrow"></span></a>
                          
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_entry.aspx">Purchase Entry</a></li>
                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Order_indent_precot.aspx">Order indent precot</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Good_Received_precot.aspx">Goods received precot</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Stock_Inventory.aspx">Product Stock</a></li>
                           </ul>
                               <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_bill_report.aspx">Purchase bill report</a></li>
                           </ul>
                            </li>
                           
                           <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Sales </span><span class="fa arrow"></span></a>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Customer_po.aspx">Customer P.O</a></li>
                           </ul>
                                                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Customer_PO_Report.aspx">Customer PO Report</a></li>
                           </ul>
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Sales_cash_bill.aspx">Cash Bill</a></li>
                           </ul>
                                  <ul class="nav nav-second-level collapse">
                                    <li><a href="Cash_bill_report.aspx">Cash bill report</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Sales_credit_bill.aspx">Credit Bill</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Credit_bill_report.aspx">Credit bill report</a></li>
                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Salesman_report.aspx">Sales man report</a></li>
                           </ul>
                             <ul class="nav nav-second-level collapse">
                                    <li><a href="Item_wise_sales.aspx">Item wise sales Report</a></li>
                           </ul>
                            </li>



                            <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Winding </span><span class="fa arrow"></span></a>
                          
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Wending_delivery.aspx">Winding Delivery</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Winding_receipt.aspx">Winding Receipt</a></li>
                           </ul>
                               
                            </li>





               <li>
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Accounts Payable </span><span class="fa arrow"></span></a>
                         
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_payment_outstanding.aspx">Supplier Outstanding</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="payment_entry.aspx">Payments Entry</a></li>
                           </ul>


                              <li>
                               <li>
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Accounts receivable </span><span class="fa arrow"></span></a>
                         
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="credit_payment_outstanding.aspx">Customer Outstanding</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Collection_entry.aspx">Collection  Entry</a></li>
                           </ul>


<%--                              <li>
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Reports </span><span class="fa arrow"></span></a>


                            





                          

                            
                           

                                    
                             </li>--%>

                           </ul>
                          
                        
                               






                    </div>
                </div>
                
            </nav>
          
          
            <div id="wrapper">
                <div class="content-wrapper container">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-title see2">
                             <h2>Customer PO entry
                                 </h2>
                                 <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
   <ContentTemplate>
   <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
    <asp:Button ID="Button7" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="<" OnClick="Button7_Click"></asp:Button>
     <asp:Button ID="Button12" runat="server" class="btn-primary" Width="70px" Height="30px"  Text=">" onclick="Button12_Click" ></asp:Button>
     
                          
    </ContentTemplate>
                             
                           </asp:UpdatePanel>
                               <br />
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
   <ContentTemplate>
   
                             <asp:Button ID="Button3" runat="server" Text="New" class="btn-primary" Width="70px" Height="30px"  OnClick="Button3_Click"></asp:Button>
   <asp:Button ID="Button5" runat="server" Text="Save" class="btn-primary" Width="70px" Height="30px"  OnClick="Button5_Click"></asp:Button>
  <asp:Button ID="Button1" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="Remove" onclick="Button1_Click" />
  <asp:Button ID="Button2" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="Clear" onclick="Button2_Click" />
   <asp:Button ID="Button6" runat="server" class="btn-primary" Width="70px" Height="30px"  Text="View" 
                                 onclick="Button6_Click"  />

  
   </ContentTemplate>
                             
                           </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
                           <asp:Button ID="Button11" runat="server" Text="Button" style="display:none"></asp:Button>
  <asp:Panel ID="Panel1" runat="server" class="panel0" BorderColor="Black" BorderStyle="Solid" BackColor="#B0C4DE" Direction="LeftToRight" style="display:none" 
                         HorizontalAlign="Left" ScrollBars="Both" Width="75%" Height="420px">
   <div style="padding:12px; border:1px solid #e5e5e5;    background-color:#000000; color:#FFFFFF; font-size:15px; font-weight:400px; font-family: 'Open Sans'"
          HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif; ">
                     <h3 style="font-size:20px; " class="control-label"> View list  <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/exit11.png" width="30px" height="30px" style="float:right" /></h3>

           
        </div> <br /><br />
        
        <div class="col-sm-9 col-sm-offset-3">
       <div class="col-sm-2"> 
        <h2 style="color: #003366">Search : </h2>
        </div>
        <div class="col-sm-6">  <asp:TextBox ID="TextBox3" runat="server" Width="100%" OnTextChanged="TextBox3_TextChanged" ></asp:TextBox></div>
       </div>   
   
                       <div style="padding:12px;">
                    <asp:GridView ID="GridView3" runat="server" CssClass="red" AutoGenerateColumns="false" Width="100%" PageSize="100" BackColor="White" 
           BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                   
                   <Columns>
                                         <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="red">
            <ItemTemplate>
         
               <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkView_Click"  CssClass="goo" Text='<%# Eval("po_invoice") %>'>LinkButton</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
           
               <asp:BoundField HeaderText="Customer" DataField="customer" HeaderStyle-CssClass="red" />
                 <asp:BoundField HeaderText="Mobile No" DataField="mobile_no" HeaderStyle-CssClass="red" />
                   <asp:BoundField HeaderText="Total Qty" DataField="Total_qty" HeaderStyle-CssClass="red"/>
                    <asp:BoundField HeaderText="Total Amount" DataField="total_amount" HeaderStyle-CssClass="red" />
                   
                     <asp:TemplateField>
                   <ItemTemplate>
                   <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/edit4.jpg" Width="20px" Height="20px" onclick="ImageButton2_Click"></asp:ImageButton>
                   </ItemTemplate>
                   
                   </asp:TemplateField>
                   </Columns>
                       
                       <FooterStyle BackColor="White" ForeColor="#000066" />
       <HeaderStyle Height="40px" BackColor="#006699" Font-Bold="True" CssClass="red" 
           ForeColor="White" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" BackColor="White" ForeColor="#000066" 
           HorizontalAlign="Left" />
       <RowStyle Height="40px" ForeColor="#000066" />
       <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
       <SortedAscendingCellStyle BackColor="#F1F1F1" />
       <SortedAscendingHeaderStyle BackColor="#007DBB" />
       <SortedDescendingCellStyle BackColor="#CAC9C9" />
       <SortedDescendingHeaderStyle BackColor="#00547E" />
       </asp:GridView> 
                         </div>
  
  
  </asp:Panel>
  <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button11" PopupControlID="Panel1" runat="server" CancelControlID="ImageButton4" BackgroundCssClass="modelbackground"></asp:ModalPopupExtender>


  </ContentTemplate>
                  <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                </Triggers>            
                           </asp:UpdatePanel>


                                
                            </div>
                            
                        </div>
                    </div><!-- end .page title-->
                     <div class="row">
                    <div class="col-md-12">
                  




                    <div class="row see"  >


                    <div class="container">
 
  <div class="panel panel-default">
  
                                        <!-- End .form-group  -->
                                        
                                       
                                       
                                        
                                   
                                </div>
                                 
                            </div><!-- End .panel -->
                            
                            <div class="container">
 
  <div class="panel panel-default">
  <div class="panel-body">
   <div class="col-md-6">
                 <div class="panel-body">
                           <div class="form-horizontal">
                               <br />
                             
                               <div class="form-group"><label class="col-lg-3 control-label">Invoice No</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
   <ContentTemplate>
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label> 
                                      </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                                      <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                  
                
            
                </Triggers>
                           </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div >

                                 <div class="form-group">  <label class="col-lg-3 control-label">Date</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel22" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox13" runat="server"   class="form-control input-x2 dropbox"  
           ontextchanged="TextBox13_TextChanged" ></asp:TextBox>
         
   <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox13"></asp:CalendarExtender>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                   
                               <div class="form-group">  <label class="col-lg-3 control-label">Party Name</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel11" runat="server">
   <ContentTemplate> 
   
     <asp:ComboBox ID="ComboBox1" runat="server"    CssClass="cbox" AutoPostBack="true" 
           iteminsertlocation="Append" Width="350px" dropdownstyle="DropDownList"  
           autocompletemode="SuggestAppend" casesensitive="false" 
           onselectedindexchanged="ComboBox1_SelectedIndexChanged">
        </asp:ComboBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                       <asp:AsyncPostBackTrigger ControlID="TextBox13" EventName="TextChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                
                                 <div class="form-group">  <label class="col-lg-3 control-label">Party address</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel12" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox4" runat="server"  class="form-control input-x2 dropbox" AutoPostBack="true" 
           TextMode="MultiLine" ontextchanged="TextBox4_TextChanged"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    <asp:AsyncPostBackTrigger ControlID="ComboBox1" EventName="TextChanged"  />
                   
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>

                                <div class="form-group">  <label class="col-lg-3 control-label">Party Mobile No</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel16" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox7" runat="server" class="form-control input-x2 dropbox" 
           ontextchanged="TextBox7_TextChanged" ></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    <asp:AsyncPostBackTrigger ControlID="ComboBox1" EventName="TextChanged"  />
                      <asp:AsyncPostBackTrigger ControlID="TextBox4" EventName="TextChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                  </div>
                                 
                                 </div>
                                 <div class="form-group">  <label class="col-lg-3 control-label">PO Number</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel28" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox17" runat="server" class="form-control input-x2 dropbox" 
            ></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    <asp:AsyncPostBackTrigger ControlID="ComboBox1" EventName="TextChanged"  />
                      <asp:AsyncPostBackTrigger ControlID="TextBox4" EventName="TextChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                  </div>
                                 
                                 </div>
                                    </div>
                                    </div>
                                    </div>
                                    </div>
                                   </div>
                                    </div>
                                    </div>
                                    </div>
                
        
            
                 
  
  
  
                  
          <h4 style="clear:both" >Product  Details</h4>
                           
                              
   
     <div class="tablestyle" style="width:100%" >
    <table border="1">
    <tr>
    <th align="center">S.No</th>
    <th align="center">SItem Name</th>
    <th align="center">shade No</th>
    <th align="center">Color</th>
    <th>Unit</th>
    <th>Rate</th>
     <th>Quantity</th>
    <th>Amount</th>
    </tr>           
  
   

   <tr>
   <td>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>
 <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
  </ContentTemplate>
                                     <Triggers>
                                        
              <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                                      <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                      <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
  
  
  </td>
  <td>
     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
   <ContentTemplate>
  <asp:ComboBox ID="ComboBox2" runat="server" CssClass="cbox" AutoPostBack="true" 
           iteminsertlocation="Append" Width="300px" Height="35px" dropdownstyle="DropDownList"  
           autocompletemode="SuggestAppend" casesensitive="false" 
           onselectedindexchanged="ComboBox2_SelectedIndexChanged" 
           >
        </asp:ComboBox>
  </ContentTemplate>
                                     <Triggers>
                                     
                 <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>
  
  
   </td>
   <td>
     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
   <ContentTemplate>
   <asp:ComboBox ID="ComboBox3" runat="server" CssClass="cbox" AutoPostBack="true" 
           iteminsertlocation="Append" Width="100px" Height="35px" dropdownstyle="DropDownList"  
           autocompletemode="SuggestAppend" casesensitive="false" onselectedindexchanged="ComboBox3_SelectedIndexChanged"  
           >
        </asp:ComboBox>
   </ContentTemplate>
                                     <Triggers>
                                          <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
            <asp:AsyncPostBackTrigger ControlID="ComboBox2" EventName="SelectedIndexChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
  </td>
   <td>
      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox1" runat="server" Width="80px" Height="42px"  class="form-control " 
           ontextchanged="TextBox1_TextChanged1"></asp:TextBox>
   </ContentTemplate>
                                     <Triggers>
                                         <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
              <asp:AsyncPostBackTrigger ControlID="ComboBox3" EventName="SelectedIndexChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
   </td>
   <td>
      <asp:UpdatePanel ID="UpdatePanel8" runat="server">
   <ContentTemplate>
  <asp:ComboBox ID="ComboBox4" runat="server" CssClass="cbox" AutoPostBack="true" 
           iteminsertlocation="Append"  dropdownstyle="DropDownList" Width="100px" Height="35px"  
           autocompletemode="SuggestAppend" casesensitive="false" onselectedindexchanged="ComboBox4_SelectedIndexChanged" 
           >
        </asp:ComboBox>
  </ContentTemplate>
                          <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
                          </Triggers>           
                           </asp:UpdatePanel>
  </td>
  <td>

      <asp:UpdatePanel ID="UpdatePanel13" runat="server">
   <ContentTemplate>
 <asp:TextBox ID="TextBox2" runat="server" Width="120px" Height="42px" class="form-control input-x2 dropbox" 
           ontextchanged="TextBox2_TextChanged"></asp:TextBox>
  </ContentTemplate>
  <Triggers>
  
    <asp:AsyncPostBackTrigger ControlID="ComboBox2" EventName="SelectedIndexChanged"  />
     <asp:AsyncPostBackTrigger ControlID="ComboBox3" EventName="SelectedIndexChanged"  />
      <asp:AsyncPostBackTrigger ControlID="ComboBox4" EventName="SelectedIndexChanged"  />
       <asp:AsyncPostBackTrigger ControlID="TextBox5" EventName="TextChanged"  />
            <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
  </Triggers>
                                     
                           </asp:UpdatePanel>
 </td>
 <td>

      <asp:UpdatePanel ID="UpdatePanel14" runat="server">
   <ContentTemplate>
 <asp:TextBox ID="TextBox5" runat="server" AutoPostBack="true" Width="72px" Height="42px" class="form-control input-x2 dropbox" 
           ontextchanged="TextBox5_TextChanged"></asp:TextBox>
  </ContentTemplate>
  <Triggers>
      <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
  </Triggers>
                                     
                           </asp:UpdatePanel>
 </td>
 <td>

      <asp:UpdatePanel ID="UpdatePanel15" runat="server">
   <ContentTemplate>
   
 <asp:TextBox ID="TextBox6" runat="server" AutoPostBack="true" Width="180px" Height="42px"  ValidationGroup="see" class="form-control input-x2 dropbox" 
           ontextchanged="TextBox6_TextChanged" ></asp:TextBox>
  </ContentTemplate>
  <Triggers>
      <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
    <asp:AsyncPostBackTrigger ControlID="TextBox5" EventName="TextChanged"  />
  </Triggers>
                                     
                           </asp:UpdatePanel>
  </td>
 </tr>
 </table>
   <asp:UpdatePanel ID="UpdatePanel23" runat="server" >
   <ContentTemplate>
 <asp:Button ID="Button8"  runat="server" CssClass="btn1" Text="ADD" ValidationGroup="see"  onclick="Button8_Click"></asp:Button>
  </ContentTemplate>
  <Triggers>
      <asp:AsyncPostBackTrigger ControlID="TextBox6" EventName="TextChanged"  />
  </Triggers>
                                     
                           </asp:UpdatePanel>
                           </div></div>

<hr />
   <asp:UpdatePanel ID="UpdatePanel10" runat="server" >
   <ContentTemplate>

  
<asp:GridView ID="GridView1" runat="server" CssClass="red" width="100%" AutoGenerateColumns="False" 
           onrowdatabound="GridView1_RowDataBound" 
           onrowcancelingedit="GridView1_RowCancelingEdit" 
            
            
           BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
           CellPadding="3">
<Columns>
 <asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "S No" HeaderStyle-CssClass="red">

    <ItemTemplate>

        <asp:Label ID="lbls_no" runat="server"  

        Text='<%# Eval("s_no")%>'></asp:Label>

    </ItemTemplate>

    
 <HeaderStyle CssClass="red" Height="40px" />
     

     <ItemStyle Width="100px" Height="50px" />
     

</asp:TemplateField>


 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Item Name" HeaderStyle-CssClass="red">

    <ItemTemplate>

         <asp:TextBox ID="txtitemName1" runat="server" Width="200px" Text='<%# Eval("item_name")%>'></asp:TextBox>

    </ItemTemplate>

 

   

    
    

     <HeaderStyle CssClass="red" />
     <ItemStyle Width="300px" />

   

    
    

</asp:TemplateField>

<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Shade No" HeaderStyle-CssClass="red">

    <ItemTemplate>

       <asp:TextBox ID="txtshadeno1" runat="server" Width="50px" Text='<%# Eval("shade_no")%>'></asp:TextBox>

    </ItemTemplate>

 

    
 <HeaderStyle CssClass="red" />
    <ItemStyle Width="200px" />

</asp:TemplateField>



 <asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Color" HeaderStyle-CssClass="red">

    <ItemTemplate>

       <asp:TextBox ID="txtcolor1" runat="server" Width="70px" Text='<%# Eval("color")%>'></asp:TextBox>

    </ItemTemplate>

 
    

     <HeaderStyle CssClass="red" />
     <ItemStyle Width="200px" />

</asp:TemplateField>


<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Unit" HeaderStyle-CssClass="red">

    <ItemTemplate>

        <asp:TextBox ID="txtunit1" runat="server" Width="70px" Text='<%# Eval("unit")%>'></asp:TextBox>

    </ItemTemplate>

 

    

    <HeaderStyle CssClass="red" />
 <ItemStyle Width="150px" />

</asp:TemplateField>


<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Rate" HeaderStyle-CssClass="red">

    <ItemTemplate>

        <asp:TextBox ID="txtrate1" runat="server" Width="70px" Text='<%# Eval("rate")%>'></asp:TextBox>

    </ItemTemplate>

   

   
 <HeaderStyle CssClass="red" />
  <ItemStyle Width="200px" />

</asp:TemplateField>


<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Qty" HeaderStyle-CssClass="red">

    <ItemTemplate>

      <asp:TextBox ID="txtqty1" runat="server" Width="70px" AutoPostBack="true" Text='<%# Eval("qty")%>' 
            ontextchanged="txtqty1_TextChanged"></asp:TextBox>

    </ItemTemplate>

   

   

    <HeaderStyle CssClass="red" />

   

  <ItemStyle Width="150px" />

</asp:TemplateField>



<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Total Amount" HeaderStyle-CssClass="red">

    <ItemTemplate>

       <asp:TextBox ID="txttotalamount1" runat="server" Width="70px" Text='<%# Eval("total_amount")%>'></asp:TextBox>

    </ItemTemplate>

  

   

  
    <HeaderStyle CssClass="red" />
   <ItemStyle Width="200px" />

</asp:TemplateField>




<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="lnkRemove" runat="server"
            CommandArgument = '<%# Eval("s_no")%>'
         OnClientClick = "return confirm('Do you want to delete?')"
        Text = "Delete" onclick="lnkRemove_Click" ></asp:LinkButton>
    </ItemTemplate>
   
</asp:TemplateField>


</Columns>


    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
    <RowStyle ForeColor="#000066" />
    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F1F1F1" />
    <SortedAscendingHeaderStyle BackColor="#007DBB" />
    <SortedDescendingCellStyle BackColor="#CAC9C9" />
    <SortedDescendingHeaderStyle BackColor="#00547E" />


</asp:GridView></div>
</ContentTemplate>
                                     <Triggers>
                                       
               <asp:AsyncPostBackTrigger ControlID="GridView1"   />
                   <asp:AsyncPostBackTrigger ControlID="Button8" EventName="Click"  />
                </Triggers>
                           </asp:UpdatePanel>     



<br />

<br />
<div class="row">
                    <div class="col-md-12">
                  




                  
  <div class="container">

  <div class="container">
 
  <div class="panel panel-default">
  <div class="panel-body">
   <div class="col-md-6">
              
                           <div class="form-horizontal">
                               <br />
                             
                               <div class="form-group"><label class="col-lg-3 control-label">Total Qty</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel17" runat="server">
   <ContentTemplate>
                                  <asp:TextBox ID="TextBox10" runat="server"   class="form-control input-x2 dropbox" ></asp:TextBox>
                                      </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                                      <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                   <asp:AsyncPostBackTrigger ControlID="GridView1"   />
                
            
                </Triggers>
                           </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div >
                                   
                               <div class="form-group">  <label class="col-lg-3 control-label">Total Amount</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel18" runat="server">
   <ContentTemplate> 
   
    <asp:TextBox ID="TextBox11" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                         <asp:AsyncPostBackTrigger ControlID="GridView1"   />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                 </div>
                                  </div>
                                   </div>
                                <div class="col-md-6">
                           <div class="form-horizontal">
                               <br />
                             
                               <div class="form-group"><label class="col-lg-3 control-label">KG in stock</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel26" runat="server">
   <ContentTemplate>
                                  <asp:TextBox ID="TextBox15" runat="server"   class="form-control " Width="90%"></asp:TextBox>
                                      </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                                      <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                   <asp:AsyncPostBackTrigger ControlID="GridView1"   />
                
            
                </Triggers>
                           </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div >
                                <div class="form-group"><label class="col-lg-3 control-label">Cones in stock</label>

                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel27" runat="server">
   <ContentTemplate>
                                  <asp:TextBox ID="TextBox16" runat="server"   class="form-control " Width="90%" ></asp:TextBox>
                                      </ContentTemplate>
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                                      <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"  />
                                        <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                   <asp:AsyncPostBackTrigger ControlID="GridView1"   />
                
            
                </Triggers>
                           </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div >
                                </div>

                                 
                               </div>  
                               
                            </div>                             <!-- End .form-group  -->
                                        
              </div>
                                       
                                       
            
             
                             </div>
                 </div>
                 </div>
                 </div>     
                
                </div>
                    
                    

                   
   <br />
                      &nbsp;

                       
   

                                        <!-- End .form-group  -->
                                   
                                       
                                       
                                        
                                   
                             
                   
                     
                      
                        



                  
                   
                   
     
                        
                        
                
                   
                  
                           
        </section>

        <script type="text/javascript" src="js/jquery.min.js"></script>
        <script type="text/javascript" src="bootstrap/js/bootstrap.min.js"></script>
        <script src="js/metisMenu.min.js"></script>
        <script src="js/jquery-jvectormap-1.2.2.min.js"></script>
        <!-- Flot -->
        <script src="js/flot/jquery.flot.js"></script>
        <script src="js/flot/jquery.flot.tooltip.min.js"></script>
        <script src="js/flot/jquery.flot.resize.js"></script>
        <script src="js/flot/jquery.flot.pie.js"></script>
        <script src="js/chartjs/Chart.min.js"></script>
        <script src="js/pace.min.js"></script>
        <script src="js/waves.min.js"></script>
        <script src="js/jquery-jvectormap-world-mill-en.js"></script>
        <!--        <script src="js/jquery.nanoscroller.min.js"></script>-->
        <script type="text/javascript" src="js/custom.js"></script>
        <script type="text/javascript">
            $(function () {

                var barData = {
                    labels: ["January", "February", "March", "April", "May", "June", "July"],
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "rgba(220,220,220,0.5)",
                            strokeColor: "rgba(220,220,220,0.8)",
                            highlightFill: "rgba(220,220,220,0.75)",
                            highlightStroke: "rgba(220,220,220,1)",
                            data: [65, 59, 80, 81, 56, 55, 40]
                        },
                        {
                            label: "My Second dataset",
                            fillColor: "rgba(14, 150, 236,0.5)",
                            strokeColor: "rgba(14, 150, 236,0.8)",
                            highlightFill: "rgba(14, 150, 236,0.75)",
                            highlightStroke: "rgba(14, 150, 236,1)",
                            data: [28, 48, 40, 19, 86, 27, 90]
                        }
                    ]
                };

                var barOptions = {
                    scaleBeginAtZero: true,
                    scaleShowGridLines: true,
                    scaleGridLineColor: "rgba(0,0,0,.05)",
                    scaleGridLineWidth: 1,
                    barShowStroke: true,
                    barStrokeWidth: 2,
                    barValueSpacing: 5,
                    barDatasetSpacing: 1,
                    responsive: true
                };


                var ctx = document.getElementById("barChart").getContext("2d");
                var myNewChart = new Chart(ctx).Bar(barData, barOptions);

            });
        </script>
        <!-- Google Analytics:  -->
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r;
                i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments);
                }, i[r].l = 1 * new Date();
                a = s.createElement(o),
                        m = s.getElementsByTagName(o)[0];
                a.async = 1;
                a.src = g;
                m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
            ga('create', 'UA-3560057-28', 'auto');
            ga('send', 'pageview');
        </script>
        </form>
    </body>
</html>

