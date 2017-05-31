#region " Using "
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
#endregion



public partial class Admin_Sales_report : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;

    public static int company_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (User.Identity.IsAuthenticated)
            {
                SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
                SqlDataReader dr1000;
                con1000.Open();
                dr1000 = cmd1000.ExecuteReader();
                if (dr1000.Read())
                {
                    company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                    Label2.Text = dr1000["company_name"].ToString();
                }
                con1000.Close();
            }




            getinvoiceno();
           
            BindData2();
            show_category();
            showrating();


            active();
            created();

            BindData();
         
          
        }


       
     
 
       
       

       
    }
    private void show_category()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from party where category='Winding' and  Com_Id='" + company_id + "' ORDER BY party_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "party_name";
                DropDownList1.DataValueField = "party_id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Select party", "0"));


                con.Close();
            }
            con1000.Close();
        }
    }


    
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;




                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from wendingreceipt_entry where wed_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["wed_invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-mm-yyyy");

                    DropDownList1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();

                    TextBox10.Text = dr2["Total_cones"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["nett_total"]).ToString("#,##0.00");

                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Wendingreceipt_entry_details where wed_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                con.Open();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                con.Close();

            }
            con1000.Close();
        }



    }

    protected void BindData()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Wendingreceipt_entry_details where wed_receipt_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }

    }

    protected void BindData2()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from wendingreceipt_entry where Com_Id='" + company_id + "' ORDER BY  wed_invoice asc", con);
                con.Open();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView3.DataSource = dt1;
                GridView3.DataBind();
                con.Close();
            }
            con1000.Close();
        }

    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("delete from country where country_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Country deleted successfully')", true);
                BindData();


                getinvoiceno();


                BindData2();

            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        getinvoiceno();
      
        BindData();
        show_category();
    

        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";

      

        TextBox3.Text = "";
        TextBox4.Text = "";
     

        TextBox7.Text = "";



    }
    private void active()
    {

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


        LinkButton Lnk = (LinkButton)sender;
        string name = Lnk.Text;
        Session["name"] = name;
        Response.Redirect("Account_show.aspx");


    }

    private void created()
    {

    }

    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    ImageButton img = (ImageButton)sender;
                    GridViewRow row = (GridViewRow)img.NamingContainer;
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("delete from country where country_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('country deleted successfully')", true);

                    BindData();
                    show_category();
                    getinvoiceno();
                }
            }
            con1000.Close();
        }


    }
    private void getinvoiceno()
    {

        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(wed_invoice) from wendingreceipt_entry where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label1.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label1.Text = a.ToString();
                    }
                }
                con1.Close();
            }
            con1000.Close();
        }
    }
   

    protected void LoginLink_OnClick(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/login.aspx");

    }

    protected void btnRandom_Click(object sender, EventArgs e)
    {
        Session["name1"] = "";
        Response.Redirect("~/Admin/Category_Add.aspx");
    }

    private void showcustomertype()
    {

    }
    private void showrating()
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers2(string prefixText, int count)
    {
        
        
       
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

                        using (SqlCommand cmd = new SqlCommand())
                        {


                            cmd.CommandText = "select Item_group from category where  Com_Id=@Com_Id and  " +
                            "Item_name like @Item_name + '%' ";
                            cmd.Parameters.AddWithValue("@Item_name", prefixText);
                            cmd.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd.Connection = conn;
                            conn.Open();
                            List<string> customers = new List<string>();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    customers.Add(sdr["Item_name"].ToString());
                                }
                            }
                            conn.Close();
                            return customers;
                        }
                    }
            

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {



    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {


        getinvoiceno();
     
        BindData();
        show_category();
       
        TextBox10.Text = "";
        TextBox11.Text = "";

        TextBox3.Text = "";
        TextBox4.Text = "";
      

        TextBox7.Text = "";

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                if (Convert.ToInt32(Label1.Text) > Convert.ToInt32(1))
                {
                    Label1.Text = (Convert.ToInt32(Label1.Text) - 1).ToString();
                }

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from wendingreceipt_entry where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    DropDownList1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();

                    TextBox10.Text = dr2["Total_cones"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["nett_total"]).ToString("#,##0.00");

                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Wendingreceipt_entry_details where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
                con.Open();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                con.Close();
               
            }
            con1000.Close();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                if (DropDownList1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from wendingreceipt_entry where wed_invoice='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {


                        string status = "Wending";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("update wendingreceipt_entry set date=@date,customer=@customer,address=@address,mobile_no=@mobile_no,Total_cones=@Total_cones,nett_total=@nett_total,Com_Id=@Com_Id,status=@status,value=@value where wed_invoice=@wed_invoice", con);
                        cmd.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@Total_cones", float.Parse(TextBox10.Text));
                        cmd.Parameters.AddWithValue("@nett_total", float.Parse(TextBox11.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@value", value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Wending delivery updated successfully')", true);
                        getinvoiceno();
                       
                        BindData();
                        show_category();
                      

                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";

                      

                        TextBox3.Text = "";
                        TextBox4.Text = "";
                      

                        TextBox7.Text = "";


                    }
                    else
                    {


                        string status = "Wending";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("INSERT INTO wendingreceipt_entry VALUES(@wed_invoice,@date,@customer,@address,@mobile_no,@Total_cones,@nett_total,@Com_Id,@status,@value)", con);
                        cmd.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@Total_cones", float.Parse(TextBox10.Text));
                        cmd.Parameters.AddWithValue("@nett_total", float.Parse(TextBox11.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@value", value);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Wending receipt created successfully')", true);
                        getinvoiceno();
                     
                        BindData();
                        show_category();
                    

                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";

                     

                        TextBox3.Text = "";
                        TextBox4.Text = "";
                     


                    }
                    con1.Close();

                }
            }
            con1000.Close();

        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());

                SqlConnection con21 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd21 = new SqlCommand("select max(wed_invoice) from wendingreceipt_entry where  Com_Id='" + company_id + "' ", con21);
                SqlDataReader dr21;
                con21.Open();
                dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    int value = Convert.ToInt32(dr21[0].ToString());
                    if (Convert.ToInt32(Label1.Text) < Convert.ToInt32(value + 1))
                    {
                        Label1.Text = (Convert.ToInt32(Label1.Text) + 1).ToString();
                    }
                }
                con21.Close();
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from wendingreceipt_entry where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    DropDownList1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();

                    TextBox10.Text = dr2["Total_cones"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["nett_total"]).ToString("#,##0.00");





                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from Wendingreceipt_entry_details where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                    da1.Fill(dt1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                   
                }
                else
                {

                    getinvoiceno();
                    
                    BindData();
                    show_category();
                  

                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";

                 

                    TextBox3.Text = "";
                    TextBox4.Text = "";
                  

                    TextBox7.Text = "";



                }
                con2.Close();
            }
            con1000.Close();
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from party where party_name='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox4.Text = dr2["address"].ToString();

                    TextBox7.Text = dr2["mobile_no"].ToString();


                }
                con2.Close();


            }
            con1000.Close();
        }
        BindData1();
    }



    protected void BindData1()
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from Wendingdly_product_stock where customer='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY wed_invoice asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView2.DataSource = dt1;
                GridView2.DataBind();
            }
            con1000.Close();
        }
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

        

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + row.Cells[1].Text + "' and shade_no='" + row.Cells[2].Text + "' and unit='" + row.Cells[4].Text + "'  AND Com_Id='" + company_id + "'  ", con1);






                cmd1.Parameters.AddWithValue("@qty", float.Parse(row.Cells[4].Text));

                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();

                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("delete from Wendingreceipt_entry_details where wed_invoice='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con3);

                con3.Open();
                cmd3.ExecuteNonQuery();
                con3.Close();





             
                BindData();
             

                
            }
            con1000.Close();
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        getshadeNo();
    }

    private void getshadeNo()
    {

    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {


    }

   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            con.Open();
            DropDownList DropDownList3 = (e.Row.FindControl("DropDownList3") as DropDownList);


            SqlCommand cmd = new SqlCommand("select * from unit", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            if (DropDownList3 != null)
            {
                DropDownList3.DataSource = dt;

                DropDownList3.DataTextField = "unit_name";
                DropDownList3.DataValueField = "unit_id";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("Cones", "2"));
            }


        }   
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindData();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10001 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10001 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con10001);
            SqlDataReader dr10001;
            con10001.Open();
            dr10001 = cmd10001.ExecuteReader();
            if (dr10001.Read())
            {
                company_id = Convert.ToInt32(dr10001["com_id"].ToString());
               
                string s_no = ((Label)GridView1.Rows[e.RowIndex]
                                .FindControl("lblsno")).Text;
                 string weddlyinvoice = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtwedly")).Text;
                 string itamName = ((TextBox)GridView1.Rows[e.RowIndex]
                                     .FindControl("txtitemName")).Text;
                string shadeno = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtshadeno")).Text;
                

               
                string unit = ((DropDownList)GridView1.Rows[e.RowIndex]
                                   .FindControl("DropDownList3")).SelectedItem.Text;

              
                string boxes = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtboxes")).Text;
                string cones = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtcones")).Text;

                string gross_wt = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txgrosswt")).Text;
                string nett_wt = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtnetwt")).Text;








                if (DropDownList1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party')", true);
                }
                else
                {

                    SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd10 = new SqlCommand("select * from Wendingreceipt_entry_details where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con10);
                    cmd10.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                    cmd10.Parameters.AddWithValue("@s_no", s_no);
                    SqlDataReader dr10;
                    con10.Open();
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.Read())
                    {
                        string itemname1 = dr10["item_name"].ToString();
                        string shadeno1 = dr10["shade_no"].ToString();
                        string unit1 = dr10["unit"].ToString();
                    
                        string wendingdly = dr10["wed_invoice"].ToString();
                        string cones1 = dr10["cones"].ToString();
                        string gross_wt1 = dr10["gross_wt"].ToString();
                        string nett_wt1 = dr10["nett_wt"].ToString();
                        SqlConnection con21 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd21 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname1 + "' and shade_no='" + shadeno1 + "' and unit='" + unit1 + "'  AND Com_Id='" + company_id + "'  ", con21);

                        cmd21.Parameters.AddWithValue("@qty", cones1);

                        con21.Open();
                        cmd21.ExecuteNonQuery();
                        con21.Close();


                        SqlConnection con22 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd22 = new SqlCommand("update Wendingdly_product_stock set Cones=Cones+@Cones,Gross_Wt=Gross_Wt+@Gross_Wt,Net_Wt=Net_Wt+@Net_Wt,Com_Id=@Com_Id where wed_invoice='" + wendingdly + "' and shade_no='" + shadeno1 + "' AND Com_Id='" + company_id + "'  ", con22);



                        cmd22.Parameters.AddWithValue("@Cones", cones1);
                        cmd22.Parameters.AddWithValue("@Gross_Wt", gross_wt1);
                        cmd22.Parameters.AddWithValue("@Net_Wt", nett_wt1);
                        cmd22.Parameters.AddWithValue("@Com_Id", company_id);
                        con22.Open();
                        cmd22.ExecuteNonQuery();
                        con22.Close();
                    }
                    con10.Close();




                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update Wendingreceipt_entry_details set customer=@customer,item_name=@item_name,shade_no=@shade_no,boxes=@boxes,unit=@unit,cones=@cones,gross_wt=@gross_wt,nett_wt=@nett_wt,Com_Id=@Com_Id where wed_receipt_invoice=@wed_receipt_invoice and s_no=@s_no", con);
                    cmd.Parameters.AddWithValue("@wed_invoice", weddlyinvoice);
                    cmd.Parameters.AddWithValue("@wed_receipt_invoice", Label1.Text);
                    cmd.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@s_no", s_no);
                    cmd.Parameters.AddWithValue("@item_name", itamName);
                    cmd.Parameters.AddWithValue("@shade_no", shadeno);
                    cmd.Parameters.AddWithValue("@boxes", boxes);
                    cmd.Parameters.AddWithValue("@unit", unit);
                    cmd.Parameters.AddWithValue("@cones", cones);
                    cmd.Parameters.AddWithValue("@gross_wt", gross_wt);
                    cmd.Parameters.AddWithValue("@nett_wt", nett_wt);
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty+@qty,Com_Id=@Com_Id where item_name='" + itamName + "' and shade_no='" + shadeno + "' and unit='" + unit+ "'  AND Com_Id='" + company_id + "'  ", con1);

                  

                    cmd1.Parameters.AddWithValue("@qty", cones);
                    cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                    con1.Open();
                    cmd1.ExecuteNonQuery();
                    con1.Close();

                    SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd2 = new SqlCommand("update Wendingdly_product_stock set Cones=Cones-@Cones,Gross_Wt=Gross_Wt-@Gross_Wt,Net_Wt=Net_Wt-@Net_Wt,Com_Id=@Com_Id where wed_invoice='" + weddlyinvoice + "' and shade_no='" + shadeno + "' AND Com_Id='" + company_id + "'  ", con2);



                    cmd2.Parameters.AddWithValue("@Cones", cones);
                    cmd2.Parameters.AddWithValue("@Gross_Wt", gross_wt);
                    cmd2.Parameters.AddWithValue("@Net_Wt", nett_wt);
                    cmd2.Parameters.AddWithValue("@Com_Id", company_id);
                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();

                    GridView1.EditIndex = -1;
                    BindData();
                  
                }
            }
            con10001.Close();

        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void lnkRemove_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con10001 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10001 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con10001);
            SqlDataReader dr10001;
            con10001.Open();
            dr10001 = cmd10001.ExecuteReader();
            if (dr10001.Read())
            {
                company_id = Convert.ToInt32(dr10001["com_id"].ToString());

                LinkButton lnkRemove = (LinkButton)sender;

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);


                SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd10 = new SqlCommand("select * from Wendingreceipt_entry_details where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con10);
                cmd10.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                cmd10.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);
                SqlDataReader dr10;
                con10.Open();
                dr10 = cmd10.ExecuteReader();
                if (dr10.Read())
                {
                    string itemname1 = dr10["item_name"].ToString();
                    string shadeno1 = dr10["shade_no"].ToString();
                    string unit1 = dr10["unit"].ToString();

                    string wendingdly = dr10["wed_invoice"].ToString();
                    string cones1 = dr10["cones"].ToString();
                    string gross_wt1 = dr10["gross_wt"].ToString();
                    string nett_wt1 = dr10["nett_wt"].ToString();
                    SqlConnection con21 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd21 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname1 + "' and shade_no='" + shadeno1 + "' and unit='" + unit1 + "'  AND Com_Id='" + company_id + "'  ", con21);

                    cmd21.Parameters.AddWithValue("@qty", cones1);

                    con21.Open();
                    cmd21.ExecuteNonQuery();
                    con21.Close();


                    SqlConnection con22 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd22 = new SqlCommand("update Wendingdly_product_stock set Cones=Cones+@Cones,Gross_Wt=Gross_Wt+@Gross_Wt,Net_Wt=Net_Wt+@Net_Wt,Com_Id=@Com_Id where wed_invoice='" + wendingdly + "' and shade_no='" + shadeno1 + "' AND Com_Id='" + company_id + "'  ", con22);



                    cmd22.Parameters.AddWithValue("@Cones", cones1);
                    cmd22.Parameters.AddWithValue("@Gross_Wt", gross_wt1);
                    cmd22.Parameters.AddWithValue("@Net_Wt", nett_wt1);
                    cmd22.Parameters.AddWithValue("@Com_Id", company_id);
                    con22.Open();
                    cmd22.ExecuteNonQuery();
                    con22.Close();
                }
                con10.Close();

                SqlCommand cmd = new SqlCommand("delete from Wendingreceipt_entry_details where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con);
                cmd.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                cmd.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
               



            }
            con10001.Close();
        }
    }

    
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlConnection con1000 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd1000 = new SqlCommand("select * from user_details where company_name='" + User.Identity.Name + "'", con1000);
            SqlDataReader dr1000;
            con1000.Open();
            dr1000 = cmd1000.ExecuteReader();
            if (dr1000.Read())
            {
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                foreach (GridViewRow gvrow in GridView2.Rows)
                {
                    //Finiding checkbox control in gridview for particular row
                    CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox2");
                    //Condition to check checkbox selected or not
                    if (chkdelete.Checked)
                    {
                        //Getting UserId of particular row using datakey value
                        int usrid = Convert.ToInt32(gvrow.Cells[1].Text);
                    
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("select * from Wendingdly_product_stock where no='" + usrid + "' and  Com_Id='" + company_id + "' ORDER BY no asc", con);
                        con.Open();
                        SqlDataReader dr1;
                        dr1 = cmd.ExecuteReader();
                        if (dr1.Read())
                        {
                            string weddly = dr1["wed_invoice"].ToString();
                            string item_name = dr1["item_name"].ToString();
                            string s_no = dr1["s_no"].ToString();
                            string shade_no = dr1["shade_no"].ToString();

                            int a1 = 0;
                        float a = 0;
                        string name = "";
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("insert into Wendingreceipt_entry_details values(@wed_invoice,@wed_receipt_invoice,@customer,@s_no,@item_name,@shade_no,@boxes,@unit,@cones,@gross_wt,@nett_wt,@Com_Id)", con1);
                        cmd1.Parameters.AddWithValue("@wed_invoice", weddly);
                        cmd1.Parameters.AddWithValue("@wed_receipt_invoice", Label1.Text);
                        cmd1.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@s_no",s_no);
                        cmd1.Parameters.AddWithValue("@item_name", item_name);
                        cmd1.Parameters.AddWithValue("@shade_no", shade_no);
                        cmd1.Parameters.AddWithValue("@boxes", a);
                        cmd1.Parameters.AddWithValue("@unit", name);
                        cmd1.Parameters.AddWithValue("@cones", a);
                        cmd1.Parameters.AddWithValue("@gross_wt", a);
                        cmd1.Parameters.AddWithValue("@nett_wt", a);
                        cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();
                        BindData();
                            }
                        con.Close();
                        
                    }
                   
                }
              
              
            }
            con1000.Close();
        }

    }
    protected void txtcones_TextChanged(object sender, EventArgs e)
    {
        string rate = ((TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtcones")).Text;
       
        TextBox total = ((TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtnetwt"));
        float nett_wt = float.Parse("1.5");
        float total_amount = float.Parse(rate) * nett_wt;
        total.Text = total_amount.ToString();
    }
}