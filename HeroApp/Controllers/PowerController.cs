using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AutoMapper;
using CodeFirst.Models;
using CodeFirst.Repositories;
using HeroApp.Models.ViewModels;

namespace HeroApp.Controllers
{
    [Authorize]
    public class PowerController : Controller
    {
        private readonly IGenericRepository<Power> _powerRep;
        private readonly IGenericRepository<Image> _imgRep;

        public PowerController(IGenericRepository<Power> powerRep, IGenericRepository<Image> imgRep)
        {
            _powerRep = powerRep;
            _imgRep = imgRep;
        }

        // GET: Power
        public ActionResult Index()
        {
            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Power, PowerViewModel>());
            // сопоставление
            var powers = Mapper.Map<List<Power>, List<PowerViewModel>>(_powerRep.List());

            return View(powers);
        }

        public ActionResult AddPower()
        {
            return View();
        }

        //тестируем автомапер
        public ActionResult Details()
        {
            Power p = _powerRep.List().FirstOrDefault();
            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Power, PowerViewModel>());
            // сопоставление
            PowerViewModel power = Mapper.Map<Power, PowerViewModel>(p);

            return View(power);
        }

        [HttpPost]
        public ActionResult AddPower(PowerViewModel newPowerViewModel, HttpPostedFileBase uploadImage)
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

                newPowerViewModel.Id = Guid.NewGuid();
                newPowerViewModel.Image = newImage;

                // Настройка AutoMapper
                Mapper.Initialize(cfg => cfg.CreateMap<PowerViewModel, Power>());
                // сопоставление
                Power newPower = Mapper.Map<PowerViewModel, Power>(newPowerViewModel);

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