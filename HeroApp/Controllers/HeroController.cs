using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeroApp.Models;
using CodeFirst;
using System.IO;

namespace HeroApp.Controllers
{
    [Authorize]
    public class HeroController : Controller
    {
        private static HeroContext _db = new HeroContext();
        HeroRepository HeroRep = new HeroRepository(_db);
        PowerRepository PowerRep = new PowerRepository(_db);
        ImageRepository ImgRep = new ImageRepository(_db);

        // GET: Hero
        public ActionResult Index()
        {
            return View(HeroRep.List());
        }

        public ActionResult AddHero()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHero(Hero newHero, HttpPostedFileBase uploadImage)
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

                newHero.Id = Guid.NewGuid();
                newHero.Image = newImage;
                HeroRep.Add(newHero);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(Guid idHero)
        {
            var hero = HeroRep.Get(idHero);
            TempData["IdHero"] = idHero;
            return View(hero);
        }

        public ActionResult ListPowers()
        {
            var allowPowers = PowerRep.List();

            Guid idHero = new Guid(TempData["IdHero"].ToString());
            TempData["IdHero"] = idHero;

            var hero = _db.Heroes.FirstOrDefault(h => h.Id == idHero);
            var addedPowers = hero.Powers.ToList();

            foreach (var addedPower in addedPowers)
            {
                allowPowers.Remove(addedPower);
            }

            return PartialView(allowPowers);
        }

        public ActionResult AddPowerToHero(Guid idPower)
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var Hero = HeroRep.Get(idHero);
            var Power = PowerRep.Get(idPower);
            Hero.Powers.Add(Power);
            _db.SaveChanges();

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult DeletePower(Guid idPower)
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var hero = HeroRep.Get(idHero);
            var power = PowerRep.Get(idPower);
            hero.Powers.Remove(power);
            _db.SaveChanges();

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult DeleteHero(Guid idHero)
        {
            var hero = HeroRep.Get(idHero);
            HeroRep.Remove(hero);

            return RedirectToAction("Index");
        }

        public ActionResult EditHero()
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var editHero = HeroRep.Get(idHero);
            return View(editHero);
        }

        [HttpPost]
        public ActionResult EditHero(Hero editHero, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var updateHero = HeroRep.Get(editHero.Id);
                updateHero.Name = editHero.Name;
                updateHero.Description = editHero.Description;

                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    updateHero.Image.Img = imageData;
                }
                _db.SaveChanges();

                return RedirectToAction("Details", new { idHero = updateHero.Id });
            }
            return View();
        }
    }
}