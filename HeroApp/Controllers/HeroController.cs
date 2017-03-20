using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeroApp.Models;
using System.IO;
using AutoMapper;
using CodeFirst.Models;
using CodeFirst.Repositories;
using HeroApp.Models.ViewModels;

namespace HeroApp.Controllers
{
    [Authorize]
    public class HeroController : Controller
    {
        private readonly IGenericRepository<Power> _powerRep;
        private readonly IGenericRepository<Hero> _heroRep;
        private readonly IGenericRepository<Image> _imgRep;

        public HeroController(IGenericRepository<Power> powerRep, IGenericRepository<Hero> heroRep, IGenericRepository<Image> imgRep)
        {
            _powerRep = powerRep;
            _heroRep = heroRep;
            _imgRep = imgRep;
        }

        // GET: Hero
        public ActionResult Index()
        {
            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Hero, HeroViewModel>());
            // сопоставление
            var heroes = Mapper.Map<List<Hero>, List<HeroViewModel>>(_heroRep.List());

            return View(heroes);
        }

        public ActionResult AddHero()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHero(HeroViewModel newHeroViewModel, HttpPostedFileBase uploadImage)
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

                newHeroViewModel.Id = Guid.NewGuid();
                newHeroViewModel.Image = newImage;

                // Настройка AutoMapper
                Mapper.Initialize(cfg => cfg.CreateMap<HeroViewModel, Hero>());
                // сопоставление
                Hero newHero = Mapper.Map<HeroViewModel, Hero>(newHeroViewModel);

                _heroRep.Add(newHero);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Hero, HeroViewModel>());
            // сопоставление
            HeroViewModel heroViewModel = Mapper.Map<Hero, HeroViewModel>(hero);

            LevelSystem level = new LevelSystem(heroViewModel.XP);
            heroViewModel.MinXP = (int)level.MinXP;
            heroViewModel.MaxXP = (int)level.MaxXP;
            heroViewModel.XPinPercent = (int)level.XPinPercent;
            
            return View(heroViewModel);
        }

        public ActionResult ListPowers(Guid idHero)
        {
            var allowPowers = _powerRep.List();
            var hero = _heroRep.Get(idHero);
            if (hero.Powers != null)
            {
                var addedPowers = hero.Powers.ToList();

                foreach (var addedPower in addedPowers)
                {
                    //allowPowers.Remove(addedPower);
                    allowPowers.RemoveAll(p => p.Id == addedPower.Id);
                }
            }

            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Power, PowerViewModel>());
            // сопоставление
            var allowPowersViewModel = Mapper.Map<List<Power>, List<PowerViewModel>>(allowPowers);
            if (allowPowersViewModel.Count != 0)
            {
                allowPowersViewModel.FirstOrDefault().IdHero = idHero;
            }

            return PartialView(allowPowersViewModel);
        }

        public ActionResult AddPowerToHero(Guid idPower, Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            var power = _powerRep.Get(idPower);
            if (hero.Powers == null)
            {
                hero.Powers = new List<Power>();
            }
            hero.Powers.Add(power);
            _heroRep.Update(hero);

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult DeletePower(Guid idPower, Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            var power = _powerRep.Get(idPower);
            hero.Powers.Remove(power);
            _heroRep.Update(hero);

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult AddXP(int xp, Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            hero.XP += xp;
            LevelSystem level = new LevelSystem(hero.XP);
            hero.Level = level.Level;
            _heroRep.Update(hero);

            return RedirectToAction("Details", new { idHero });
        }

        public ActionResult DeleteHero(Guid idHero)
        {
            var hero = _heroRep.Get(idHero);
            _heroRep.Remove(hero);

            return RedirectToAction("Index");
        }

        public ActionResult EditHero(Guid idHero)
        {
            var editHero = _heroRep.Get(idHero);
            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Hero, HeroViewModel>());
            // сопоставление
            HeroViewModel heroViewModel = Mapper.Map<Hero, HeroViewModel>(editHero);

            return View(heroViewModel);
        }

        [HttpPost]
        public ActionResult EditHero(HeroViewModel editHero, HttpPostedFileBase uploadImage)
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
                _heroRep.Update(updateHero);

                return RedirectToAction("Details", new { idHero = updateHero.Id });
            }
            return View();
        }
    }
}