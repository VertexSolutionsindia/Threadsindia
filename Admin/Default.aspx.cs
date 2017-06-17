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
using System.Globalization;
#endregion

public partial class Admin_Default : System.Web.UI.Page
{
    float tot = 0;
    float tot1 = 0;

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
            TextBox13.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");

            getinvoiceno();
            getinvoiceno1();
            BindData2();

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
                SqlCommand cmd = new SqlCommand("Select * from party where Com_Id='" + company_id + "' and category='Customer'  ORDER BY party_id asc", con);
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
                SqlCommand cmd2 = new SqlCommand("select * from customerpo_entry where purchase_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["purchase_invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("MM-dd-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    TextBox17.Text = dr2["ponumber"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
                }
                con2.Close();


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from customerpo_details where invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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
                SqlCommand CMD = new SqlCommand("select * from customerpo_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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
                SqlCommand CMD = new SqlCommand("select * from customerpo_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY  purchase_invoice asc", con);
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


                BindData2();

            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        getinvoiceno1();
        BindData();
        TextBox17.Text = "";

        TextBox10.Text = "";
        TextBox11.Text = "";


        TextBox2.Text = "";
        TextBox1.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        TextBox12.Text = "";
        showcustomer();
        showitem();
        showunit();
        DateTime date = DateTime.Now;
        TextBox13.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
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
                string query = "Select Max(purchase_invoice) from customerpo_entry where Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
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
                string query = "Select Max(s_no) from customerpo_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ";
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



    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        getinvoiceno();
        getinvoiceno1();
        BindData();
        TextBox17.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";


        TextBox2.Text = "";
        TextBox1.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        TextBox12.Text = "";
        showcustomer();
        showitem();
        showunit();
        DateTime date = DateTime.Now;
        TextBox13.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
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
                SqlCommand cmd2 = new SqlCommand("select * from Customerpo_entry where purchase_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("MM-dd-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    TextBox17.Text = dr2["ponumber"].ToString();
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
                SqlCommand CMD = new SqlCommand("select * from customerpo_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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
                    SqlCommand cmd1 = new SqlCommand("select * from customerpo_entry where purchase_invoice='" + Label1.Text + "' AND Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {


                        string status = "Customer po";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("update customerpo_entry set date=@date,customer=@customer,address=@address,mobile_no=@mobile_no,Total_qty=@Total_qty,total_amount=@total_amount,vat=@vat,vat_amount=@vat_amount,sub_total=@sub_total,round_off=@round_off,Grand_total=@Grand_total,Com_id=@Com_Id,status=@status,value=@value,ponumber=@ponumber where purchase_invoice=@purchase_invoice and year='" + Label4.Text + "'", con);
                        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
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
                        cmd.Parameters.AddWithValue("@ponumber", TextBox17.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Customer po updated successfully')", true);
                        getinvoiceno();
                        getinvoiceno1();
                        BindData();
                        BindData2();

                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";

                        TextBox2.Text = "";
                        TextBox1.Text = "";
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox6.Text = "";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        TextBox9.Text = "";
                        TextBox14.Text = "";
                        TextBox12.Text = "";
                        showcustomer();
                        showitem();
                        showunit();
                        DateTime date = DateTime.Now;

                    }
                    else
                    {


                        string status = "Customer po";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("INSERT INTO customerpo_entry VALUES(@purchase_invoice,@date,@customer,@address,@mobile_no,@Total_qty,@total_amount,@vat,@vat_amount,@sub_total,@round_off,@Grand_total,@Com_Id,@status,@value,@ponumber,@year)", con);
                        cmd.Parameters.AddWithValue("@purchase_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
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
                        cmd.Parameters.AddWithValue("@ponumber", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@year", Label4.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();



















                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Customer po created successfully')", true);
                        getinvoiceno();
                        getinvoiceno1();
                        BindData();


                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";

                        TextBox2.Text = "";
                        TextBox1.Text = "";
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox6.Text = "";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        TextBox9.Text = "";
                        TextBox14.Text = "";
                        TextBox12.Text = "";
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
                SqlCommand cmd21 = new SqlCommand("select max(purchase_invoice) from customerpo_entry where  Com_Id='" + company_id + "' and year='" + Label4.Text + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from customerpo_entry where purchase_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("MM-dd-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");
                    TextBox10.Text = dr2["Total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["total_amount"]).ToString("#,##0.00");
                    DropDownList5.SelectedItem.Text = dr2["vat"].ToString();
                    TextBox8.Text = Convert.ToDecimal(dr2["vat_amount"]).ToString("#,##0.00");
                    TextBox14.Text = Convert.ToDecimal(dr2["sub_total"]).ToString("#,##0.00");
                    TextBox12.Text = Convert.ToDecimal(dr2["round_off"]).ToString("#,##0.00");
                    TextBox9.Text = Convert.ToDecimal(dr2["Grand_total"]).ToString("#,##0.00");


                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand CMD = new SqlCommand("select * from customerpo_details where invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "' ORDER BY s_no asc", con);
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
                    getinvoiceno1();
                    BindData();


                    TextBox10.Text = "";
                    TextBox11.Text = "";

                    TextBox17.Text = "";
                    TextBox2.Text = "";
                    TextBox1.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox17.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox14.Text = "";
                    TextBox12.Text = "";
                    DateTime date = DateTime.Now;
                    TextBox13.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
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



                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("delete from customerpo_details where invoice='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con3);
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
                        SqlCommand cmd111 = new SqlCommand("select * from customerpo_details where invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "' and year='" + Label4.Text + "'  ", con111);
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
                            SqlCommand cmd = new SqlCommand("insert into customerpo_details values(@invoice,@customer,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year)", con);
                            cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                            cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
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
                        SqlCommand cmd111 = new SqlCommand("select * from customerpo_details where invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "'  ", con111);
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
                            SqlCommand cmd = new SqlCommand("insert into customerpo_details values(@invoice,@customer,@s_no,@item_name,@shade_no,@color,@unit,@rate,@qty,@total_amount,@Com_Id,@year)", con);
                            cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                            cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
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
            Label rate = (Label)e.Row.Cells[6].FindControl("lblqty");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot = tot + float.Parse(rate1);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label rate = (Label)e.Row.Cells[7].FindControl("lbltotalamount");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot1 = tot1 + float.Parse(rate1);
            }
        }






        TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
        TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
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
        }
        catch (Exception er)
        { }
    }





    protected void TextBox19_TextChanged(object sender, EventArgs e)
    {

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
    protected void txtqty1_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox rate = row.FindControl("txtrate1") as TextBox;
                TextBox qty = row.FindControl("txtqty1") as TextBox;
                TextBox total = row.FindControl("txttotalamount1") as TextBox;
                float total_amount = float.Parse(rate.Text) * float.Parse(qty.Text);
                total.Text = total_amount.ToString();
            }
        }

       

     





    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (ComboBox1.SelectedItem.Text == "Select party")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party')", true);
        }
        else
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
                                    .FindControl("lbls_no")).Text;
                    string itamName = ((TextBox)GridView1.Rows[e.RowIndex]
                                        .FindControl("txtitemName")).Text;
                    string shadeno = ((TextBox)GridView1.Rows[e.RowIndex]
                                        .FindControl("txtshadeno")).Text;

                    string color = ((TextBox)GridView1.Rows[e.RowIndex]
                                       .FindControl("txtcolor")).Text;

                    string unit = ((TextBox)GridView1.Rows[e.RowIndex]
                                       .FindControl("txtunit")).Text;

                    string rate = ((TextBox)GridView1.Rows[e.RowIndex]
                                       .FindControl("txtrate")).Text;
                    string qty = ((TextBox)GridView1.Rows[e.RowIndex]
                                       .FindControl("txtqty")).Text;

                    string Total_amount = ((TextBox)GridView1.Rows[e.RowIndex]
                                      .FindControl("txttotalamount")).Text;








                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update customerpo_details set customer=@customer,item_name=@item_name,shade_no=@shade_no,color=@color,unit=@unit,rate=@rate,qty=@qty,total_amount=@total_amount where invoice=@invoice and s_no=@s_no and year='" + Label4.Text + "'", con);
                    cmd.Parameters.AddWithValue("@invoice", Label1.Text);
                    cmd.Parameters.AddWithValue("@customer", ComboBox1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@s_no", s_no);
                    cmd.Parameters.AddWithValue("@item_name", itamName);
                    cmd.Parameters.AddWithValue("@shade_no", shadeno);
                    cmd.Parameters.AddWithValue("@color", color);
                    cmd.Parameters.AddWithValue("@unit", unit);
                    cmd.Parameters.AddWithValue("@rate", rate);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@total_amount", float.Parse(Total_amount));
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.EditIndex = -1;
                    BindData();
                    getinvoiceno1();
                }
                con10001.Close();

            }
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

                SqlCommand cmd = new SqlCommand("delete from customerpo_details where invoice=@invoice and s_no=@s_no and Com_Id='" + company_id + "' and year='" + Label4.Text + "'", con);
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
    protected void TextBox2_TextChanged1(object sender, EventArgs e)
    {

    }
    protected void TextBox7_TextChanged(object sender, EventArgs e)
    {

    }
}