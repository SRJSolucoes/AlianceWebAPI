﻿using AutoMapper;
using Cross.Cutting.DependencyInjection;
using Cross.Cutting.Mapping;
using Domain.Config;
using Domain.Handlers;
using Domain.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AcessoWebApi
{
    public class Startup
    {
        public IWebHostEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.HostingEnvironment = env;
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Injeção de Dependência
            var settingsSection = Configuration.GetSection("AlianceApiSettings");
            ConfigurarSoDatabaseVariables(settingsSection);
            services.Configure<AlianceApiSettings>(settingsSection);

            ConfigureRepository.ConfigureDependenceRepository(services);
            ConfigureService.ConfigureDependenceInjection(services);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntitiToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var signingConfiguration = new SigningConfigurations();
            services.AddSingleton(signingConfiguration);

            // TODO Comentei para retirar a nossa autenticação por Token
            // Authentication
            /*
            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfiguration")).Configure(tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Admin", new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser()
                                .RequireRole("Admin").Build());

                auth.AddPolicy("UserAdmin", new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser()
                                .RequireRole("UserAdmin", "Admin").Build());

                auth.AddPolicy("User", new AuthorizationPolicyBuilder()
                              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                              .RequireAuthenticatedUser()
                              .RequireRole("UserAdmin", "Admin", "User").Build());
            });
            */

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Shopping Aliansce - WebAPI",
                    Version = "1.0.0",
                    Description = "Projeto Aliansce - MXM Sistemas - Version 1.0.0 ",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "MXM Sistemas",
                        Email = "contato@mxm.com.br"
                    }
                });

                // TODO Comentei para retirar a nossa autenticação por Token
                //c.AddSecurityDefinition("User", new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Description = "Entre com o Token JWT",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey
                //});

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "User" }
                            }, new List<string>() }
                    });
            });

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        private void ConfigurarSoDatabaseVariables(IConfigurationSection settingsSection)
        {
            settingsSection["DatabaseConfigFromSO:Usuario"] = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Usuario").Value).Value;
            settingsSection["DatabaseConfigFromSO:Senha"] = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Senha").Value).Value;
            settingsSection["DatabaseConfigFromSO:Host"] = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Host").Value).Value;
            settingsSection["DatabaseConfigFromSO:ServiceName"] = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:ServiceName").Value).Value;
            settingsSection["DatabaseConfigFromSO:Port"] = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Port").Value).Value;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                if (exception is HttpStatusException httpException)
                {
                    context.Response.StatusCode = (int)httpException.Status;
                }

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsync(result);
            }));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "MXM Sistemas");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^S", "swagger");
            app.UseRewriter(option);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoint => { endpoint.MapControllers(); });
        }
    }
}
