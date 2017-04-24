using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace webclient
{
    public partial class _Default : Page
    {
        #region PageLoad Events and Load Grid

        string remoteUri = "http://localhost:30251/EvolventHea.svc/Evolvents/";

       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            GetRESTData(remoteUri);
            string strCount = this.gdvDetails.Rows.Count.ToString();
            int count = Convert.ToInt32(strCount);
            if (count<=0)
            {
                lblError.Text = "There is no data present to view in the grid";
                lblError.Text = "";

            }
          
        }
        #endregion

        #region Function to retrieve data to gridview   GET

        private void GetRESTData(string uri)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    List<EHealth> DeserializeObject = (List<EHealth>)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(List<EHealth>));
                    gdvDetails.DataSource = DeserializeObject;
                    gdvDetails.DataBind();
                }
                else
                {
                    lblError.Text = "Retrieving Data failed";
                    lblError.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        #endregion

        #region insert data  POST

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            string FName = txtFirstName.Text;
            string LName = txtLastName.Text;
            string Phone = txtPhone.Text;
            string Email = txtEamil.Text;
            string status = ddlStatus.SelectedItem.Text;
           


            EHealth p = new EHealth {FirstName = FName, LastName = LName, EmailID = Email, PhoneNo = Phone, Status = status };


            //string sURL = "http://localhost:30251/EvolventHea.svc/EvolventAdd/";

            try
            {
                string WebServiceURL = "http://localhost:30251/EvolventHea.svc/EvolventAdd/"; // store Url Of service in string

                // Convert our JSON in into bytes using ascii encoding
                //ASCIIEncoding encoding = new ASCIIEncoding();
                string s = JsonConvert.SerializeObject(p);
                //byte[] data = encoding.GetBytes(s);

                //  HttpWebRequest 
                var httpRequest = (HttpWebRequest)WebRequest.Create(WebServiceURL);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(s);
                }
               

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                GetRESTData(remoteUri);
                lblError.Text = "New Record inserted";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblError.Text = "An exception was thrown: " + ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }

        }
    
        #endregion

        #region Update Data  PUT

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            int id = 0;
            string FName = txtFirstName.Text;
            string LName = txtLastName.Text;
            string Phone = txtPhone.Text;
            string Email = txtEamil.Text;
            string status = ddlStatus.SelectedItem.Text;

            if (txtId.Text==string.Empty)
            {
                id=0;
                lblError.Text = "Plase select clear button and select again the record to be updated from grid ";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                id = Convert.ToInt32(txtId.Text);
            }
           
           


            EHealth p = new EHealth { Id = id, FirstName = FName, LastName = LName, EmailID = Email, PhoneNo = Phone, Status = status };
            // string URI = "http://localhost:30251/EvolventHea.svc/EvolventAdd/{id}";
                   
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("c");
            

           

            try
            {
                string WebServiceURL = "http://localhost:30251/EvolventHea.svc/EvolventUpdate/"; // store Url Of service in string

                // Convert our JSON in into bytes using ascii encoding
                //ASCIIEncoding encoding = new ASCIIEncoding();
                string s = JsonConvert.SerializeObject(p);
                //byte[] data = encoding.GetBytes(s);

                //  HttpWebRequest 
                var httpRequest = (HttpWebRequest)WebRequest.Create(WebServiceURL);
                httpRequest.Method = "PUT";
                httpRequest.ContentType = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(s);
                }
               

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
                GetRESTData(remoteUri);
                lblError.Text = "Record Updated Successfully";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                lblError.Text = "An exception was thrown: " + ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }         
                Session.Remove("id");
        }
       
          #endregion

        #region Delete Data  DELETE

        protected void DeleteButton_Click(object sender, EventArgs e)

        {
            try {
                string id = Session["id"].ToString();

                string sURL = "http://localhost:30251/EvolventHea.svc/EvolventDel/";
                string UrlTem = sURL + id;
                WebRequest request = WebRequest.Create(UrlTem);
                request.Method = "DELETE";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                GetRESTData(remoteUri);
                lblError.Text = "Record Deleted successfully";
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            catch(Exception ex) {
                lblError.Text = ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
            

        }
        #endregion

        #region  Clear TextBox Fields Data      
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ResetAll();
        }
         
        private void ResetAll()
        {
            txtId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEamil.Text = "";
            txtPhone.Text = "";
            lblError.Text = "";
           
        }
        #endregion

        #region Selection Change Event"
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Accessing BoundField Column
           int  id =Convert.ToInt32(gdvDetails.SelectedRow.Cells[1].Text);
       
           EHealth p = new EHealth();
           GridViewRow row = gdvDetails.SelectedRow;
           p.Id = id;
           Session["id"] = id;
            //Accessing TemplateField Column controls
           txtId.Text = (gdvDetails.SelectedRow.Cells[1].Text);
           txtFirstName.Text = (gdvDetails.SelectedRow.FindControl("lblFirstName") as Label).Text;
           txtLastName.Text = (gdvDetails.SelectedRow.FindControl("lblLastName") as Label).Text;
           txtEamil.Text = (gdvDetails.SelectedRow.FindControl("lblEmail") as Label).Text;
           txtPhone.Text = (gdvDetails.SelectedRow.FindControl("lblPhone") as Label).Text;
          // ddlStatus.SelectedItem.Text = (gdvDetails.SelectedRow.FindControl("lblStatus") as Label).Text; 

           
        }

        #endregion

        

    }
    [Serializable]
    public class EHealth
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
    }

   

    }
