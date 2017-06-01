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

public partial class Admin_Order_indent_precot : System.Web.UI.Page
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
         
            DateTime date = DateTime.Now;
            TextBox13.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");


            getinvoiceno();
            getinvoiceno1();
            BindData2();
           
            showrating();


            active();
            created();
            show_tax();
            BindData();
            show_category();
            show_item();
            show_tax();
            show_half_order();
            show_pay_terms();


          
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
                string query = "Select Max(S_no) from odr_indent_precot_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' ";
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

    private void show_item()
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

                ComboBox3.DataSource = ds;
                ComboBox3.DataTextField = "item_name";
                ComboBox3.DataValueField = "item_id";
                ComboBox3.DataBind();
                ComboBox3.Items.Insert(0, new ListItem("Select item", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_tax()
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
                SqlCommand cmd = new SqlCommand("Select * from Tax_entry where Com_Id='" + company_id + "'  ORDER BY tax_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox4.DataSource = ds;
                ComboBox4.DataTextField = "tax_name";
                ComboBox4.DataValueField = "tax_id";
                ComboBox4.DataBind();
                ComboBox4.Items.Insert(0, new ListItem("Select tax", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_half_order()
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
                SqlCommand cmd = new SqlCommand("Select * from Order_Half where Com_Id='" + company_id + "'  ORDER BY order_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox5.DataSource = ds;
                ComboBox5.DataTextField = "order_name";
                ComboBox5.DataValueField = "order_id";
                ComboBox5.DataBind();
                ComboBox5.Items.Insert(0, new ListItem("Select Half ID", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    private void show_pay_terms()
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
                SqlCommand cmd = new SqlCommand("Select * from PaymentTerms where Com_Id='" + company_id + "'  ORDER BY Pay_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox2.DataSource = ds;
                ComboBox2.DataTextField = "Payment_terms";
                ComboBox2.DataValueField = "Pay_id";
                ComboBox2.DataBind();
                ComboBox2.Items.Insert(0, new ListItem("Select pay terms", "1"));


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

                    ComboBox1.SelectedItem.Text = dr2["customer"].ToString();
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
                SqlCommand CMD = new SqlCommand("select * from odr_indent_precot_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY S_no asc", con);
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
                SqlCommand cmd = new SqlCommand("delete from odr_indent_precot where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("delete from odr_indent_precot_details where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con);
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Order-indent precot deleted successfully')", true);
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
        BindData2();

        showrating();


        active();
        created();
        show_tax();
        BindData();
        show_category();
        show_item();
        show_tax();
        show_half_order();
        show_pay_terms();

        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";



      
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
                string query = "Select Max(invoice_no) from odr_indent_precot where Com_Id='" + company_id + "' ";
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
        getinvoiceno1();
        BindData2();

        showrating();


        active();
        created();
        show_tax();
        BindData();
        show_category();
        show_item();
        show_tax();
        show_half_order();
        show_pay_terms();

        TextBox10.Text = "";
        TextBox11.Text = "";

     
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
                SqlCommand cmd2 = new SqlCommand("select * from odr_indent_precot where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["supplier_name"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    ComboBox3.SelectedItem.Text = dr2["item_name"].ToString();
                    ComboBox2.SelectedItem.Text = dr2["pay_terms"].ToString();
                    ComboBox4.SelectedItem.Text = dr2["tax_template"].ToString();
                    ComboBox5.SelectedItem.Text = dr2["half_ID"].ToString();
                    TextBox10.Text = dr2["total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["Total_boxes"]).ToString("#,##0.00");





                    BindData();
                    getinvoiceno1();
                    getshadeno();




                 
                        
                       
                }

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
                    SqlCommand cmd1 = new SqlCommand("select * from odr_indent_precot where invoice_no='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {



                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("update odr_indent_precot set date=@date,supplier_name=@supplier_name,address=@address,mobile_no=@mobile_no,item_name=@item_name,pay_terms=@pay_terms,tax_template=@tax_template,half_ID=@half_ID,total_qty=@total_qty,Total_boxes=@Total_boxes,Com_Id=@Com_Id where invoice_no=@invoice_no", con);
                        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@supplier_name", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@item_name", ComboBox3.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@pay_terms", ComboBox2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@tax_template", ComboBox4.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@half_ID", ComboBox5.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@total_qty", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@Total_boxes", TextBox11.Text);
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Order-indent precot updated successfully ')", true);
                        getinvoiceno();
                        getinvoiceno1();
                        BindData();
                        show_category();


                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";



                     
                        TextBox4.Text = "";


                        TextBox7.Text = "";


                    }
                    else
                    {


                    
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("INSERT INTO odr_indent_precot VALUES(@invoice_no,@date,@supplier_name,@address,@mobile_no,@item_name,@pay_terms,@tax_template,@half_ID,@total_qty,@Total_boxes,@Com_Id)", con);
                        cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                        cmd.Parameters.AddWithValue("@date", TextBox13.Text);
                        cmd.Parameters.AddWithValue("@supplier_name", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox7.Text);
                        cmd.Parameters.AddWithValue("@item_name", ComboBox3.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@pay_terms", ComboBox2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@tax_template", ComboBox4.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@half_ID",ComboBox5.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@total_qty", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@Total_boxes",TextBox11.Text);
                        cmd.Parameters.AddWithValue("@Com_Id",company_id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('order-indent precot created successfully')", true);
                        getinvoiceno();
                        getinvoiceno1();
                        BindData();
                        show_category();


                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";



                     
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
                SqlCommand cmd21 = new SqlCommand("select max(invoice_no) from odr_indent_precot where  Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from odr_indent_precot where invoice_no='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("dd-MM-yyyy");

                    ComboBox1.SelectedItem.Text = dr2["supplier_name"].ToString();
                    TextBox4.Text = dr2["address"].ToString();
                    TextBox7.Text = dr2["mobile_no"].ToString();
                    ComboBox3.SelectedItem.Text=dr2["item_name"].ToString();
                    ComboBox2.SelectedItem.Text = dr2["pay_terms"].ToString();
                    ComboBox4.SelectedItem.Text = dr2["tax_template"].ToString();
                    ComboBox5.SelectedItem.Text = dr2["half_ID"].ToString();
                    TextBox10.Text = dr2["total_qty"].ToString();
                    TextBox11.Text = Convert.ToDecimal(dr2["Total_boxes"]).ToString("#,##0.00");





                    BindData();
                    getinvoiceno1();
                    getshadeno();
                }
                else
                {

                    getinvoiceno();
                    getinvoiceno1();
                    BindData2();

                    showrating();


                    active();
                    created();
                    show_tax();
                    BindData();
                    show_category();
                    show_item();
                    show_tax();
                    show_half_order();
                    show_pay_terms();

                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";



                   
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
            Label rate = (Label)e.Row.Cells[2].FindControl("lblprecotorder");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot = tot + Convert.ToInt32(rate1);
            }
        }

       






        TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
     
       
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
                string shade_no = ((TextBox)GridView1.Rows[e.RowIndex]
                                  .FindControl("txtshadeno")).Text;
                string precotorder= ((TextBox)GridView1.Rows[e.RowIndex]
                                    .FindControl("txtprecotorder")).Text;
              







                if (ComboBox1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party')", true);
                }
                else
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update odr_indent_precot_details set supplier_name=@supplier_name,shade_no=@shade_no,precotorder=@precotorder,Com_Id=@Com_Id where invoice_no=@invoice_no and S_no=@S_no", con);
                    cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                    cmd.Parameters.AddWithValue("@supplier_name", ComboBox1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@S_no",s_no);
                    cmd.Parameters.AddWithValue("@shade_no", shade_no);
                    cmd.Parameters.AddWithValue("@precotorder", precotorder);
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

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




                SqlCommand cmd = new SqlCommand("delete from odr_indent_precot_details where invoice_no=@invoice_no and S_no=@S_no and Com_Id='" + company_id + "'", con);
                cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                cmd.Parameters.AddWithValue("@S_no", lnkRemove.CommandArgument);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();




            }
            con10001.Close();
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
    protected void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
        getshadeno();
    }

    private void getshadeno()
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
                SqlCommand cmd = new SqlCommand("Select distinct shade_no,shade_id from shade_master_details where Com_Id='" + company_id + "'  ORDER BY shade_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                ComboBox6.DataSource = ds;
                ComboBox6.DataTextField = "shade_no";
                ComboBox6.DataValueField = "shade_id";
                ComboBox6.DataBind();
                ComboBox6.Items.Insert(0, new ListItem("Select party", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    protected void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox3.SelectedItem.Text + "' and shade_no='" + ComboBox6.SelectedItem.Text + "' and unit='KG' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox2.Text = dr2["qty"].ToString();

                }
                else
                {
                    TextBox2.Text = "";
                }
                con2.Close();
                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from Product_stock where item_name='" + ComboBox3.SelectedItem.Text + "' and shade_no='" + ComboBox6.SelectedItem.Text + "' and unit='Cones' and Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {

                    TextBox3.Text = dr3["qty"].ToString();

                }
                else
                {
                    TextBox3.Text = "";
                }
                con3.Close();

                SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd4 = new SqlCommand("select * from Wendingdly_product_stock where item_name='" + ComboBox3.SelectedItem.Text + "' and shade_no='" + ComboBox6.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con4);
                SqlDataReader dr4;
                con4.Open();
                dr4 = cmd4.ExecuteReader();
                if (dr4.Read())
                {

                    TextBox14.Text = dr4["Net_Wt"].ToString();

                }
                else
                {
                    TextBox14.Text = "";
                }
                con4.Close();
            }
            con1000.Close();
        }
        TextBox2.Focus();
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
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("INSERT INTO odr_indent_precot_details VALUES(@invoice_no,@supplier_name,@S_no,@shade_no,@precotorder,@Com_Id)", con);
                cmd.Parameters.AddWithValue("@invoice_no", Label1.Text);
                cmd.Parameters.AddWithValue("@supplier_name", ComboBox1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@S_no", Label3.Text);
                cmd.Parameters.AddWithValue("@shade_no", ComboBox6.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@precotorder", TextBox15.Text);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
                getinvoiceno1();
            }
            con1000.Close();
        }
    }
  
  
    protected void Button10_Click(object sender, EventArgs e)
    {



     
    }
   
    
}