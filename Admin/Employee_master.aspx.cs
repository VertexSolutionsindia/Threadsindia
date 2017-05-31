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
public partial class Admin_Cutomer_type : System.Web.UI.Page
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




            getinvoiceno();
            show_category();
            showrating();
            BindData();
           
            active();
            created();

            BindData2();
            show_department();
            show_designation();

        }


    }
    private void show_department()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from department where Com_Id='" + company_id + "' ORDER BY dep_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "dep_name";
        DropDownList2.DataValueField = "dep_id";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("Select department", "0"));
        con.Close();
    }

    private void show_designation()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd = new SqlCommand("Select * from designation where Com_Id='" + company_id + "' ORDER BY des_id asc", con);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        DropDownList3.DataSource = ds;
        DropDownList3.DataTextField = "des_name";
        DropDownList3.DataValueField = "des_id";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("Select Designation", "0"));
        con.Close();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;

        SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd2 = new SqlCommand("select * from employee_master where emp_id='" + row.Cells[0].Text + "' and  Com_Id='" + company_id + "'", con2);
        SqlDataReader dr2;
        con2.Open();
        dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Label1.Text = dr2["emp_id"].ToString();
            TextBox2.Text = dr2["emp_name"].ToString();
            DropDownList1.SelectedItem.Text = dr2["gender"].ToString();
            TextBox4.Text =Convert.ToDateTime( dr2["dob"]).ToString("MM/dd/yyyy");
            TextBox5.Text = Convert.ToDateTime(dr2["doj"]).ToString("MM/dd/yyyy");
            DropDownList2.SelectedItem.Text = dr2["dep"].ToString();
            DropDownList3.SelectedItem.Text = dr2["des"].ToString();
            TextBox3.Text = dr2["com_amount"].ToString();
            TextBox1.Text = dr2["salary"].ToString();
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
                SqlCommand CMD = new SqlCommand("select * from employee_master where Com_Id='" + company_id + "' ORDER BY emp_id asc", con);
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

                 SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from employee_master where emp_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("delete from employee_master where emp_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Employee deleted successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Employee details does not exist')", true);

                    }
                BindData();


                getinvoiceno();


              
                BindData2();
                show_department();
             
                show_designation();

            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        BindData();
        show_designation();
        TextBox2.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";

        getinvoiceno();

        TextBox1.Text = "";
       
        BindData2();
        show_department();
        TextBox3.Text = "";
        DropDownList1.SelectedItem.Text = "Select Gender";
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
                    SqlCommand cmd = new SqlCommand("delete from employee_master where emp_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('employee deleted successfully')", true);
                    BindData();


                    getinvoiceno();


                   
                    BindData2();
                    show_department();
                  
                    show_designation();
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
                string query = "Select Max(emp_id) from employee_master where Com_Id='" + company_id + "' ";
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
        
        BindData2();
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox2.Text = "";
        show_designation();
       show_department();
       TextBox3.Text = "";
       TextBox1.Text = "";
       DropDownList1.SelectedItem.Text = "Select Gender";

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
                SqlCommand cmd2 = new SqlCommand("select * from employee_master where emp_id='" + Label1.Text + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                   
                    TextBox2.Text = dr2["emp_name"].ToString();
                    DropDownList1.SelectedItem.Text = dr2["gender"].ToString();
                    TextBox4.Text = Convert.ToDateTime(dr2["dob"]).ToString("MM/dd/yyyy");
                    TextBox5.Text = Convert.ToDateTime(dr2["doj"]).ToString("MM/dd/yyyy");
                    DropDownList2.SelectedItem.Text = dr2["dep"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["des"].ToString();
                    TextBox3.Text = dr2["com_amount"].ToString();
                    TextBox1.Text = dr2["salary"].ToString();
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
                if (TextBox2.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter employee name')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from employee_master where emp_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("Update employee_master set emp_name=@emp_name,gender=@gender,dob=@dob,doj=@doj,dep=@dep,des=@des,Com_Id=@Com_Id,com_amount=@com_amount,salary=@salary where emp_id=@emp_id", CON);
                        cmd.Parameters.AddWithValue("@emp_id", Label1.Text);
                    
                        cmd.Parameters.AddWithValue("@emp_name", TextBox2.Text);
                        cmd.Parameters.AddWithValue("@gender", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@dob", TextBox4.Text);
                        cmd.Parameters.AddWithValue("@doj", TextBox5.Text);
                        cmd.Parameters.AddWithValue("@dep", DropDownList2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@des", DropDownList3.SelectedItem.Text);

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@com_amount", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@salary", TextBox1.Text);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Employee updated successfully')", true);
                        getinvoiceno();

                        BindData2();
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox2.Text = "";
                        show_designation();
                        show_department();
                        TextBox3.Text = "";
                        TextBox1.Text = "";
                        DropDownList1.SelectedItem.Text = "Select Gender";
                        BindData();
                        DropDownList1.SelectedItem.Text = "Select Gender";
                    }
                    else
                    {


                        SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("insert into employee_master values(@emp_id,@emp_name,@gender,@dob,@doj,@dep,@des,@Com_Id,@com_amount,@salary)", CON);
                        cmd.Parameters.AddWithValue("@emp_id", Label1.Text);
                     
                        cmd.Parameters.AddWithValue("@emp_name",TextBox2.Text);
                        cmd.Parameters.AddWithValue("@gender", DropDownList1.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@dob",TextBox4.Text);
                        cmd.Parameters.AddWithValue("@doj",TextBox5.Text);
                        cmd.Parameters.AddWithValue("@dep", DropDownList2.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@des", DropDownList3.SelectedItem.Text);

                        cmd.Parameters.AddWithValue("@Com_Id", company_id);
                        cmd.Parameters.AddWithValue("@com_amount", TextBox3.Text);
                        cmd.Parameters.AddWithValue("@salary", TextBox1.Text);
                        CON.Open();
                        cmd.ExecuteNonQuery();
                        CON.Close();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Employee created successfully')", true);
                        BindData();


                 



                        getinvoiceno();

                        BindData2();
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                        TextBox2.Text = "";
                        show_designation();
                        show_department();
                        TextBox3.Text = "";
                        TextBox1.Text = "";
                        DropDownList1.SelectedItem.Text = "Select Gender";
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
                SqlCommand cmd21 = new SqlCommand("select max(dep_id) from department where  Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from employee_master where emp_id='" + Label1.Text + "' and  Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    
                    TextBox2.Text = dr2["emp_name"].ToString();
                    DropDownList1.SelectedItem.Text = dr2["gender"].ToString();
                    TextBox4.Text = Convert.ToDateTime(dr2["dob"]).ToString("MM/dd/yyyy");
                    TextBox5.Text = Convert.ToDateTime(dr2["doj"]).ToString("MM/dd/yyyy");
                    DropDownList2.SelectedItem.Text = dr2["dep"].ToString();
                    DropDownList3.SelectedItem.Text = dr2["des"].ToString();
                    TextBox3.Text = dr2["com_amount"].ToString();
                    TextBox1.Text = dr2["salary"].ToString();
                }
                else
                {

                    BindData();


                    getinvoiceno();


                    TextBox1.Text = "";
                    TextBox4.Text = "";
                    TextBox2.Text = "";
                    TextBox5.Text = "";
                    BindData2();
                    show_department();
                 
                    show_designation();
                    TextBox3.Text = "";
                    DropDownList1.SelectedItem.Text = "Select Gender";
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
   
    protected void Button10_Click(object sender, EventArgs e)
    {

       
    }
}
