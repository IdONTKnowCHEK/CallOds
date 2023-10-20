using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CallOds.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallOdsController : ControllerBase
    {
        // GET: api/<CallOdsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Process.Start(@"D:\ASP\LibreOffice\LibreOffice\bin\Debug\LibreOffice.exe");
            return new string[] { "ODS Save!" };
        }

        // GET api/<CallOdsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CallOdsController>
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = @"D:\ASP\LibreOffice\LibreOffice\bin\Debug\LibreOffice.exe",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = "\"" + value + "\"",
                }
            };
            process.Start();
            process.WaitForExit();

            // 檢查檔案是否存在
            var filePath = @"C:\file\file.ods";
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("檔案未找到");
            }

            // 讀取檔案並回傳
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new FileContentResult(fileBytes, "application/vnd.oasis.opendocument.spreadsheet")
            {
                FileDownloadName = "file.ods"
            };

            return response;
        }

        // PUT api/<CallOdsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CallOdsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
