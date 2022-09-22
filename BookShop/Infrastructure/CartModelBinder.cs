

using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}
