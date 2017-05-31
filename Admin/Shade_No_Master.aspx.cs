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


public partial class Admin_email_report : System.Web.UI.Page
{
    public static int company_id = 0;
    float m = 0;
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
            show_shade_no();
           
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
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from item_master where Com_Id='" + company_id + "' ORDER BY item_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
        }

    }
    private void show_shade_no()
    {

        //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        //SqlCommand cmd = new SqlCommand("Select * from shade_no where Com_Id='" + company_id + "' ORDER BY shade_id asc", con);
        //con.Open();
        //DataSet ds = new DataSet();
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(ds);

        //DropDownList1.DataSource = ds;
        //DropDownList1.DataTextField = "shade";
        //DropDownList1.DataValueField = "shade_id";
        //DropDownList1.DataBind();
        //DropDownList1.Items.Insert(0, new ListItem("All", "0"));
        //con.Close();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton img = (ImageButton)sender;
        GridViewRow row = (GridViewRow)img.NamingContainer;

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
                SqlCommand cmd2 = new SqlCommand("select * from shade_master where shade_id='" +row.Cells[0].Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Label1.Text = dr2["shade_id"].ToString();
                    TextBox2.Text= dr2["shade_no"].ToString();
                    TextBox1.Text = dr2["shade_color"].ToString();

                }
                con2.Close();
                BindData();

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
                SqlCommand CMD = new SqlCommand("select * from shade_master where Com_Id='" + company_id + "'", con);
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

                 SqlConnection con10 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd10 = new SqlCommand("select * from shade_master where shade_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con10);
                    con10.Open();
                    SqlDataReader dr10;
                    dr10 = cmd10.ExecuteReader();
                    if (dr10.HasRows)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd = new SqlCommand("delete from shade_master where shade_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd1 = new SqlCommand("delete from shade_master_details where shade_id='" + Label1.Text + "' and Com_Id='" + company_id + "' ", con1);
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Shade No not exist on database please add data')", true);
                       
                    }
                    con10.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Shade No deleted successfully')", true);
                BindData();


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
        getinvoiceno();
        TextBox1.Text = "";
        BindData2();
        BindData();
        TextBox2.Text = "";

        show_shade_no();
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
                    SqlCommand cmd = new SqlCommand("delete from Shade_no where shade_id='" + row.Cells[1].Text + "' and Com_Id='" + company_id + "' ", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Shade No deleted successfully')", true);

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
                string query = "Select Max(shade_id) from shade_master where Com_Id='" + company_id + "' ";
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
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {


      
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        getinvoiceno();
        TextBox1.Text = "";
        BindData2();
        BindData();
        TextBox2.Text = "";
     
        show_shade_no();

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
                SqlCommand cmd2 = new SqlCommand("select * from shade_master where shade_id='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                  TextBox2.Text = dr2["shade_no"].ToString();
                    TextBox1.Text = dr2["shade_color"].ToString();
                   
                }
                con2.Close();
                BindData();
               
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
                if (TextBox2.Text=="")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Please enter shade no')", true);
                }
                else
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                    SqlCommand cmd1 = new SqlCommand("select * from shade_master where shade_id='" + Label1.Text + "' AND Com_Id='" + company_id + "'  ", con1);
                    con1.Open();
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {

                        SqlConnection con100 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);

                        con100.Open();
                        SqlCommand cmd100 = new SqlCommand("delete from shade_master_details where shade_id='" + Label1.Text + "' and Com_Id='" + company_id + "'", con100);
                        cmd100.ExecuteNonQuery();
                        con100.Close();

                        SqlConnection con32 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd32 = new SqlCommand("update shade_master set shade_no=@shade_no,shade_color=@shade_color where shade_id='" + Label1.Text + "'", con32);
                        cmd32.Parameters.AddWithValue("@shade_no", TextBox2.Text);
                        cmd32.Parameters.AddWithValue("@shade_color", TextBox1.Text);
                        con32.Open();
                        cmd32.ExecuteNonQuery();
                        con32.Close();


                        foreach (GridViewRow gvrow in GridView1.Rows)
                        {

                            CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox1");

                            if (chkdelete.Checked)
                            {


                               

                                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                                SqlCommand cmd = new SqlCommand("insert into shade_master_details values(@shade_id,@shade_no,@color,@item_name,@item_code,@Com_Id)", CON);
                                cmd.Parameters.AddWithValue("@shade_id", Label1.Text);
                                cmd.Parameters.AddWithValue("@shade_no", TextBox2.Text);
                                cmd.Parameters.AddWithValue("@color", TextBox1.Text);
                                cmd.Parameters.AddWithValue("@item_name", gvrow.Cells[1].Text);
                                cmd.Parameters.AddWithValue("@item_code", gvrow.Cells[2].Text);


                                cmd.Parameters.AddWithValue("@Com_Id", company_id);

                                CON.Open();
                                cmd.ExecuteNonQuery();
                                CON.Close();
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Shade No updated successfully')", true);

                            }
                        }
                        BindData();
                        show_category();
                        getinvoiceno();
                        TextBox1.Text = "";

                        BindData2();
                        show_shade_no();
                        BindData();
                        TextBox2.Text = "";

                    }
                    else
                    {


                         SqlConnection con11 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                         SqlCommand cmd111 = new SqlCommand("select * from shade_master where shade_no='" + TextBox2.Text + "' AND Com_Id='" + company_id + "'  ", con11);
                    con11.Open();
                    SqlDataReader dr11;
                    dr11 = cmd111.ExecuteReader();
                    if (dr11.HasRows)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Shade No already exist')", true);
                    }
                    else

                    {


                        SqlConnection CON1 = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                        SqlCommand cmd11 = new SqlCommand("insert into shade_master values(@shade_id,@shade_no,@shade_color,@Com_Id)", CON1);
                        cmd11.Parameters.AddWithValue("@shade_id", Label1.Text);
                        cmd11.Parameters.AddWithValue("@shade_no", TextBox2.Text);
                        cmd11.Parameters.AddWithValue("@shade_color", TextBox1.Text);



                        cmd11.Parameters.AddWithValue("@Com_Id", company_id);

                        CON1.Open();
                        cmd11.ExecuteNonQuery();
                        CON1.Close();


                        foreach (GridViewRow gvrow in GridView1.Rows)
                        {

                            CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBox1");

                            if (chkdelete.Checked)
                            {



                                SqlConnection CON = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                                SqlCommand cmd = new SqlCommand("insert into shade_master_details values(@shade_id,@shade_no,@color,@item_name,@item_code,@Com_Id)", CON);
                                cmd.Parameters.AddWithValue("@shade_id", Label1.Text);
                                cmd.Parameters.AddWithValue("@shade_no", TextBox2.Text);
                                cmd.Parameters.AddWithValue("@color", TextBox1.Text);
                                cmd.Parameters.AddWithValue("@item_name", gvrow.Cells[1].Text);
                                cmd.Parameters.AddWithValue("@item_code", gvrow.Cells[2].Text);


                                cmd.Parameters.AddWithValue("@Com_Id", company_id);

                                CON.Open();
                                cmd.ExecuteNonQuery();
                                CON.Close();
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert Message", "alert('Shade No created successfully')", true);

                            }
                        }

                    }

                    con11.Close();






            BindData();
            show_category();
            getinvoiceno();


            BindData2();
            show_shade_no();
            BindData();
            TextBox2.Text = "";
            TextBox1.Text = "";
    }
   con1.Close();

     }
  }
con1000.Close();

        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {


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
                SqlCommand cmd21 = new SqlCommand("select max(shade_id) from shade_master where  Com_Id='" + company_id + "' ", con21);
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
                SqlCommand cmd2 = new SqlCommand("select * from shade_master where shade_id='" + Label1.Text + "' and Com_Id='" + company_id + "'", con2);
                SqlDataReader dr2;
                con2.Open();
                dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                  TextBox2.Text = dr2["shade_no"].ToString();
                    TextBox1.Text = dr2["shade_color"].ToString();
                   
                }
              
                else
                {

                    getinvoiceno();

                    BindData();
                    TextBox2.Text = "";
                    TextBox1.Text = "";
                }
                con2.Close();
              
               
            }
            con1000.Close();
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
        show_shade_no();
        BindData2();
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
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                SqlCommand CMD = new SqlCommand("select * from shade_master where shade_no='" + DropDownList1.SelectedItem.Text + "' and Com_Id='" + company_id + "' ORDER BY shade_id asc", con);
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(CMD);
                da1.Fill(dt1);
                GridView3.DataSource = dt1;
                GridView3.DataBind();
            }
        }
        this.ModalPopupExtender1.Show();
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        getcheckboxselect(e);
    }

    private void getcheckboxselect(GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int x = System.Convert.ToInt32(e.Row.RowIndex);
            CheckBox chk = (CheckBox)e.Row.FindControl("CheckBox1");

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
            SqlCommand CMD = new SqlCommand("select * from shade_master_details where item_name='" + e.Row.Cells[1].Text + "' and shade_id='" + Label1.Text + "'  and Com_Id='" + company_id + "' ORDER BY shade_id asc", con);
            SqlDataReader dr;
            con.Open();
            dr = CMD.ExecuteReader();
            if (dr.HasRows)
            {
                chk.Checked = true;

            }
            else
            {
                chk.Checked = false;
            }
            con.Close();

        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
        SqlCommand cmd=new SqlCommand("select * from item_master where item_name='"+TextBox2.Text+"'",con);
        con.Open();
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox1.Text = dr["item_code"].ToString();
        }
        con.Close();
    }
}