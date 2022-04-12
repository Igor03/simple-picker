﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using SpecFlow.Autofac;
using Microsoft.EntityFrameworkCore;
using JIgor.Projects.SimplePicker.Api.Database.DataContexts;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Linq;
using TechTalk.SpecFlow;
using System.IO;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers;
using System.Net.Http;

namespace JIgor.Projects.SimplePicker.IntegrationTests.Support.DependencyInjection
{
    internal class ProjectDependencies
    {
        public static IConfiguration? _configuration;

        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();
            
            // Here, the order matter
            RegisterConfigurationRoot(builder);
            RegisterDatabase(builder, _configuration!);
            RegisterHelpers(builder, _configuration!);

            // Registering all the binding types - SpecFlow
            _ = builder.RegisterTypes(
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))).ToArray());

            return builder;
        }

        private static void RegisterHelpers(ContainerBuilder builder, IConfiguration configuration)
        {
            _ = builder.RegisterType<DatabaseHelper>();

            _ = builder.Register(c =>
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:8080/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                _ = builder.RegisterInstance(client).As<HttpClient>();

                return new HttpClientHelper(client);
            }).As<HttpClientHelper>().SingleInstance();
        }

        private static void RegisterConfigurationRoot(ContainerBuilder builder)
        {
            const string machineUser = "jigor";
            const string secretsJsonId = "1a47e734-38fd-45dc-aac3-4c4690921215";
            var secretsJson =
                $"C:/Users/{machineUser}/AppData/Roaming/Microsoft/UserSecrets/{secretsJsonId}/secrets.json";

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile(secretsJson, false)
                .AddJsonFile("appsettings.Json", false)
                .Build();

            builder.RegisterInstance(_configuration)
                .As<IConfiguration>();
        }

        private static void RegisterDatabase(ContainerBuilder builder, IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder(
                new DbContextOptions<SimplePickerDatabaseContext>());
            dbContextBuilder.UseSqlServer(_configuration.GetConnectionString("mssqlserver"));

            builder.RegisterInstance(dbContextBuilder.Options)
                .As<DbContextOptions<SimplePickerDatabaseContext>>();

            // Dont need to specify constructor because we already registered all the types needed
            builder.RegisterType<SimplePickerDatabaseContext>().As<ISimplePickerDatabaseContext>();
                //.UsingConstructor(typeof(DbContextOptions<SimplePickerDatabaseContext>), typeof(IConfiguration));
        }
    }
}
