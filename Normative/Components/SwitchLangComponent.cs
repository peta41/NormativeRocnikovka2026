using Microsoft.AspNetCore.Mvc;

namespace FRS.UI.Components;

public class SwitchLangComponent() : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult(View("SwitchLang"));
    }
}
