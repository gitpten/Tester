using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesterModel;

namespace Tester
{
    public class ResultModelBinder: IModelBinder
    {
        private const string resultSessionKey = "_result";
        private const string expNoRefresh = "Не удалось обновить экземпляры";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
            {
                throw new InvalidOperationException(expNoRefresh);
            }

            TestResult result = (TestResult)controllerContext.HttpContext.Session[resultSessionKey];

            if (result == null)
            {
                result = new TestResult();
                controllerContext.HttpContext.Session[resultSessionKey] = result;                
            }
            return result;
        }

    }
}