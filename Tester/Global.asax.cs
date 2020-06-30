using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TesterModel;
using TesterModel.Abstract;
using TesterModel.Concrete;

namespace Tester
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private const string PathTestResults = @"C:/TestResults.xml";
        private const string resultSessionKey = "_result";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(TestResult), new ResultModelBinder());
            AuthConfig.RegisterAuth();
        }
                
        public void Session_End(object sender, EventArgs e)
        {
            IResultRepository resultRepository = new XMLResultRepository(PathTestResults);
            resultRepository.AddTestResult((TestResult)Session[resultSessionKey]);
        }
    }
}