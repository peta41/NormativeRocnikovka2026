using Microsoft.AspNetCore.Mvc;


namespace Components
{
    public class SelectEx : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ModelSelectEx model)
        {
            return await Task.FromResult(View("SelectEx", model));
        }
    }
}
