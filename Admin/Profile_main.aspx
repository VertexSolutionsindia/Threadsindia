﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_main.aspx.cs" Inherits="Profile_main" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
        <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
        <title>Dream Garments</title>

        <!-- Bootstrap -->
        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
        <link href="css/waves.min.css" type="text/css" rel="stylesheet">
        <!--        <link rel="stylesheet" href="css/nanoscroller.css">-->
        <link href="css/menu.css" type="text/css" rel="stylesheet">
        <link href="css/style.css" type="text/css" rel="stylesheet">
          <link href="css1/Profile_maincss.css" type="text/css" rel="stylesheet">
        <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
        <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
 
         <script>

             $(document).ready(function () {
                 $(".open1").click(function () {
                     $(".close1").toggle();
                 });
             });

             $(document).ready(function () {
                 $(".open2").click(function () {
                     $(".close2").toggle();
                 });
             });



             $(document).ready(function () {
                 $(".open3").click(function () {
                     $(".close3").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open4").click(function () {
                     $(".close4").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open5").click(function () {
                     $(".close5").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open6").click(function () {
                     $(".close6").toggle();
                 });
             });

             $(document).ready(function () {
                 $(".open7").click(function () {
                     $(".close7").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open8").click(function () {
                     $(".close8").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open9").click(function () {
                     $(".close9").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open14").click(function () {
                     $(".close14").toggle();
                 });
             });
             $(document).ready(function () {
                 $(".open13").click(function () {
                     $(".close13").toggle();
                 });
             });
   </script>
 
   
    <script>




        function validate7() {
            var result = confirm('Are you sure you want to delete selected engineer name Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function validate6() {
            var result = confirm('Are you sure you want to delete selected Category Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function validate5() {
            var result = confirm('Are you sure you want to delete selected sub Category Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

        function validate4() {
            var result = confirm('Are you sure you want to delete selected unit Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function validate2() {
            var result = confirm('Are you sure you want to delete selected units?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
        function validate3() {
            var result = confirm('Are you sure you want to delete selected customer details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
   
 
   <style>
        .modelbackground
    {
        background-color:gray;
        filter:alpha(opacity=80);
        opacity:0.8;
        Z-index:10000;
        
    }
     .modelbackground
    {
        background-color:gray;
        filter:alpha(opacity=80);
        opacity:0.8;
        Z-index:10000;
        
    }
       .panelx
       {
      
      
       margin-top:-170px;
       width:50%;
       height:220px;
       border-radius:10px;
       overflow:scroll;
       
      
       }
         .panelx1
       {
      
      
       margin-top:-70px;
       width:50%;
       height:500px;
       border-radius:10px;
       overflow:scroll;
       
      
       }
       .panely
       {
      
      
       
       margin-top:-140px;
       width:50%;
       height:200px;
       border-radius:10px;
       overflow:scroll;
      
      
       }
        .panelz
       {
      
      
       
       margin-top:-140px;
       width:50%;
       height:250px;
       border-radius:10px;
       overflow:scroll;
      
      
       }
    .close1
   {
        display:none;
   }
     .close2
   {
        display:none;
   }
   .close3
   {
         display:none;
   }
    .close4
   {
       display:none;
   }
    .close5
   {
       display:none;
   }
     .close6
   {
       display:none;
   }
     .close7
   {
       display:none;
   }
     .close8
   {
       display:none;
   }
     .close9
   {
       display:none;
   }
     .close14
   {
       display:none;
   }
    .close13
   {
        display:none;
   }
   </style>
    <style>
        .hiddencolumn
        {
            display:none;
        }
       
    .UNDER
    {
        text-decoration:NONE;
        font-size:15PX;
        color:White;
    }
    .modelbackground
    {
        background-color:gray;
        filter:alpha(opacity=80);
        opacity:0.8;
        Z-index:10000;
        
    }
   
    
        .style2
        {
            width: 568px;
        }
   .menudesign
   {
       background-color:Gray;
       filter:alpha(opacity=80);
       opacity:0.8;
       z-index:10000;
       
   }
   .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 120px;
        overflow:auto;
        background-color: #FFFFFF;     
        } 
        .listItem {
        color: #191919;
        } 
        .itemHighlighted {
        background-color: #ADD6FF;       
        }

    
    </style>
    <script>
        function validate() {
            var result = confirm('Are you sure you want to delete selected peoduct(s) Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }

        function validate1() {
            var result = confirm('Are you sure you want to delete selected category Details?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
     <script type="text/javascript">
         function ShowCurrentTime() {
             var dt = new Date();
             document.getElementById("lblTime").innerHTML = dt.toLocaleTimeString();
             window.setTimeout("ShowCurrentTime()", 1000); // Here 1000(milliseconds) means one 1 Sec  
         }
</script>
    </head>
    <body>
        <!-- Static navbar -->
 <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        
</asp:ToolkitScriptManager>
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
                    <a class="navbar-brand" href="#">Dream Garments</a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li class="dropdown">
                           
                         <li class="dropdown">
                            <a href="#" class="dropdown-toggle button-wave" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
<asp:Button ID="Button4" runat="server"  Text="ADD" class="btn btn-primary"></asp:Button> <span aria-hidden="true" class="glyphicon glyphicon-plus"></span> </a>
                            <ul class="dropdown-menu">
                                <li><a href="Main.aspx"><i class="fa fa-home fa-2x" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Category</a></li>
                                   <li role="separator" class="divider"></li>
                                <li><a href="Sub_category.aspx"><i class="fa fa-hdd-o" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Sub Category </a></li>
                                 <li role="separator" class="divider"></li>
                                <li><a href="Product_entry.aspx"><i class="fa fa-building" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Product Entry </a></li>
                                   <li role="separator" class="divider"></li>
                                <li><a href="Purchase_entry.aspx"><i class="fa fa-check-square-o" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Purchase Entry </a></li>
                                  <li role="separator" class="divider"></li>
                                <li><a href="Stock_Inventory.aspx"><i class="fa fa-edit"></i> &nbsp;&nbsp&nbsp;Stock / Inventory </a></li>
                                 <li role="separator" class="divider"></li>
                                <li><a href="Customer-Entry.aspx"><i class="fa fa-lightbulb-o" aria-hidden="true"></i>  &nbsp;&nbsp&nbsp;New Customer Entry</a></li>

                                <li role="separator" class="divider"></li>
                                <li><a href="Vendor.aspx"><i class="fa fa-thumbs-o-up" aria-hidden="true"></i> &nbsp;&nbsp&nbsp;Supplier Entry </a></li>
                               
                                  <li role="separator" class="divider"></li>
                                <li><a href="Department-Entry.aspx"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;New Department Entry  </a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="Sales_entry.aspx"><i class="fa fa-ticket" aria-hidden="true"></i>&nbsp;&nbsp&nbsp;Sales Entry </a></li>
                               
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
                                    <li><a href="">Compnay</a></li>
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
                                <h2>Profile  <small></small></h2>
                             
  



                                
                            </div>
                            
                        </div>
                    </div><!-- end .page title-->
                       

                          <div class="row">
                    <div class="col-md-12">


                    <div class="row see"  >

                    <br />
                    
                    <div class="container">
 
  <div class="panel panel-default">
  <div class="panel-body">
    <div class="col-md-6">
                 <div class="panel-body">
                           <div class="form-horizontal">
                               <br />
                               <div class="form-group"><label class="col-lg-3 control-label">Name</label>

                                    <div class="col-lg-9"><asp:TextBox ID="TextBox2" runat="server" class="form-control input-x2 dropbox"></asp:TextBox> 
                                    </div>
                                </div>
                                <div class="form-group"><label class="col-lg-3 control-label">Designation</label>

                                    <div class="col-lg-9"><asp:DropDownList ID="DropDownList1" runat="server" CssClass="dropbox"></asp:DropDownList></asp:TextBox></div>
                                </div>

                                  <div class="form-group"><label class="col-lg-3 control-label">Email</label>

                                    <div class="col-lg-9"><asp:TextBox ID="TextBox4" runat="server" class="form-control input-x2 dropbox"></asp:TextBox></div>
                                </div>

                                 <div class="form-group"><label class="col-lg-3 control-label">Mobile</label>

                                    <div class="col-lg-9"><asp:TextBox ID="TextBox1" runat="server" class="form-control input-x2 dropbox"></asp:TextBox></div>
                                </div>

                                  
                                 
                               
                            </div>
                      </div>

    </div>
    
   </div>
 </div>
    
     <div class="col-md-12" style="margin-top:-10px;">


   
 


                                    <div class="open1 "  style="font-size:20px;  border: 1px solid #cccccc; padding:7px; margin:10px;color: #3a5a7a; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;">
                                         Change Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span   class="fa fa-sort-desc" aria-hidden="true"></span></div>

                                         <div class="close1" >
                                             <div  style="clear:both;">
                                               
                                               
                                               <div class="container">
 
  <div class="panel panel-default">
  <div class="panel-body">
    <div class="col-md-6">
                 <div class="panel-body">
                           <div class="form-horizontal">
                               <br />
                               <div class="form-group"><label class="col-lg-3 control-label">Current Password</label>

                                    <div class="col-lg-9">
                                    
                                      <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                    <ContentTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" class="form-control input-x2 dropbox"></asp:TextBox> 
                                     </ContentTemplate>
                                     <Triggers>
        
           <asp:AsyncPostBackTrigger ControlID = "Button1" EventName="Click" />
        </Triggers>
                             </asp:UpdatePanel>    

                                    </div>
                                </div>
                              

                                  <div class="form-group"><label class="col-lg-3 control-label">New Password</label>

                                    <div class="col-lg-9">
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" class="form-control input-x2 dropbox"></asp:TextBox>
                                     </ContentTemplate>
                                     <Triggers>
        
           <asp:AsyncPostBackTrigger ControlID = "Button1" EventName="Click" />
        </Triggers>
                             </asp:UpdatePanel>    
                                    
                                    </div>
                                </div>

                                 <div class="form-group"><label class="col-lg-3 control-label">Confirm Password</label>

                                    <div class="col-lg-9">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server"  class="form-control input-x2 dropbox"></asp:TextBox>
                                     </ContentTemplate>
                                     <Triggers>
        
           <asp:AsyncPostBackTrigger ControlID = "Button1" EventName="Click"  />
        </Triggers>
                             </asp:UpdatePanel>    
                                    
                                    </div>
                                </div>

                                  
                                 
                               
                            </div>
                              <asp:UpdatePanel ID="UpdatePanel41" runat="server" >
                    <ContentTemplate> 
                            <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click"></asp:Button>

                            </ContentTemplate>
                             </asp:UpdatePanel>    
                      </div>

    </div>
    
   </div>
 </div>
                                               
                                               
                                               </div>
                                          </div>
                                     </div>   
                                     
                                     <div class="col-md-12" style="margin-top:-16px;">
                                     <div class="open2 "  style="font-size:20px;  border: 1px solid #cccccc; padding:7px; margin:10px;color: #3a5a7a; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;">
                                         Smtp Settings&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span   class="fa fa-sort-desc" aria-hidden="true"></span></div>

                                         <div class="close2" >
                                             <div  style="clear:both;">
                                               
                                               
                                               
                                               
                                               
                                               </div>
                                          </div>
                                     </div>     

                                     <div class="col-md-12" style="margin-top:-16px;">
                                     <div class="open3 "  style="font-size:20px;  border: 1px solid #cccccc; padding:7px; margin:10px;color: #3a5a7a; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;">
                                         Mail signature&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span   class="fa fa-sort-desc" aria-hidden="true"></span></div>

                                         <div class="close3" >
                                             <div  style="clear:both;">
                                               
                                               
                                               
                                               
                                               
                                               </div>
                                          </div>
                                     </div>     

                                     <div class="col-md-12" style="margin-top:-16px;">
                                     <div class="open4 "  style="font-size:20px;  border: 1px solid #cccccc; padding:7px; margin:10px;color: #3a5a7a; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;">
                                         User Access Mangement&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span   class="fa fa-sort-desc" aria-hidden="true"></span></div>

                                         <div class="close4" >
                                             <div  style="clear:both;">
                                               
                                               
                                               
                                               
                                               
                                               </div>
                                          </div>
                                     </div>     

                                      <div class="col-md-12" style="margin-top:-16px;">
                                     <div class="open5 "  style="font-size:20px;  border: 1px solid #cccccc; padding:7px; margin:10px;color: #3a5a7a; font-family: 'Open Sans',"HelveticaNeue", "Helvetica Neue", Helvetica, Arial,sans-serif;">
                                         User Settings&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span   class="fa fa-sort-desc" aria-hidden="true"></span></div>

                                         <div class="close5" >
                                             <div  style="clear:both;">
                                               
                                               
                                               
                                               
                                               
                                               </div>
                                          </div>
                                     </div>     


                                    


    </div>
 
    </div>
  
                
                
               
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  </div></div>
                  
                  
                  
                  
                  
                  
                  
                  
                  
                  
              

                                    
                                 
                         
                   
                        
                      


                        
                    </div><!--end .row-->

                  

                   
                           
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

