using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AuthServer.Host.Pages
{
    public class IndexModel : AbpPageModel
    {
        public void OnGet()
        {
            
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            base.OnPageHandlerExecuted(context);
        }
    }
}