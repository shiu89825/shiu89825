using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private MyDbContext _MyDbContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductController(MyDbContext MyDbContext, IHostingEnvironment hostingEnvironment)
        {
            _MyDbContext = MyDbContext;
            _hostingEnvironment = hostingEnvironment;

        }


        // GET: ProductController
        public ActionResult Index()
        {
            //string filePath = $"{this._hostingEnvironment.WebRootPath}/Contenttxt/" + "20230329151826_content.txt";
            //string fileContents = System.IO.File.ReadAllText(filePath); // 讀取文件內容

            //ViewBag.FileContents = fileContents; // 將文件內容傳遞給ViewBag
            List<Product> result = _MyDbContext.Products.ToList();
            ViewBag.data = result;

            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var a = _MyDbContext.Products.Find(id);
            string file = a.Contents.ToString();
            string filePath = $"{this._hostingEnvironment.WebRootPath}/Contenttxt/" + file;


            //if (!System.IO.File.Exists(filePath)) // 檢查文件是否存在
            //{
            //    return NotFound(); // 返回404頁面
            //}

            string fileContents = System.IO.File.ReadAllText(filePath); // 讀取文件內容

            ViewBag.FileContents = fileContents; // 將文件內容傳遞給ViewBag
            return View(a);
        }
        public ActionResult myDetails(int id)
        {
            var a = _MyDbContext.Products.Find(id);
            string file = a.Contents.ToString();
            string filePath = $"{this._hostingEnvironment.WebRootPath}/Contenttxt/" + file;


            //if (!System.IO.File.Exists(filePath)) // 檢查文件是否存在
            //{
            //    return NotFound(); // 返回404頁面
            //}

            string fileContents = System.IO.File.ReadAllText(filePath); // 讀取文件內容

            ViewBag.FileContents = fileContents; // 將文件內容傳遞給ViewBag
            return View(a);
        }
        public ActionResult myproduct()
        {

            var a = _MyDbContext.Products.Where(x=>x.PAccount == HttpContext.Session.GetString("account")).ToList();
            



            //if (!System.IO.File.Exists(filePath)) // 檢查文件是否存在
            //{
            //    return NotFound(); // 返回404頁面
            //}


            return View(a);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            string? PName = collection["PName"];
            string? Category = collection["Category"];
            string? Img = collection["Img"];
            int? Price = int.Parse(collection["Price"]);
            int? Stock = int.Parse(collection["Stock"]);
            string? Contents = collection["Contents"];


            string filePath = Path.Combine($"{this._hostingEnvironment.WebRootPath}/contenttxt", DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + "content.txt");

            System.IO.File.WriteAllText(filePath, Contents);
            _MyDbContext.Products.Add(new Product
                {
                    PAccount=HttpContext.Session.GetString("account"),
                    PName=PName,
                    Category=Category,
                    Img= DateTime.Now.ToString("yyyyMMddHHmmss") + "_"+Img,
                    Price=Price,
                    Stock=Stock,
                    Contents= DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + "content.txt",
                });
                _MyDbContext.SaveChanges();

                return RedirectToAction("Index", "Home");

        }
        
        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var a = _MyDbContext.Products.Find(id);
            string file = a.Contents.ToString();
            string filePath = $"{this._hostingEnvironment.WebRootPath}/Contenttxt/" + file;
            ViewBag.aaa = a.Contents;

            //if (!System.IO.File.Exists(filePath)) // 檢查文件是否存在
            //{
            //    return NotFound(); // 返回404頁面
            //}

            string fileContents = System.IO.File.ReadAllText(filePath); // 讀取文件內容

            ViewBag.FileContents = fileContents; // 將文件內容傳遞給ViewBag
            ViewBag.imgname = a.Img.ToString();
            return View(a);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var product = _MyDbContext.Products.Find(id);

            string? PName = collection["PName"];
            string? Category = collection["Category"];
            string? Img = collection["Img"];
            int? Price = int.Parse(collection["Price"]);
            int? Stock = int.Parse(collection["Stock"]);
            string? Contents = collection["Contents"];


            if (product.Img == Img)
            {
                Img = collection["Img"];
            }
            else
            {
                var filepath = Path.Combine($"{this._hostingEnvironment.WebRootPath}/fileUploads", product.Img);

                Img = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Img;
                System.IO.File.Delete(filepath);//刪除舊檔案

            }
            var Contentstxt = Path.Combine($"{this._hostingEnvironment.WebRootPath}/contenttxt", product.Contents);
            System.IO.File.Delete(Contentstxt);//刪除舊檔案


            string filePath = Path.Combine($"{this._hostingEnvironment.WebRootPath}/contenttxt", DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + "content.txt");

            System.IO.File.WriteAllText(filePath, Contents);
            if (product != null)
            {
                product.PName = PName;
                product.Category = Category;
                product.Img = Img;
                product.Price = Price;
                product.Stock = Stock;
                product.Contents = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + "content.txt";
                _MyDbContext.SaveChanges();

            }


            return RedirectToAction("Index", "Home");
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
