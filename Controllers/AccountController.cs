﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using signalr_core_demo.Data;
using signalr_core_demo.DTO;
using signalr_core_demo.Entities;
using signalr_core_demo.Hubs;
using signalr_core_demo.Models;


namespace signalr_core_demo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        //https://stackoverflow.com/questions/24892222/using-claims-types-properly-in-owin-identity-and-asp-net-mvc

        public async Task<ActionResult> LoginAsync(LoginDTO dtoModel)
        {
            UserEntity userEntity = new UserEntity(); 

            using (ChatContext dbContext = new ChatContext())
            {
                userEntity = dbContext.Users
                    .Where(x => x.emailAddress == dtoModel.emailAddress)
                    .SingleOrDefault(); 
            }

            if (userEntity != null)
            {
                Claim[] claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userEntity.id), ClaimValueTypes.Integer32),
                    new Claim(ClaimTypes.Name, userEntity.firstName + " " + userEntity.lastName, ClaimValueTypes.String),
                };
                
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Account");
        }
    }
}