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
            TextBox18.Focus();
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
         
            BindData2();

            showrating();


            active();


            BindGridview();

        
          
         
        }


      
           
       
    }

    protected void BindGridview()
    {
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Customer_po_details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@status", "SELECT");
            cmd.Parameters.AddWithValue("@invoice", Label1.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                generateautoid();
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
             
                GridViewRow row = gvDetails.FooterRow;
                TextBox s_no = (TextBox)row.FindControl("sno");
                s_no.Text = "1";
               
            }
        }
    }
    private void generateautoid()
    {
        try
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
                        GridViewRow row = gvDetails.FooterRow;
                        TextBox s_no = (TextBox)row.FindControl("sno") as TextBox;
                        string val = dr[0].ToString();
                        if (val == "")
                        {
                            s_no.Text = "1";
                        }
                        else
                        {
                            a = Convert.ToInt32(dr[0].ToString());
                            a = a + 1;
                            s_no.Text = a.ToString();
                        }
                    }



                    con1.Close();
                }
                con1000.Close();
            }
        }
        catch (Exception er)
        { }

    }
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            int invoice =Convert.ToInt32(Label1.Text);
            string customer = TextBox18.Text;
            string year = Label4.Text;
            int Com_id = company_id;
            TextBox sno = (TextBox)gvDetails.FooterRow.FindControl("sno");
            TextBox itemname = (TextBox)gvDetails.FooterRow.FindControl("itemname");
          
            TextBox shadeno = (TextBox)gvDetails.FooterRow.FindControl("shadeno");
            TextBox color = (TextBox)gvDetails.FooterRow.FindControl("color");
            TextBox unit = (TextBox)gvDetails.FooterRow.FindControl("unit");
            TextBox rate = (TextBox)gvDetails.FooterRow.FindControl("rate");
            TextBox qty = (TextBox)gvDetails.FooterRow.FindControl("qty");
            TextBox total = (TextBox)gvDetails.FooterRow.FindControl("total");
            crudoperations("INSERT", invoice, customer,Convert.ToInt32(sno.Text), itemname.Text, shadeno.Text, color.Text, unit.Text, rate.Text, qty.Text, total.Text,Com_id, year);
        }


    }




    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
    }
    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       
    }
    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        BindGridview();
    }
    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void crudoperations( string status,int invoice,string customer,int productid, string itemname, string shadeno, string color, string unit, string rate, string qty, string total,int com_id,string year)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Customer_po_details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (status == "INSERT")
            {
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@invoice", invoice);
                cmd.Parameters.AddWithValue("@customer",customer);
                cmd.Parameters.AddWithValue("@s_no", productid);
                cmd.Parameters.AddWithValue("@item_name", itemname);
                cmd.Parameters.AddWithValue("@shade_no", shadeno);
                cmd.Parameters.AddWithValue("@color", color);
                cmd.Parameters.AddWithValue("@unit", unit);
                cmd.Parameters.AddWithValue("@rate", rate);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@total_amount", total);
                cmd.Parameters.AddWithValue("@Com_Id", com_id);
                cmd.Parameters.AddWithValue("@year",year);
            }
            else if (status == "UPDATE")
            {


            }
            else if (status == "DELETE")
            {

            }
            cmd.ExecuteNonQuery();
            lblresult.ForeColor = Color.Green;
            lblresult.Text = itemname + " details " + status.ToLower() + "d successfully";
            gvDetails.EditIndex = -1;
            BindGridview();
            generateautoid();
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
                SqlCommand cmd2 = new SqlCommand("select * from customerpo_entry where purchase_invoice='" + row.Cells[0].Text + "' and Com_Id='" + company_id + "' and year='"+Label4.Text+"'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["purchase_invoice"].ToString();
                    TextBox13.Text = Convert.ToDateTime(dr2["date"]).ToString("MM-dd-yyyy");

                    TextBox18.Text = dr2["customer"].ToString();
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
         


                getinvoiceno();
                BindGridview();

                BindData2();

            }
            con1000.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        BindGridview();
     
        TextBox17.Text = "";

        TextBox10.Text = "";
        TextBox11.Text = "";


   
        TextBox3.Text = "";
        TextBox4.Text = "";
      
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        TextBox12.Text = "";
       
      
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
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {



    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        getinvoiceno();
        BindGridview();
        TextBox17.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";


   
        TextBox3.Text = "";
        TextBox4.Text = "";
     
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox14.Text = "";
        TextBox12.Text = "";
    
      
    
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

                    TextBox18.Text = dr2["customer"].ToString();
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

                BindGridview();
              
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
                if (TextBox18.Text== "")
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
                        cmd.Parameters.AddWithValue("@customer", TextBox18.Text);
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
                   
                        BindData2();

                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";

                  
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                     
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        TextBox9.Text = "";
                        TextBox14.Text = "";
                        TextBox12.Text = "";
                      
                   
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
                        cmd.Parameters.AddWithValue("@customer", TextBox18.Text);
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
                    

                        TextBox10.Text = "";
                        TextBox11.Text = "";
                        TextBox13.Text = "";

                    
                        TextBox3.Text = "";
                        TextBox4.Text = "";
                      
                        TextBox7.Text = "";
                        TextBox8.Text = "";
                        TextBox9.Text = "";
                        TextBox14.Text = "";
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

                    TextBox18.Text = dr2["customer"].ToString();
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

                    BindGridview();
                  
                }
                else
                {

                    getinvoiceno();

                    BindGridview();

                    TextBox10.Text = "";
                    TextBox11.Text = "";

                    TextBox17.Text = "";
                
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                 
                    TextBox7.Text = "";
                    TextBox17.Text="";
                    TextBox8.Text = "";
                    TextBox9.Text = "";
                    TextBox14.Text = "";
                    TextBox12.Text = "";
                    DateTime date = DateTime.Now;
                    TextBox13.Text = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
                  
                 
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
                SqlCommand cmd2 = new SqlCommand("select * from party where party_name='" + TextBox18.Text + "' and Com_Id='" + company_id + "'", con2);
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




   
   



   
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

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




             



            }
            con10001.Close();
        }
    }
   
   
   
    
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchparty(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                string category = "Customer";
                cmd.CommandText = "select party_name  from party where category='"+category+"' and  Com_Id=@Com_Id and  " +
                "party_name like @party_name + '%' ";
                cmd.Parameters.AddWithValue("@party_name", prefixText);
                cmd.Parameters.AddWithValue("@Com_Id", company_id);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["party_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> SearchCustomers1(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = @"Data Source=BESTSHOPPEE2\SQLEXPRESS;Initial Catalog=Threadsindia;Integrated Security=True";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct item_name from shade_master_details where " +
                "item_name like @item_name + '%'";
                cmd.Parameters.AddWithValue("@item_name", prefixText);

                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["item_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
   

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]

    public static List<string> Searchshadeno(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.AppSettings["connection"];

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct shade_no from shade_master_details where item_name='" + item_name1 + "' and " +
                "shade_no like @shade_no + '%'";
                cmd.Parameters.AddWithValue("@shade_no", prefixText);

                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using ( SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["shade_no"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    
    protected void TextBox18_TextChanged(object sender, EventArgs e)
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
                SqlCommand cmd2 = new SqlCommand("select * from party where party_name='" + TextBox18.Text + "' and Com_Id='" + company_id + "'", con2);
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
  
    
    protected void itemname_TextChanged(object sender, EventArgs e)
    {
        

         
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {








        
    }
}