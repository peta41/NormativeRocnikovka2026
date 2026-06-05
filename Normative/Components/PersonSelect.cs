using Microsoft.AspNetCore.Mvc;

namespace Components
{
    public class PersonSelect : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ModelPerson model)
        {
            return await Task.FromResult(View("PersonSelect", model));
        }
    }
}
