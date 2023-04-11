using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApplication1.Controllers
{
    public class AjaxfileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AjaxfileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UploadFilesName(IFormFile files)
        {
            var filesPath = $"{this._hostingEnvironment.WebRootPath}/fileUploads";
            string fileList = "";

                string ImageName = Path.GetFileName(files.FileName).Replace(" ", "");//get filename
            var fileNameWithTimeStamp =  ImageName;
            var fullFilePath = Path.Combine(filesPath, fileNameWithTimeStamp);

            fileList = fileNameWithTimeStamp;

                try
                {
                    //using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    //{
                    //    await file.CopyToAsync(stream);
                    //    if (fileList == "")
                    //    {
                    //        fileList = fileNameWithTimeStamp;
                    //    }
                    //    else
                    //    {
                    //        fileList += "," + fileNameWithTimeStamp;
                    //    }

                    //}
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            


            //fileList = fileList +  files[i].FileName + "\n";

            //return Ok("ddddd");
            return Json(fileList);

        }
        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile files)
        {
            var filesPath = $"{this._hostingEnvironment.WebRootPath}/fileUploads";
            string fileList = "";

                string ImageName = Path.GetFileName(files.FileName).Replace(" ", "");//get filename
                var fileNameWithTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ImageName;
                var fullFilePath = Path.Combine(filesPath, fileNameWithTimeStamp);

                try
                {
                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                        if (fileList == "")
                        {
                            fileList = fileNameWithTimeStamp;
                        }
                        else
                        {
                            fileList += "," + fileNameWithTimeStamp;
                        }

                    }
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }




                //fileList = fileList +  files[i].FileName + "\n";
            
            //return Ok("ddddd");
            return Json(fileList);
        }
    }
}
