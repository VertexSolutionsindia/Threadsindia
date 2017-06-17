<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer_po.aspx.cs" Inherits="Admin_Default" %>

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


  <style type="text/css">
.GridviewDiv {font-size: 100%; font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helevetica, sans-serif; color: #303933;}
.headerstyle
{
color:#FFFFFF;border-right-color:#abb079;border-bottom-color:#abb079;background-color: #df5015;padding:0.5em 0.5em 0.5em 0.5em;text-align:center;
}
</style>
   
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
 .grid
 {
     font-size:17px;
     padding:10px;
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
                            </li>
                           

                             <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Purchase </span><span class="fa arrow"></span></a>
                          
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_entry.aspx">Purchase Entry</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Stock_Inventory.aspx">Product Stock</a></li>
                           </ul>
                               
                            </li>
                           
                           <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Sales </span><span class="fa arrow"></span></a>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Customer_po.aspx">Customer P.O</a></li>
                           </ul>
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Sales_cash_bill.aspx">Cash Bill</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Sales_credit_bill.aspx">Credit Bill</a></li>
                           </ul>
                               
                            </li>



                            <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Winding </span><span class="fa arrow"></span></a>
                          
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Wending_delivery.aspx">Winding delivery</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="Winding_receipt.aspx">Winding receeipt</a></li>
                           </ul>
                               
                            </li>





               <li>
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Finance Management </span><span class="fa arrow"></span></a>
                         
                          <ul class="nav nav-second-level collapse">
                          
                             <li>
                                <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Outstanding </span><span class="fa arrow"></span></a>
                                 <ul class="nav nav-second-level collapse">
                                    <li><a href="Supplier_wise.aspx">Supplier</a></li>
                                 </ul>
                                  <ul class="nav nav-second-level collapse">
                                    <li><a href="Customer_wise.aspx">Customer</a></li>
                                  </ul>
                                   <ul class="nav nav-second-level collapse">
                                    <li><a href="salesman_salary.aspx">sales man</a></li>
                                  </ul>
                              </li>
                            </ul>


                            





                          

                            
                           

                                    
                             </li>

                              <li>
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Reports </span><span class="fa arrow"></span></a>
                         
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="">Sales man</a></li>
                           </ul>
                           


                            





                          

                            
                           

                                    
                             </li>

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
                 
                   <asp:BoundField HeaderText="Invoice No" DataField="purchase_invoice" HeaderStyle-CssClass="red"  />
           
               <asp:BoundField HeaderText="Customer" DataField="customer" HeaderStyle-CssClass="red" />
                 <asp:BoundField HeaderText="Mobile No" DataField="mobile_no" HeaderStyle-CssClass="red" />
                   <asp:BoundField HeaderText="Total Qty" DataField="Total_qty" HeaderStyle-CssClass="red"/>
                    <asp:BoundField HeaderText="Total Amount" DataField="total_amount" HeaderStyle-CssClass="red" />
                     <asp:BoundField HeaderText="vat" DataField="vat" HeaderStyle-CssClass="red" />
                  <asp:BoundField HeaderText="Nett Total" DataField="Grand_total" HeaderStyle-CssClass="red" />
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
   
    <asp:TextBox ID="TextBox18" runat="server" AutoPostBack="true" class="form-control input-x2 dropbox"   ontextchanged="TextBox18_TextChanged"></asp:TextBox>
    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="Searchparty" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox18"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
    </asp:AutoCompleteExtender>
<asp:DropDownExtender ID="DropDownExtender1" runat="server" TargetControlID="TextBox18">
</asp:DropDownExtender>
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
           TextMode="MultiLine"></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    <asp:AsyncPostBackTrigger ControlID="TextBox18" EventName="TextChanged"  />
                   
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>

                                <div class="form-group">  <label class="col-lg-3 control-label">Party Mobile No</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel16" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox7" runat="server" class="form-control input-x2 dropbox" 
           ></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    <asp:AsyncPostBackTrigger ControlID="TextBox18" EventName="TextChanged"  />
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
                    <asp:AsyncPostBackTrigger ControlID="TextBox18" EventName="TextChanged"  />
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
                           
                              
   
   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>
<asp:GridView runat="server" ID="gvDetails" ShowFooter="true" AllowPaging="true" 
           PageSize="10" AutoGenerateColumns="false" DataKeyNames="s_no,item_name" 
           OnPageIndexChanging="gvDetails_PageIndexChanging" OnRowCancelingEdit="gvDetails_RowCancelingEdit"
OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating" 
           OnRowDeleting="gvDetails_RowDeleting" OnRowCommand ="gvDetails_RowCommand" onrowdatabound="gvDetails_RowDataBound" 
            >
<HeaderStyle CssClass="headerstyle" />
<Columns>
<asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "S No" HeaderStyle-CssClass="grid">

    <ItemTemplate>
   <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
    </ItemTemplate>
    <FooterTemplate>
  
     <asp:TextBox ID="sno" runat="server"  width="100px" Height="30px" />
    </FooterTemplate>
   
    <HeaderStyle CssClass="grid" />
    <ItemStyle Width="30px" />
   
</asp:TemplateField>


<asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txtProductname" runat="server" width="300px" Height="30px"  Text='<%# Eval("item_name")%>'/>
</ItemTemplate>
<FooterTemplate>
 
<asp:TextBox ID="itemname" runat="server" width="300px" Height="30px"
        ontextchanged="itemname_TextChanged" />
  <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" ServiceMethod="SearchCustomers1" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="itemname"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
    </asp:AutoCompleteExtender>
                    
 
</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>




<asp:TemplateField HeaderText = "Shade No" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txtshadeno" runat="server" width="100px" Height="30px" Text='<%# Eval("shade_no")%>'/>
</ItemTemplate>
<FooterTemplate>

<asp:TextBox ID="shadeno" runat="server" width="100px" Height="30px" 
         />
         <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" ServiceMethod="Searchshadeno" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="shadeno"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
    </asp:AutoCompleteExtender>
            </ContentTemplate>
                                      
</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>


<asp:TemplateField HeaderText = "color" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txtcolor" runat="server" width="100px" Height="30px" Text='<%# Eval("color")%>'/>
</ItemTemplate>
<FooterTemplate>
<asp:TextBox ID="color" runat="server" width="100px" Height="30px" />

</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>

<asp:TemplateField HeaderText = "unit" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txtunit" runat="server" width="100px" Height="30px" Text='<%# Eval("unit")%>'/>
</ItemTemplate>
<FooterTemplate>
<asp:TextBox ID="unit" runat="server" width="100px" Height="30px" />

</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>

<asp:TemplateField HeaderText = "rate" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txtrate" width="100px" Height="30px" runat="server" Text='<%# Eval("rate")%>'/>
</ItemTemplate>
<FooterTemplate>
<asp:TextBox ID="rate" width="100px" Height="30px" runat="server" />

</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>

<asp:TemplateField HeaderText = "qty" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txtqty" width="100px" Height="30px" runat="server" Text='<%# Eval("qty")%>'/>
</ItemTemplate>
<FooterTemplate>
<asp:TextBox ID="qty" width="100px" Height="30px" runat="server" />

</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>


<asp:TemplateField HeaderText = "total" HeaderStyle-CssClass="grid">
<ItemTemplate>
<asp:TextBox ID="txttotal" width="100px" Height="30px" runat="server" Text='<%# Eval("total_amount")%>'/>
</ItemTemplate>
<FooterTemplate>
<asp:TextBox ID="total" width="100px" Height="30px" runat="server" />
<asp:Button ID="btnAdd" CommandName="AddNew" runat="server" Text="Add" />
</FooterTemplate>
    <HeaderStyle CssClass="grid" />
</asp:TemplateField>

</Columns>
</asp:GridView>
     </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="gvDetails" />
          
                </Triggers>
                           </asp:UpdatePanel>
     <asp:Label ID="lblresult" runat="server"></asp:Label>
    

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
                      
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                
                                 <div class="form-group">  <label class="col-lg-3 control-label">
                                  <asp:UpdatePanel ID="UpdatePanel24" runat="server">
   <ContentTemplate> 
                                    Vat  <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true"
                                         onselectedindexchanged="DropDownList5_SelectedIndexChanged">
                                           
                                     <asp:ListItem>5</asp:ListItem>
                                     <asp:ListItem>2</asp:ListItem>
                                     <asp:ListItem>4</asp:ListItem>
                                     </asp:DropDownList>%
                                     
                                     </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                    
                            <asp:AsyncPostBackTrigger ControlID="DropDownList5" EventName="SelectedIndexChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                     
                                     </label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel19" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox8" runat="server" class="form-control input-x2 dropbox" ></asp:TextBox>
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
                                 <div class="form-group">  <label class="col-lg-3 control-label">Sub Total</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel25" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox14" runat="server" class="form-control input-x2 dropbox" ></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                  <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
              
                      <asp:AsyncPostBackTrigger ControlID="DropDownList5" EventName="SelectedIndexChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                 <div class="form-group">  <label class="col-lg-3 control-label">Round Off</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel21" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox12" runat="server" class="form-control input-x2 dropbox" ></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
                
                      <asp:AsyncPostBackTrigger ControlID="DropDownList5" EventName="SelectedIndexChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                <div class="form-group">  <label class="col-lg-3 control-label">Nett Total Amount</label>
                              
                                    <div class="col-lg-9">
                                     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
   <ContentTemplate>
   <asp:TextBox ID="TextBox9" runat="server" class="form-control input-x2 dropbox" ></asp:TextBox>
                                    </ContentTemplate>
                                     <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="Button7" EventName="Click"  />
                <asp:AsyncPostBackTrigger ControlID="Button5" EventName="Click"  />
                  <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click"  />
                     <asp:AsyncPostBackTrigger ControlID="Button12" EventName="Click"  />
              
                      <asp:AsyncPostBackTrigger ControlID="DropDownList5" EventName="SelectedIndexChanged"  />
                </Triggers>
                           </asp:UpdatePanel>
                                    
                                    </div>
                                
                                
                                </div>
                                </div>
</div>
</div>                                <div class="col-md-6">
                           <div class="form-horizontal">
                               <br />
                             
                               <div class="form-group"><label class="col-lg-3 control-label">Total KG</label>

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
          
                
            
                </Triggers>
                           </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div >
                                <div class="form-group"><label class="col-lg-3 control-label">Total Cones</label>

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

