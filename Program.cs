using Microsoft.AspNetCore.ResponseCompression;
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
        "image/svg+xml"
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

// Static files with caching
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static files for 1 year
        const int durationInSeconds = 60 * 60 * 24 * 365;
        ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={durationInSeconds}");
    }
});

// Add security headers middleware
app.Use(async (context, next) =>
{
    // Security headers for better SEO and security
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Append("Permissions-Policy", "geolocation=(), microphone=(), camera=()");
    
    // Content Security Policy
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.tailwindcss.com https://cloud.umami.is; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
        "font-src 'self' https://fonts.gstatic.com; " +
        "img-src 'self' data: https: http:; " +
        "connect-src 'self' https://cloud.umami.is;");

    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Serve robots.txt
app.MapGet("/robots.txt", async context =>
{
    context.Response.ContentType = "text/plain";
    await context.Response.SendFileAsync("wwwroot/robots.txt");
});

// Serve sitemap.xml
app.MapGet("/sitemap.xml", async context =>
{
    context.Response.ContentType = "application/xml";
    await context.Response.SendFileAsync("wwwroot/sitemap.xml");
});

// Serve humans.txt
app.MapGet("/humans.txt", async context =>
{
    context.Response.ContentType = "text/plain";
    await context.Response.SendFileAsync("wwwroot/humans.txt");
});

// Serve schema.json for structured data
app.MapGet("/schema.json", async context =>
{
    context.Response.ContentType = "application/json";
    await context.Response.SendFileAsync("wwwroot/schema.json");
});

app.Run();

