using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CodeFirst;

namespace HeroApp.Controllers
{
    [Authorize]
    public class PowerController : Controller
    {
        private static readonly HeroContext _db = new HeroContext();
        readonly PowerRepository _powerRep = new PowerRepository(_db);
        readonly ImageRepository _imgRep = new ImageRepository(_db);

        // GET: Power
        public ActionResult Index()
        {
            return View(_powerRep.List());
        }

        public ActionResult AddPower()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPower(Power newPower, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                Image newImage = new Image(imageData);
                _imgRep.Add(newImage);

                newPower.Id = Guid.NewGuid();
                newPower.Image = newImage;
                _powerRep.Add(newPower);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(Guid idPower)
        {
            var power = _powerRep.Get(idPower);
            _powerRep.Remove(power);
            return RedirectToAction("Index");
        }
    }
}