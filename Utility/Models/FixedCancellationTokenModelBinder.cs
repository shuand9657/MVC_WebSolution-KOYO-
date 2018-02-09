using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Utility
{
    public class FixedCancellationTokenModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var source = CancellationTokenSource.CreateLinkedTokenSource(default(CancellationToken),
                    controllerContext.HttpContext.Response.ClientDisconnectedToken);

            return source.Token;
        }
    }
}
