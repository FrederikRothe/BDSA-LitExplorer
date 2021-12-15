global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.Data.Sqlite;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using System.Net;
global using System.Net.Http.Json;
global using System.Net.Http.Headers;
global using System.Security.Claims;
global using System.Text.Encodings.Web;
global using Xunit;


global using LitExplore.Storage;
global using LitExplore.ApplicationLogic;
global using static LitExplore.Storage.Roles;
global using static LitExplore.Storage.Status;