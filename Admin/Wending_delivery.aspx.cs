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

public partial class Admin_Sales_entry_wholesales : System.Web.UI.Page
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
            getinvoiceno1();
            BindData2();
            show_category();
            showrating();


            active();
            created();

           
            show_shade_no();
            show_unit();
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
                SqlCommand cmd = new SqlCommand("Select * from party where  Com_Id='" + company_id + "' ORDER BY party_id asc", con);
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

    
    private void show_shade_no()
    {
        
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
                company_id = Convert.ToInt32(dr1000["com_id"].ToString());
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("Select * from unit where Com_Id='" + company_id + "' ORDER BY unit_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "unit_name";
                DropDownList2.DataValueField = "unit_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("KG", "1"));
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
                SqlCommand cmd2 = new SqlCommand("select * from wendingdly_entry where wed_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "'", con2);
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
                SqlCommand CMD = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
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
                SqlCommand CMD = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
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
                SqlCommand CMD = new SqlCommand("select * from wendingdly_entry where Com_Id='" + company_id + "' ORDER BY  wed_invoice asc", con);
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
        show_category();
        show_unit();
        show_shade_no();
       
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
       
        TextBox2.Text = "";
       
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
    
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
                string query = "Select Max(wed_invoice) from wendingdly_entry where Com_Id='" + company_id + "' ";
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
                string query = "Select Max(s_no) from Wendingdly_entry_details where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ";
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
        show_category();
        show_unit();
        show_shade_no();
      
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
       
        TextBox2.Text = "";
        
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
      
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
                SqlCommand cmd2 = new SqlCommand("select * from wendingdly_entry where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
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
                SqlCommand CMD = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
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
                if (DropDownList2.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from wendingdly_entry where wed_invoice='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {


                        string status = "Wending";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("update wendingdly_entry set date=@date,customer=@customer,address=@address,mobile_no=@mobile_no,Total_cones=@Total_cones,nett_total=@nett_total,Com_Id=@Com_Id,status=@status,value=@value where wed_invoice=@wed_invoice", con);
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
                        getinvoiceno1();
                        BindData();
                        show_category();
                        show_unit();
                        show_shade_no();
                      
                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";
                        
                        TextBox2.Text = "";
                     
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                   
                        TextBox7.Text = "";
                        

                    }
                    else
                    {


                        string status = "Wending";
                        float value = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);



                        SqlCommand cmd = new SqlCommand("INSERT INTO wendingdly_entry VALUES(@wed_invoice,@date,@customer,@address,@mobile_no,@Total_cones,@nett_total,@Com_Id,@status,@value)", con);
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

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Wending delivery created successfully')", true);
                        getinvoiceno();
                        getinvoiceno1();
                        BindData();
                        show_category();
                        show_unit();
                        show_shade_no();
                     
                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";
                     
                        TextBox2.Text = "";
                       
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                   
                     
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
                SqlCommand cmd21 = new SqlCommand("select max(purchase_invoice) from cashbill_entry where  Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from wendingdly_entry where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
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
                    SqlCommand CMD = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice='" + Label1.Text + "' and Com_Id='" + company_id + "' ORDER BY s_no asc", con);
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
                    show_category();
                    show_unit();
                    show_shade_no();
                  
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                    TextBox13.Text = "";
                   
                    TextBox2.Text = "";
               
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    
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

    }

    private void getpartyname()
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
                SqlCommand cmd = new SqlCommand("Select * from party where category='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY party_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList2.DataSource = ds;
                DropDownList2.DataTextField = "party_name";
                DropDownList2.DataValueField = "party_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, new ListItem("Select party", "0"));
                con.Close();
            }
            con1000.Close();
        }
    }


    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

        if (DropDownList1.SelectedItem.Text == "Select Category")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select category')", true);
        }
        else if (DropDownList2.SelectedItem.Text == "Select party")
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
                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("insert into party_wise_rate_details values(@party_id,@category,@party_name,@s_no,@item_name,@unit,@credit_rate,@cash_rate,@Com_Id)", con);
                    cmd.Parameters.AddWithValue("@party_id", Label1.Text);
                    cmd.Parameters.AddWithValue("@category", DropDownList1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@party_name", DropDownList2.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                    cmd.Parameters.AddWithValue("@item_name", DropDownList3.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@unit", DropDownList4.SelectedItem.Text);
                
                    cmd.Parameters.AddWithValue("@cash_rate", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    show_shade_no();
                    show_unit();
                    BindData();
                    getinvoiceno1();
                    
                    TextBox2.Text = "";

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

                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + row.Cells[1].Text + "' and shade_no='" + row.Cells[2].Text + "' and unit='" + row.Cells[4].Text + "'  AND Com_Id='" + company_id + "'  ", con1);






                cmd1.Parameters.AddWithValue("@qty", float.Parse(row.Cells[4].Text));

                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();

                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("delete from Wendingdly_entry_details where wed_invoice='" + Label1.Text + "' and s_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con3);

                con3.Open();
                cmd3.ExecuteNonQuery();
                con3.Close();





                show_shade_no();
                show_unit();
                BindData();
                getinvoiceno1();
           
                TextBox2.Text = "";
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
                SqlCommand cmd = new SqlCommand("Select * from shade_master_details where shade_no='" + DropDownList4.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY item_name asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = "item_name";
                DropDownList3.DataValueField = "shade_id";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("Select item_name", "0"));
                con.Close();
            }
            con1000.Close();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void TextBox5_TextChanged(object sender, EventArgs e)
    {
    
       
    }
   
    protected void Button8_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "Select party")
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
                    SqlCommand cmd111 = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice='" + Label1.Text + "' and s_no='" + Label3.Text + "' and Com_Id='" + company_id + "'  ", con111);
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
                        SqlCommand cmd = new SqlCommand("insert into Wendingdly_entry_details values(@wed_invoice,@customer,@s_no,@shade_no,@item_name,@lot,@Cones,@Gross_Wt,@Net_Wt,@Com_Id)", con);
                        cmd.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                        cmd.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@s_no", Label3.Text);
                        cmd.Parameters.AddWithValue("@shade_no", DropDownList4.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@item_name", DropDownList3.SelectedItem.Text);
                      
                      
                        cmd.Parameters.AddWithValue("@lot", DropDownList2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Cones", TextBox6.Text);
                        cmd.Parameters.AddWithValue("@Gross_Wt", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@Net_Wt", TextBox5.Text);
                     
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();



                        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("insert into Wendingdly_product_stock values(@wed_invoice,@customer,@s_no,@shade_no,@item_name,@lot,@Cones,@Gross_Wt,@Net_Wt,@Com_Id)", con1);
                        cmd1.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                        cmd1.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@s_no", Label3.Text);
                        cmd1.Parameters.AddWithValue("@shade_no", DropDownList4.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@item_name", DropDownList3.SelectedItem.Text);


                        cmd1.Parameters.AddWithValue("@lot", DropDownList2.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@Cones", TextBox6.Text);
                        cmd1.Parameters.AddWithValue("@Gross_Wt", TextBox2.Text);
                        cmd1.Parameters.AddWithValue("@Net_Wt", TextBox5.Text);

                        cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();
                    }
                    con111.Close();


                    SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd2 = new SqlCommand("update Product_stock set qty=qty-@qty where item_name='" + DropDownList3.SelectedItem.Text + "' and shade_no='" + DropDownList4.SelectedItem.Text + "' and unit='" + DropDownList2.SelectedItem.Text + "'  AND Com_Id='" + company_id + "'  ", con2);

                    cmd2.Parameters.AddWithValue("@qty", TextBox5.Text);

                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();











                }

                con1000.Close();
              
                getinvoiceno1();
                BindData();
               
                show_unit();
                show_shade_no();
                BindData2();

                TextBox2.Text = "";
                TextBox6.Text = "";

                TextBox5.Text = "";
               

            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label rate = (Label)e.Row.Cells[4].FindControl("lblcones");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot = tot +float.Parse(rate1);
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label rate = (Label)e.Row.Cells[6].FindControl("lblnetwt");
            if (rate != null)
            {
                string rate1 = rate.Text;
                tot1 = tot1 +float.Parse(rate1);
            }
        }






        TextBox10.Text = Convert.ToDecimal(tot).ToString("#,##0.00");
        TextBox11.Text = Convert.ToDecimal(tot1).ToString("#,##0.00");
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

       
    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {
        try
        {
            float cones = float.Parse(TextBox6.Text);
            float nett_wt = float.Parse("1.5");
            float nett_total = (cones * nett_wt);
            TextBox5.Text = string.Format("{0:0.00}", nett_total).ToString();
        }
        catch (Exception er)
        { }
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
                                .FindControl("lbls_no")).Text;
                string shadeno = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtshadeno")).Text;
                string itamName = ((TextBox)GridView1.Rows[e.RowIndex]
                                    .FindControl("txtitemName")).Text;
               

                string lot = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtlot")).Text;

                string cones = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtcones")).Text;

                string gross_wt = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtgrosswt")).Text;
                string nett_wt = ((TextBox)GridView1.Rows[e.RowIndex]
                                   .FindControl("txtnetwt")).Text;


                SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd10 = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con10);
                cmd10.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                cmd10.Parameters.AddWithValue("@s_no", s_no);
                SqlDataReader dr10;
                con10.Open();
                dr10 = cmd10.ExecuteReader();
                if (dr10.Read())
                {
                    string itemname1 = dr10["item_name"].ToString();
                    string shadeno1 = dr10["shade_no"].ToString();
                    string unit1 = dr10["lot"].ToString();
                    float qty1 = float.Parse(dr10["Net_Wt"].ToString());
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + itemname1 + "' and shade_no='" + shadeno1 + "' and unit='" + unit1 + "'  AND Com_Id='" + company_id + "'  ", con2);

                    cmd1.Parameters.AddWithValue("@qty", qty1);

                    con2.Open();
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                }
                con10.Close();


                if (DropDownList1.SelectedItem.Text == "Select party")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please select party')", true);
                }
                else
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd = new SqlCommand("update Wendingdly_entry_details set customer=@customer,shade_no=@shade_no,item_name=@item_name,lot=@lot,Cones=@Cones,Gross_Wt=@Gross_Wt,Net_Wt=@Net_Wt,Com_Id=@Com_Id where wed_invoice=@wed_invoice and s_no=@s_no", con);
                    cmd.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                    cmd.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@s_no", s_no);
                    cmd.Parameters.AddWithValue("@shade_no", shadeno);
                    cmd.Parameters.AddWithValue("@item_name", itamName);


                    cmd.Parameters.AddWithValue("@lot", lot);
                    cmd.Parameters.AddWithValue("@Cones", cones);
                    cmd.Parameters.AddWithValue("@Gross_Wt", gross_wt);
                    cmd.Parameters.AddWithValue("@Net_Wt", nett_wt);

                    cmd.Parameters.AddWithValue("@Com_Id", company_id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update Wendingdly_product_stock set customer=@customer,shade_no=@shade_no,item_name=@item_name,lot=@lot,Cones=@Cones,Gross_Wt=@Gross_Wt,Net_Wt=@Net_Wt,Com_Id=@Com_Id where wed_invoice=@wed_invoice and s_no=@s_no", con1);
                    cmd1.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                    cmd1.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
                    cmd1.Parameters.AddWithValue("@s_no", s_no);
                    cmd1.Parameters.AddWithValue("@shade_no", shadeno);
                    cmd1.Parameters.AddWithValue("@item_name", itamName);


                    cmd1.Parameters.AddWithValue("@lot", lot);
                    cmd1.Parameters.AddWithValue("@Cones", cones);
                    cmd1.Parameters.AddWithValue("@Gross_Wt", gross_wt);
                    cmd1.Parameters.AddWithValue("@Net_Wt", nett_wt);

                    cmd1.Parameters.AddWithValue("@Com_Id", company_id);
                    con1.Open();
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                    GridView1.EditIndex = -1;
                    BindData();
                    getinvoiceno1();
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



                SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd10 = new SqlCommand("select * from Wendingdly_entry_details where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con10);
                cmd10.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                cmd10.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);
                SqlDataReader dr10;
                con10.Open();
                dr10 = cmd10.ExecuteReader();
                if (dr10.Read())
                {
                    string itemname = dr10["item_name"].ToString();
                    string shadeno = dr10["shade_no"].ToString();
                    string unit = dr10["lot"].ToString();
                    float qty = float.Parse(dr10["Net_Wt"].ToString());
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("update Product_stock set qty=qty+@qty where item_name='" + itemname + "' and shade_no='" + shadeno + "' and unit='" + unit + "'  AND Com_Id='" + company_id + "'  ", con2);

                    cmd1.Parameters.AddWithValue("@qty", qty);

                    con2.Open();
                    cmd1.ExecuteNonQuery();
                    con2.Close();
                }
                con10.Close();

                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                SqlCommand cmd = new SqlCommand("delete from Wendingdly_entry_details where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con);
                cmd.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                cmd.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlConnection con101 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd101 = new SqlCommand("delete from Wendingdly_product_stock where wed_invoice=@wed_invoice and s_no=@s_no and Com_Id='" + company_id + "'", con101);
                cmd101.Parameters.AddWithValue("@wed_invoice", Label1.Text);
                cmd101.Parameters.AddWithValue("@s_no", lnkRemove.CommandArgument);
                con101.Open();
                cmd101.ExecuteNonQuery();
                con101.Close();


                BindData();
                getinvoiceno1();



            }
            con10001.Close();
        }
    }

    protected void txtcones_TextChanged(object sender, EventArgs e)
    {


       



        string cones = ((TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtcones")).Text;
        float nett_wt = float.Parse("1.5");
      
        TextBox total = ((TextBox)GridView1.Rows[GridView1.EditIndex].FindControl("txtnetwt"));
        float nett_total = (float.Parse( cones) * nett_wt);

        total.Text = nett_total.ToString();

        
    }
    protected void DropDownList3_SelectedIndexChanged1(object sender, EventArgs e)
    {
        SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd3 = new SqlCommand("select * from Product_stock where item_name='" + DropDownList3.SelectedItem.Text + "' and shade_no='" + DropDownList4.SelectedItem.Text + "' and unit='KG' and  Com_Id='" + company_id + "'", con3);
        SqlDataReader dr3;
        con3.Open();
        dr3 = cmd3.ExecuteReader();
        if (dr3.Read())
        {

            TextBox1.Text = dr3["qty"].ToString();



        }
        con3.Close();
    }
}