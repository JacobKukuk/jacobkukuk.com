# GitHub Copilot Instructions for jacobkukuk.com

## Project Overview

This is a personal portfolio website for Jacob Kukuk built with **ASP.NET Core Razor Pages** targeting **.NET 9**. The site showcases professional experience, skills, and projects with a modern, responsive design.

## Technology Stack

- **Framework**: ASP.NET Core 9.0 with Razor Pages
- **Styling**: Tailwind CSS (via CDN)
- **Fonts**: Google Fonts (Inter, JetBrains Mono)
- **Analytics**: Umami Cloud
- **Deployment**: Static file hosting with optimized caching

## Code Style Guidelines

### C# / .NET

- Use **nullable reference types** (`<Nullable>enable</Nullable>`)
- Use **implicit usings** (`<ImplicitUsings>enable</ImplicitUsings>`)
- Follow Microsoft's C# coding conventions
- Use file-scoped namespaces when applicable
- Prefer `var` for locally-scoped variables when the type is obvious
- Use async/await patterns for I/O operations
- Keep `Program.cs` minimal and delegate to extension methods for complex configuration

### Razor Pages

- Place page models in the same directory as their `.cshtml` files
- Use `@page` directive at the top of Razor pages
- Leverage `ViewData` for page titles
- Use partial views for reusable components (place in `Pages/Shared/`)
- Follow the convention: `Pages/{PageName}.cshtml` with `Pages/{PageName}.cshtml.cs`

### HTML/CSS

- Use semantic HTML5 elements (`<header>`, `<main>`, `<footer>`, `<nav>`, `<section>`, `<article>`)
- Ensure all images have descriptive `alt` attributes
- Include ARIA labels for accessibility
- Use Tailwind CSS utility classes for styling
- Follow mobile-first responsive design principles
- Maintain dark theme consistency with the existing color palette:
  - Primary: `#ed6f1f` (orange)
  - Dark backgrounds: `#0f172a`, `#1e293b`, `#334155`

### SEO & Performance

- Include comprehensive meta tags (Open Graph, Twitter Cards)
- Implement structured data (JSON-LD Schema.org)
- Use response compression (Brotli, Gzip)
- Enable static file caching (1 year for assets)
- Include security headers (CSP, X-Frame-Options, etc.)

## Project Structure

```
jacobkukuk.com/
??? .github/
?   ??? instructions/       # Copilot custom instructions
?   ??? workflows/          # GitHub Actions workflows
??? Pages/
?   ??? Shared/
?   ?   ??? _Layout.cshtml      # Main layout template
?   ?   ??? _Layout.cshtml.css  # Layout-specific styles
?   ?   ??? _ValidationScriptsPartial.cshtml
?   ??? _ViewImports.cshtml
?   ??? _ViewStart.cshtml
?   ??? Error.cshtml(.cs)
?   ??? Index.cshtml(.cs)
??? wwwroot/                # Static files
?   ??? css/
?   ??? js/
?   ??? images/
?   ??? robots.txt
?   ??? sitemap.xml
?   ??? humans.txt
?   ??? schema.json
??? Program.cs              # Application entry point
??? jacobkukuk.com.csproj   # Project file
```

## Common Tasks

### Adding a New Page

1. Create `Pages/{PageName}.cshtml` with `@page` directive
2. Create corresponding `Pages/{PageName}.cshtml.cs` with PageModel class
3. Set `ViewData["Title"]` in the PageModel or Razor page
4. Add navigation link in `_Layout.cshtml` if needed

### Adding Static Files

1. Place files in appropriate `wwwroot/` subdirectory
2. Reference using root-relative paths (e.g., `/images/photo.jpg`)
3. Files will automatically benefit from caching headers

### Security Considerations

- Content Security Policy is configured in `Program.cs`
- Update CSP if adding new external resources
- All external scripts must be from allowed domains

## Don't Do

- Don't use inline JavaScript without updating CSP
- Don't remove security headers from `Program.cs`
- Don't use absolute URLs for internal resources
- Don't add packages without considering bundle size impact
- Don't bypass the existing response compression middleware

## Testing

- Verify pages render correctly after changes
- Test responsive design at multiple breakpoints
- Validate accessibility with screen reader testing
- Check Lighthouse scores for performance/SEO

## Additional Resources

- [ASP.NET Core Razor Pages Documentation](https://learn.microsoft.com/aspnet/core/razor-pages/)
- [Tailwind CSS Documentation](https://tailwindcss.com/docs)
- [Schema.org Documentation](https://schema.org/)
