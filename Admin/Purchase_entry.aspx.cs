﻿#region " Using "
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
using System.Globalization;
#endregion

public partial class Admin_Purchase_entry : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;
    public static string searchby = "";
    public static int company_id = 0;
    public static string item_name1 = "";
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
            getno();
            showrating();


            active();
            created();



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
                SqlCommand cmd = new SqlCommand("Select * from party where Com_Id='" + company_id + "' and category='Supplier'  ORDER BY party_id asc", con);
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
                SqlCommand cmd = new SqlCommand("Select * from unit where Com_Id='" + company_id + "'  ORDER BY unit_id asc", con);
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
                ComboBox2.DataValueField = "item_code";
                ComboBox2.DataBind();
                ComboBox2.Items.Insert(0, new ListItem("Select item", "1"));


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
                SqlCommand cmd2 = new SqlCommand("select * from purchase_entry where purchase_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='"+Label4.Text+"'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["purchase_invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["supplier"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();

                    TextBox10.Text = dr2["Total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from purchase_entry_details where invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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
                SqlCommand CMD = new SqlCommand("select * from purchase_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
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
                string query = "Select max(no) from purchase_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
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
                        Label5.Text = a.ToString();
                    }
                }
                con1.Close();
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
                SqlCommand CMD = new SqlCommand("select * from purchase_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  purchase_invoice asc", con);
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
        BindData();


        TextBox10.Text = "";
        TextBox11.Text = "";


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
        TextBox100.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        showcustomer();
        showitem();
        showunit();
        DateTime date = DateTime.Now;
        TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
    }
    private void active()
    {

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
                string query = "Select max(convert(int,SubString(purchase_invoice,PATINDEX('%[0-9]%',purchase_invoice),Len(purchase_invoice)))) from purchase_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label1.Text = name + "-PH-" + "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label1.Text = name + "-PH-" + a.ToString();
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
                string query = "Select Max(s_no) from purchase_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
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
  

    protected void LinkButton1_Click(object sender, EventArgs e)
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
        BindData();

        TextBox10.Text = "";
        TextBox11.Text = "";


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
        TextBox100.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        showcustomer();
        showitem();
        showunit();
        DateTime date = DateTime.Now;
        TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
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

                if (Convert.ToInt32(Label5.Text) > Convert.ToInt32(1))
                {
                    Label5.Text = (Convert.ToInt32(Label5.Text) - 1).ToString();
                }

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("select * from purchase_entry where no='" + Label5.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["purchase_invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["supplier"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();

                    TextBox10.Text = dr2["Total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from purchase_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from purchase_entry where purchase_invoice='" + Label1.Text + "' AND Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {


                        string status = "Purchase";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("update purchase_entry set date=@date,supplier=@supplier,address=@address,mobile_no=@mobile_no,Total_qty=@Total_qty,total_amount=@total_amount,vat=@vat,vat_amount=@vat_amount,sub_total=@sub_total,round_off=@round_off,Grand_total=@Grand_total,Com_id=@Com_Id,status=@status,value=@value where purchase_invoice=@purchase_invoice and year='" + Label4.Text + "'", con);
                       
                        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(TextBox13.Text).ToString("MM-dd-yyyy"));
                        cmd.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@Total_qty", float.Parse(TextBox10.Text));
                        cmd.Parameters.AddWithValue("@total_amount", float.Parse(TextBox11.Text));
                        cmd.Parameters.AddWithValue("@vat", float.Parse(DropDownList5.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@vat_amount", float.Parse(TextBox8.Text));
                        cmd.Parameters.AddWithValue("@sub_total", float.Parse(TextBox14.Text));
                        cmd.Parameters.AddWithValue("@round_off", float.Parse(TextBox12.Text));
                        cmd.Parameters.AddWithValue("@Grand_total", float.Parse(TextBox9.Text));

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@value", value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        float b11 = 0;
                        float f11 = 0;
                        float c11 = 0;

                        SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand check_User_Name100 = new SqlCommand("SELECT * FROM pay_amount_status WHERE Supplier = @Supplier and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con100);
                        check_User_Name100.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                        con100.Open();
                        SqlDataReader reader100 = check_User_Name100.ExecuteReader();
                        if (reader100.HasRows)
                        {
                            SqlConnection con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd11 = new SqlCommand("Select * from pay_amount where Supplier='" + ComboBox1.SelectedItem.Text + "' and invoice_no='" + Label1.Text + "'  and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con11);
                            con11.Open();
                            SqlDataReader dr11;
                            dr11 = cmd11.ExecuteReader();
                            if (dr11.Read())
                            {

                                b11 = float.Parse(dr11["pending_amount"].ToString());






                                SqlConnection con27 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cd27 = new SqlCommand("update pay_amount_status set pending_amount=pending_amount-@pending_amount where Supplier='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con27);
                                cd27.Parameters.AddWithValue("@pending_amount", b11);
                                con27.Open();
                                cd27.ExecuteNonQuery();
                                con27.Close();

                                SqlConnection con272 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cd272 = new SqlCommand("update pay_amount set pending_amount=pending_amount-@pending_amount,outstanding=outstanding-@outstanding where Supplier='" + ComboBox1.SelectedItem.Text + "' and  invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con272);
                                cd272.Parameters.AddWithValue("@pending_amount", b11);
                                cd272.Parameters.AddWithValue("@outstanding", b11);
                                con272.Open();
                                cd272.ExecuteNonQuery();
                                con272.Close();

                                SqlConnection con271 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cd271 = new SqlCommand("update pay_amount_status set pending_amount=pending_amount+@pending_amount where Supplier='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con271);
                                cd271.Parameters.AddWithValue("@pending_amount", float.Parse(TextBox9.Text));
                                con271.Open();
                                cd271.ExecuteNonQuery();
                                con271.Close();





                                SqlConnection con26 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cmd26 = new SqlCommand("update pay_amount set Estimate_value=@Estimate_value,address=@address,total_amount=@total_amount,pay_amount=@pay_amount,pending_amount=@pending_amount,outstanding=outstanding+@outstanding where Supplier='" + ComboBox1.SelectedItem.Text + "' AND invoice_no='" + Label1.Text + "' and year='" + Label4.Text + "'", con26);


                                cmd26.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox11.Text));
                                cmd26.Parameters.AddWithValue("@address", TextBox4.Text);

                                cmd26.Parameters.AddWithValue("@total_amount", float.Parse(TextBox9.Text));
                                cmd26.Parameters.AddWithValue("@pay_amount", float.Parse(TextBox7.Text));
                                cmd26.Parameters.AddWithValue("@pending_amount", float.Parse(TextBox9.Text));
                                cmd26.Parameters.AddWithValue("@outstanding", float.Parse(TextBox9.Text));



                                con26.Open();
                                cmd26.ExecuteNonQuery();
                                con26.Close();



                            }
                            con11.Close();
                        }

                        con100.Close();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Product updated into the stock')", true);
                        getinvoiceno();
                        getno();
                        getinvoiceno1();
                        BindData();
                        BindData2();

                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        DateTime date = DateTime.Now;
                        TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

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
                        TextBox100.Text = "";
                        TextBox17.Text = "";
                        TextBox18.Text = "";
                        showcustomer();
                        showitem();
                        showunit();
                      

                    }
                    else
                    {


                        string status = "Purchase";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("INSERT INTO purchase_entry VALUES(@no,@purchase_invoice,@date,@supplier,@address,@mobile_no,@Total_qty,@total_amount,@vat,@vat_amount,@sub_total,@round_off,@Grand_total,@Com_Id,@status,@value,@year)", con);
                        cmd.Parameters.AddWithValue("@no", Label5.Text);
                        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@date",Convert.ToDateTime( TextBox13.Text).ToString("MM-dd-yyyy"));
                        cmd.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@Total_qty", float.Parse(TextBox10.Text));
                        cmd.Parameters.AddWithValue("@total_amount", float.Parse(TextBox11.Text));
                        cmd.Parameters.AddWithValue("@vat", float.Parse(DropDownList5.SelectedItem.Text));
                        cmd.Parameters.AddWithValue("@vat_amount", float.Parse(TextBox8.Text));
                        cmd.Parameters.AddWithValue("@sub_total", float.Parse(TextBox14.Text));
                        cmd.Parameters.AddWithValue("@round_off", float.Parse(TextBox12.Text));
                        cmd.Parameters.AddWithValue("@Grand_total", float.Parse(TextBox9.Text));

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@value", value);
                        cmd.Parameters.AddWithValue("@year", Label4.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        int a111 = 0;
                        float b11 = 0;
                        float f11 = 0;
                        float c11 = 0;
                        float pay1 = 0;
                        string status1="Bill";
                            float value1=0;
                        SqlConnection con100 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand cmd100 = new SqlCommand("SELECT * FROM pay_amount_status WHERE Supplier = @Supplier and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con100);
                        cmd100.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                        con100.Open();
                        SqlDataReader reader1 = cmd100.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            SqlConnection con11 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd11 = new SqlCommand("Select * from pay_amount_status where Supplier='" + ComboBox1.SelectedItem.Text + "' and  Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con11);
                            con11.Open();
                            SqlDataReader dr11;
                            dr11 = cmd11.ExecuteReader();
                            if (dr11.Read())
                            {

                                b11 = float.Parse(dr11["pending_amount"].ToString());


                                f11 = float.Parse(TextBox9.Text);

                                c11 = (b11 + f11);





                                
                                SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cmd24 = new SqlCommand("insert into pay_amount values(@Supplier,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@value,@year)", con24);
                                cmd24.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                                cmd24.Parameters.AddWithValue("@Pay_date", Convert.ToDateTime(TextBox13.Text).ToString("MM-dd-yyyy"));
                                cmd24.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox9.Text));
                                cmd24.Parameters.AddWithValue("@address", TextBox4.Text);

                                cmd24.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                                cmd24.Parameters.AddWithValue("@pay_amount", pay1);
                                cmd24.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                                cmd24.Parameters.AddWithValue("@outstanding", float.Parse(string.Format("{0:0.00}", c11)));

                                cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
                                cmd24.Parameters.AddWithValue("@Com_Id", company_id);
                                cmd24.Parameters.AddWithValue("@status",status1);
                                      cmd24.Parameters.AddWithValue("@value",value1);
                                      cmd24.Parameters.AddWithValue("@year", Label4.Text);
                                con24.Open();
                                cmd24.ExecuteNonQuery();
                                con24.Close();


                                SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                                SqlCommand cmd23 = new SqlCommand("update pay_amount_status set address=@address,total_amount=total_amount+@total_amount,pending_amount=pending_amount+@pending_amount where Supplier='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con23);

                                cmd23.Parameters.AddWithValue("@address", TextBox4.Text);

                                cmd23.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));

                                cmd23.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));

                                con23.Open();
                                cmd23.ExecuteNonQuery();
                                con23.Close();


                            }

                            con11.Close();






                        }
                        else
                        {


                            SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd23 = new SqlCommand("insert into pay_amount_status values(@Supplier,@address,@total_amount,@pending_amount,@paid_amount,@Com_Id,@year)", con23);
                            cmd23.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                            cmd23.Parameters.AddWithValue("@address", TextBox4.Text);

                            cmd23.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));

                            cmd23.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                            cmd23.Parameters.AddWithValue("@paid_amount",pay1);
                            cmd23.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd23.Parameters.AddWithValue("@year", Label4.Text);
                            con23.Open();
                            cmd23.ExecuteNonQuery();
                            con23.Close();
                           
                            SqlConnection con24 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                            SqlCommand cmd24 = new SqlCommand("insert into pay_amount values(@Supplier,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@value,@year)", con24);
                            cmd24.Parameters.AddWithValue("@Supplier", ComboBox1.SelectedItem.Text);
                            cmd24.Parameters.AddWithValue("@Pay_date", Convert.ToDateTime(TextBox13.Text).ToString("MM-dd-yyyy"));
                            cmd24.Parameters.AddWithValue("@Estimate_value", float.Parse(TextBox9.Text));
                            cmd24.Parameters.AddWithValue("@address", TextBox4.Text);

                            cmd24.Parameters.AddWithValue("@total_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                            cmd24.Parameters.AddWithValue("@pay_amount", pay1);
                            cmd24.Parameters.AddWithValue("@pending_amount", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                            cmd24.Parameters.AddWithValue("@outstanding", float.Parse(string.Format("{0:0.00}", TextBox9.Text)));
                            cmd24.Parameters.AddWithValue("@invoice_no", Label1.Text);
                            cmd24.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd24.Parameters.AddWithValue("@status", status1);
                            cmd24.Parameters.AddWithValue("@value", value1);
                            cmd24.Parameters.AddWithValue("@year", Label4.Text);
                            con24.Open();
                            cmd24.ExecuteNonQuery();
                            con24.Close();


                        }
                        con100.Close();


















                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Product added into the stock')", true);
                        getinvoiceno();
                        getno();
                        getinvoiceno1();
                        BindData();


                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        DateTime date = DateTime.Now;
                        TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

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
                        TextBox100.Text = "";
                        TextBox17.Text = "";
                        TextBox18.Text = "";
                        showcustomer();
                        showitem();
                        showunit();
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
                SqlCommand cmd21 = new SqlCommand("select max(purchase_invoice) from purchase_entry where ='" + company_id + "' and year='" + Label4.Text + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from purchase_entry where purchase_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["supplier"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();

                    TextBox10.Text = dr2["Total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");


                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from purchase_entry_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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


                    TextBox10.Text = "";
                    TextBox11.Text = "";


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
                    TextBox100.Text = "";
                    TextBox17.Text = "";
                    TextBox18.Text = "";
                    DateTime date = DateTime.Now;
                    TextBox13.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
                    showcustomer();
                    showitem();
                    showunit();
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
        if (DropDownList1.SelectedItem.Text == "Invoice No")
        {
            searchby = "purchase_invoice";
            
        }
        if (DropDownList1.SelectedItem.Text == "Date")
        {
            searchby = "date";

        }
        if (DropDownList1.SelectedItem.Text == "Supplier")
        {
            searchby = "Supplier";

        }
        if (DropDownList1.SelectedItem.Text == "Mobile No")
        {
            searchby = "mobile_no";

        }
        if (DropDownList1.SelectedItem.Text == "Total Qty")
        {
            searchby = "Total_qty";

        }
        if (DropDownList1.SelectedItem.Text == "Grand Total")
        {
            searchby = "Grand_total";

        }
        if (DropDownList1.SelectedItem.Text == "ALL")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand CMD = new SqlCommand("select * from purchase_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  purchase_invoice asc", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(CMD);
            da1.Fill(dt1);
            GridView3.DataSource = dt1;
            GridView3.DataBind();
            this.ModalPopupExtender1.Show();
        }
        this.ModalPopupExtender1.Show();
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
                SqlCommand cmd1 = new SqlCommand("update Product_stock set supplier=@supplier,color=@color,qty=qty-@qty,Com_Id=@Com_Id where item_name='" + row.Cells[1].Text + "' and shade_no='" + row.Cells[2].Text + "' and unit='" + row.Cells[4].Text + "'  AND Com_Id='" + company_id + "'  ", con1);

                cmd1.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);


                cmd1.Parameters.AddWithValue("@color", TextBox1.Text);

                cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);
                cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();

                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("delete from purchase_entry_details where invoice='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con3);
                SqlDataReader dr3;
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
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }




    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
        try
        {
            float rate = float.Parse(TextBox2.Text);
            float qty = float.Parse(TextBox5.Text);
            float total = rate * qty;
            TextBox6.Text = total.ToString();
        }
        catch (Exception er)
        { }

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Button8_Click(object sender, EventArgs e)
    {

        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {

            if (ComboBox1.SelectedItem.Text == "Select party")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party')", true);
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
                        SqlCommand cmd111 = new SqlCommand("select * from purchase_entry_details where invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con111);
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
                            SqlCommand cmd = new SqlCommand("insert into purchase_entry_details values(@invoice,@supplier,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year)", con);
                            cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                            cmd.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                            cmd.Parameters.AddWithValue("@item_name", ComboBox2.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@shade_no", ComboBox3.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@color", TextBox1.Text);
                            cmd.Parameters.AddWithValue("@unit", ComboBox4.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@rate", TextBox2.Text);
                            cmd.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd.Parameters.AddWithValue("@total_amount", TextBox6.Text);
                            cmd.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd.Parameters.AddWithValue("@year", Label4.Text);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        con111.Close();

                        SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd11 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='" + ComboBox4.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con11);
                        con11.Open();
                        SqlDataReader dr11;
                        dr11 = cmd11.ExecuteReader();
                        if (dr11.HasRows)
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd1 = new SqlCommand("update Product_stock set supplier=@supplier,color=@color,qty=qty+@qty,Com_Id=@Com_Id where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='" + ComboBox4.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con1);

                            cmd1.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);


                            cmd1.Parameters.AddWithValue("@color", TextBox1.Text);

                            cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                            con1.Open();
                            cmd1.ExecuteNonQuery();
                            con1.Close();


                        }
                        else
                        {


                            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd1 = new SqlCommand("insert into Product_stock values(@supplier,@item_name,@shade_no,@color,@unit,@qty,@Com_Id)", con1);

                            cmd1.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@item_name", ComboBox2.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@shade_no", ComboBox3.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@color", TextBox1.Text);
                            cmd1.Parameters.AddWithValue("@unit", ComboBox4.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                            con1.Open();
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                        }
                        con11.Close();








                    }

                    con1000.Close();

                    con1000.Close();

                }
            }

            getinvoiceno1();
            BindData();





            TextBox2.Text = "";
            TextBox1.Text = "";

            TextBox5.Text = "";
            TextBox6.Text = "";
          
            

            showitem();
            showunit();
          

        }
        else
        {
            if (ComboBox1.SelectedItem.Text == "Select party")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party')", true);
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
                        SqlCommand cmd111 = new SqlCommand("select * from purchase_entry_details where invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con111);
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
                            SqlCommand cmd = new SqlCommand("insert into purchase_entry_details values(@invoice,@supplier,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year)", con);
                            cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                            cmd.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                            cmd.Parameters.AddWithValue("@item_name", ComboBox2.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@shade_no", ComboBox3.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@color", TextBox1.Text);
                            cmd.Parameters.AddWithValue("@unit", ComboBox4.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@rate", TextBox2.Text);
                            cmd.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd.Parameters.AddWithValue("@total_amount", TextBox6.Text);
                            cmd.Parameters.AddWithValue("@Com_Id", company_id);
                            cmd.Parameters.AddWithValue("@year", Label4.Text);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        con111.Close();

                        SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd11 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='" + ComboBox4.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con11);
                        con11.Open();
                        SqlDataReader dr11;
                        dr11 = cmd11.ExecuteReader();
                        if (dr11.HasRows)
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd1 = new SqlCommand("update Product_stock set supplier=@supplier,color=@color,qty=qty+@qty,Com_Id=@Com_Id where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='" + ComboBox4.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con1);

                            cmd1.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);


                            cmd1.Parameters.AddWithValue("@color", TextBox1.Text);

                            cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                            con1.Open();
                            cmd1.ExecuteNonQuery();
                            con1.Close();


                        }
                        else
                        {


                            SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                            SqlCommand cmd1 = new SqlCommand("insert into Product_stock values(@supplier,@item_name,@shade_no,@color,@unit,@qty,@Com_Id)", con1);

                            cmd1.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@item_name", ComboBox2.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@shade_no", ComboBox3.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@color", TextBox1.Text);
                            cmd1.Parameters.AddWithValue("@unit", ComboBox4.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@qty", TextBox5.Text);
                            cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                            con1.Open();
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                        }
                        con11.Close();








                    }

                    con1000.Close();

                    con1000.Close();

                }
            }

            getinvoiceno1();
            BindData();





            TextBox2.Text = "";
            TextBox1.Text = "";

            TextBox5.Text = "";
            TextBox6.Text = "";
            showitem();
            showunit();
            ComboBox2.Focus();
        }
    
        TextBox10.Focus();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[6].FindControl("txtqty1");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot = tot + Convert.ToInt32(rate1);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox rate = (TextBox)e.Row.Cells[7].FindControl("txttotalamount1");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot1 = tot1 + Convert.ToInt32(rate1);
            }
        }






        TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
        TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
        float total = float.Parse(TextBox11.Text);
        if (DropDownList5.SelectedItem.Text != "")
        {
            float tax = float.Parse(DropDownList5.SelectedItem.Text);
            float tax_amount = (total * tax / 100);
            TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
            float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax_amount)));
            TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
            TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
            float a1 = float.Parse(TextBox9.Text);
            float b1 = float.Parse(TextBox14.Text);
            TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");
            if (TextBox17.Text != "")
            {
                float new_balance = float.Parse(TextBox9.Text);
                float old = float.Parse(TextBox17.Text);
                TextBox18.Text = (old + new_balance).ToString("#,##0.00");
            }
        }
       
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            float total = float.Parse(TextBox11.Text);
            float tax = float.Parse(DropDownList5.SelectedItem.Text);
            float tax_amount = (total * tax / 100);
            TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
            float netvalue = float.Parse(string.Format("{0:0.00}", (total + tax_amount)));
            TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
            TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
            float a1 = float.Parse(TextBox9.Text);
            float b1 = float.Parse(TextBox14.Text);
            TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");

            float new_balance = float.Parse(TextBox9.Text);
            float old = float.Parse(TextBox17.Text);
            TextBox18.Text = (old + new_balance).ToString("#,##0.00");
        }
        catch (Exception er)
        { }
    }





    
    protected void TextBox13_TextChanged(object sender, EventArgs e)
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
    protected void txtrate1_TextChanged(object sender, EventArgs e)
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
                SqlCommand cmd10 = new SqlCommand("select * from purchase_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
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

                SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd10 = new SqlCommand("select * from purchase_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
                cmd10.Parameters.AddWithValue("@invoice", Label1.Text);
                cmd10.Parameters.AddWithValue("@s_no", sno.Text);
                SqlDataReader dr10;
                con10.Open();
                dr10 = cmd10.ExecuteReader();
                if (dr10.Read())
                {
                    string itemname1 = dr10["item_name"].ToString();
                    string shadeno1 = dr10["shade_no"].ToString();
                    string unit1 = dr10["unit"].ToString();
                    float qty1 = float.Parse(dr10["qty"].ToString());
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname1 + "' and shade_no='" + shadeno1 + "' and unit='" + unit1 + "'  AND Com_Id='" + company_id + "'  ", con2);

                    cmd1.Parameters.AddWithValue("@qty", qty1);

                    con2.Open();
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                }
                con10.Close();

                SqlConnection con100 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd100 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + itemname.Text + "' and shade_no='" + shadeno.Text + "' and unit='" + unit.Text + "'  AND Com_Id='" + company_id + "'  ", con100);



                cmd100.Parameters.AddWithValue("@qty", qty.Text);

                con100.Open();
                cmd100.ExecuteNonQuery();
                con100.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("update purchase_entry_details set supplier=@supplier,item_name=@item_name,shade_no=@shade_no,color=@color,unit=@unit,rate=@rate,qty=@qty,total_amount=@total_amount where invoice=@invoice and s_no=@s_no and year='" + Label4.Text + "'", con);
                cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                cmd.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);
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

            TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
            TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
            float total3 = float.Parse(TextBox11.Text);
            if (DropDownList5.SelectedItem.Text != "")
            {
                float tax = float.Parse(DropDownList5.SelectedItem.Text);
                float tax_amount = (total3 * tax / 100);
                TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
                float netvalue = float.Parse(string.Format("{0:0.00}", (total3 + tax_amount)));
                TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
                TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
                float a1 = float.Parse(TextBox9.Text);
                float b1 = float.Parse(TextBox14.Text);
                TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");
                
                
                float new_balance = float.Parse(TextBox9.Text);
                float old = float.Parse(TextBox17.Text);
                TextBox18.Text = (old + new_balance).ToString("#,##0.00");
            }





        }
    }
    protected void txtqty1_TextChanged(object sender, EventArgs e)
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
                        SqlCommand cmd10 = new SqlCommand("select * from purchase_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
                        cmd10.Parameters.AddWithValue("@invoice", Label1.Text);
                        cmd10.Parameters.AddWithValue("@s_no", sno.Text);
                        SqlDataReader dr10;
                        con10.Open();
                        dr10 = cmd10.ExecuteReader();
                        if (dr10.Read())
                        {

                            qty.Text = dr10["qty"].ToString();
                            total.Text = dr10["total_amount"].ToString();
                        }
                        con10.Close();
                    }
                    else
                    {

                    SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd10 = new SqlCommand("select * from purchase_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
                    cmd10.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd10.Parameters.AddWithValue("@s_no", sno.Text);
                    SqlDataReader dr10;
                    con10.Open();
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.Read())
                    {
                        string itemname1 = dr10["item_name"].ToString();
                        string shadeno1 = dr10["shade_no"].ToString();
                        string unit1 = dr10["unit"].ToString();
                        float qty1 = float.Parse(dr10["qty"].ToString());
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname1 + "' and shade_no='" + shadeno1 + "' and unit='" + unit1 + "'  AND Com_Id='" + company_id + "'  ", con2);

                        cmd1.Parameters.AddWithValue("@qty", qty1);

                        con2.Open();
                        cmd1.ExecuteNonQuery();
                        con2.Close();
                    }
                    con10.Close();

                    SqlConnection con100 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd100 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + itemname.Text + "' and shade_no='" + shadeno.Text + "' and unit='" + unit.Text + "'  AND Com_Id='" + company_id + "'  ", con100);



                    cmd100.Parameters.AddWithValue("@qty", qty.Text);

                    con100.Open();
                    cmd100.ExecuteNonQuery();
                    con100.Close();


                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update purchase_entry_details set supplier=@supplier,item_name=@item_name,shade_no=@shade_no,color=@color,unit=@unit,rate=@rate,qty=@qty,total_amount=@total_amount where invoice=@invoice and s_no=@s_no and year='" + Label4.Text + "'", con);
                    cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd.Parameters.AddWithValue("@supplier", ComboBox1.SelectedItem.Text);
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

                TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
                TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
                float total3 = float.Parse(TextBox11.Text);
                if (DropDownList5.SelectedItem.Text != "")
                {
                    float tax = float.Parse(DropDownList5.SelectedItem.Text);
                    float tax_amount = (total3 * tax / 100);
                    TextBox8.Text = Convert.ToDecimal(tax_amount).ToString("#,##0.00");
                    float netvalue = float.Parse(string.Format("{0:0.00}", (total3 + tax_amount)));
                    TextBox14.Text = Convert.ToDecimal(string.Format("{0:0.00}", netvalue)).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(string.Format("{0:0.00}", Math.Round(netvalue))).ToString("#,##0.00");
                  
                    
                    float a1 = float.Parse(TextBox9.Text);
                    float b1 = float.Parse(TextBox14.Text);
                    TextBox12.Text = Convert.ToDecimal((a1 - b1)).ToString("#,##0.00");

                    float new_balance =float.Parse(TextBox9.Text);
                    float old = float.Parse(TextBox17.Text);
                    TextBox18.Text = (old + new_balance).ToString("#,##0.00");
                }





            }



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
                SqlCommand cmd10 = new SqlCommand("select * from purchase_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con10);
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
                    float qty = float.Parse(dr10["qty"].ToString());
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + itemname + "' and shade_no='" + shadeno + "' and unit='" + unit + "'  AND Com_Id='" + company_id + "'  ", con2);

                    cmd1.Parameters.AddWithValue("@qty", qty);

                    con2.Open();
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                }
                con10.Close();

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                SqlCommand cmd = new SqlCommand("delete from purchase_entry_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con);
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
            TextBox6.Text = total.ToString();
        }
        catch (Exception er)
        { }

    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

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

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select Max(purchase_invoice),date from purchase_entry where supplier='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  group by date,purchase_invoice order by purchase_invoice desc";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    TextBox100.Text = Convert.ToDateTime(dr["date"]).ToString("dd-MM-yyyy");

                }
                con1.Close();


                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from pay_amount_status where Supplier='" + ComboBox1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {



                    TextBox17.Text = Convert.ToDecimal(dr3["pending_amount"]).ToString("#,##0.00");


                }
                con3.Close();

            }
            con1000.Close();
        }

       


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
                SqlCommand cmd = new SqlCommand("Select * from shade_master_details where item_name='" + ComboBox2.SelectedItem.Text + "' and Com_Id='" + company_id + "'  ORDER BY shade_id asc", con);
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

                    TextBox15.Text = dr3["qty"].ToString();



                }
                con3.Close();

                SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd4 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox2.SelectedItem.Text + "' and shade_no='" + ComboBox3.SelectedItem.Text + "' and unit='Cones' and  Com_Id='" + company_id + "'", con4);
                SqlDataReader dr4;
                con4.Open();
                dr4 = cmd4.ExecuteReader();
                if (dr4.Read())
                {

                    TextBox16.Text = dr4["qty"].ToString();



                }
                con4.Close();
            }
            con1000.Close();
        }


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
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct "+searchby+" from purchase_entry where Com_Id=@Com_Id and " +
                "" + searchby + " like @" + searchby + " + '%'";
                cmd.Parameters.AddWithValue("@" + searchby + "", prefixText);
                cmd.Parameters.AddWithValue("@Com_id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["" + searchby + ""].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
   
    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
       
        if (searchby == "date")
        {
            if (TextBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from purchase_entry where " + searchby + "='" + Convert.ToDateTime(TextBox3.Text).ToString("MM-dd-yyyy") + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  purchase_invoice asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView3.DataSource = dt1;
                GridView3.DataBind();
                this.ModalPopupExtender1.Show();
            }

        }
        else
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand CMD = new SqlCommand("select * from purchase_entry where " + searchby + "='" + TextBox3.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  purchase_invoice asc", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(CMD);
            da1.Fill(dt1);
            GridView3.DataSource = dt1;
            GridView3.DataBind();
            this.ModalPopupExtender1.Show();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedItem.Text == "Acending")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand CMD = new SqlCommand("select * from purchase_entry where  Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  "+searchby+" asc", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(CMD);
            da1.Fill(dt1);
            GridView3.DataSource = dt1;
            GridView3.DataBind();
         
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand CMD = new SqlCommand("select * from purchase_entry where  Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  " + searchby + " desc", con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(CMD);
            da1.Fill(dt1);
            GridView3.DataSource = dt1;
            GridView3.DataBind();
        }
        this.ModalPopupExtender1.Show();

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


        LinkButton Lnk = (LinkButton)sender;
        string name = Lnk.Text;
        Session["name"] = name;
        searchby = name;
        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from purchase_entry where purchase_invoice='" + name + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["purchase_invoice"].ToString();
            TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

            ComboBox1.SelectedItem.Text = dr2["supplier"].ToString();
            TextBox4.Text = dr2["address"].ToString();
            TextBox7.Text = dr2["mobile_no"].ToString();

            TextBox10.Text = dr2["Total_qty"].ToString();
            TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
            DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
            TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
            TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
            TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
            TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
        }
        con2.Close();


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from purchase_entry_details where invoice='" + name + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        getinvoiceno1();

    }
    protected void lnkView1_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;


        LinkButton Lnk = (LinkButton)sender;
        string name1 = Lnk.Text;
        Session["name"] = name1;

        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from purchase_entry where purchase_invoice='" +searchby + "' and Supplier='" + name1 + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["purchase_invoice"].ToString();
            TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

            ComboBox1.SelectedItem.Text = dr2["Supplier"].ToString();
            TextBox4.Text = dr2["address"].ToString();
            TextBox7.Text = dr2["mobile_no"].ToString();

            TextBox10.Text = dr2["Total_qty"].ToString();
            TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
            DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
            TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
            TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
            TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
            TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
        }
        con2.Close();


        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from purchase_entry_details where invoice='" + searchby  + "' and supplier='" + name1 + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        getinvoiceno1();

    }
}
    