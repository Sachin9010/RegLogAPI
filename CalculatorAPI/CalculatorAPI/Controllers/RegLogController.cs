using CalculatorAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace RegLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegLogController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegLogController(IConfiguration configuration) { 
            _configuration = configuration; 
        }
        [HttpPost]
        [Route("registration")]

        public async Task<ActionResult<Registration>> RegistrationAPI(Registration registration) {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("RegLogCS").ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert INTO Registration(Username, Password, Email, IsActive) Values('"+registration.Username+"','"+registration.Password+"','"+registration.Email+"',"+registration.IsActive+")", con);
            int i = await cmd.ExecuteNonQueryAsync();
            con.Close();
            if (i > 0)
            {
                return Content("User Registered");
            }
            else {
                return Content("Not Registered or Already Exist");
            }
        
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Registration>> LoginAPI(Registration registration) {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("RegLogCS").ToString());
            SqlDataAdapter cmd = new SqlDataAdapter("SELECT * FROM Registration where username='"+registration.Username+"'and password='"+registration.Password+"'and IsActive=1",con);
           
            DataTable dt= new DataTable();
            cmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return Content("User Found");
            }
            else
            {
                return Content("Invailed User");
            }
        }


    }
}
