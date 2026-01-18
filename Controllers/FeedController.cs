using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace jacobkukuk.com.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedController : ControllerBase
{
    [HttpGet("rss")]
    [Produces("application/rss+xml")]
    public IActionResult Rss()
    {
        var feed = GenerateRssFeed();
        return Content(feed, "application/rss+xml", Encoding.UTF8);
    }

    [HttpGet("atom")]
    [Produces("application/atom+xml")]
    public IActionResult Atom()
    {
        var feed = GenerateAtomFeed();
        return Content(feed, "application/atom+xml", Encoding.UTF8);
    }

    private static string GenerateRssFeed()
    {
        var lastBuildDate = DateTime.UtcNow.ToString("R");
        
        return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<rss version=""2.0"" xmlns:atom=""http://www.w3.org/2005/Atom"" xmlns:content=""http://purl.org/rss/1.0/modules/content/"">
  <channel>
    <title>Jacob Kukuk - Systems Engineer &amp; Full-Stack Developer</title>
    <link>https://jacobkukuk.com/</link>
    <description>Professional portfolio and blog of Jacob Kukuk, a Las Vegas-based Systems Engineer and Full-Stack Developer specializing in C#, .NET, Azure, and enterprise systems.</description>
    <language>en-us</language>
    <lastBuildDate>{lastBuildDate}</lastBuildDate>
    <atom:link href=""https://jacobkukuk.com/feed/rss"" rel=""self"" type=""application/rss+xml"" />
    <webMaster>me@jacobkukuk.com (Jacob Kukuk)</webMaster>
    <managingEditor>me@jacobkukuk.com (Jacob Kukuk)</managingEditor>
    <copyright>Copyright {DateTime.Now.Year} Jacob Kukuk</copyright>
    <category>Technology</category>
    <category>Software Development</category>
    <category>Systems Engineering</category>
    <ttl>1440</ttl>
    <image>
      <url>https://jacobkukuk.com/jacobkukukcityscape.png</url>
      <title>Jacob Kukuk - Systems Engineer &amp; Full-Stack Developer</title>
      <link>https://jacobkukuk.com/</link>
    </image>
    
    <!-- Sample item - replace with actual blog posts when available -->
    <item>
      <title>Welcome to jacobkukuk.com</title>
      <link>https://jacobkukuk.com/</link>
      <guid isPermaLink=""true"">https://jacobkukuk.com/</guid>
      <pubDate>{lastBuildDate}</pubDate>
      <description>Professional portfolio showcasing 20+ years of experience in systems engineering and full-stack development.</description>
      <author>me@jacobkukuk.com (Jacob Kukuk)</author>
      <category>Portfolio</category>
    </item>
  </channel>
</rss>";
    }

    private static string GenerateAtomFeed()
    {
        var updated = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        
        return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<feed xmlns=""http://www.w3.org/2005/Atom"">
  <title>Jacob Kukuk - Systems Engineer &amp; Full-Stack Developer</title>
  <subtitle>Professional portfolio and blog of Jacob Kukuk</subtitle>
  <link href=""https://jacobkukuk.com/feed/atom"" rel=""self"" type=""application/atom+xml"" />
  <link href=""https://jacobkukuk.com/"" rel=""alternate"" type=""text/html"" />
  <id>https://jacobkukuk.com/</id>
  <updated>{updated}</updated>
  <author>
    <name>Jacob Kukuk</name>
    <email>me@jacobkukuk.com</email>
    <uri>https://jacobkukuk.com/</uri>
  </author>
  <rights>Copyright {DateTime.Now.Year} Jacob Kukuk</rights>
  <category term=""Technology"" />
  <category term=""Software Development"" />
  <icon>https://jacobkukuk.com/favicon.ico</icon>
  <logo>https://jacobkukuk.com/jacobkukukcityscape.png</logo>
  
  <!-- Sample entry - replace with actual blog posts when available -->
  <entry>
    <title>Welcome to jacobkukuk.com</title>
    <link href=""https://jacobkukuk.com/"" rel=""alternate"" type=""text/html"" />
    <id>https://jacobkukuk.com/</id>
    <updated>{updated}</updated>
    <summary>Professional portfolio showcasing 20+ years of experience in systems engineering and full-stack development.</summary>
    <author>
      <name>Jacob Kukuk</name>
      <email>me@jacobkukuk.com</email>
    </author>
    <category term=""Portfolio"" />
  </entry>
</feed>";
    }
}
