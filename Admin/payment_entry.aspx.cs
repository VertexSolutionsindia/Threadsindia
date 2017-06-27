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

public partial class Admin_payment_entry : System.Web.UI.Page
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
            SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand cmd10 = new SqlCommand("select * from currentfinancialyear where no='1'", con10);
            SqlDataReader dr10;
            con10.Open();
            dr10 = cmd10.ExecuteReader();
            if (dr10.Read())
            {
                Label3.Text = dr10["financial_year"].ToString();

            }
            con10.Close();


            DateTime date = DateTime.Now;
            TextBox9.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

            getinvoiceno();
            show_category();
            showrating();
            BindData();

            active();
            created();

            BindData2();
            showcustomer();

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

                ComboBox2.DataSource = ds;
                ComboBox2.DataTextField = "party_name";
                ComboBox2.DataValueField = "party_id";
                ComboBox2.DataBind();
                ComboBox2.Items.Insert(0, new ListItem("Select party", "1"));


                con.Close();
            }
            con1000.Close();
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;

        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from payment_entry where ID='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["cat_id"].ToString();
            TextBox9.Text = Convert.ToDateTime(dr2["Date"]).ToString("dd-MM-yyyy");
            ComboBox1.SelectedItem.Text = dr2["pay_mode"].ToString();
            ComboBox2.SelectedItem.Text = dr2["sup_name"].ToString();
            TextBox10.Text = dr2["address"].ToString();
            TextBox11.Text = dr2["mobile_no"].ToString();
            TextBox1.Text = dr2["bank_name"].ToString();
            TextBox2.Text = dr2["amount"].ToString();
            TextBox4.Text = dr2["data"].ToString();
            TextBox6.Text = dr2["utr_no"].ToString();
            TextBox7.Text = dr2["oldbalance"].ToString();
            TextBox8.Text = dr2["coll_amount"].ToString();
            TextBox5.Text = dr2["newbalance"].ToString();


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
                SqlCommand CMD = new SqlCommand("select * from payment_entry where Com_Id='" + company_id + "' ORDER BY ID asc", con);
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
                SqlCommand cmd = new SqlCommand("delete from pay_amount where invoice_no='" + Label1.Text + "' and  Com_Id='" + company_id + "'", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from collection_entry where ID='" + Label1.Text + "' and  Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    float pending = float.Parse(dr3["coll_amount"].ToString());
                    string supplier = dr3["sup_name"].ToString();
                    SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd4 = new SqlCommand("update pay_amount_status set pending_amount=pending_amount+@pending_amount where Supplier='" + supplier + "' and Com_Id='" + company_id + "'", con4);
                    cmd4.Parameters.AddWithValue("@pending_amount", pending);
                    con4.Open();
                    cmd4.ExecuteNonQuery();
                    con4.Close();
                }
                con3.Close();

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("delete from payment_entry where ID='" + Label1.Text + "' and  Com_Id='" + company_id + "'", con2);

                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();


               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Collected amount deleted successfully')", true);
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
        BindData2();
        showcustomer();
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        ComboBox1.SelectedItem.Text = "Select Item";
        DateTime date = DateTime.Now;
        TextBox9.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

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
                SqlCommand CMD = new SqlCommand("select * from payment_entry where Com_Id='" + company_id + "' ORDER BY ID asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView2.DataSource = dt1;
                GridView2.DataBind();
            }
            con1000.Close();
        }


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
                    SqlCommand cmd = new SqlCommand("delete from category_entry where cat_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Category deleted successfully')", true);

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
                string query = "Select Max(ID) from payment_entry where Com_Id='" + company_id + "' ";
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
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        BindData();
        BindData2();
        showcustomer();
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        ComboBox1.SelectedItem.Text = "Select Item";
        DateTime date = DateTime.Now;
        TextBox9.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");


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
                SqlCommand cmd2 = new SqlCommand("select * from payment_entry where ID='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    TextBox9.Text = Convert.ToDateTime(dr2["Date"]).ToString("dd-MM-yyyy");
                    ComboBox1.SelectedItem.Text = dr2["pay_mode"].ToString();
                    ComboBox2.SelectedItem.Text = dr2["sup_name"].ToString();
                    TextBox1.Text = dr2["bank_name"].ToString();
                    TextBox10.Text = dr2["address"].ToString();
                    TextBox11.Text = dr2["mobile_no"].ToString();
                    TextBox2.Text = dr2["amount"].ToString();
                    TextBox4.Text = dr2["data"].ToString();
                    TextBox6.Text = dr2["utr_no"].ToString();
                    TextBox7.Text = dr2["oldbalance"].ToString();
                    TextBox8.Text = dr2["coll_amount"].ToString();
                    TextBox5.Text = dr2["newbalance"].ToString();
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
                if (Label1.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter category Name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from payment_entry where ID='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("Update payemnt_entry set pay_mode=@pay_mode,sup_name=@sup_name,address=@address,mobile_no=@mobile_no,bank_name=@bank_name,amount=@amount,data=@data,utr_no=@utr_no,oldbalance=@oldbalance,coll_amount=@coll_amount,newbalance=@newbalance where ID=ID and Com_Id=@Com_Id", CON);
                        cmd.Parameters.AddWithValue("@ID", Label1.Text);
                        cmd.Parameters.AddWithValue("@pay_mode", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@sup_name", ComboBox2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox11.Text);
                        cmd.Parameters.AddWithValue("@bank_name", TextBox1.Text);
                        cmd.Parameters.AddWithValue("@amount", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@data", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@utr_no", TextBox6.Text);
                        cmd.Parameters.AddWithValue("@oldbalance", float.Parse(TextBox7.Text));
                        cmd.Parameters.AddWithValue("@coll_amount", float.Parse(TextBox8.Text));
                        cmd.Parameters.AddWithValue("@newbalance", float.Parse(TextBox5.Text));
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();



                        string return_by = "";
                        int value1 = 0;
                        string status1 = "Collection amount";
                        SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand cmd23 = new SqlCommand("update pay_amount set Supplier=@Supplier,Pay_date=@Pay_date,Estimate_value=@Estimate_value,address=@address,total_amount=@total_amount,pay_amount=@pay_amount,pending_amount=@pending_amount,outstanding=@outstanding,Com_Id=@Com_Id,status=@status,value=@value,year=@year where invoice_no=@invoice_no and Com_Id=@Com_Id", con23);
                        cmd23.Parameters.AddWithValue("@Supplier", ComboBox2.SelectedItem.Text);
                        cmd23.Parameters.AddWithValue("@Pay_date", Convert.ToDateTime(TextBox9.Text).ToString("MM-dd-yyyy"));
                        cmd23.Parameters.AddWithValue("@Estimate_value", DBNull.Value);
                        cmd23.Parameters.AddWithValue("@address", TextBox10.Text);
                        cmd23.Parameters.AddWithValue("@total_amount", float.Parse(TextBox7.Text));


                        cmd23.Parameters.AddWithValue("@pay_amount", TextBox8.Text);




                        cmd23.Parameters.AddWithValue("@pending_amount", TextBox5.Text);
                        cmd23.Parameters.AddWithValue("@outstanding", TextBox5.Text);
                        cmd23.Parameters.AddWithValue("@invoice_no", Label1.Text);
                        cmd23.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd23.Parameters.AddWithValue("@status", status1);
                        cmd23.Parameters.AddWithValue("@value", value1);
                        cmd23.Parameters.AddWithValue("@year", Label3.Text);
                        con23.Open();
                        cmd23.ExecuteNonQuery();
                        con23.Close();


                        SqlConnection con22 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand cmd22 = new SqlCommand("update pay_amount_status set Supplier=@Supplier,address=@address,total_amount=@total_amount,pending_amount=@pending_amount,paid_amount=@paid_amount,Com_Id=@Com_Id where Supplier='" + ComboBox2.SelectedItem.Text + "' and year='" + Label3.Text + "' ", con22);


                        cmd22.Parameters.AddWithValue("@Supplier", ComboBox2.SelectedItem.Text);

                        cmd22.Parameters.AddWithValue("@address", TextBox10.Text);






                        cmd22.Parameters.AddWithValue("@total_amount", TextBox5.Text);
                        cmd22.Parameters.AddWithValue("@pending_amount", TextBox5.Text);
                        cmd22.Parameters.AddWithValue("@paid_amount", TextBox8.Text);
                        cmd22.Parameters.AddWithValue("@Com_Id", company_id);

                        con22.Open();
                        cmd22.ExecuteNonQuery();
                        con22.Close();






                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Amount updated successfully')", true);
                        getinvoiceno();
                        BindData();
                        BindData2();
                        showcustomer();
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox6.Text = "";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        ComboBox1.SelectedItem.Text = "Select Item";
                        DateTime date = DateTime.Now;
                        TextBox9.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");

                    }
                    else
                    {


                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into payment_entry values(@ID,@Date,@pay_mode,@sup_name,@address,@mobile_no,@bank_name,@amount,@data,@utr_no,@oldbalance,@coll_amount,@newbalance,@Com_Id)", CON);
                        cmd.Parameters.AddWithValue("@ID", Label1.Text);
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(TextBox9.Text).ToString("MM-dd-yyyy"));
                        cmd.Parameters.AddWithValue("@pay_mode", ComboBox1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@sup_name", ComboBox2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@address", TextBox10.Text);
                        cmd.Parameters.AddWithValue("@mobile_no", TextBox11.Text);
                        cmd.Parameters.AddWithValue("@bank_name", TextBox1.Text);
                        if (TextBox2.Text == "")
                        {
                            float a1 = 0;
                            cmd.Parameters.AddWithValue("@amount", a1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@amount", TextBox2.Text);
                        }

                        if (TextBox4.Text == "")
                        {
                            cmd.Parameters.AddWithValue("@data", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data", Convert.ToDateTime(TextBox4.Text).ToString("MM-dd-yyyy"));
                        }

                        cmd.Parameters.AddWithValue("@utr_no", TextBox6.Text);
                        cmd.Parameters.AddWithValue("@oldbalance", float.Parse(TextBox7.Text));
                        cmd.Parameters.AddWithValue("@coll_amount", TextBox8.Text);
                        cmd.Parameters.AddWithValue("@newbalance", TextBox5.Text);
                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();




                        string return_by = "";
                        int value1 = 0;
                        string status1 = "Paid amount";
                        SqlConnection con23 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand cmd23 = new SqlCommand("insert into pay_amount values(@Supplier,@Pay_date,@Estimate_value,@address,@total_amount,@pay_amount,@pending_amount,@outstanding,@invoice_no,@Com_Id,@status,@value,@year)", con23);
                        cmd23.Parameters.AddWithValue("@Supplier", ComboBox2.SelectedItem.Text);
                        cmd23.Parameters.AddWithValue("@Pay_date", Convert.ToDateTime(TextBox9.Text).ToString("MM-dd-yyyy"));
                        cmd23.Parameters.AddWithValue("@Estimate_value", DBNull.Value);
                        cmd23.Parameters.AddWithValue("@address", TextBox10.Text);
                        cmd23.Parameters.AddWithValue("@total_amount", float.Parse(TextBox7.Text));


                        cmd23.Parameters.AddWithValue("@pay_amount", TextBox8.Text);




                        cmd23.Parameters.AddWithValue("@pending_amount", TextBox5.Text);
                        cmd23.Parameters.AddWithValue("@outstanding", TextBox5.Text);
                        cmd23.Parameters.AddWithValue("@invoice_no", Label1.Text);
                        cmd23.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd23.Parameters.AddWithValue("@status", status1);
                        cmd23.Parameters.AddWithValue("@value", value1);
                        cmd23.Parameters.AddWithValue("@year", Label3.Text);
                        con23.Open();
                        cmd23.ExecuteNonQuery();
                        con23.Close();



                        SqlConnection con22 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                        SqlCommand cmd22 = new SqlCommand("update pay_amount_status set Supplier=@Supplier,address=@address,total_amount=@total_amount,pending_amount=@pending_amount,paid_amount=@paid_amount,Com_Id=@Com_Id where Supplier='" + ComboBox2.SelectedItem.Text + "' and year='" + Label3.Text + "' ", con22);


                        cmd22.Parameters.AddWithValue("@Supplier", ComboBox2.SelectedItem.Text);

                        cmd22.Parameters.AddWithValue("@address", TextBox10.Text);






                        cmd22.Parameters.AddWithValue("@total_amount", TextBox5.Text);
                        cmd22.Parameters.AddWithValue("@pending_amount", TextBox5.Text);
                        cmd22.Parameters.AddWithValue("@paid_amount", TextBox8.Text);
                        cmd22.Parameters.AddWithValue("@Com_Id", company_id);

                        con22.Open();
                        cmd22.ExecuteNonQuery();
                        con22.Close();






                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Amount added successfully')", true);
                        BindData();

                        TextBox5.Text = "";








                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Amount Collected successfully')", true);
                        getinvoiceno();
                        BindData();
                        BindData2();
                        showcustomer();
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox6.Text = "";
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        ComboBox1.SelectedItem.Text = "Select Item";
                        DateTime date = DateTime.Now;
                        TextBox9.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");
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
                SqlCommand cmd21 = new SqlCommand("select max(ID) from payment_entry where  Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from payment_Entry where ID='" + Label1.Text + "' and  Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {

                    TextBox9.Text = Convert.ToDateTime(dr2["Date"]).ToString("dd-MM-yyyy");
                    ComboBox1.SelectedItem.Text = dr2["pay_mode"].ToString();
                    ComboBox2.SelectedItem.Text = dr2["sup_name"].ToString();
                    TextBox10.Text = dr2["address"].ToString();
                    TextBox11.Text = dr2["mobile_no"].ToString();
                    TextBox1.Text = dr2["bank_name"].ToString();
                    TextBox2.Text = dr2["amount"].ToString();
                    TextBox4.Text = dr2["data"].ToString();
                    TextBox6.Text = dr2["utr_no"].ToString();
                    TextBox7.Text = dr2["oldbalance"].ToString();
                    TextBox8.Text = dr2["coll_amount"].ToString();
                    TextBox5.Text = dr2["newbalance"].ToString();

                }
                else
                {

                    getinvoiceno();
                    BindData();
                    BindData2();
                    showcustomer();
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    TextBox8.Text = "";
                    ComboBox1.SelectedItem.Text = "Select Item";
                    DateTime date = DateTime.Now;
                    TextBox9.Text = Convert.ToDateTime(date).ToString("dd-MM-yyyy");



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
                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from pay_amount_status where Supplier='" + ComboBox2.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {



                    TextBox7.Text = Convert.ToDecimal(dr3["pending_amount"]).ToString("#,##0.00");


                }
                con3.Close();
                SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd4 = new SqlCommand("select * from party where party_name='" + ComboBox2.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con4);
                SqlDataReader dr4;
                con4.Open();
                dr4 = cmd4.ExecuteReader();
                if (dr4.Read())
                {



                    TextBox10.Text = dr4["address"].ToString();
                    TextBox11.Text = dr4["mobile_no"].ToString();

                }
                con4.Close();
            }
            con1000.Close();
        }
    }
    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {
        try
        {

            float old = float.Parse(TextBox7.Text);
            float collected = float.Parse(TextBox8.Text);
            TextBox5.Text = (old - collected).ToString();
        }
        catch (Exception er)
        { }
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


                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd = new SqlCommand("delete from pay_amount where invoice_no='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd3 = new SqlCommand("select * from payment_entry where ID='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con3);
                SqlDataReader dr3;
                con3.Open();
                dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    float pending = float.Parse(dr3["coll_amount"].ToString());
                    string supplier = dr3["cus_name"].ToString();
                    SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd4 = new SqlCommand("update pay_amount_status set pending_amount=pending_amount+@pending_amount where Supplier='" + supplier + "' and Com_Id='" + company_id + "'", con4);
                    cmd4.Parameters.AddWithValue("@pending_amount", pending);
                    con4.Open();
                    cmd4.ExecuteNonQuery();
                    con4.Close();
                }
                con3.Close();

                SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand cmd2 = new SqlCommand("delete from payment_entry where ID='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con2);

                con2.Open();
                cmd2.ExecuteNonQuery();
                con2.Close();

            }
            con1000.Close();
        }
    }
}