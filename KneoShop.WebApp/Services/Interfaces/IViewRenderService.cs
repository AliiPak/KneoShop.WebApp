namespace KenoShop.WebApp.Services.Interfaces
{
    public interface IViewRenderService
    {
        string RenderToStringAsync(string viewName, object model);
    }
}
