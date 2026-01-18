using Microsoft.AspNetCore.Mvc.RazorPages;

namespace jacobkukuk.com.Pages;

public class NotFoundModel : PageModel
{
    public void OnGet()
    {
        Response.StatusCode = 404;
    }
}
