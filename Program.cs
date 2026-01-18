using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add response compression for better performance
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "text/html",
        "text/css",
        "application/javascript",
        "text/javascript",
        "application/json",
        "application/xml",
        "text/xml",
        "image/svg+xml",
        "application/font-woff",
        "application/font-woff2",
        "font/woff",
        "font/woff2"
    });
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

// Add HTTP caching
builder.Services.AddResponseCaching();

// Add HSTS (HTTP Strict Transport Security)
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Enable response compression
app.UseResponseCompression();

// Enable response caching
app.UseResponseCaching();

// Custom content type provider for additional file types
var contentTypeProvider = new FileExtensionContentTypeProvider();
contentTypeProvider.Mappings[".webmanifest"] = "application/manifest+json";
contentTypeProvider.Mappings[".webp"] = "image/webp";
contentTypeProvider.Mappings[".avif"] = "image/avif";
contentTypeProvider.Mappings[".woff2"] = "font/woff2";

// Static files with optimized caching
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = contentTypeProvider,
    OnPrepareResponse = ctx =>
    {
        var headers = ctx.Context.Response.Headers;
        var contentType = ctx.Context.Response.ContentType ?? "";
        
        // Different cache durations based on file type
        int maxAge;
        
        if (contentType.Contains("image/") || contentType.Contains("font/"))
        {
            // Images and fonts: 1 year (immutable assets)
            maxAge = 60 * 60 * 24 * 365;
            headers.Append(HeaderNames.CacheControl, $"public,max-age={maxAge},immutable");
        }
        else if (contentType.Contains("text/css") || contentType.Contains("javascript"))
        {
            // CSS and JS: 1 week (may change more frequently)
            maxAge = 60 * 60 * 24 * 7;
            headers.Append(HeaderNames.CacheControl, $"public,max-age={maxAge}");
        }
        else if (ctx.File.Name == "sitemap.xml" || ctx.File.Name == "robots.txt")
        {
            // SEO files: 1 day
            maxAge = 60 * 60 * 24;
            headers.Append(HeaderNames.CacheControl, $"public,max-age={maxAge}");
        }
        else
        {
            // Everything else: 1 month
            maxAge = 60 * 60 * 24 * 30;
            headers.Append(HeaderNames.CacheControl, $"public,max-age={maxAge}");
        }
    }
});

// Add security and SEO headers middleware
app.Use(async (context, next) =>
{
    var headers = context.Response.Headers;
    
    // Security headers
    headers.Append("X-Content-Type-Options", "nosniff");
    headers.Append("X-Frame-Options", "DENY");
    headers.Append("X-XSS-Protection", "1; mode=block");
    headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    headers.Append("Permissions-Policy", "geolocation=(), microphone=(), camera=(), payment=(), usb=()");
    headers.Append("Cross-Origin-Embedder-Policy", "unsafe-none");
    headers.Append("Cross-Origin-Opener-Policy", "same-origin");
    headers.Append("Cross-Origin-Resource-Policy", "cross-origin");
    
    // Content Security Policy (updated for Font Awesome CDN)
    headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.tailwindcss.com https://cloud.umami.is https://www.googletagmanager.com https://www.google-analytics.com; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
        "font-src 'self' https://fonts.gstatic.com https://cdnjs.cloudflare.com data:; " +
        "img-src 'self' data: https: http:; " +
        "connect-src 'self' https://cloud.umami.is https://www.google-analytics.com https://analytics.google.com; " +
        "frame-ancestors 'none'; " +
        "base-uri 'self'; " +
        "form-action 'self';");

    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// SEO-friendly endpoints with proper headers

// Serve robots.txt with cache headers
app.MapGet("/robots.txt", async context =>
{
    context.Response.ContentType = "text/plain; charset=utf-8";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=86400"); // 1 day
    await context.Response.SendFileAsync("wwwroot/robots.txt");
});

// Serve sitemap.xml with cache headers
app.MapGet("/sitemap.xml", async context =>
{
    context.Response.ContentType = "application/xml; charset=utf-8";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=86400"); // 1 day
    await context.Response.SendFileAsync("wwwroot/sitemap.xml");
});

// Serve humans.txt
app.MapGet("/humans.txt", async context =>
{
    context.Response.ContentType = "text/plain; charset=utf-8";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=604800"); // 1 week
    await context.Response.SendFileAsync("wwwroot/humans.txt");
});

// Serve security.txt (RFC 9116 compliant location)
app.MapGet("/.well-known/security.txt", async context =>
{
    context.Response.ContentType = "text/plain; charset=utf-8";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=604800"); // 1 week
    await context.Response.SendFileAsync("wwwroot/.well-known/security.txt");
});

// Also serve security.txt from root for compatibility
app.MapGet("/security.txt", async context =>
{
    context.Response.ContentType = "text/plain; charset=utf-8";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=604800"); // 1 week
    await context.Response.SendFileAsync("wwwroot/.well-known/security.txt");
});

// Serve schema.json for structured data
app.MapGet("/schema.json", async context =>
{
    context.Response.ContentType = "application/ld+json; charset=utf-8";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=86400"); // 1 day
    if (File.Exists("wwwroot/schema.json"))
    {
        await context.Response.SendFileAsync("wwwroot/schema.json");
    }
    else
    {
        context.Response.StatusCode = 404;
    }
});

// Favicon fallback for older browsers
app.MapGet("/favicon.ico", async context =>
{
    context.Response.ContentType = "image/x-icon";
    context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=31536000,immutable"); // 1 year
    if (File.Exists("wwwroot/favicon.ico"))
    {
        await context.Response.SendFileAsync("wwwroot/favicon.ico");
    }
    else
    {
        context.Response.StatusCode = 204; // No content
    }
});

app.Run();

