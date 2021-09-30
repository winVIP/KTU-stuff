﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OOP_rest_web_service.Controllers;
using OOP_rest_web_service.Models;
using OOP_rest_web_service.Models.TemplateStuff;

namespace OOP_rest_web_service
{
    public class Startup
    {
        public static List<bool> allHitboxes;
        public static List<Color> allColors;
        public static List<DateTime> lastPosts;
        public static CommandInvoker commandInvoker;

        int sizeChange = 1;

        Random rnd = new Random();

        public Startup(IConfiguration configuration)
        {
            commandInvoker = new CommandInvoker();
            Configuration = configuration;

            allColors = new List<Color>();
            allColors.Add(Color.Red);
            allColors.Add(Color.Blue);
            allColors.Add(Color.Yellow);
            allColors.Add(Color.Green);
            allColors.Add(Color.Pink);
            allColors.Add(Color.Brown);
            allColors.Add(Color.Orange);
            allColors.Add(Color.Violet);

            allHitboxes = new List<bool>();
            for (int i = 0; i < 8; i++)
            {
                allHitboxes.Add(true);
            }

            lastPosts = new List<DateTime>();
            for(int i = 0; i < 8; i++)
            {
                lastPosts.Add(DateTime.MaxValue);
            }

            PeriodicMapCheck(TimeSpan.FromSeconds(0.1));
            CheckForPowerUps(TimeSpan.FromSeconds(0.1));
            PeriodicGeneratorCheck(TimeSpan.FromSeconds(5));

        }

        public async Task CheckForPowerUps(TimeSpan timeSpan)
        {
            while (true)
            {
                for (int i = 0; i < 8; i++)
                {

                    string[] powerUps = Map.getInstance().GetPlayer(i).getName().Split(";");

                    if (powerUps.Length > 1)
                    {
                        //Debug.WriteLine("Toks string name: " + Map.getInstance().GetPlayer(i).getName());
                        //Debug.WriteLine("Gavau tokius: ");
                        foreach (string p in powerUps)
                        {
                            Debug.Write(p + " ");
                        }
                        Debug.WriteLine("");

                        for (int j = powerUps.Length - 1; j > 0; j--)
                        {
                            if (powerUps[j] == "Shield")
                            {
                                PlayerDecorator playerDecorator = (PlayerDecorator)Map.getInstance().GetPlayer(i);
                                Map.getInstance().setPlayer(i, playerDecorator.getWrappee());
                                //Debug.WriteLine("Toks string name: " + Map.getInstance().GetPlayer(i).getName());
                                PowerUpHitBoxes(i);
                                //Debug.WriteLine("Iejau i Shield: " + j);
                            }
                            else if (powerUps[j] == "SizeUp")
                            {
                                PlayerDecorator playerDecorator = (PlayerDecorator)Map.getInstance().GetPlayer(i);
                                Map.getInstance().setPlayer(i, playerDecorator.getWrappee());
                                //Debug.WriteLine("Toks string name: " + Map.getInstance().GetPlayer(i).getName());
                                PowerUpSize(i);
                                //Debug.WriteLine("Iejau i SizeUp: " + j);
                            }
                            else if (powerUps[j] == "SizeDown")
                            {
                                PlayerDecorator playerDecorator = (PlayerDecorator)Map.getInstance().GetPlayer(i);
                                Map.getInstance().setPlayer(i, playerDecorator.getWrappee());
                               // Debug.WriteLine("Toks string name: " + Map.getInstance().GetPlayer(i).getName());
                                PowerDownSize(i);
                                //Debug.WriteLine("Iejau i SizeDown: " + j);
                            }
                        }
                    }
                }
                await Task.Delay(timeSpan);
            }
        }

        public async Task PowerUpSize(int index)
        {
            Map.getInstance().GetPlayer(index).setSize(new Size(Map.getInstance().GetPlayer(index).getSize().Width + 50, Map.getInstance().GetPlayer(index).getSize().Height + 50));
            await Task.Delay(TimeSpan.FromSeconds(5));
            Map.getInstance().GetPlayer(index).setSize(new Size(Map.getInstance().GetPlayer(index).getSize().Width - 50, Map.getInstance().GetPlayer(index).getSize().Height - 50));
        }

        public async Task PowerDownSize(int index)
        {
            Map.getInstance().GetPlayer(index).setSize(new Size(Map.getInstance().GetPlayer(index).getSize().Width / 2, Map.getInstance().GetPlayer(index).getSize().Height / 2));
            await Task.Delay(TimeSpan.FromSeconds(5));
            Map.getInstance().GetPlayer(index).setSize(new Size(Map.getInstance().GetPlayer(index).getSize().Width * 2, Map.getInstance().GetPlayer(index).getSize().Height * 2));
        }

        public async Task PowerUpHitBoxes(int index)
        {
            allHitboxes[index] = false;
            await Task.Delay(TimeSpan.FromSeconds(5));
            allHitboxes[index] = true;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public async Task PeriodicMapCheck(TimeSpan interval)
        {
            while (true)
            {
                for(int i = 0; i < 8; i++)
                {
                    //Debug.WriteLine(i + ": " + lastPosts[i]);
                    if (DateTime.Now.Subtract(lastPosts[i]) >= TimeSpan.FromSeconds(5))
                    {
                        //Debug.WriteLine("Iejom i if: " + i);
                        Map.getInstance().setPlayer(i, UnitCreator.createUnit(0));
                    }
                }
                await Task.Delay(interval);
            }
        }

        public async Task PeriodicGeneratorCheck(TimeSpan interval)
        {
            var r = new Random();
            while (true)
            {
                var mode = Map.getInstance().mode;
                var generator = Map.getInstance().getGenerator();


                generator.operate();


                var X = generator.position.X;
                var Y = generator.position.Y;

                List<Point> points = new List<Point>();

                points.Add(new Point(X + 55, Y + 47));
                points.Add(new Point(X + 60, Y + 9));
                points.Add(new Point(X + 43, Y - 41));
                points.Add(new Point(X + 54, Y + 66));
                points.Add(new Point(X + 7, Y + 49));
                points.Add(new Point(X - 40, Y + 56));
                points.Add(new Point(X + 55, Y + 47));
                points.Add(new Point(X + 60, Y + 9));
                points.Add(new Point(X + 43, Y - 41));
                points.Add(new Point(X + 54, Y + 66));
                points.Add(new Point(X + 7, Y + 49));
                points.Add(new Point(X - 40, Y + 56));

                Point nextPoint = points[r.Next(0, 6)];


                switch (generator.generatingObjectType)
                {
                    case 3:
                        var food1 = new SizeDownFood(nextPoint);
                        food1.SetColor(Color.DarkSalmon);
                        food1.makeFood();
                        Map.getInstance().putFood(food1, r.Next(1, 49));
                        break;
                    case 2:
                        var food2 = new SizeUpFood(nextPoint);
                        food2.SetColor(Color.DarkRed);
                        food2.makeFood();
                        Map.getInstance().putFood(food2, r.Next(1, 49));
                        break;
                    case 1:
                        var food3 = new Food(nextPoint);
                        food3.SetColor(Color.DarkGreen);
                        food3.makeFood();
                        Map.getInstance().putFood(food3, r.Next(1, 49));
                        break;
                    case 0:
                        break;
                    default: break;
                }
                await Task.Delay(interval);
            }
        }





    }
}
