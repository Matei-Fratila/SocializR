global using System;
global using System.IO;
global using System.Linq;
global using System.Diagnostics;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using System.Text.Encodings.Web;
global using System.Collections.Generic;

global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.Extensions.Configuration;

global using Microsoft.EntityFrameworkCore;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.AspNetCore.Razor.TagHelpers;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Authorization;

global using X.PagedList;
global using SocializR.Services;
global using SocializR.Web.Code.ExtensionMethods;
global using AutoMapper;

global using SocializR.Web.Code.Base;
global using SocializR.Web.Code.Configuration;

global using Common.Interfaces;

global using SocializR.Models.ViewModels.Account;
global using SocializR.Models.ViewModels.Album;
global using SocializR.Models.ViewModels.Map;
global using SocializR.Models.ViewModels.Friend;
global using SocializR.Models.ViewModels.Feed;
global using SocializR.Models.ViewModels.Media;
global using SocializR.Models.ViewModels.Common;
global using SocializR.Models.ViewModels.Interest;
global using SocializR.Models.ViewModels.Profile;
global using SocializR.Models.ViewModels.Search;
global using SocializR.Models.Enums;
global using SocializR.Models.Entities;

global using SocializR.Services.UserServices;
global using SocializR.Services.MediaServices;
global using SocializR.Services.ValidationService;

global using SocializR.DataAccess;
global using SocializR.DataAccess.Seeds;
global using SocializR.DataAccess.UnitOfWork;

global using SocializR.Services.Interfaces;
global using Utils;
