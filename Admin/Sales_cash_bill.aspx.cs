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
using System.Globalization;
#endregion




public partial class Admin_Sales_entry : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;
    float tot2 = 0;
    float tot3 = 0;
    float tot4 = 0;
    float tot5 = 0;

    public static int company_id = 0;
    public static string item_name1 = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showtax();
            ComboBox1.Focus();
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
                   TextBox20.Text =dr1000["com_id"].ToString();
                    Label2.Text = dr1000["company_name"].ToString();
                }
                con1000.Close();
            }
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from currentfinancialyear where no='1'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                Label4.Text = dr10["financial_year"].ToString();

            }
            con10.Close();
            DateTime date = DateTime.Now;
            TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

         
            getinvoiceno();
            getno();
            getinvoiceno1();
            BindData2();
            show_category();
            showrating();
            getno();

            active();
            created();

            show_employee();
         
         
            BindData();
            showcustomer();
            showitem();
            showunit();
          
           
        }

       
    }





    private void showcustomer()
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
                SqlCommand cmd = new SqlCommand("Select * from party where Com_Id='"+company_id+"' and category='Customer'  ORDER BY party_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox1.DataSource = ds;
                ComboBox1.DataTextField = "party_name";
                ComboBox1.DataValueField = "party_id";
                ComboBox1.DataBind();
                ComboBox1.Items.Insert(0, new ListItem("Select party", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void showtax()
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
                SqlCommand cmd = new SqlCommand("Select * from Tax_entry where Com_Id='" + company_id + "'  ORDER BY Tax_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
        
                //DropDownList5.DataSource = ds;
                //DropDownList5.DataTextField = "Tax_name";
                //DropDownList5.DataValueField = "Tax_id";
                //DropDownList5.DataBind();
             


                con.Close();
            }
            con1000.Close();
        }
    }
    private void showunit()
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
                SqlCommand cmd = new SqlCommand("Select * from unit where Com_Id='"+company_id+"'  ORDER BY unit_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox4.DataSource = ds;
                ComboBox4.DataTextField = "unit_name";
                ComboBox4.DataValueField = "unit_id";
                ComboBox4.DataBind();
                ComboBox4.Items.Insert(0, new ListItem("Select unit", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void showitem()
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
        SqlCommand cmd = new SqlCommand("Select * from item_master where Com_Id='" + company_id + "'  ORDER BY item_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        ComboBox2.DataSource = ds;
        ComboBox2.DataTextField = "item_name";
        ComboBox2.DataValueField = "item_id";
        ComboBox2.DataBind();
        ComboBox2.Items.Insert(0, new ListItem("Select item", "1"));


        con.Close();
                }
                con1000.Close();
    }
    }
    private void show_category()
    {
       
    }

    private void show_employee()
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
                SqlCommand cmd = new SqlCommand("Select * from employee_master where Com_Id='" + company_id + "' ORDER BY emp_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox5.DataSource = ds;
                ComboBox5.DataTextField = "emp_name";
                ComboBox5.DataValueField = "emp_id";
                ComboBox5.DataBind();
                ComboBox5.Items.Insert(0, new ListItem("Select Employee", "0"));
                ComboBox6.DataSource = ds;
                ComboBox6.DataTextField = "emp_name";
                ComboBox6.DataValueField = "emp_id";
                ComboBox6.DataBind();
                ComboBox6.Items.Insert(0, new ListItem("Select Employee", "0"));

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
                SqlCommand cmd2 = new SqlCommand("select * from cashbill_entry where invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='"+Label4.Text+"'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("MM/dd/yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    ComboBox5.SelectedItem.Text = dr2["sales_man"].ToString();
                    TextBox10.Text = dr2["Total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                   // DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
                    TextBox15.Text = dr2["prepared_by"].ToString();
                    ComboBox6.SelectedItem.Text = dr2["delivery_person"].ToString();
                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                getinvoiceno1();

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
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }

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
                SqlCommand CMD = new SqlCommand("select * from customerpo_details where customer='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "' and qty!=0 and year='" + Label4.Text + "' ORDER BY po_invoice asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView2.DataSource = dt1;
                GridView2.DataBind();
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
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  invoice asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView3.DataSource = dt1;
                GridView3.DataBind();
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
                getno();
               

                BindData2();

            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        getno();
        getinvoiceno1();

        show_category();
        showitem();
        showcustomer();
        showunit();
        show_employee();
        TextBox10.Text = "";
        TextBox11.Text = "";

        TextBox13.Text = "";
        TextBox2.Text = "";
        TextBox1.Text = "";
       
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        TextBox12.Text = "";
        TextBox15.Text = "";
        TextBox17.Text = "";
        TextBox16.Text = "";
        TextBox27.Text = "";
        TextBox28.Text = "";
        TextBox29.Text = "";
        TextBox30.Text = "";
        TextBox31.Text = "";
        TextBox32.Text = "";
        TextBox33.Text = "";
        TextBox34.Text = "";
        TextBox35.Text = "";
        DateTime date = DateTime.Now;
        TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
        BindData();

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

        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from cashbill_entry where invoice='" + name + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["invoice"].ToString();
            TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

            ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
            TextBox4.Text = dr2["address"].ToString();
            TextBox7.Text = dr2["mobile_no"].ToString();
            ComboBox5.SelectedItem.Text = dr2["sales_man"].ToString();
            TextBox10.Text = dr2["Totl_Amt_Before_Tax"].ToString();
            TextBox11.Text = Convert.ToDecimal(dr2["Total_CGST"]).ToString("#,##0.00");
            TextBox8.Text = Convert.ToDecimal(dr2["Total_SGST"]).ToString("#,##0.00");
            TextBox14.Text = Convert.ToDecimal(dr2["Total_IGST"]).ToString("#,##0.00");
            TextBox12.Text = Convert.ToDecimal(dr2["Tax_Amt_GST"]).ToString("#,##0.00");
            TextBox9.Text = Convert.ToDecimal(dr2["Tol_Amt_After_Tax"]).ToString("#,##0.00");
            TextBox15.Text = dr2["prepared_by"].ToString();
            ComboBox6.SelectedItem.Text = dr2["delivery_person"].ToString();

            TextBox29.Text = dr2["Gstno1"].ToString();
            TextBox27.Text = dr2["state1"].ToString();
            TextBox28.Text = dr2["stateCode1"].ToString();
            TextBox33.Text = dr2["Gstno2"].ToString();
            TextBox34.Text = dr2["state2"].ToString();
            TextBox35.Text = dr2["stateCode2"].ToString();
            TextBox30.Text = dr2["Name2"].ToString();
            TextBox31.Text = dr2["Address_2"].ToString();
            TextBox32.Text = dr2["MobioleNo_2"].ToString();


        }
        con2.Close();


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where invoice='" + name + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        getinvoiceno1();


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
                    getno();
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
                string company_name = dr1000["company_name"].ToString();
                string name = company_name.Substring(0, 2);
                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(convert(int,SubString(invoice,PATINDEX('%[0-9]%',invoice),Len(invoice)))) from cashbill_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label1.Text = name+"-CH-"+"1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        TextBox19.Text = name + "-CH-" + a.ToString();
                        a = a + 1;
                        Label1.Text = name + "-CH-" + a.ToString();
                    }
                }
                con1.Close();
            }
            con1000.Close();
        }
    }
    private void getno()
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
                string company_name = dr1000["company_name"].ToString();
                string name = company_name.Substring(0, 2);
                int a;

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(no) from cashbill_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label5.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                     
                        a = a + 1;
                        Label5.Text =  a.ToString();
                    }
                }
                con1.Close();
            }
            con1000.Close();
        }
    }
    private void getinvoiceno1()
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
                string query = "Select Max(s_no) from cashbill_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label3.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label3.Text = a.ToString();
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
        float total = float.Parse(TextBox37.Text);
        float TaxRate = float.Parse(TextBox3.Text);
        float CGST_amount = (total * TaxRate / 100);
        TextBox21.Text = Convert.ToDecimal(CGST_amount).ToString("#,##0.00");
        float tax = float.Parse(TextBox21.Text);
        float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax)));
        TextBox26.Text = Convert.ToDecimal(netvalue).ToString("#,##0.00");


    }
    protected void TextBo22_TextChanged(object sender, EventArgs e)
    {
        float total = float.Parse(TextBox37.Text);
        float TaxRate = float.Parse(TextBox22.Text);
        float SGST_amount = (total * TaxRate / 100);
        TextBox23.Text = Convert.ToDecimal(SGST_amount).ToString("#,##0.00");

        float tax = float.Parse(TextBox21.Text);
        float tax1 = float.Parse(TextBox23.Text);
        float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax + tax1)));
        TextBox26.Text = Convert.ToDecimal(netvalue).ToString("#,##0.00");


    }
    protected void TextBo24_TextChanged(object sender, EventArgs e)
    {
        float total = float.Parse(TextBox37.Text);
        float TaxRate = float.Parse(TextBox24.Text);
        float IGST_amount = (total * TaxRate / 100);
        TextBox25.Text = Convert.ToDecimal(IGST_amount).ToString("#,##0.00");

        float tax = float.Parse(TextBox21.Text);
        float tax1 = float.Parse(TextBox23.Text);
        float tax2 = float.Parse(TextBox25.Text);
        float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax + tax1 + tax2)));
        TextBox26.Text = Convert.ToDecimal(netvalue).ToString("#,##0.00");
    }

    protected void TextBox36_TextChanged(object sender, EventArgs e)
    {
        float total = float.Parse(TextBox6.Text);
        float tax = float.Parse(TextBox36.Text);
        float tax_amount = (total * tax / 100);
        float netvalue = float.Parse(string.Format("{0:0.00}", (total - tax_amount)));
        TextBox37.Text = Convert.ToDecimal(netvalue).ToString("#,##0.00");


    }

    protected void TextBox21_TextChanged(object sender, EventArgs e)
    {



    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {


        getinvoiceno();
        getno();
        getinvoiceno1();
     
        show_category();
        showitem();
        showcustomer();
        showunit();
        show_employee();
        TextBox10.Text = "";
        TextBox11.Text = "";
    
         TextBox13.Text = "";
        TextBox2.Text = "";
        TextBox1.Text = "";
    
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        TextBox12.Text = "";
        TextBox15.Text = "";
        TextBox17.Text = "";
        TextBox16.Text = "";
        TextBox27.Text = "";
        TextBox28.Text = "";
        TextBox29.Text = "";
        TextBox30.Text = "";
        TextBox31.Text = "";
        TextBox32.Text = "";
        TextBox33.Text = "";
        TextBox34.Text = "";
        TextBox35.Text = "";
        DateTime date = DateTime.Now;
        TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
        BindData();
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
                if (ComboBox1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party name')", true);
                }

                if (ComboBox5.SelectedItem.Text == "Select Employee")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select sales man')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from cashbill_entry where invoice='" + Label1.Text + "' AND Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                          SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd10 = new SqlCommand("select * from employee_master where emp_name='" + ComboBox5.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                float comm1 = float.Parse (dr10["com_amount"].ToString());
                string status = "Cash Bill";
                float value = 0;
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                SqlCommand cmd = new SqlCommand("update cashbill_entry set date=@date,customer=@customer,address=@address,mobile_no=@mobile_no,Com_Id=@Com_Id,status=@status,value=@value,sales_man=@sales_man,prepared_by=@prepared_by,delivery_person=@delivery_person,com_amount=@com_amount,Gstno1=@Gstno1,state1=@state1,stateCode1=@stateCode1,Gstno2=@Gstno2,state2=@state2,stateCode2=@stateCode2,Name2=@Name2,Address_2=@Address_2,MobioleNo_2=@MobioleNo_2,Totl_Amt_Before_Tax=@Totl_Amt_Before_Tax,Total_CGST=@Total_CGST,Total_SGST=@Total_SGST,Total_IGST=@Total_IGST,Tax_Amt_GST=@Tax_Amt_GST,Tol_Amt_After_Tax=@Tol_Amt_After_Tax where invoice=@invoice and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con);
              
                cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox13.Text).ToString("MM-dd-yyyy"));
                cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);


                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@sales_man", ComboBox5.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@prepared_by", TextBox15.Text);
                cmd.Parameters.AddWithValue("@delivery_person", ComboBox6.SelectedItem.Text);
                float totlcom = comm1 * float.Parse(TextBox10.Text);
                cmd.Parameters.AddWithValue("@com_amount", totlcom);
                cmd.Parameters.AddWithValue("@Gstno1", TextBox29.Text);
                cmd.Parameters.AddWithValue("@state1", TextBox27.Text);
                cmd.Parameters.AddWithValue("@stateCode1", TextBox28.Text);
                cmd.Parameters.AddWithValue("@Gstno2", TextBox33.Text);
                cmd.Parameters.AddWithValue("@state2", TextBox34.Text);
                cmd.Parameters.AddWithValue("@stateCode2", TextBox35.Text);
                cmd.Parameters.AddWithValue("@Name2", TextBox30.Text);
                cmd.Parameters.AddWithValue("@Address_2", TextBox31.Text);
                cmd.Parameters.AddWithValue("@MobioleNo_2", TextBox32.Text);
                cmd.Parameters.AddWithValue("@Totl_Amt_Before_Tax", float.Parse(TextBox10.Text));
                cmd.Parameters.AddWithValue("@Total_CGST", float.Parse(TextBox11.Text));
                cmd.Parameters.AddWithValue("@Total_SGST", float.Parse(TextBox8.Text));
                cmd.Parameters.AddWithValue("@Total_IGST", float.Parse(TextBox14.Text));
                cmd.Parameters.AddWithValue("@Tax_Amt_GST", float.Parse(TextBox12.Text));
                cmd.Parameters.AddWithValue("@Tol_Amt_After_Tax", float.Parse(TextBox9.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Cash bill updated successfully')", true);
                getinvoiceno();
                getno();
                getinvoiceno1();
                BindData();
                show_category();
                BindData2();
               
            
                show_employee();
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox13.Text = "";

                TextBox2.Text = "";
                TextBox1.Text = "";
               
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox8.Text = "";
                TextBox9.Text = "";
                TextBox14.Text = "";
                TextBox12.Text = "";
                TextBox17.Text = "";
                TextBox16.Text = "";
                TextBox27.Text = "";
                TextBox28.Text = "";
                TextBox29.Text = "";
                TextBox30.Text = "";
                TextBox31.Text = "";
                TextBox32.Text = "";
                TextBox33.Text = "";
                TextBox34.Text = "";
                TextBox35.Text = "";
            }
            con10.Close();

                    }
                    else
                    {


                        SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd10 = new SqlCommand("select * from employee_master where emp_name='" + ComboBox5.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                float comm = float.Parse(dr10["com_amount"].ToString());

                string status = "Cash Bill";
                float value = 0;
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                SqlCommand cmd = new SqlCommand("INSERT INTO cashbill_entry VALUES(@no,@invoice,@date,@customer,@address,@mobile_no,@Com_Id,@status,@value,@sales_man,@prepared_by,@delivery_person,@com_amount,@year,@Gstno1,@state1,@stateCode1,@Gstno2,@state2,@stateCode2,@Name2,@Address_2,@MobioleNo_2,@Totl_Amt_Before_Tax,@Total_CGST,@Total_SGST,@Total_IGST,@Tax_Amt_GST,@Tol_Amt_After_Tax)", con);
                cmd.Parameters.AddWithValue("@no", Label5.Text);
                cmd.Parameters.AddWithValue("invoice", Label1.Text);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox13.Text).ToString("MM-dd-yyyy"));
                cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);

                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@sales_man", ComboBox5.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@prepared_by", TextBox15.Text);
                cmd.Parameters.AddWithValue("@delivery_person", ComboBox5.SelectedItem.Text);
                float totlcom = comm * float.Parse(TextBox10.Text);
                cmd.Parameters.AddWithValue("@com_amount", totlcom);
                cmd.Parameters.AddWithValue("@year", Label4.Text);
                cmd.Parameters.AddWithValue("@Gstno1", TextBox29.Text);
                cmd.Parameters.AddWithValue("@state1", TextBox27.Text);
                cmd.Parameters.AddWithValue("@stateCode1", TextBox28.Text);
                cmd.Parameters.AddWithValue("@Gstno2", TextBox33.Text);
                cmd.Parameters.AddWithValue("@state2", TextBox34.Text);
                cmd.Parameters.AddWithValue("@stateCode2", TextBox35.Text);
                cmd.Parameters.AddWithValue("@Name2", TextBox30.Text);
                cmd.Parameters.AddWithValue("@Address_2", TextBox31.Text);
                cmd.Parameters.AddWithValue("@MobioleNo_2", TextBox32.Text);
                cmd.Parameters.AddWithValue("@Totl_Amt_Before_Tax", float.Parse(TextBox10.Text));
                cmd.Parameters.AddWithValue("@Total_CGST", float.Parse(TextBox11.Text));
                cmd.Parameters.AddWithValue("@Total_SGST", float.Parse(TextBox8.Text));
                cmd.Parameters.AddWithValue("@Total_IGST", float.Parse(TextBox14.Text));
                cmd.Parameters.AddWithValue("@Tax_Amt_GST", float.Parse(TextBox12.Text));
                cmd.Parameters.AddWithValue("@Tol_Amt_After_Tax", float.Parse(TextBox9.Text));


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('cash bill created successfully')", true);
                getinvoiceno();
                getno();
                getinvoiceno1();
                BindData();
                BindData2();
                show_category();
                showcustomer();
                showcustomertype();
                showitem();
                showtax();
                showunit();
             
              
                show_employee();
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox13.Text = "";
                TextBox15.Text = "";

                TextBox2.Text = "";
                TextBox1.Text = "";
             
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                TextBox8.Text = "";
                TextBox9.Text = "";
                TextBox14.Text = "";
                TextBox12.Text = "";
                TextBox17.Text = "";
                TextBox16.Text = "";
                TextBox27.Text = "";
                TextBox28.Text = "";
                TextBox29.Text = "";
                TextBox30.Text = "";
                TextBox31.Text = "";
                TextBox32.Text = "";
                TextBox33.Text = "";
                TextBox34.Text = "";
                TextBox35.Text = "";
            }
            con10.Close();
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
                SqlCommand cmd21 = new SqlCommand("select max(invoice) from cashbill_entry where  Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from cashbill_entry where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("MM/dd/yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    ComboBox5.SelectedItem.Text = dr2["sales_man"].ToString();
                    TextBox10.Text = dr2["Total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
            //        DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
                    TextBox15.Text = dr2["prepared_by"].ToString();
                    ComboBox6.SelectedItem.Text = dr2["delivery_person"].ToString();



                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                    da1.Fill(dt1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    getinvoiceno1();
                }
                else
                {

                    getinvoiceno();
                    getno();
                    getinvoiceno1();
                    BindData();
                    show_category();
                    showitem();
                    showcustomer();
                    showunit();
                 
                    show_employee();
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";
                   
                    TextBox2.Text = "";
                    TextBox1.Text = "";
               
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox14.Text = "";
                    TextBox12.Text = "";
                    TextBox17.Text = "";
                    TextBox16.Text = "";
                    TextBox27.Text = "";
                    TextBox28.Text = "";
                    TextBox29.Text = "";
                    TextBox30.Text = "";
                    TextBox31.Text = "";
                    TextBox32.Text = "";
                    TextBox33.Text = "";
                    TextBox34.Text = "";
                    TextBox35.Text = "";

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






                cmd1.Parameters.AddWithValue("@qty", float.Parse( row.Cells[6].Text));
             
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();

                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("delete from cashbill_entry_details where invoice='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con3);
             
                con3.Open();
                cmd3.ExecuteNonQuery();
                con3.Close();





             
             
                BindData();
                getinvoiceno1();
                TextBox1.Text = "";
                TextBox2.Text = "";
            }
            con1000.Close();
        }
    }
   

    
    
  
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        try
        {
            float rate = float.Parse(TextBox2.Text);
            float qty = float.Parse(TextBox5.Text);
            float total = rate * qty;
            TextBox6.Text = Convert.ToDecimal(total).ToString("#,##0.00");
            
        }
        catch (Exception er)
        { }
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (ComboBox2.SelectedItem.Text == "Select item")
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (ComboBox1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter party')", true);
                }
                else
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


                            SqlConnection con111 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd111 = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con111);
                            con111.Open();
                            SqlDataReader dr111;
                            dr111 = cmd111.ExecuteReader();
                            if (dr111.HasRows)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('The entry already exit')", true);
                            }
                            else
                            {

                                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                                SqlCommand cmd = new SqlCommand("insert into cashbill_entry_details values(@invoice,@customer,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year,@Discount,@Taxable_value,@CGST_Rate,@CGST_Amount,@SGST_Rate,@SGST_Amount,@IGST_Rate,@IGST_Amount,@Grand_Total)", con);
                                cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                                cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                                cmd.Parameters.AddWithValue("@item_name", ComboBox2.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@shade_no", ComboBox3.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@color", TextBox1.Text);
                                cmd.Parameters.AddWithValue("@unit", ComboBox4.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@rate", TextBox2.Text);
                                cmd.Parameters.AddWithValue("@qty", TextBox5.Text);
                                cmd.Parameters.AddWithValue("@total_amount", float.Parse(TextBox6.Text));
                                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                                cmd.Parameters.AddWithValue("@year", Label4.Text);

                                cmd.Parameters.AddWithValue("@Discount", float.Parse(TextBox36.Text));
                                cmd.Parameters.AddWithValue("@Taxable_value", float.Parse(TextBox37.Text));
                                cmd.Parameters.AddWithValue("@CGST_Rate", float.Parse(TextBox3.Text));
                                cmd.Parameters.AddWithValue("@CGST_Amount", float.Parse(TextBox21.Text));
                                cmd.Parameters.AddWithValue("@SGST_Rate", float.Parse(TextBox22.Text));
                                cmd.Parameters.AddWithValue("@SGST_Amount", float.Parse(TextBox23.Text));
                                cmd.Parameters.AddWithValue("@IGST_Rate", float.Parse(TextBox24.Text));
                                cmd.Parameters.AddWithValue("@IGST_Amount", float.Parse(TextBox25.Text));
                                cmd.Parameters.AddWithValue("@Grand_Total", float.Parse(TextBox26.Text));
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            con111.Close();


                            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='" + ComboBox4.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con1);

                            cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);

                            con1.Open();
                            cmd1.ExecuteNonQuery();
                            con1.Close();











                        }

                        con1000.Close();

                        getinvoiceno1();
                        BindData();




                        ComboBox2.SelectedItem.Text = "";
                        ComboBox3.SelectedItem.Text = "";
                        showunit();
                        TextBox2.Text = "";
                        TextBox1.Text = "";

                        TextBox5.Text = "";
                        TextBox6.Text = "";
                        TextBox36.Text = "";
                        TextBox3.Text = "";
                        TextBox21.Text = "";
                        TextBox22.Text = "";
                        TextBox23.Text = "";
                        TextBox24.Text = "";
                        TextBox25.Text = "";

                        showitem();



                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Select Item Value')", true);
            }

        }
        else
        {
            if (ComboBox1.SelectedItem.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter party')", true);
            }
            else
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


                        SqlConnection con111 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd111 = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'   ", con111);
                        con111.Open();
                        SqlDataReader dr111;
                        dr111 = cmd111.ExecuteReader();
                        if (dr111.HasRows)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('The entry already exit')", true);
                        }
                        else
                        {

                            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd = new SqlCommand("insert into cashbill_entry_details values(@invoice,@customer,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year,@Discount,@Taxable_value,@CGST_Rate,@CGST_Amount,@SGST_Rate,@SGST_Amount,@IGST_Rate,@IGST_Amount,@Grand_Total)", con);
                            cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                            cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                            cmd.Parameters.AddWithValue("@item_name", ComboBox2.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@shade_no", ComboBox3.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@color", TextBox1.Text);
                            cmd.Parameters.AddWithValue("@unit", ComboBox4.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@rate", TextBox2.Text);
                            cmd.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd.Parameters.AddWithValue("@total_amount", float.Parse(TextBox6.Text));
                            cmd.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd.Parameters.AddWithValue("@year", Label4.Text);
                            cmd.Parameters.AddWithValue("@Discount", float.Parse(TextBox36.Text));
                            cmd.Parameters.AddWithValue("@Taxable_value", float.Parse(TextBox37.Text));
                            cmd.Parameters.AddWithValue("@CGST_Rate", float.Parse(TextBox3.Text));
                            cmd.Parameters.AddWithValue("@CGST_Amount", float.Parse(TextBox21.Text));
                            cmd.Parameters.AddWithValue("@SGST_Rate", float.Parse(TextBox22.Text));
                            cmd.Parameters.AddWithValue("@SGST_Amount", float.Parse(TextBox23.Text));
                            cmd.Parameters.AddWithValue("@IGST_Rate", float.Parse(TextBox24.Text));
                            cmd.Parameters.AddWithValue("@IGST_Amount", float.Parse(TextBox25.Text));
                            cmd.Parameters.AddWithValue("@Grand_Total", float.Parse(TextBox26.Text));
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        con111.Close();


                        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='" + ComboBox4.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con1);

                        cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);

                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();

                    }

                    con1000.Close();

                    getinvoiceno1();
                    BindData();




                    ComboBox2.SelectedItem.Text = "";
                    ComboBox3.SelectedItem.Text = "";
                    ComboBox4.SelectedItem.Text = "";
                    TextBox2.Text = "";
                    TextBox1.Text = "";

                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox36.Text = "";
                    TextBox3.Text = "";
                    TextBox21.Text = "";
                    TextBox22.Text = "";
                    TextBox23.Text = "";
                    TextBox24.Text = "";
                    TextBox25.Text = "";
                    showitem();
                    showunit();
                }
            }
            TextBox15.Focus();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[7].FindControl("txttotalamount1");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot = tot + Convert.ToInt32(rate1);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[11].FindControl("txtCGST_Amount");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot1 = tot1 + float.Parse(rate1);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[13].FindControl("txtSGST_Amount");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot2 = tot2 + float.Parse(rate1);
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[15].FindControl("txtIGST_Amount");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot3 = tot3 + float.Parse(rate1);
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[16].FindControl("txtGrand_Total");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot4 = tot4 + float.Parse(rate1);
            }
        }




        TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
        TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
        TextBox8.Text = Convert.ToDecimal(tot2).ToString("#,##0.00");
        TextBox14.Text = Convert.ToDecimal(tot3).ToString("#,##0.00");
        TextBox9.Text = Convert.ToDecimal(tot4).ToString("#,##0.00");

        float AddCGST = float.Parse(TextBox11.Text);
        float AddSGST = float.Parse(TextBox8.Text);
        float AddIGST = float.Parse(TextBox14.Text);
        float FinalValue = float.Parse(string.Format("{0:0.00}", (AddCGST + AddSGST + AddIGST)));
        TextBox12.Text = Convert.ToDecimal(FinalValue).ToString("#,##0.00");

        //float total = float.Parse(TextBox11.Text);
      //  if (DropDownList5.SelectedItem.Text != "")
      //  {
            //float tax = float.Parse(DropDownList5.SelectedItem.Text);
            //float tax_amount = (total * tax / 100);
            //TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
            //float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax_amount)));
            //TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
            //TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
            //float a1 = float.Parse(TextBox9.Text);
            //float b1 = float.Parse(TextBox14.Text);
            //TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");
           
      //  }
    }
    //protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    float total = float.Parse(TextBox11.Text);
    //    float tax = float.Parse(DropDownList5.SelectedItem.Text);
    //    float tax_amount = (total * tax / 100);
    //    TextBox8.Text =Convert.ToDecimal(tax_amount).ToString("#,##0.00");
    //    float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax_amount)));
    //    TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
    //    TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
    //    float a1 = float.Parse(TextBox9.Text);
    //    float b1 = float.Parse(TextBox14.Text);
    //    TextBox12.Text =Convert.ToDecimal(  (a1 - b1)).ToString("#,##0.00");
    //}
    private void getcrytal()
    {
      /*  ReportDocument rprt = new ReportDocument();

        rprt.Load(Server.MapPath("CrystalReport2.rpt"));

        cashsalesTableAdapters.DataTable1TableAdapter ta = new cashsalesTableAdapters.DataTable1TableAdapter();


        cashsales.DataTable1DataTable table = ta.GetData(Convert.ToInt32(Label1.Text), Convert.ToInt32(company_id), Convert.ToInt32(company_id));

        rprt.SetDataSource(table.DefaultView);



        CrystalReportViewer1.ReportSource = rprt;

        CrystalReportViewer1.DataBind();
        CrystalReportViewer1.RefreshReport();*/
    }
    protected void Button10_Click(object sender, EventArgs e)
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

                if (Convert.ToInt32(Label5.Text) > Convert.ToInt32(1))
                {
                    Label5.Text = (Convert.ToInt32(Label5.Text) - 1).ToString();
                }
               
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from cashbill_entry where no='" + Label5.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    ComboBox5.SelectedItem.Text = dr2["sales_man"].ToString();
                    TextBox10.Text = dr2["Totl_Amt_Before_Tax"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["Total_CGST"]).ToString("#,##0.00");
                    TextBox8.Text = Convert.ToDecimal(dr2["Total_SGST"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["Total_IGST"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["Tax_Amt_GST"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Tol_Amt_After_Tax"]).ToString("#,##0.00");
                    TextBox15.Text = dr2["prepared_by"].ToString();
                    ComboBox6.SelectedItem.Text = dr2["delivery_person"].ToString();

                    TextBox29.Text = dr2["Gstno1"].ToString();
                    TextBox27.Text = dr2["state1"].ToString();
                    TextBox28.Text = dr2["stateCode1"].ToString();
                    TextBox33.Text = dr2["Gstno2"].ToString();
                    TextBox34.Text = dr2["state2"].ToString();
                    TextBox35.Text = dr2["stateCode2"].ToString();
                    TextBox30.Text = dr2["Name2"].ToString();
                    TextBox31.Text = dr2["Address_2"].ToString();
                    TextBox32.Text = dr2["MobioleNo_2"].ToString();
                }
                con2.Close();

                TextBox20.Text = "1";
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                getinvoiceno1();
                ReportViewer1.LocalReport.Refresh();
            }
            con1000.Close();
        }
        getcrytal();
    }
    protected void Button13_Click(object sender, EventArgs e)
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
                SqlCommand cmd21 = new SqlCommand("select max(no) from cashbill_entry where  Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con21);
                SqlDataReader dr21;
                con21.Open();
                dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    int value = Convert.ToInt32(dr21[0].ToString());
                    if (Convert.ToInt32(Label5.Text) < Convert.ToInt32(value + 1))
                    {
                        Label5.Text = (Convert.ToInt32(Label5.Text) + 1).ToString();
                    }
                }
                con21.Close();
                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from cashbill_entry where no='" + Label5.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    ComboBox5.SelectedItem.Text = dr2["sales_man"].ToString();
                    TextBox10.Text = dr2["Totl_Amt_Before_Tax"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["Total_CGST"]).ToString("#,##0.00");
                    TextBox8.Text = Convert.ToDecimal(dr2["Total_SGST"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["Total_IGST"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["Tax_Amt_GST"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Tol_Amt_After_Tax"]).ToString("#,##0.00");
                    TextBox15.Text = dr2["prepared_by"].ToString();
                    ComboBox6.SelectedItem.Text = dr2["delivery_person"].ToString();

                    TextBox29.Text = dr2["Gstno1"].ToString();
                    TextBox27.Text = dr2["state1"].ToString();
                    TextBox28.Text = dr2["stateCode1"].ToString();
                    TextBox33.Text = dr2["Gstno2"].ToString();
                    TextBox34.Text = dr2["state2"].ToString();
                    TextBox35.Text = dr2["stateCode2"].ToString();
                    TextBox30.Text = dr2["Name2"].ToString();
                    TextBox31.Text = dr2["Address_2"].ToString();
                    TextBox32.Text = dr2["MobioleNo_2"].ToString();
                    TextBox20.Text = "1";
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ORDER BY s_no asc", con);
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                    da1.Fill(dt1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    getinvoiceno1();
                    getcrytal();
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {

                    getinvoiceno();
                    getno();
                    getinvoiceno1();
                    BindData();
                    show_category();
                  
               
                    show_employee();
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";

                    TextBox2.Text = "";
                    TextBox1.Text = "";
                 
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox14.Text = "";
                    TextBox12.Text = "";
                    TextBox15.Text = "";


                }
                con2.Close();
            }
            con1000.Close();
        }
       
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
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
                ImageButton img = (ImageButton)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
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
                        SqlCommand CMD = new SqlCommand("select * from customerpo_details where invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "'", con);
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                        da1.Fill(dt1);
                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {



                            string s_no = ((Label)GridView1.Rows[i].FindControl("lbls_no")).Text;
                            string itemname = ((Label)GridView1.Rows[i].FindControl("lblitemName")).Text;
                            string shade_no = ((Label)GridView1.Rows[i].FindControl("lblshadeno")).Text;
                            string color = ((Label)GridView1.Rows[i].FindControl("lblcolor")).Text;
                            string unit = ((Label)GridView1.Rows[i].FindControl("lblunit")).Text;
                            string rate = ((Label)GridView1.Rows[i].FindControl("lblrate")).Text;
                            string qty = ((Label)GridView1.Rows[i].FindControl("lblqty")).Text;
                            string total_amount = ((Label)GridView1.Rows[i].FindControl("lbltotalamount")).Text;
                             SqlConnection con111 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                             SqlCommand cmd111 = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and s_no='" + s_no + "' and Com_Id='" + company_id + "'  ", con111);
                    con111.Open();
                    SqlDataReader dr111;
                    dr111 = cmd111.ExecuteReader();
                    if (dr111.HasRows)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('The entry already exit')", true);
                    }
                    else
                    {




                        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into cashbill_entry_details values(@invoice,@customer,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year)", con1);
                        cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@s_no", s_no);
                        cmd.Parameters.AddWithValue("@item_name", itemname);
                        cmd.Parameters.AddWithValue("@shade_no", shade_no);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@unit", unit);
                        cmd.Parameters.AddWithValue("@rate", rate);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@total_amount", float.Parse(total_amount));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@year", Label4.Text);
                        con1.Open();
                        cmd.ExecuteNonQuery();
                        con1.Close();

                        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname + "' and shade_no='" + shade_no + "' and unit='" + unit + "'  AND Com_Id='" + company_id + "'  ", con2);

                        cmd1.Parameters.AddWithValue("@qty", qty);

                        con2.Open();
                        cmd1.ExecuteNonQuery();
                        con2.Close();


                    }


                        }


                    }
                    con1000.Close();
                }
            }
            con10001.Close();
        }
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
   
    protected void txtrate1_TextChanged(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                TextBox rate = (TextBox)gRow.FindControl("txtrate1");
                TextBox qty = (TextBox)gRow.FindControl("txtqty1");
                TextBox total2 = (TextBox)gRow.FindControl("txttotalamount1");
                float total_amount = float.Parse(rate.Text) * float.Parse(qty.Text);
                total2.Text = total_amount.ToString();
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                TextBox qty = (TextBox)gRow.FindControl("txtqty1");
                if (qty.Text != null)
                {
                    string rate1 = qty.Text;
                    tot = tot + Convert.ToInt32(rate1);
                }



                TextBox total1 = (TextBox)gRow.FindControl("txttotalamount1");
                if (total1 != null)
                {
                    string rate1 = total1.Text;
                    tot1 = tot1 + Convert.ToInt32(rate1);
                }
            }

            TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
            TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");




            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                Label sno = (Label)gRow.FindControl("lbls_no");
                TextBox itemname = (TextBox)gRow.FindControl("txtitemName1");
                TextBox shadeno = (TextBox)gRow.FindControl("txtshadeno1");
                TextBox color = (TextBox)gRow.FindControl("txtcolor1");
                TextBox unit = (TextBox)gRow.FindControl("txtunit1");
                TextBox rate = (TextBox)gRow.FindControl("txtrate1");
                TextBox qty = (TextBox)gRow.FindControl("txtqty1");
                TextBox total = (TextBox)gRow.FindControl("txttotalamount1");

                if (ComboBox1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party name')", true);
                    SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd10 = new SqlCommand("select * from cashbill_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
                    cmd10.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd10.Parameters.AddWithValue("@s_no", sno.Text);
                    SqlDataReader dr10;
                    con10.Open();
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.Read())
                    {
                        rate.Text = dr10["rate"].ToString();
                        qty.Text = dr10["qty"].ToString();
                        total.Text = dr10["total_amount"].ToString();
                    }
                    con10.Close();
                
                
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update cashbill_entry_details set customer=@customer,item_name=@item_name,shade_no=@shade_no,color=@color,unit=@unit,rate=@rate,qty=@qty,total_amount=@total_amount where invoice=@invoice and s_no=@s_no and year='" + Label4.Text + "'", con);
                    cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@s_no", sno.Text);
                    cmd.Parameters.AddWithValue("@item_name", itemname.Text);
                    cmd.Parameters.AddWithValue("@shade_no", shadeno.Text);
                    cmd.Parameters.AddWithValue("@color", color.Text);
                    cmd.Parameters.AddWithValue("@unit", unit.Text);
                    cmd.Parameters.AddWithValue("@rate", rate.Text);
                    cmd.Parameters.AddWithValue("@qty", qty.Text);
                    cmd.Parameters.AddWithValue("@total_amount", float.Parse(total.Text));
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
            TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
            float total3 = float.Parse(TextBox11.Text);
            //if (DropDownList5.SelectedItem.Text != "")
            //{
            //    float tax = float.Parse(DropDownList5.SelectedItem.Text);
            //    float tax_amount = (total3 * tax / 100);
            //    TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
            //    float netvalue = float.Parse(string.Format("{0:0.00}", (total3 + tax_amount)));
            //    TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
            //    TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
            //    float a1 = float.Parse(TextBox9.Text);
            //    float b1 = float.Parse(TextBox14.Text);
            //    TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");
            //}
        }
        catch (Exception er)
        { }
    }
    protected void txtqty1_TextChanged(object sender, EventArgs e)
    {

        try
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                TextBox rate = (TextBox)gRow.FindControl("txtrate1");
                TextBox qty = (TextBox)gRow.FindControl("txtqty1");
                TextBox total2 = (TextBox)gRow.FindControl("txttotalamount1");
                float total_amount = float.Parse(rate.Text) * float.Parse(qty.Text);
                total2.Text = total_amount.ToString();
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                TextBox qty = (TextBox)gRow.FindControl("txtqty1");
                if (qty.Text != null)
                {
                    string rate1 = qty.Text;
                    tot = tot + Convert.ToInt32(rate1);
                }



                TextBox total1 = (TextBox)gRow.FindControl("txttotalamount1");
                if (total1 != null)
                {
                    string rate1 = total1.Text;
                    tot1 = tot1 + Convert.ToInt32(rate1);
                }
            }

            TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
            TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");




            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                Label sno = (Label)gRow.FindControl("lbls_no");
                TextBox itemname = (TextBox)gRow.FindControl("txtitemName1");
                TextBox shadeno = (TextBox)gRow.FindControl("txtshadeno1");
                TextBox color = (TextBox)gRow.FindControl("txtcolor1");
                TextBox unit = (TextBox)gRow.FindControl("txtunit1");
                TextBox rate = (TextBox)gRow.FindControl("txtrate1");
                TextBox qty = (TextBox)gRow.FindControl("txtqty1");
                TextBox total = (TextBox)gRow.FindControl("txttotalamount1");


                if (ComboBox1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party name')", true);
                    SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd10 = new SqlCommand("select * from cashbill_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
                    cmd10.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd10.Parameters.AddWithValue("@s_no", sno.Text);
                    SqlDataReader dr10;
                    con10.Open();
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.Read())
                    {
                        rate.Text = dr10["rate"].ToString();
                        qty.Text = dr10["qty"].ToString();
                        total.Text = dr10["total_amount"].ToString();
                    }
                    con10.Close();
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update cashbill_entry_details set customer=@customer,item_name=@item_name,shade_no=@shade_no,color=@color,unit=@unit,rate=@rate,qty=@qty,total_amount=@total_amount where invoice=@invoice and s_no=@s_no and year='" + Label4.Text + "'", con);
                    cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@s_no", sno.Text);
                    cmd.Parameters.AddWithValue("@item_name", itemname.Text);
                    cmd.Parameters.AddWithValue("@shade_no", shadeno.Text);
                    cmd.Parameters.AddWithValue("@color", color.Text);
                    cmd.Parameters.AddWithValue("@unit", unit.Text);
                    cmd.Parameters.AddWithValue("@rate", rate.Text);
                    cmd.Parameters.AddWithValue("@qty", qty.Text);
                    cmd.Parameters.AddWithValue("@total_amount", float.Parse(total.Text));
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
            TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
            float total3 = float.Parse(TextBox11.Text);
            //if (DropDownList5.SelectedItem.Text != "")
            //{
            //    float tax = float.Parse(DropDownList5.SelectedItem.Text);
            //    float tax_amount = (total3 * tax / 100);
            //    TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
            //    float netvalue = float.Parse(string.Format("{0:0.00}", (total3 + tax_amount)));
            //    TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
            //    TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
            //    float a1 = float.Parse(TextBox9.Text);
            //    float b1 = float.Parse(TextBox14.Text);
            //    TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");
            //}
        }
        catch (Exception er)
        { }



    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        

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



                SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd10 = new SqlCommand("select * from cashbill_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con10);
                cmd10.Parameters.AddWithValue("@invoice", Label1.Text);
                cmd10.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);    
                SqlDataReader dr10;
                    con10.Open();
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.Read())
                    {
                        string itemname = dr10["item_name"].ToString();
                        string shadeno = dr10["shade_no"].ToString();
                        string unit = dr10["unit"].ToString();
                        float qty =float.Parse(dr10["qty"].ToString());
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + itemname + "' and shade_no='" + shadeno + "' and unit='" + unit + "'  AND Com_Id='" + company_id + "'  ", con2);

                        cmd1.Parameters.AddWithValue("@qty", qty);

                        con2.Open();
                        cmd1.ExecuteNonQuery();
                        con2.Close();
                    }
                    con10.Close();

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                SqlCommand cmd = new SqlCommand("delete from cashbill_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con);
                cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                cmd.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


               

                BindData();
                getinvoiceno1();



            }
            con10001.Close();
        }
    }
    protected void TextBox18_TextChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from party where party_name='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con2);
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
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers3(string prefixText, int count)
    {


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.CommandText = "select party_name from party where  Com_Id=@Com_Id and category='Customer' and  " +
                "party_name like @party_name + '%' ";
                cmd.Parameters.AddWithValue("@party_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["party_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }


    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchitem(string prefixText, int count)
    {


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.CommandText = "select item_name from item_master where  Com_Id=@Com_Id and  " +
                "item_name like @item_name + '%' ";
                cmd.Parameters.AddWithValue("@item_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["item_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }


    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchshade(string prefixText, int count)
    {


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.CommandText = "select shade_no from shade_master_details where item_name='"+item_name1+"' and  Com_Id=@Com_Id and  " +
                "shade_no like @shade_no + '%' ";
                cmd.Parameters.AddWithValue("@shade_no", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["shade_no"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }


    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchunit(string prefixText, int count)
    {


        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.CommandText = "select unit_name from unit where  Com_Id=@Com_Id and   " +
                "unit_name like @unit_name + '%' ";
                cmd.Parameters.AddWithValue("@unit_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["unit_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }


    }
    protected void TextBox13_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void TextBox19_TextChanged(object sender, EventArgs e)
    {
        item_name1 = ComboBox2.SelectedItem.Text;
      
    }
    protected void TextBox20_TextChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from shade_master_details where shade_no='" + ComboBox3.SelectedItem.Text + "' and  Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox1.Text = dr2["color"].ToString();



                }
                con2.Close();

                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='KG' and  Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {

                    TextBox16.Text = dr3["qty"].ToString();



                }
                con3.Close();

                SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd4 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='Cones' and  Com_Id='" + company_id + "'", con4);
                SqlDataReader dr4;
                con4.Open();
                dr4 = cmd4.ExecuteReader();
                if (dr4.Read())
                {

                    TextBox17.Text = dr4["qty"].ToString();



                }
                con4.Close();
            }
            con1000.Close();
        }
        TextBox1.Focus();
    }


    protected void TextBox1_TextChanged1(object sender, EventArgs e)
    {
      
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            float rate = float.Parse(TextBox2.Text);
            float qty = float.Parse(TextBox5.Text);
            float total = rate * qty;
            TextBox6.Text = Convert.ToDecimal(total).ToString("#,##0.00");

        }
        catch (Exception er)
        { }
    }
    protected void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from party where party_name='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox4.Text = dr2["address"].ToString();

                    TextBox7.Text = dr2["mobile_no"].ToString();


                }
                con2.Close();

                this.ModalPopupExtender2.Show();
                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(invoice),date from cashbill_entry where customer='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  group by date ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    TextBox18.Text =Convert.ToDateTime(dr["date"]).ToString("dd-MM-yyyy");
                    
                }
                con1.Close();
            }
            con1000.Close();
        }
        BindData1();
        TextBox4.Focus();
       
    }
    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("Select * from shade_master_details where item_name='" + ComboBox2.SelectedItem.Text + "' and Com_Id='"+company_id+"'  ORDER BY shade_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox3.DataSource = ds;
                ComboBox3.DataTextField = "shade_no";

                ComboBox3.DataBind();
                ComboBox3.Items.Insert(0, new ListItem("Select shade No", "1"));


                con.Close();
            }
            con1000.Close();
        }
       
    }
    protected void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from shade_master_details where shade_no='" + ComboBox3.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox1.Text = dr2["color"].ToString();

                 


                }
                con2.Close();


                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='KG' and  Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {

                    TextBox16.Text = dr3["qty"].ToString();



                }
                con3.Close();

                SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd4 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='Cones' and  Com_Id='" + company_id + "'", con4);
                SqlDataReader dr4;
                con4.Open();
                dr4 = cmd4.ExecuteReader();
                if (dr4.Read())
                {

                    TextBox17.Text = dr4["qty"].ToString();



                }
                con4.Close();
            }
            con1000.Close();
        }
        BindData1();
        
    }
    protected void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from party_wise_rate_details where category='Customer' and party_name='" + ComboBox1.SelectedItem.Text + "' and  item_name='" + ComboBox2.SelectedItem.Text + "' and  unit='" + ComboBox4.SelectedItem.Text + "' and  Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox2.Text = dr2["cash_rate"].ToString();



                }
                con2.Close();


                SqlConnection con21 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd21 = new SqlCommand("select * from itemunit where item_name='" + ComboBox2.SelectedItem.Text + "' and  unit='" + ComboBox4.SelectedItem.Text + "' and  Com_Id='" + company_id + "'", con21);
                SqlDataReader dr21;
                con21.Open();
                dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {

                    TextBox2.Text = dr21["cash_rate"].ToString();



                }
                con21.Close();
            }
            con1000.Close();
        }
       
    }
    #region " [ Button Event ] "
    protected void Button9_Click(object sender, EventArgs e)
    {
        // select appropriate contenttype, while binary transfer it identifies filetype
        string contentType = string.Empty;
        if (DropDownList2.SelectedValue.Equals(".pdf"))
            contentType = "application/pdf";
        if (DropDownList2.SelectedValue.Equals(".doc"))
            contentType = "application/ms-word";
        if (DropDownList2.SelectedValue.Equals(".xls"))
            contentType = "application/xls";

        DataTable dsData = new DataTable();

        DataSet ds = null;
        SqlDataAdapter da = null;



        try
        {
            string constring = ConfigurationManager.AppSettings["connection"];
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Cash_bill_Sales", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@No", TextBox19.Text);
                    cmd.Parameters.AddWithValue("@com_id", company_id);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    con.Open();
                    da.Fill(ds);
                    con.Close();

                }
            }
        }
        catch
        {
            throw;
        }
        dsData = ds.Tables[0];

        string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + DropDownList2.SelectedValue;
        string extension;
        string encoding;
        string mimeType;
        string[] streams;
        Warning[] warnings;

        LocalReport report = new LocalReport();
        report.ReportPath = Server.MapPath("~/Admin/Report5.rdlc");
        ReportDataSource rds = new ReportDataSource();
        rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file
        rds.Value = dsData;
        report.DataSources.Add(rds);

        Byte[] mybytes = report.Render(DropDownList2.SelectedItem.Text, null,
                        out extension, out encoding,
                        out mimeType, out streams, out warnings); //for exporting to PDF
        using (FileStream fs = File.Create(Server.MapPath("~/img/") + FileName))
        {
            fs.Write(mybytes, 0, mybytes.Length);
        }

        Response.ClearHeaders();
        Response.ClearContent();
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = contentType;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
        Response.WriteFile(Server.MapPath("~/img/" + FileName));
        Response.Flush();
        Response.Close();
        Response.End();


    }
    #endregion
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
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
                CheckBox img = (CheckBox)sender;
                GridViewRow row = (GridViewRow)img.NamingContainer;
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
                        SqlCommand CMD = new SqlCommand("select * from customerpo_details where po_invoice='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con);
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                        da1.Fill(dt1);
                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {



                            string s_no = ((Label)GridView1.Rows[i].FindControl("lbls_no")).Text;
                            string itemname = ((TextBox)GridView1.Rows[i].FindControl("txtitemName1")).Text;
                            string shade_no = ((TextBox)GridView1.Rows[i].FindControl("txtshadeno1")).Text;
                            string color = ((TextBox)GridView1.Rows[i].FindControl("txtcolor1")).Text;
                            string unit = ((TextBox)GridView1.Rows[i].FindControl("txtunit1")).Text;
                            string rate = ((TextBox)GridView1.Rows[i].FindControl("txtrate1")).Text;
                            string qty = ((TextBox)GridView1.Rows[i].FindControl("txtqty1")).Text;
                            string total_amount = ((TextBox)GridView1.Rows[i].FindControl("txttotalamount1")).Text;
                            SqlConnection con111 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd111 = new SqlCommand("select * from cashbill_entry_details where invoice='" + Label1.Text + "' and s_no='" + s_no + "' and Com_Id='" + company_id + "'  ", con111);
                            con111.Open();
                            SqlDataReader dr111;
                            dr111 = cmd111.ExecuteReader();
                            if (dr111.HasRows)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('The entry already exit')", true);
                            }
                            else
                            {


                                SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                                SqlCommand cmd11 = new SqlCommand("update customerpo_details set customer=@customer,item_name=@item_name,shade_no=@shade_no,color=@color,unit=@unit,rate=@rate,qty=qty-@qty,total_amount=@total_amount where po_invoice=@po_invoice and s_no=@s_no and Com_Id='"+company_id+"' and year='"+Label4.Text+"'", con11);
                                cmd11.Parameters.AddWithValue("@po_invoice", row.Cells[1].Text);
                                cmd11.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                                cmd11.Parameters.AddWithValue("@s_no", s_no);
                                cmd11.Parameters.AddWithValue("@item_name", itemname);
                                cmd11.Parameters.AddWithValue("@shade_no", shade_no);
                                cmd11.Parameters.AddWithValue("@color", color);
                                cmd11.Parameters.AddWithValue("@unit", unit);
                                cmd11.Parameters.AddWithValue("@rate", rate);
                                cmd11.Parameters.AddWithValue("@qty", qty);
                                cmd11.Parameters.AddWithValue("@total_amount", float.Parse(total_amount));
                             
                                con11.Open();
                                cmd11.ExecuteNonQuery();
                                con11.Close();

                                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                                SqlCommand cmd = new SqlCommand("insert into cashbill_entry_details values(@invoice,@customer,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year)", con1);
                                cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                                cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@s_no", s_no);
                                cmd.Parameters.AddWithValue("@item_name", itemname);
                                cmd.Parameters.AddWithValue("@shade_no", shade_no);
                                cmd.Parameters.AddWithValue("@color", color);
                                cmd.Parameters.AddWithValue("@unit", unit);
                                cmd.Parameters.AddWithValue("@rate", rate);
                                cmd.Parameters.AddWithValue("@qty", qty);
                                cmd.Parameters.AddWithValue("@total_amount", float.Parse(total_amount));
                                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                                cmd.Parameters.AddWithValue("@year", Label4.Text);
                                con1.Open();
                                cmd.ExecuteNonQuery();
                                con1.Close();


                                



                                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                                SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname + "' and shade_no='" + shade_no + "' and unit='" + unit + "'  AND Com_Id='" + company_id + "'  ", con2);

                                cmd1.Parameters.AddWithValue("@qty", qty);

                                con2.Open();
                                cmd1.ExecuteNonQuery();
                                con2.Close();


                            }


                        }


                    }
                    con1000.Close();
                }
            }
            con10001.Close();
        }
        this.ModalPopupExtender2.Show();
    }
}