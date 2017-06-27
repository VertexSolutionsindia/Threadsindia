<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="RabbitDashboard" %>
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

              <script type="text/javascript">

                  $(document).ready(function () {

                      $(".selectpicker").selectpicker();

                  });

                 </script>

                  <script type="text/javascript" language="javascript">
                      function controlEnter(obj, event) {
                          var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
                          if (keyCode == 13) {
                              document.getElementById(obj).focus();
                              return false;
                          }
                          else {
                              return true;
                          }
                      }
                   


</script>
   <script type = "text/javascript">

       function Confirm1() {
           var confirm_value = document.createElement("INPUT");
           confirm_value.type = "hidden";
           confirm_value.name = "confirm_value";
           if (confirm("Are you sure you want to delete this Item Group?")) {
               confirm_value.value = "Yes";
           } else {
               confirm_value.value = "No";
           }
           document.forms[0].appendChild(confirm_value);
       }
    </script>
   
<style>

.tablestyles table tr td
{
    padding:6px;
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
                    <ul class="nav navbar-nav">
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
                    </ul>
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
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Accounts Payable </span><span class="fa arrow"></span></a>
                         
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_payment_outstanding.aspx">Supplier Outstanding</a></li>
                           </ul>
                            <ul class="nav nav-second-level collapse">
                                    <li><a href="payment_entry.aspx">Payemnts Entry</a></li>
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


                              <li>
                    <a href=""><i class="fa fa-folder-open fa-2x" aria-hidden="true"></i> <span class="nav-label">&nbsp;&nbsp;Reports </span><span class="fa arrow"></span></a>
                          <ul class="nav nav-second-level collapse">
                                    <li><a href="Purchase_bill_report.aspx">Purchase bill report</a></li>
                           </ul>
                           <ul class="nav nav-second-level collapse">
                                    <li><a href="Cash_bill_report.aspx">Cash bill report</a></li>
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

                           </ul>
                          
                        
                               






                    </div>
                </div>
                
            </nav>
          
      
            <div id="wrapper">
                <div class="content-wrapper container">
                    <div class="row">
                        <div class="col-sm-12">
                           <div class="page-title">
                                <h2 style=" color:black">Dashboard  <small></small></h2>
                             
  
  Financial Year : <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
  

                                
                            </div>
                            
                        </div>
                    </div><!-- end .page title-->
                     <div class="row see">
                     <br />
 <div class="container">
 
  <div class="panel panel-default">
         <div class="panel-body">
 
              <div class="col-md-6">
              <h3>Supplier Name :</h3>
               <asp:DropDownList ID="DropDownList2" class="form-control" Width="100%" runat="server"></asp:DropDownList>
              <asp:Button ID="Button1" runat="server" Text="Accounts Ledger" 
                      onclick="Button1_Click"></asp:Button>
              <h3>Supplier Outstanding</h3>
              <div style="overflow:scroll; width:100%; height:200px;">
             <asp:GridView ID="GridView2" runat="server" Width="100%" Font-Size="16px" 
               AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="100">
      
           <Columns>
           <asp:BoundField HeaderText="Supplier" DataField="Supplier" />
           <asp:BoundField HeaderText="Outstanding amount" DataField="pending_amount" DataFormatString="{0:#,##0.00}" />
           
           </Columns>
       <HeaderStyle Height="40px" CssClass="red" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" />
       <RowStyle Height="40px" />
           
           </asp:GridView>
           </div>

           <div style="overflow:scroll; width:100%; height:200px;">
             <asp:GridView ID="GridView5" runat="server" Width="100%" Font-Size="16px" 
               AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="100">
      
           <Columns>
           <asp:BoundField HeaderText="Date" DataField="Pay_date" DataFormatString="{0:#,##0.00}" />
           <asp:BoundField HeaderText="Particulars" DataField="status"  />
           <asp:BoundField HeaderText="Credit" DataField="value" />
             <asp:BoundField HeaderText="Debit" DataField="Estimate_value" />
              <asp:BoundField HeaderText="Balance" DataField="outstanding" />
           </Columns>
       <HeaderStyle Height="40px" CssClass="red" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" />
       <RowStyle Height="40px" />
           
           </asp:GridView>
           </div>







              </div>


              <div class="col-md-6">
              <h3>Customer Name:</h3>
              <asp:DropDownList ID="DropDownList1" class="form-control" runat="server"></asp:DropDownList>
               <asp:Button ID="Button2" runat="server" Text="Accounts Ledger" 
                      onclick="Button2_Click"></asp:Button>
              <h3>Customer Outstanding</h3>
              <div style="overflow:scroll; width:100%; height:200px;">
             <asp:GridView ID="GridView1" runat="server" Width="100%" Font-Size="16px" 
               AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="100">
      
           <Columns>
           <asp:BoundField HeaderText="Customer" DataField="Supplier" />
           <asp:BoundField HeaderText="Outstanding amount" DataField="pending_amount" DataFormatString="{0:#,##0.00}" />
           
           </Columns>
       <HeaderStyle Height="40px" CssClass="red" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" />
       <RowStyle Height="40px" />
           
           </asp:GridView>
           </div>
            <div style="overflow:scroll; width:100%; height:200px;">
             <asp:GridView ID="GridView6" runat="server" Width="100%" Font-Size="16px" 
               AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="100">
      
           <Columns>
           <asp:BoundField HeaderText="Date" DataField="Pay_date" DataFormatString="{0:#,##0.00}" />
           <asp:BoundField HeaderText="Particulars" DataField="status"  />
           <asp:BoundField HeaderText="Credit" DataField="Estimate_value" />
             <asp:BoundField HeaderText="Debit" DataField="value" />
              <asp:BoundField HeaderText="Balance" DataField="outstanding" />
           </Columns>
       <HeaderStyle Height="40px" CssClass="red" />
       <PagerSettings FirstPageText="First" LastPageText="Last" />
       <PagerStyle Wrap="true" BorderStyle="Solid" Width="100%" 
           CssClass="gvwCasesPager" />
       <RowStyle Height="40px" />
           
           </asp:GridView>
           </div>
              </div>
        </div>
  </div>
</div>





<div class="container">
 
  <div class="panel panel-default">
         <div class="panel-body">
 
              <div class="col-md-6">
             <h3> Sales Man Salary report :</h3>
            
              Employee Name: 
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <ContentTemplate>
              <asp:DropDownList ID="DropDownList3" runat="server" class="form-control" AutoPostBack="true" 
                  onselectedindexchanged="DropDownList3_SelectedIndexChanged"></asp:DropDownList>
              </ContentTemplate>
                       <Triggers>
              
                
                                    
                                     </Triggers>               
 </asp:UpdatePanel>

              <br />
             <div class="form-group"><label class="col-lg-3 control-label">  From Date:</label>
              <div class="col-lg-9">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox1"></asp:CalendarExtender>
               </ContentTemplate>
                      <Triggers>
              
                 
                                      
                                     </Triggers>                
 </asp:UpdatePanel>
 </div></div>

 <br />
              <div class="form-group"><label class="col-lg-3 control-label">To date:</label>
              <div class="col-lg-9">
              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
   <ContentTemplate>
              <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" 
                      ontextchanged="TextBox2_TextChanged"></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox2"></asp:CalendarExtender>
               </ContentTemplate>
                         <Triggers>
              
                         </Triggers>             
 </asp:UpdatePanel>
            </div>
            
            </div>
              <br />
              <br />
          
            <div class="form-group"><label class="col-lg-3 control-label"> Total Comm :</label>
               <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
   <ContentTemplate>
              <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </ContentTemplate>
                         <Triggers>
               <asp:AsyncPostBackTrigger ControlID="GridView3"   />
                
                       </Triggers>             
 </asp:UpdatePanel>
   </div>
            
            </div>
              <br />
               <div class="form-group"><label class="col-lg-3 control-label"> Salary :</label>
               <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
   <ContentTemplate>
              <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </ContentTemplate>
                         <Triggers>
               <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged"  />
                
                       </Triggers>             
 </asp:UpdatePanel>
 
   </div>
            
            </div>
   <br />
              <div class="form-group"><label class="col-lg-3 control-label">Total amount :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
   <ContentTemplate>
              <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </ContentTemplate>
                         <Triggers>
               <asp:AsyncPostBackTrigger ControlID="GridView3"   />
                
                       </Triggers>             
 </asp:UpdatePanel>
   </div>
            
            </div>
              <br />
              <br />
              

               <div style="overflow:scroll; width:100%; height:200px;">

               <asp:UpdatePanel ID="UpdatePanel4" runat="server">
   <ContentTemplate>

              <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="false" 
           onrowdatabound="GridView1_RowDataBound">
           <Columns>
           <asp:BoundField DataField="date" HeaderText="Date" />
            <asp:BoundField DataField="Total_qty" HeaderText="Total Qty" />
             <asp:BoundField DataField="com_amount" HeaderText="Com Amount" />
           
           
           </Columns>
 
             </asp:GridView>

              </ContentTemplate>
                         <Triggers>
              
                  <asp:AsyncPostBackTrigger ControlID="TextBox2" EventName="TextChanged"  />
                                      
                          </Triggers>
              </asp:UpdatePanel>

             </div>


             


               </div>
               <div class="col-md-6">
               <h3> Product Stock</h3> 
                 <br />
                 <br />
                  <div class="form-group"><label class="col-lg-3 control-label">Item Name :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
   <ContentTemplate>
             <asp:TextBox ID="TextBox19" runat="server" class="form-control input-x2 dropbox" 
           AutoPostBack="true" ontextchanged="TextBox19_TextChanged" ></asp:TextBox>
  
                           <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" ServiceMethod="Searchitem" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox19"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                </ContentTemplate>
                                
 </asp:UpdatePanel>
   </div>
   </div>
   <br />
                 <br />
                  <div class="form-group"><label class="col-lg-3 control-label">Shade No :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
   <ContentTemplate>
             <asp:TextBox ID="TextBox6" runat="server" class="form-control input-x2 dropbox" 
           AutoPostBack="true" ontextchanged="TextBox6_TextChanged" ></asp:TextBox>
  
                           <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" ServiceMethod="Searchshade" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox6"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TextBox19" EventName="TextChanged"  />
                     
                       </Triggers>                
 </asp:UpdatePanel>
   </div>
   </div>
    <br />
                 <br />
                  <div class="form-group"><label class="col-lg-3 control-label">Total Kg :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
   <ContentTemplate>
             <asp:TextBox ID="TextBox7" runat="server" class="form-control input-x2 dropbox"  ></asp:TextBox>
  
                         
                </ContentTemplate>
                       <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TextBox19" EventName="TextChanged"  />
                       <asp:AsyncPostBackTrigger ControlID="TextBox6" EventName="TextChanged"  />
                       </Triggers>         
 </asp:UpdatePanel>
   </div>


     <br />
                 <br />
                  <div class="form-group"><label class="col-lg-3 control-label">Total Cones :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
   <ContentTemplate>
             <asp:TextBox ID="TextBox8" runat="server" class="form-control input-x2 dropbox"  ></asp:TextBox>
  
                           
                </ContentTemplate>
                           <Triggers>
                       
                         <asp:AsyncPostBackTrigger ControlID="TextBox19" EventName="TextChanged"  />
                       <asp:AsyncPostBackTrigger ControlID="TextBox6" EventName="TextChanged"  />
                       </Triggers>      
 </asp:UpdatePanel>
   </div>
   </div>

             </div>
        </div>
  </div>
</div>
</div>




<div class="container">
 
  <div class="panel panel-default">
         <div class="panel-body">
 
              <div class="col-md-6">
              <h3>Winding Delivery pending</h3>
              
              <br />
                 <br />
                  <div class="form-group"><label class="col-lg-3 control-label">Item Name :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
   <ContentTemplate>
             <asp:TextBox ID="TextBox9" runat="server" class="form-control input-x2 dropbox" 
           AutoPostBack="true" ontextchanged="TextBox9_TextChanged"   ></asp:TextBox>
  
                           <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" ServiceMethod="Searchitem1" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox9"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                </ContentTemplate>
                                
 </asp:UpdatePanel>
   </div>
   </div>
   <br />
                 <br />
                  <div class="form-group"><label class="col-lg-3 control-label">Shade No :</label>
                <div class="col-lg-9">
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
   <ContentTemplate>
             <asp:TextBox ID="TextBox10" runat="server" class="form-control input-x2 dropbox" 
           AutoPostBack="true" ontextchanged="TextBox10_TextChanged"></asp:TextBox>
  
                           <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" MinimumPrefixLength="1" ServiceMethod="Searchshade1" FirstRowSelected = "false" CompletionInterval="0" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox10"  CompletionListCssClass="completionList"
     CompletionListItemCssClass="listItem"
     CompletionListHighlightedItemCssClass="itemHighlighted">
      </asp:AutoCompleteExtender>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TextBox9" EventName="TextChanged"  />
                     
                       </Triggers>                
 </asp:UpdatePanel>
   </div>
   </div>
    <br />
                 <br />
 <asp:UpdatePanel ID="UpdatePanel14" runat="server">
   <ContentTemplate>
  <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" Width="100%">
  <Columns>
 
   <asp:TemplateField>
           
           <ItemTemplate>
               <asp:CheckBox ID="CheckBox2" runat="server" />
            </ItemTemplate>
           
           </asp:TemplateField>
         
  <asp:BoundField DataField="customer" HeaderText="Customer Name" />
   <asp:BoundField DataField="item_name" HeaderText="Item name"  />
  <asp:BoundField DataField="shade_no" HeaderText="Shade No"  />
  <asp:BoundField DataField="Cones" HeaderText="Cones" />
  <asp:BoundField DataField="Gross_Wt" HeaderText="Gross Wt" />
  <asp:BoundField DataField="Net_Wt" HeaderText="Nett Wt" />
 
  </Columns>
  <EditRowStyle BackColor="#999999" />
       <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
       <HeaderStyle Height="40px" BackColor="#fafbfc" Font-Bold="True" CssClass="red" ForeColor="#656565" />
     
       <RowStyle Height="40px" BackColor="white" ForeColor="#333333" />
       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
       <SortedAscendingCellStyle BackColor="#E9E7E2" />
       <SortedAscendingHeaderStyle BackColor="#506C8C" />
       <SortedDescendingCellStyle BackColor="#FFFDF8" />
       <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
  
  
  </asp:GridView>
  </ContentTemplate>
  <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TextBox10" EventName="TextChanged"  />
                     
                       </Triggers>                
 </asp:UpdatePanel>


              </div>
               <div class="col-md-6">
              <h3>Representative report</h3>
                              
              </div>
  </div>
</div>
</div>



















      </div>



      
                        
                    </div><!--end .row-->
    </div>            


                    <div>
                    
                  </div>
                  

                  
                   
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
