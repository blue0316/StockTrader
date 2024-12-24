using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTrader.Data;
using StockTrader.Models.ModelBuilders;
using StockTrader.APIHandler.Interfaces;
using StockTrader.APIHandler;
using StockTrader.APIHandler.HttpHandlers.GetRequests;
using StockTrader.APIHandler.HttpHandlers.UrlBuilder;
using StockTrader.APIHandler.HttpHandlers.PutRequests;
using StockTrader.APIHandler.HttpHandlers;
using StockTrader.APIHandler.HttpHandlers.PostRequests;
using StockTrader.APIHandler.HttpHandlers.DeleteRequests;
using StockTrader.Models.EnvVariables;
using TradingViewUdfProvider;

namespace StockTrader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddProtectedBrowserStorage();
            services.AddTransient<IPurchaseViewModelBuilder, PurchaseViewModelBuilder>();
            services.AddTransient<IApiGateway, ApiGateway>();
            services.AddTransient<IGetRequestHandler, GetRequestHandler>();
            services.AddTransient<IGetRequestMaker, GetRequestMaker>();
            services.AddTransient<IUrlBuilder, UrlBuilder>();
            services.AddTransient<IPutRequestHandler, PutRequestHandler>();
            services.AddTransient<IPutRequestMaker, PutRequestMaker>();
            services.AddTransient<IStatusCodeHandler, StatusCodeHandler>();
            services.AddTransient<IHttpWrapper, HttpWrapper>();
            services.AddTransient<IPostRequestHandler, PostRequestHandler>();
            services.AddTransient<IPostRequestMaker, PostRequestMaker>();
            services.AddTransient<IDeleteHandler, DeleteHandler>();
            services.AddTransient<IDeleteMaker, DeleteMaker>();
            services.AddTransient<ITradeIdsBuilder, TradeIdsBuilder>();
            services.AddTransient<IStockIdModelBuilder, StockIdModelBuilder>();

            services.Configure<TvConf>(Configuration.GetSection(TvConf.Location));

            services.AddTradingViewProvider<MyTvProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
