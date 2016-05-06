using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentScheduler;
using NCraftDisplay.Data;
using NCraftDisplay.Data.Engine;

namespace NCraftDisplay
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            JobManager.Initialize(new GameRegistry());
        }
    }

    public class GameRegistry : Registry
    {
        public GameRegistry()
        {
            string workingDirectory = WebConfigurationManager.AppSettings["CsvFilesLocation"];
            int refreshRate = int.Parse(WebConfigurationManager.AppSettings["RefreshRate"]);

            Schedule(() =>
            {
                var repo = new CsvFileRepository(workingDirectory);
                var execRepo = new ExecReportRepository(workingDirectory);
                var runner = new EngineExecutor(workingDirectory, execRepo);

                try
                {
                    var results = runner.Process();
                    runner.WriteCsvResults(repo, results);
                }
                catch (AggregateException aex)
                {
                    foreach (var ex in aex.InnerExceptions)
                    {
                        throw ex;
                    }
                }
            })
            .NonReentrant()
            .ToRunNow().AndEvery(refreshRate).Seconds();
        }
    }
}
