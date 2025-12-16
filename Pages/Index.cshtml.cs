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

        public string PageTitle { get; set; } = "KUKUK_OS v1.0 | Digital Polymath & Systems Engineer";
        public string MetaDescription { get; set; } = "Jacob Kukuk - Digital Polymath. Las Vegas-based Full-Stack Developer and Systems Engineer with 15+ years of experience in C#, .NET, Azure, AWS, SQL Server, and building resilient infrastructure.";
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
            _logger.LogInformation("KUKUK_OS initialized at {Time}", DateTime.UtcNow);
        }
    }
}
