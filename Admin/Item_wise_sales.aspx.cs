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

public partial class Admin_Item_wise_sales : System.Web.UI.Page
{
    float m = 0;
    float m1 = 0;
    float m2 = 0;
    public static int company_id = 0;
    public static int company_id1 = 0;
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
                    Label1.Text = dr1000["company_name"].ToString();
                }
                con1000.Close();
            }







            BindData();



            show_cus();

            show_item();





        }
    }
    private void show_cus()
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
                SqlCommand cmd = new SqlCommand("Select * from party where category='Customer' and Com_Id='" + company_id + "' ORDER BY party_code asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList1.DataSource = ds;
                DropDownList1.DataTextField = "party_name";
                DropDownList1.DataValueField = "party_code";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Select item", "0"));
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
                SqlCommand cmd = new SqlCommand("Select * from item_master where Com_Id='" + company_id + "' ORDER BY item_code asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList3.DataSource = ds;
                DropDownList3.DataTextField = "item_name";
                DropDownList3.DataValueField = "item_code";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, new ListItem("Select item", "0"));
                con.Close();
            }
            con1000.Close();
        }
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







    private void show_company()
    {


    }

    private void show_barcode()
    {


    }
    private void show_category()
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
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry_details where Com_Id='" + company_id + "' union all select * from creditbill_entry_details where Com_Id='" + company_id + "'", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }

    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {



    }
    private void getinvoiceno()
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total : ";
            }

          

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Salary = (Label)e.Row.FindControl("lbltotal");

                m1 = m1 + float.Parse(Salary.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("total");
                lblTotalPrice.Text = m1.ToString();

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Salary = (Label)e.Row.FindControl("lbltotalqty");

                m2 = m2 + float.Parse(Salary.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("totalqty");
                lblTotalPrice.Text = m2.ToString();

            }

        }
        catch (Exception er)
        { }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from creditbill_entry_details where customer='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "' union all select * from cashbill_entry_details where customer='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
   
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("Select * from shade_master_details where item_name='"+DropDownList3.SelectedItem.Text+"' and Com_Id='" + company_id + "' ORDER BY shade_id asc", con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                DropDownList4.DataSource = ds;
                DropDownList4.DataTextField = "shade_no";
                DropDownList4.DataValueField = "shade_id";
                DropDownList4.DataBind();
                DropDownList4.Items.Insert(0, new ListItem("Select shade no", "0"));
                con.Close();



                SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD1 = new SqlCommand("select * from creditbill_entry_details where item_name='" + DropDownList3.SelectedItem.Text + "' and  Com_Id='" + company_id + "' union all select * from cashbill_entry_details where item_name='" + DropDownList3.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con1);
                DataTable dt11 = new DataTable();
                SqlDataAdapter da11 = new SqlDataAdapter(CMD1);
                da11.Fill(dt11);
                GridView1.DataSource = dt11;
                GridView1.DataBind();
            }
            con1000.Close();



                  
              
        }
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
                SqlCommand CMD = new SqlCommand("select * from creditbill_entry_details where item_name='" + DropDownList3.SelectedItem.Text + "' and shade_no='" + DropDownList4.SelectedItem.Text + "' and Com_Id='" + company_id + "' union all select * from cashbill_entry_details where item_name='" + DropDownList3.SelectedItem.Text + "' and shade_no='" + DropDownList4.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
}