# Architecture Guide - jacobkukuk.com

## Overview

This document describes the architectural patterns and decisions for the jacobkukuk.com portfolio website built with ASP.NET Core Razor Pages.

## Application Architecture

### Framework Choice: Razor Pages

Razor Pages was chosen over MVC or Blazor for this project because:

- **Simplicity**: Page-focused model ideal for content-driven portfolio sites
- **Convention over configuration**: Minimal boilerplate code
- **SEO-friendly**: Server-side rendering ensures optimal search engine indexing
- **Performance**: Static content renders quickly without JavaScript framework overhead

### Layer Structure

```
???????????????????????????????????????????????
?              Presentation Layer              ?
?  (Razor Pages, Layouts, Partial Views)       ?
???????????????????????????????????????????????
?              Middleware Pipeline             ?
?  (Compression, Caching, Security Headers)    ?
???????????????????????????????????????????????
?              Static Assets                   ?
?  (wwwroot: CSS, JS, Images)                  ?
???????????????????????????????????????????????
```

## Request Pipeline

The middleware pipeline is configured in `Program.cs` in the following order:

1. **Exception Handler** - Catches unhandled exceptions in production
2. **HSTS** - HTTP Strict Transport Security
3. **HTTPS Redirection** - Forces secure connections
4. **Response Compression** - Brotli/Gzip compression
5. **Response Caching** - Caches responses for performance
6. **Static Files** - Serves wwwroot content with 1-year cache headers
7. **Security Headers** - Custom middleware for CSP, X-Frame-Options, etc.
8. **Routing** - Maps requests to Razor Pages
9. **Authorization** - (configured but minimal for public site)
10. **Razor Pages Endpoints** - Renders page content

### Security Headers Middleware

Custom inline middleware adds security headers to all responses:

```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Append("Content-Security-Policy", "...");
    await next();
});
```

## Page Structure

### Layout Hierarchy

```
_Layout.cshtml (Base layout)
??? Shared navigation
??? @RenderBody() (Page content)
??? Shared footer
??? @RenderSectionAsync("Scripts")
```

### Page Model Pattern

Each Razor Page follows this structure:

```csharp
public class ExampleModel : PageModel
{
    public void OnGet()
    {
        ViewData["Title"] = "Page Title";
    }
}
```

### View Imports

`_ViewImports.cshtml` provides global imports:

```razor
@using jacobkukuk.com
@namespace jacobkukuk.com.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

## Styling Architecture

### Tailwind CSS (CDN)

Tailwind CSS is loaded via CDN with custom configuration:

```javascript
tailwind.config = {
    theme: {
        extend: {
            colors: {
                primary: { /* Orange palette */ },
                dark: { /* Slate palette */ }
            },
            fontFamily: {
                sans: ['Inter', 'system-ui', 'sans-serif'],
                mono: ['JetBrains Mono', 'Consolas', 'monospace']
            }
        }
    }
}
```

### Custom CSS

Custom styles are defined in `<style>` blocks within `_Layout.cshtml`:

- **Animations**: `fadeIn`, `slideUp`, `float`, `glow`
- **Glass morphism**: `.glass`, `.dark-glass` classes
- **Gradients**: `.gradient-bg`, `.gradient-text`
- **Navigation**: `.nav-item` with underline animation

### Design System Colors

| Token | Hex | Usage |
|-------|-----|-------|
| `primary-500` | `#ed6f1f` | Primary brand orange |
| `primary-600` | `#de5615` | Hover states |
| `dark-900` | `#0f172a` | Main background |
| `dark-800` | `#1e293b` | Card backgrounds |
| `dark-700` | `#334155` | Secondary backgrounds |

## Performance Optimizations

### Response Compression

```csharp
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
```

### Static File Caching

```csharp
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", 
            "public,max-age=31536000" // 1 year
        );
    }
});
```

### Resource Hints

DNS prefetch and preconnect for external resources:

```html
<link rel="dns-prefetch" href="//fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
```

## SEO Architecture

### Meta Tags Strategy

- **Primary**: Title, description, keywords, author
- **Social**: Open Graph (Facebook), Twitter Cards
- **Technical**: Canonical URL, robots, language alternates
- **Geographic**: Geo region, position, ICBM coordinates

### Structured Data (JSON-LD)

Two Schema.org types are implemented:

1. **Person Schema**: Professional profile, skills, employment
2. **WebSite Schema**: Site metadata, search action

### Static SEO Files

| File | Purpose |
|------|---------|
| `robots.txt` | Search engine crawl directives |
| `sitemap.xml` | URL listing for search engines |
| `humans.txt` | Team/technology credits |
| `schema.json` | Additional structured data |

## Content Security Policy

The CSP is configured to allow:

| Directive | Allowed Sources |
|-----------|-----------------|
| `default-src` | `'self'` |
| `script-src` | `'self'`, `'unsafe-inline'`, `'unsafe-eval'`, `cdn.tailwindcss.com`, `cloud.umami.is` |
| `style-src` | `'self'`, `'unsafe-inline'`, `fonts.googleapis.com` |
| `font-src` | `'self'`, `fonts.gstatic.com` |
| `img-src` | `'self'`, `data:`, `https:`, `http:` |
| `connect-src` | `'self'`, `cloud.umami.is` |

## Analytics

**Umami Cloud** is used for privacy-friendly analytics:

```html
<script defer 
    src="https://cloud.umami.is/script.js" 
    data-website-id="908a8a9d-5953-47bc-b888-f278297c6060">
</script>
```

## Accessibility (a11y)

### Implemented Features

- Skip navigation link for keyboard users
- ARIA labels on all interactive elements
- Semantic HTML structure
- Color contrast compliant with WCAG 2.1
- Focus indicators on interactive elements
- Mobile menu with proper `aria-expanded` state

### Accessibility Testing Checklist

- [ ] Keyboard navigation works throughout
- [ ] Screen reader announces all content correctly
- [ ] Color contrast ratio ? 4.5:1 for text
- [ ] Focus states are visible
- [ ] Touch targets are ? 44x44px on mobile

## Extending the Architecture

### Adding a New Page

1. Create `Pages/NewPage.cshtml`:
   ```razor
   @page
   @model NewPageModel
   @{
       ViewData["Title"] = "New Page";
   }
   <section id="new-section">...</section>
   ```

2. Create `Pages/NewPage.cshtml.cs`:
   ```csharp
   public class NewPageModel : PageModel
   {
       public void OnGet() { }
   }
   ```

3. Update navigation in `_Layout.cshtml`
4. Add to `sitemap.xml`

### Adding External Scripts

1. Update CSP in `Program.cs` to allow the domain
2. Add DNS prefetch/preconnect hints in `_Layout.cshtml`
3. Load script with `defer` or `async` attribute

### Adding New API Endpoints

For simple endpoints (like the existing robots.txt), use minimal APIs:

```csharp
app.MapGet("/api/data", () => Results.Json(new { ... }));
```

For complex logic, consider adding a dedicated API controller or service layer.

## Deployment Considerations

### Production Checklist

- [ ] HTTPS certificate configured
- [ ] Environment set to Production
- [ ] Response compression enabled
- [ ] Static file caching active
- [ ] Security headers present
- [ ] Analytics tracking verified

### Environment-Specific Behavior

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
```

## Future Architecture Considerations

### Potential Enhancements

1. **Build-time Tailwind**: Switch from CDN to build-time compilation for smaller CSS bundle
2. **Image Optimization**: Add responsive image generation pipeline
3. **Caching Layer**: Add distributed caching if dynamic content is added
4. **Health Checks**: Add `/health` endpoint for monitoring
5. **Feature Flags**: Consider adding feature flag system for gradual rollouts

### When to Consider Blazor

- If significant client-side interactivity is needed
- If component reuse across multiple pages becomes complex
- If real-time updates (SignalR) are required

Keep the current Razor Pages architecture for predominantly static, SEO-focused content.
