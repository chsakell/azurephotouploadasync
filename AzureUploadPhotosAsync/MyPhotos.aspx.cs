using AzureUploadPhotosAsync.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureUploadPhotosAsync
{
    public partial class MyPhotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string username = "chsakell"; // USE Context.User.Identity.Name for authenticated users..
                int albumID = 10;// Int32.Parse(Request.QueryString["AblumID"].ToString());
                using (SqlConnection conn = new SqlConnection(Utilities.Params.GetConnectionString()))
                {
                    string query = @"SELECT * FROM AzurePhotos WHERE PhotoAlbum = @albumID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@albumID", SqlDbType.Int).Value = albumID;
                        using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable("Photos");
                            List<AzurePhoto> albumPhotos = new List<AzurePhoto>();
                            da.Fill(dt);
                            foreach(DataRow row in dt.Rows)
                            {
                                AzurePhoto photo = new AzurePhoto(row["PhotoGuid"].ToString(), row["PhotoUploadFormat"].ToString(), row["PhotoTitle"].ToString(),username, Int32.Parse(row["PhotoAlbum"].ToString()));
                                albumPhotos.Add(photo);
                            }
                            dlImages.DataSource = albumPhotos;
                            dlImages.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}