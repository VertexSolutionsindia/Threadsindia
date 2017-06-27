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

public partial class Admin_Cash_bill_report : System.Web.UI.Page
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



            show_item();







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
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry where Com_Id='" + company_id + "'", con);
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
                Label Salary = (Label)e.Row.FindControl("lblgrand");

                m = m + float.Parse(Salary.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("grand");
                lblTotalPrice.Text = m.ToString();

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
                using (SqlCommand cmd = new SqlCommand("cash_bill_report", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fromdate", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@todate", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@customer", DropDownList1.SelectedItem.Text);
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
        report.ReportPath = Server.MapPath("~/Admin/CASH_BILL_REPORT.rdlc");
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
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry where customer='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "'", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
            con1000.Close();
        }
    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
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
                SqlCommand CMD = new SqlCommand("select * from cashbill_entry where date between '" + TextBox1.Text + "' and '" + TextBox2.Text + "' and  Com_Id='" + company_id + "'", con);
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