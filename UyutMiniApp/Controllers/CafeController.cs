using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CafeController(IConfiguration configuration) : ControllerBase
    {
        private readonly string _filePath = "appsettings.json";

        /// <summary>
        /// Open cafe (Admins only)
        /// </summary>
        [HttpPatch("open"), Authorize(Roles = "Admin")]
        public void OpenCafe()
        {
            var json = System.IO.File.ReadAllText(_filePath);
            var jsonObject = JsonNode.Parse(json);

            jsonObject["IsWorking"] = true;
            var options = new JsonSerializerOptions { WriteIndented = true };
            System.IO.File.WriteAllText(_filePath, jsonObject.ToJsonString(options));
        }

        /// <summary>
        /// Close cafe (Admins only)
        /// </summary>
        [HttpPatch("close"), Authorize(Roles = "Admin")]
        public void CloseCafe()
        {
            var json = System.IO.File.ReadAllText(_filePath);
            var jsonObject = JsonNode.Parse(json);

            jsonObject["IsWorking"] = "false";
            var options = new JsonSerializerOptions { WriteIndented = true };
            System.IO.File.WriteAllText(_filePath, jsonObject.ToJsonString(options));
        }

        /// <summary>
        /// Get if cafe is working
        /// </summary>
        /// <returns>true or false</returns>
        [HttpGet, AllowAnonymous]
        public IActionResult Get()
        {
            var isWorking = configuration["IsWorking"];

            return Ok(isWorking);
        } 
    }
}
