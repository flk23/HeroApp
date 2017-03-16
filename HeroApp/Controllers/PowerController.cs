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
        private static HeroContext _db = new HeroContext();
        HeroRepository HeroRep = new HeroRepository(_db);
        PowerRepository PowerRep = new PowerRepository(_db);
        ImageRepository ImgRep = new ImageRepository(_db);

        // GET: Power
        public ActionResult Index()
        {
            return View(PowerRep.List());
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
                ImgRep.Add(newImage);

                newPower.Id = Guid.NewGuid();
                newPower.Image = newImage;
                PowerRep.Add(newPower);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(Guid idPower)
        {
            var power = PowerRep.Get(idPower);
            PowerRep.Remove(power);
            return RedirectToAction("Index");
        }
    }
}