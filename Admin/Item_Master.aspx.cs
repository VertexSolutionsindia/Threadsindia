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


public partial class Admin_Sub_category : System.Web.UI.Page
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

            TextBox3.Focus();


            getinvoiceno();
            getinvoiceno1();
            getinvoiceno2();
            show_category();
            showrating();
            BindData();
            show_unit();
            show_location();
            active();
            created();
            show_itemgroup();
            BindData2();
            BindData3();
            BindData4();
            getitemcode();
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
        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;
     
        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from item_master where item_id='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["item_id"].ToString();
            DropDownList1.SelectedItem.Text = dr2["group_name"].ToString();
            TextBox2.Text = dr2["item_code"].ToString();
            TextBox3.Text = dr2["item_name"].ToString();
            TextBox7.Text = dr2["item_Des"].ToString();
           
            string name = dr2["isprecot"].ToString();
            if (name == "True")
            {
                CheckBox1.Checked = true;
            }
            BindData3();
            getinvoiceno1();
            BindData4();
            getinvoiceno2();
        }
        con2.Close();
            }
     con1000.Close();
 }


       
    }
    protected void BindData3()
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
                SqlCommand CMD = new SqlCommand("select * from itemunit where item_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY item_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
    protected void BindData4()
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
                SqlCommand CMD = new SqlCommand("select * from itemlocation where item_id='"+Label1.Text+"' and Com_Id='" + company_id + "' ORDER BY item_id asc", con);
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
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand CMD = new SqlCommand("select * from item_master where Com_Id='"+company_id+"' ORDER BY item_id asc", con);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(CMD);
        da1.Fill(dt1);
        GridView3.DataSource = dt1;
        GridView3.DataBind();
            }
             con1000.Close();
         }
    }
    private void show_itemgroup()
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
                SqlCommand cmd = new SqlCommand("Select * from Item_group where Com_Id='" + company_id + "' ORDER BY item_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "Item_name";
                DropDownList1.DataValueField = "Item_id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Select Group", "0"));
                con.Close();
            } con1000.Close();
        }
    }


    private void show_unit()
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
                SqlCommand cmd = new SqlCommand("Select * from unit where Com_Id='" + company_id + "' ORDER BY unit_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList4.DataSource = ds;
                DropDownList4.DataTextField = "unit_name";
                DropDownList4.DataValueField = "unit_id";
                DropDownList4.DataBind();
                DropDownList4.Items.Insert(0, new ListItem("Select Unit", "0"));
                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = "unit_name";
                DropDownList3.DataValueField = "unit_id";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("Select Unit", "0"));
                con.Close();
            } con1000.Close();
        }
    }
    private void show_location()
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
                SqlCommand cmd = new SqlCommand("Select * from location where Com_Id='" + company_id + "' ORDER BY loc_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList5.DataSource = ds;
                DropDownList5.DataTextField = "loc_name";
                DropDownList5.DataValueField = "loc_id";
                DropDownList5.DataBind();
                DropDownList5.Items.Insert(0, new ListItem("Select Unit", "0"));
               
                con.Close();
            } con1000.Close();
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
                 SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                 SqlCommand cmd10 = new SqlCommand("select * from item_master where Item_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con10);
                    con10.Open();
                    SqlDataReader dr10;
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.HasRows)
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("delete from item_master where Item_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("delete from itemlocation where Item_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con1);
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();

                        SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd11 = new SqlCommand("delete from itemunit where Item_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con11);
                        con11.Open();
                        cmd11.ExecuteNonQuery();
                        con11.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('item not exist on database so please add data')", true);

                    }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Item deleted successfully')", true);
                BindData();

             
                getinvoiceno();
                show_itemgroup();
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox7.Text = "";
                BindData2();
                BindData3();
                BindData4();
                show_location(); 
              
            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        show_itemgroup();
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox7.Text = "";
        BindData3();
        getinvoiceno1();
        BindData4();
        getinvoiceno2();
        BindData2();
        getitemcode();
        CheckBox1.Checked = false;
        show_location(); 
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
    protected void BindData()
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
                    SqlCommand cmd = new SqlCommand("delete from Item_group where Item_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Item deleted successfully')", true);

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
                string query = "Select Max(item_id) from item_master where Com_Id='" + company_id + "' ";
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
               

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                con1.Open();
                string query = "Select max(convert(int,SubString(item_code,PATINDEX('%[0-9]%',item_code),Len(item_code)))) from item_master where Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        TextBox2.Text = "IT001";
                    }
                    else
                    {
                        int a=0;
                        if (a <= 9)
                        {
                            a = Convert.ToInt32(dr[0].ToString());
                            a = a + 1;
                            TextBox2.Text = "IT00" + a.ToString();
                        }
                        if ((a >= 10) && (a <= 99))
                        {
                            a = Convert.ToInt32(dr[0].ToString());
                            a = a + 1;
                            TextBox2.Text = "IT0" + a.ToString();
                        }
                        if ((a >= 100) && (a <= 999))
                        {
                            a = Convert.ToInt32(dr[0].ToString());
                            a = a + 1;
                            TextBox2.Text = "IT" + a.ToString();
                        }
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
                string query = "Select Max(s_no) from itemunit where item_id='" + Label1.Text+ "' and Com_Id='" + company_id + "' ";
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
    private void getinvoiceno2()
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
                string query = "Select Max(s_no) from itemlocation where item_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ";
                SqlCommand cmd1 = new SqlCommand(query, con1);
                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        Label4.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        Label4.Text = a.ToString();
                    }
                }
                con1.Close();
            }
            con1000.Close();
        }
    }
    private void show_category()
    {


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
  
    protected void Button3_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        show_itemgroup();
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox7.Text = "";
        BindData3();
        getinvoiceno1();
        BindData4();
        getinvoiceno2();
        BindData2();
        CheckBox1.Checked = false;
        getitemcode();
        show_location();
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
                SqlCommand cmd2 = new SqlCommand("select * from item_master where Com_Id='"+company_id+"' and  item_id='" + Label1.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    DropDownList1.SelectedItem.Text = dr2["group_name"].ToString();
                    TextBox2.Text = dr2["item_code"].ToString();
                    TextBox3.Text = dr2["item_name"].ToString();
                    TextBox7.Text = dr2["item_Des"].ToString();
                 
                    string name = dr2["isprecot"].ToString();
                    if (name == "True")
                    {
                        CheckBox1.Checked = true;
                    }

                    BindData3();
                    getinvoiceno1();
                    BindData4();
                    getinvoiceno2();
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
                if (TextBox3.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter Item name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from item_master where item_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("Update item_master set group_id=@group_id,group_name=@group_name,item_code=@item_code,item_name=@item_name,item_Des=@item_Des,Com_Id=@Com_Id,isprecot=@isprecot where item_id=@item_id", CON);
                        cmd.Parameters.AddWithValue("@item_id", Label1.Text);
                        cmd.Parameters.AddWithValue("@group_id", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@group_name", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@item_code", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@item_name", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@Item_Des", TextBox7.Text);
                       

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        if (CheckBox1.Checked == true)
                        {
                            string value = "True";
                            cmd.Parameters.AddWithValue("@isprecot", value);
                        }
                        else
                        {
                            string value = "False";
                            cmd.Parameters.AddWithValue("@isprecot", value);
                        }

                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Item master updated successfully')", true);
                        getinvoiceno();
                        show_itemgroup();
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox7.Text = "";
                        BindData3();
                        getinvoiceno1();
                        BindData4();
                        getinvoiceno2();
                        BindData2();
                        CheckBox1.Checked = false;
                        getitemcode();
                        show_location();
                    }
                    else
                    {


                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into item_master values(@item_id,@group_id,@group_name,@item_code,@item_name,@item_Des,@Com_Id,@isprecot)", CON);
                        cmd.Parameters.AddWithValue("@item_id", Label1.Text);
                        cmd.Parameters.AddWithValue("@group_id", DropDownList1.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@group_name", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@item_code",TextBox2.Text);
                        cmd.Parameters.AddWithValue("@item_name",TextBox3.Text);
                        cmd.Parameters.AddWithValue("@item_Des",TextBox7.Text);
                        

                        cmd.Parameters.AddWithValue("@Com_Id",company_id);
                        if (CheckBox1.Checked == true)
                        {
                            string value = "True";
                            cmd.Parameters.AddWithValue("@isprecot", value);
                        }
                        else
                        {
                            string value = "False";
                            cmd.Parameters.AddWithValue("@isprecot", value);
                        }

                    
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Item master created successfully')", true);
                        getinvoiceno();
                        show_itemgroup();
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox7.Text = "";
                        BindData3();
                        getinvoiceno1();
                        BindData4();
                        getinvoiceno2();
                        BindData2();
                        CheckBox1.Checked = false;
                        getitemcode();
                        show_location();
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
                SqlCommand cmd21 = new SqlCommand("select max(item_id) from item_master where Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from item_master where item_id='" + Label1.Text + "' and Com_Id='"+company_id+"'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    DropDownList1.SelectedItem.Text = dr2["group_name"].ToString();
                    TextBox2.Text = dr2["item_code"].ToString();
                    TextBox3.Text = dr2["item_name"].ToString();
                    TextBox7.Text = dr2["item_Des"].ToString();
                   
                    string name = dr2["isprecot"].ToString();
                    if (name == "True")
                    {
                        CheckBox1.Checked = true;
                    }
                    BindData3();
                    getinvoiceno1();
                    BindData4();
                    getinvoiceno2();
                }
                else
                {

                    getinvoiceno();
                    show_itemgroup();
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox7.Text = "";
                    getitemcode();
                    CheckBox1.Checked = false;
                    BindData3();
                    getinvoiceno1();
                    BindData4();
                    getinvoiceno2();
                    show_location();
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
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList4.SelectedItem.Text == "Select Unit")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select unit')", true);
        }
        else if (TextBox3.Text=="")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter item name')", true);
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
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("insert into itemunit values(@item_id,@s_no,@item_name,@unit,@credit_rate,@cash_rate,@Com_Id)", con);
                    cmd.Parameters.AddWithValue("@item_id", Label1.Text);
                    cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                    cmd.Parameters.AddWithValue("@item_name", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@unit", DropDownList4.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@credit_rate", TextBox10.Text);
                    cmd.Parameters.AddWithValue("@cash_rate", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    show_unit();
                    BindData();
                    BindData3();
                    getinvoiceno1();
                    TextBox1.Text = "";
                 
                    TextBox10.Text = "";

                }

                con1000.Close();
            }
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
                SqlCommand cmd3 = new SqlCommand("delete from itemunit where item_id='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                cmd3.ExecuteNonQuery();
                con3.Close();
              
                show_unit();
                BindData();
                getinvoiceno1();
                TextBox1.Text = "";
                TextBox2.Text = "";
                BindData3();
            }
            con1000.Close();
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
         if (TextBox3.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter item name')", true);
        }
         else  if (DropDownList5.SelectedItem.Text == "Select Unit")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select location')", true);
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
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("insert into itemlocation values(@item_id,@s_no,@item_name,@location,@unit,@Com_Id)", con);
                    cmd.Parameters.AddWithValue("@item_id", Label1.Text);
                    cmd.Parameters.AddWithValue("@s_no", Label4.Text);
                    cmd.Parameters.AddWithValue("@item_name", TextBox3.Text);
                    cmd.Parameters.AddWithValue("@location", DropDownList5.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@unit", DropDownList3.SelectedItem.Text);
                  
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    show_unit();
                 
                    BindData4();
                    getinvoiceno2();
                    

                }

                con1000.Close();
            }
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
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
                SqlCommand cmd3 = new SqlCommand("delete from itemlocation where item_id='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                cmd3.ExecuteNonQuery();
                con3.Close();

                show_unit();
                BindData();
                getinvoiceno1();
                TextBox1.Text = "";
                
                BindData4();
            }
            con1000.Close();
        }
    }
}