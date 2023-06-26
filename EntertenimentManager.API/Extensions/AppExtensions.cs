using EntertenimentManager.API.Services;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Commands.Item.Movie;
using EntertenimentManager.Domain.Commands.PersonalList;
using EntertenimentManager.Domain.Entities.Categories;
using EntertenimentManager.Domain.Entities.Categories.Contracts;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Domain.Repositories.Contracts;
using EntertenimentManager.Infra.Contexts;
using EntertenimentManager.Infra.Repositories;
using EntertenimentManager.Infra.Storages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace EntertenimentManager.API.Extensions
{
    public static class AppExtensions
    {
        public static void LoadConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey");
            Configuration.ApiKeyName = builder.Configuration.GetValue<string>("ApiKeyName");
            Configuration.ApiKey = builder.Configuration.GetValue<string>("ApiKey");

            var smtp = new Configuration.SmtpConfiguration();
            builder.Configuration.GetSection("SmtpConfiguration").Bind(smtp);
            Configuration.Smtp = smtp;
            Configuration.StorageConnectionString = builder.Configuration.GetValue<string>("StorageConnectionString");
            Configuration.ImageContainer = "user-images";

        }

        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }

        public static void ConfigureMvc(this WebApplicationBuilder builder)
        {
            builder.Services
           .AddControllers()
           .ConfigureApiBehaviorOptions(options =>
           {
               options.SuppressModelStateInvalidFilter = true;
           })
           .AddJsonOptions(x =>
           {
               x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
               x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
           });
        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EntertenimentManagementDataContext>(options =>
                options.UseSqlServer(connectionString, x =>
                x.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddTransient<IImageStorage>(provider => new AzureImageStorage(
                Configuration.StorageConnectionString, 
                Configuration.ImageContainer));
            builder.Services.AddTransient<ICategoryFactory, CategoryFactory>();
            #region Repositories
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IPersonalListRepository, PersonalListRepository>();
            builder.Services.AddTransient<IMovieRepository, MovieRepository>();
            #endregion
            #region Handlers
            builder.Services.AddTransient<AccountHandler, AccountHandler>();
            builder.Services.AddTransient<CategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<PersonalListHandler, PersonalListHandler>();
            builder.Services.AddTransient<MovieHandler, MovieHandler>();
            #endregion
            #region Commands
            builder.Services.AddTransient<DeleteAccountCommand, DeleteAccountCommand>();
            builder.Services.AddTransient<GetAllCategoriesCommand, GetAllCategoriesCommand>();
            builder.Services.AddTransient<GetCategoryByIdCommand, GetCategoryByIdCommand>();
            builder.Services.AddTransient<GetAllPersonalListsByCategoryIdCommand, GetAllPersonalListsByCategoryIdCommand>();
            builder.Services.AddTransient<GetPersonalListByIdCommand, GetPersonalListByIdCommand>();
            builder.Services.AddTransient<GetMovieByIdCommand, GetMovieByIdCommand>();
            builder.Services.AddTransient<DeleteMovieCommand, DeleteMovieCommand>();
            builder.Services.AddTransient<GetAllByPersonalListId, GetAllByPersonalListId>();
            #endregion
        }
    }
}
