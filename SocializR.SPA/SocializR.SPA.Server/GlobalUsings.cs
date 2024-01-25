﻿global using SocializR.DataAccess;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.IdentityModel.Tokens;
global using SocializR.Models.Entities;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Microsoft.Extensions.Options;
global using SocializR.SPA.Server.Configuration;
global using Microsoft.AspNetCore.Identity;
global using SocializR.SPA.Server.Services;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using SocializR.Services.Interfaces;
global using Common.Interfaces;
global using SocializR.DataAccess.UnitOfWork;
global using SocializR.Models.ViewModels.Common;
global using SocializR.Services.Configuration;
global using SocializR.SPA.Server.Services.ImageStorage;
global using SocializR.Services.ValidationService;
global using SocializR.SPA.Server.Configuration.ExtensionMethods;
global using SocializR.Common;