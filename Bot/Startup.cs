using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.Bot;
using Bot.Bot.Handlers;
using Bot.Bot.Handlers.Implementations;
using Bot.Bot.Interactor;
using Bot.Bot.Senders;
using Bot.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwitchLib.Client;

namespace Bot
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
            services.AddHostedService<BotHostedService>();

            services.AddSingleton(Configuration);

            //services.AddSingleton<IJoinChannelHandler, DefaultJoinChannelHandler>();
            services.AddSingleton<IModeratorJoinedHandler, ModeratorJoinedWelcomeHandler>();
            services.AddSingleton<IUserJoinedHandler, UserJoinedWelcomeHandler>();
            services.AddSingleton<ICommandHandler, HelpCommandHandler>();
            services.AddSingleton<ICommandHandler, BlamesCommandHandler>();
            services.AddSingleton<ICommandHandler, WinsCommandHandler>();
            
            services.AddSingleton<Func<TwitchClient, IInteractor>>(provider =>
            {
                return client => new SimpleInteractor(new TwitchMessageSender(provider.GetService<ILogger<TwitchMessageSender>>(), client),
                    new TwitchWhisperSender(provider.GetService<ILogger<TwitchWhisperSender>>(), client),
                    new TwitchMessageDeletionHandler(provider.GetService<ILogger<TwitchMessageDeletionHandler>>(), client));
            });
            services.AddSingleton<IBot, TwitchBot>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
    }
}