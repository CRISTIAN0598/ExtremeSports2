﻿using ExtremeSports2.Data;
using ExtremeSports2.Data.Entities;
using ExtremeSports2.Enums;
using ExtremeSports2.Helpers;
using ExtremeSports2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;

namespace ExtremeSports2.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;
        /*private readonly IMailHelper _mailHelper*/

        public UsersController(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper, ICombosHelper combosHelper/*, IMailHelper mailHelper*/,IFlashMessage flashMessage)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _combosHelper = combosHelper;
            //_mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Users.Include(u => u.City).ThenInclude(c => c.State).ThenInclude(s => s.Country).ToListAsync());

        }

        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                UserType = UserType.Admin,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                model.ImageId = imageId;
                User user = await _userHelper.AddUserAsync(model);
                if (user == null)
                {
                    _flashMessage.Danger("Este correo ya está siendo usado.");
                    model.Countries = await _combosHelper.GetComboCountriesAsync();
                    model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
                    model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
                    return View(model);
                }
                //string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //string tokenLink = Url.Action("ConfirmEmail", "Account", new
                //{
                //    userid = user.Id,
                //    token = myToken
                //}, protocol: HttpContext.Request.Scheme);

                //Response response = _mailHelper.SendMail(
                //    $"{model.FirstName} {model.LastName}",
                //    model.Username,
                //    "Shopping - Confirmación de Email",
                //    $"<h1>Shopping - Confirmación de Email</h1>" +
                //        $"Para habilitar el usuario por favor hacer click en el siguiente link:, " +
                //        $"<hr/><br/><p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                //if (response.IsSuccess)
                //{
                //    _flashMessage.Info("Usuario registrado. Para poder ingresar al sistema, siga las instrucciones que han sido enviadas a su correo.");
                //    return RedirectToAction(nameof(Login));

                //}

                //ModelState.AddModelError(string.Empty, response.Message);


            }

            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
            return View(model);
        }

        public JsonResult GetStates(int countryId)
        {
            if (countryId == 0)
            {
                return null;
            }
            Country country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return null;
            }

            return Json(country.States.OrderBy(d => d.Name));
        }

        public JsonResult GetCities(int stateId)
        {
            State state = _context.States
                .Include(s => s.Cities)
                .FirstOrDefault(s => s.Id == stateId);
            if (state == null)
            {
                return null;
            }

            return Json(state.Cities.OrderBy(c => c.Name));
        }
    }
}