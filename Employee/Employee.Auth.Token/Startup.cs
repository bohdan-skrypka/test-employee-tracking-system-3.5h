using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.RequireHttpsMetadata = false;
                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        // ????????, ????? ?? ?????????????? ???????? ??? ????????? ??????
                                        ValidateIssuer = true,
                                        // ??????, ?????????????? ????????
                                        ValidIssuer = AuthOptions.ISSUER,

                                        // ????? ?? ?????????????? ??????????? ??????
                                        ValidateAudience = true,
                                        // ????????? ??????????? ??????
                                        ValidAudience = AuthOptions.AUDIENCE,
                                        // ????? ?? ?????????????? ????? ?????????????
                                        ValidateLifetime = true,

                                        // ????????? ????? ????????????
                                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                                        // ????????? ????? ????????????
                                        ValidateIssuerSigningKey = true,
                                    };
                                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
