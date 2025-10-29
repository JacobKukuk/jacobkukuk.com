using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace jacobkukuk.com.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string PageTitle { get; set; } = "Jacob Kukuk - Full-Stack Developer & Systems Engineer | Las Vegas, NV";
        public string MetaDescription { get; set; } = "Jacob Kukuk is a Las Vegas-based Full-Stack Developer and Systems Engineer with 15+ years of experience. Expert in C#, .NET, Azure, AWS, SQL Server, SharePoint, and enterprise system architecture.";
        public string CanonicalUrl { get; set; } = "https://jacobkukuk.com/";
        public string OgImage { get; set; } = "https://jacobkukuk.com/jacobkukukcityscape.png";
        public string CurrentYear { get; set; } = DateTime.Now.Year.ToString();
        public string LastModified { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        public void OnGet()
        {
            // Set dynamic SEO properties
            ViewData["Title"] = PageTitle;
            ViewData["Description"] = MetaDescription;
            ViewData["CanonicalUrl"] = CanonicalUrl;
            ViewData["OgImage"] = OgImage;
            ViewData["CurrentYear"] = CurrentYear;
            ViewData["LastModified"] = LastModified;

            // Log page view for analytics
            _logger.LogInformation("Homepage accessed at {Time}", DateTime.UtcNow);
        }
    }
}
