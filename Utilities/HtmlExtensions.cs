using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ABCPDFNET5MVCExample.Utilities
{
    public static class HtmlExtensions
    {
        public static async Task<string> ViewToStringAsync(this Controller controller, string viewName, object model)
        {
            using (var writer = new StringWriter())
            {
                var services = controller.ControllerContext.HttpContext.RequestServices;
                var viewEngine = services.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                //var viewName = partialView.ViewName ?? controller.ControllerContext.ActionDescriptor.ActionName;
                var view = viewEngine.FindView(controller.ControllerContext, viewName, false).View;
                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };
                var viewContext = new ViewContext(controller.ControllerContext, view, viewDictionary, controller.TempData, writer, new HtmlHelperOptions());
                await view.RenderAsync(viewContext);
                return writer.ToString();
            }
        }

    }
    }
