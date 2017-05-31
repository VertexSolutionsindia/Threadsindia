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
#endregion
public partial class Seetings : System.Web.UI.Page
{
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


            getinvoiceno1();

            getinvoiceno();
           
            show_City();
           

            active();
            created();
            show_state();
            BindData2();
            show_employee();
            show_category();
        }

    }
    private void getitemcode()
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

                if (DropDownList1.SelectedItem.Text == "Customer")
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    con1.Open();
                    string query = "Select max(convert(int,SubString(party_code,PATINDEX('%[0-9]%',party_code),Len(party_code)))) from party where category='Customer' and  Com_Id='" + company_id + "' ";
                    SqlCommand cmd1 = new SqlCommand(query, con1);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        string val = dr[0].ToString();
                        if (val == "")
                        {
                            TextBox1.Text = "TIC001";
                        }
                        else
                        {
                            int a = 0;
                            if (a <= 9)
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIC00" + a.ToString();
                            }
                            if ((a >= 10) && (a <= 99))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIC0" + a.ToString();
                            }
                            if ((a >= 100) && (a <= 999))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIC" + a.ToString();
                            }
                        }
                    }
                    con1.Close();
                }
                if (DropDownList1.SelectedItem.Text == "Supplier")
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    con1.Open();
                    string query = "Select max(convert(int,SubString(party_code,PATINDEX('%[0-9]%',party_code),Len(party_code)))) from party where category='Supplier' and  Com_Id='" + company_id + "' ";
                    SqlCommand cmd1 = new SqlCommand(query, con1);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        string val = dr[0].ToString();
                        if (val == "")
                        {
                            TextBox1.Text = "TIS001";
                        }
                        else
                        {
                            int a = 0;
                            if (a <= 9)
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIS00" + a.ToString();
                            }
                            if ((a >= 10) && (a <= 99))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIS0" + a.ToString();
                            }
                            if ((a >= 100) && (a <= 999))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIS" + a.ToString();
                            }
                        }
                    }
                    con1.Close();
                }
                if (DropDownList1.SelectedItem.Text == "Winding")
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    con1.Open();
                    string query = "Select max(convert(int,SubString(party_code,PATINDEX('%[0-9]%',party_code),Len(party_code)))) from party where category='Winding' and  Com_Id='" + company_id + "' ";
                    SqlCommand cmd1 = new SqlCommand(query, con1);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        string val = dr[0].ToString();
                        if (val == "")
                        {
                            TextBox1.Text = "TIW001";
                        }
                        else
                        {
                            int a = 0;
                            if (a <= 9)
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIW00" + a.ToString();
                            }
                            if ((a >= 10) && (a <= 99))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIW0" + a.ToString();
                            }
                            if ((a >= 100) && (a <= 999))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TIW" + a.ToString();
                            }
                        }
                    }
                    con1.Close();
                }

                if (DropDownList1.SelectedItem.Text == "Dyeing")
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    con1.Open();
                    string query = "Select max(convert(int,SubString(party_code,PATINDEX('%[0-9]%',party_code),Len(party_code)))) from party where category='Dyeing' and  Com_Id='" + company_id + "' ";
                    SqlCommand cmd1 = new SqlCommand(query, con1);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        string val = dr[0].ToString();
                        if (val == "")
                        {
                            TextBox1.Text = "TID001";
                        }
                        else
                        {
                            int a = 0;
                            if (a <= 9)
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TID00" + a.ToString();
                            }
                            if ((a >= 10) && (a <= 99))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TID0" + a.ToString();
                            }
                            if ((a >= 100) && (a <= 999))
                            {
                                a = Convert.ToInt32(dr[0].ToString());
                                a = a + 1;
                                TextBox1.Text = "TID" + a.ToString();
                            }
                        }
                    }
                    con1.Close();
                }
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
                string query = "Select Max(cat_id) from category_entry where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label16.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label16.Text = a.ToString();
                    }
                }
                con1.Close();
            }
            con1000.Close();
        }
    }
    private void show_category()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from category_entry where Com_Id='" + company_id + "' ORDER BY cat_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "cat_name";
        DropDownList1.DataValueField = "cat_id";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("Select Category", "0"));
        con.Close();
    }
    private void show_City()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from city where Com_Id='" + company_id + "' ORDER BY city_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        DropDownList6.DataSource = ds;
        DropDownList6.DataTextField = "city_name";
        DropDownList6.DataValueField = "city_id";
        DropDownList6.DataBind();
        DropDownList6.Items.Insert(0, new ListItem("Select City", "0"));
        con.Close();
    }
    private void show_state()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from state where Com_Id='" + company_id + "' ORDER BY state_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "state_name";
        DropDownList3.DataValueField = "state_id";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("Select state", "0"));
        con.Close();
    }
    private void show_employee()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from representative where Com_Id='" + company_id + "' ORDER BY rep_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        DropDownList7.DataSource = ds;
        DropDownList7.DataTextField = "rep_name";
        DropDownList7.DataValueField = "rep_id";
        DropDownList7.DataBind();
        DropDownList7.Items.Insert(0, new ListItem("Select Representative", "0"));
        con.Close();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;

        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from party where party_id='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["party_id"].ToString();
            TextBox1.Text = dr2["party_code"].ToString();
            DropDownList1.SelectedItem.Text = dr2["category"].ToString();
            TextBox2.Text = dr2["party_name"].ToString();
            DropDownList2.SelectedItem.Text = dr2["sal"].ToString();
          
            DropDownList4.SelectedItem.Text = dr2["con_acc"].ToString();
            TextBox5.Text = dr2["tin"].ToString();
       
          
            TextBox7.Text = dr2["credit_limit"].ToString();
            TextBox8.Text = dr2["credit_days"].ToString();
            TextBox9.Text = dr2["trans_limit"].ToString();
            TextBox10.Text = dr2["intro_by"].ToString();
            DropDownList7.SelectedItem.Text = dr2["repres"].ToString();
            TextBox11.Text = dr2["address"].ToString();
            DropDownList6.SelectedItem.Text = dr2["city"].ToString();
            TextBox14.Text = dr2["pin_code"].ToString();
            DropDownList3.SelectedItem.Text = dr2["state"].ToString();
            TextBox12.Text = dr2["Email"].ToString();
            TextBox13.Text = dr2["phone"].ToString();
            TextBox15.Text = dr2["mobile_no"].ToString();
            TextBox16.Text = dr2["cst_no"].ToString();
            TextBox17.Text = dr2["pan"].ToString();
            TextBox18.Text = dr2["bank"].ToString();
            TextBox19.Text = dr2["bank_acc"].ToString();
            TextBox20.Text = dr2["ifs_code"].ToString();

        }
        con2.Close();



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
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from party where Com_Id='" + company_id + "' ORDER BY party_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView3.DataSource = dt1;
                GridView3.DataBind();
            }
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
               


                getinvoiceno();

                TextBox2.Text = "";
                TextBox1.Text = "";
                BindData2();

            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        show_category();
        show_City();
        show_employee();
        show_state();
        showcustomertype();
        getinvoiceno();
        TextBox1.Text = "";

        TextBox2.Text = "";
        BindData2();
        TextBox3.Text = "";
      
        TextBox5.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        TextBox21.Text = "";
        TextBox5.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
      
        TextBox12.Text = "";
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
                string query = "Select Max(party_id) from party where Com_Id='" + company_id + "' ";
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
        show_category();
        show_City();
        show_employee();
        show_state();
        showcustomertype();
        getinvoiceno();
        TextBox1.Text = "";
     
        TextBox2.Text = "";
        BindData2();
        TextBox3.Text = "";
     
        TextBox5.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
        TextBox17.Text = "";
        TextBox18.Text = "";
        TextBox19.Text = "";
        TextBox20.Text = "";
        TextBox21.Text = "";
        TextBox5.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
       
        TextBox12.Text = "";
       

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
                SqlCommand cmd2 = new SqlCommand("select * from party where party_id='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    TextBox1.Text = dr2["party_code"].ToString();
                    DropDownList1.SelectedItem.Text = dr2["category"].ToString();
                    TextBox2.Text = dr2["party_name"].ToString();
                    DropDownList2.SelectedItem.Text = dr2["sal"].ToString();
                 
                    DropDownList4.SelectedItem.Text = dr2["con_acc"].ToString();
                    TextBox5.Text = dr2["tin"].ToString();
                  
              
                    TextBox7.Text = dr2["credit_limit"].ToString();
                    TextBox8.Text = dr2["credit_days"].ToString();
                    TextBox9.Text = dr2["trans_limit"].ToString();
                    TextBox10.Text = dr2["intro_by"].ToString();
                    DropDownList7.SelectedItem.Text = dr2["repres"].ToString();
                    TextBox11.Text = dr2["address"].ToString();
                    DropDownList6.SelectedItem.Text = dr2["city"].ToString();
                    TextBox14.Text = dr2["pin_code"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["state"].ToString();
                    TextBox12.Text = dr2["Email"].ToString();
                    TextBox13.Text = dr2["phone"].ToString();
                    TextBox15.Text = dr2["mobile_no"].ToString();
                    TextBox16.Text = dr2["cst_no"].ToString();
                    TextBox17.Text = dr2["pan"].ToString();
                    TextBox18.Text = dr2["bank"].ToString();
                    TextBox19.Text = dr2["bank_acc"].ToString();
                    TextBox20.Text = dr2["ifs_code"].ToString();
                }
                con2.Close();
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
                if (TextBox1.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter party code')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from party where party_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("Update party set party_code=@party_code,category=@category,party_name=@party_name,sal=@sal,con_acc=@con_acc,tin=@tin,credit_limit=@credit_limit,credit_days=@credit_days,trans_limit=@trans_limit,intro_by=@intro_by,repres=@repres,address=@address,city=@city,pin_code=@pin_code,state=@state,Email=@Email,phone=@phone,mobile_no=@mobile_no,cst_no=@cst_no,pan=@pan,bank=@bank,bank_acc=@bank_acc,ifs_code=@ifs_code,Com_Id=@Com_Id where party_id=@party_id", CON);
                        cmd.Parameters.AddWithValue("@party_id", Label1.Text);
                        cmd.Parameters.AddWithValue("@party_code", TextBox1.Text);
                        cmd.Parameters.AddWithValue("@category", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@party_name", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@sal", DropDownList2.SelectedItem.Text);
       
                        cmd.Parameters.AddWithValue("@con_acc", DropDownList4.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@tin", TextBox5.Text);
                       
                
                        cmd.Parameters.AddWithValue("@credit_limit", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@credit_days", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@trans_limit", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@intro_by", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@repres", DropDownList7.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox11.Text);
                        cmd.Parameters.AddWithValue("@city", DropDownList6.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@pin_code", TextBox14.Text);
                        cmd.Parameters.AddWithValue("@state", DropDownList3.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Email", TextBox12.Text);
                        cmd.Parameters.AddWithValue("@phone", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@cst_no", TextBox16.Text);
                        cmd.Parameters.AddWithValue("@pan", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@bank", TextBox18.Text);
                        cmd.Parameters.AddWithValue("@bank_acc", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@ifs_code", TextBox20.Text);

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Party updated successfully')", true);

                        show_category();
                        show_City();
                        show_employee();
                        show_state();
                        showcustomertype();
                        getinvoiceno();
                        TextBox1.Text = "";

                        TextBox2.Text = "";
                        BindData2();
                        TextBox3.Text = "";
                    
                        TextBox5.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";
                        TextBox14.Text = "";
                        TextBox15.Text = "";
                        TextBox16.Text = "";
                        TextBox17.Text = "";
                        TextBox18.Text = "";
                        TextBox19.Text = "";
                        TextBox20.Text = "";
                        TextBox21.Text = "";
                        TextBox5.Text = "";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        TextBox9.Text = "";
                     
                        TextBox12.Text = "";
       

                    }
                    else
                    {


                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into party values(@party_id,@party_code,@category,@party_name,@sal,@con_acc,@tin,@credit_limit,@credit_days,@trans_limit,@intro_by,@repres,@address,@city,@pin_code,@state,@Email,@phone,@mobile_no,@cst_no,@pan,@bank,@bank_acc,@ifs_code,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@party_id", Label1.Text);
                        cmd.Parameters.AddWithValue("@party_code", TextBox1.Text);
                        cmd.Parameters.AddWithValue("@category", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@party_name", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@sal", DropDownList2.SelectedItem.Text);
                       
                        cmd.Parameters.AddWithValue("@con_acc", DropDownList4.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@tin", TextBox5.Text);
         
                     
                        cmd.Parameters.AddWithValue("@credit_limit", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@credit_days", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@trans_limit", TextBox9.Text);
                        cmd.Parameters.AddWithValue("@intro_by", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@repres", DropDownList7.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox11.Text);
                        cmd.Parameters.AddWithValue("@city", DropDownList6.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@pin_code", TextBox14.Text);
                        cmd.Parameters.AddWithValue("@state", DropDownList3.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Email", TextBox12.Text);
                        cmd.Parameters.AddWithValue("@phone", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox15.Text);
                        cmd.Parameters.AddWithValue("@cst_no", TextBox16.Text);
                        cmd.Parameters.AddWithValue("@pan", TextBox17.Text);
                        cmd.Parameters.AddWithValue("@bank", TextBox18.Text);
                        cmd.Parameters.AddWithValue("@bank_acc", TextBox19.Text);
                        cmd.Parameters.AddWithValue("@ifs_code", TextBox20.Text);

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Party created successfully')", true);

                        show_category();
                        show_City();
                        show_employee();
                        show_state();
                        showcustomertype();
                        getinvoiceno();
                        TextBox1.Text = "";

                        TextBox2.Text = "";
                        BindData2();
                        TextBox3.Text = "";
                       
                        TextBox5.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";
                        TextBox14.Text = "";
                        TextBox15.Text = "";
                        TextBox16.Text = "";
                        TextBox17.Text = "";
                        TextBox18.Text = "";
                        TextBox19.Text = "";
                        TextBox20.Text = "";
                        TextBox21.Text = "";
                        TextBox5.Text = "";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        TextBox9.Text = "";
                  
                        TextBox12.Text = "";
       
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
                SqlCommand cmd21 = new SqlCommand("select max(party_id) from party where  Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from party where party_id='" + Label1.Text + "' and  Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    TextBox1.Text = dr2["party_code"].ToString();
                    DropDownList1.SelectedItem.Text = dr2["category"].ToString();
                    TextBox2.Text = dr2["party_name"].ToString();
                    DropDownList2.SelectedItem.Text = dr2["sal"].ToString();
                   
                    DropDownList4.SelectedItem.Text = dr2["con_acc"].ToString();
                    TextBox5.Text = dr2["tin"].ToString();
                 
                 
                    TextBox7.Text = dr2["credit_limit"].ToString();
                    TextBox8.Text = dr2["credit_days"].ToString();
                    TextBox9.Text = dr2["trans_limit"].ToString();
                    TextBox10.Text = dr2["intro_by"].ToString();
                    DropDownList7.SelectedItem.Text = dr2["repres"].ToString();
                    TextBox11.Text = dr2["address"].ToString();
                    DropDownList6.SelectedItem.Text = dr2["city"].ToString();
                    TextBox14.Text = dr2["pin_code"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["state"].ToString();
                    TextBox12.Text = dr2["Email"].ToString();
                    TextBox13.Text = dr2["phone"].ToString();
                    TextBox15.Text = dr2["mobile_no"].ToString();
                    TextBox16.Text = dr2["cst_no"].ToString();
                    TextBox17.Text = dr2["pan"].ToString();
                    TextBox18.Text = dr2["bank"].ToString();
                    TextBox19.Text = dr2["bank_acc"].ToString();
                    TextBox20.Text = dr2["ifs_code"].ToString();

                }
                else
                {
                  
                    TextBox12.Text = "";
                    show_category();
                    show_City();
                    show_employee();
                    show_state();
                    showcustomertype();
                    getinvoiceno();
                    TextBox1.Text = "";

                    TextBox2.Text = "";
                    BindData2();
                    TextBox3.Text = "";
                  
                    TextBox5.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";
                    TextBox14.Text = "";
                    TextBox15.Text = "";
                    TextBox16.Text = "";
                    TextBox17.Text = "";
                    TextBox18.Text = "";
                    TextBox19.Text = "";
                    TextBox20.Text = "";
                    TextBox21.Text = "";
                    TextBox5.Text = "";
                    TextBox7.Text = "";
                    TextBox8.Text = "";
                    TextBox9.Text = "";

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
    protected void Button8_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender2.Show();
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

                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("insert into category_entry values(@cat_id,@cat_name,@Com_Id)", CON);
                cmd.Parameters.AddWithValue("@cat_id", Label16.Text);
                cmd.Parameters.AddWithValue("@cat_name", TextBox21.Text);



                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                CON.Open();
                cmd.ExecuteNonQuery();
                CON.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert( 'Category created successfully')", true);



                getinvoiceno();
                getinvoiceno1();
                show_category();

                TextBox1.Text = "";
                BindData2();

                getinvoiceno1();

                this.ModalPopupExtender2.Show();
            }
            con1000.Close();
        }
             
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getitemcode();
    }
}