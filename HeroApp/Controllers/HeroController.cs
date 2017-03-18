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
        private static readonly HeroContext _db = new HeroContext();
        readonly HeroRepository _heroRep = new HeroRepository(_db);
        readonly PowerRepository _powerRep = new PowerRepository(_db);
        readonly ImageRepository _imgRep = new ImageRepository(_db);

        // GET: Hero
        public ActionResult Index()
        {
            return View(_heroRep.List());
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
                byte[] imageData;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                Image newImage = new Image(imageData);
                _imgRep.Add(newImage);

                newHero.Id = Guid.NewGuid();
                newHero.Image = newImage;
                _heroRep.Add(newHero);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            TempData["IdHero"] = idHero;
            LevelSystem level = new LevelSystem(hero.XP);
            ViewBag.MinXP = level.MinXP;
            ViewBag.MaxXP = level.MaxXP;
            ViewBag.XPinPercent = (int)level.XPinPercent;
            
            return View(hero);
        }

        public ActionResult ListPowers()
        {
            var allowPowers = _powerRep.List();

            Guid idHero = new Guid(TempData["IdHero"].ToString());
            TempData["IdHero"] = idHero;

            var hero = _heroRep.Get(idHero);
            if (hero.Powers != null)
            {
                var addedPowers = hero.Powers.ToList();

                foreach (var addedPower in addedPowers)
                {
                    allowPowers.Remove(addedPower);
                }
            }

            return PartialView(allowPowers);
        }

        public ActionResult AddPowerToHero(Guid idPower)
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var hero = _heroRep.Get(idHero);
            var power = _powerRep.Get(idPower);
            hero.Powers.Add(power);
            _db.SaveChanges();

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult DeletePower(Guid idPower)
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var hero = _heroRep.Get(idHero);
            var power = _powerRep.Get(idPower);
            hero.Powers.Remove(power);
            _db.SaveChanges();

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult AddXP(int xp)
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var hero = _heroRep.Get(idHero);
            hero.XP += xp;
            LevelSystem level = new LevelSystem(hero.XP);
            hero.Level = level.Level;

            _db.SaveChanges();

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult DeleteHero(Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            _heroRep.Remove(hero);

            return RedirectToAction("Index");
        }

        public ActionResult EditHero()
        {
            Guid idHero = new Guid(TempData["IdHero"].ToString());
            var editHero = _heroRep.Get(idHero);
            return View(editHero);
        }

        [HttpPost]
        public ActionResult EditHero(Hero editHero, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                var updateHero = _heroRep.Get(editHero.Id);
                updateHero.Name = editHero.Name;
                updateHero.Description = editHero.Description;

                if (uploadImage != null)
                {
                    byte[] imageData;
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