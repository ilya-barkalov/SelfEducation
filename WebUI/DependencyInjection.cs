public static class DependencyInjection
{
    public static IServiceCollection AddWebUI(this IServiceCollection services)
    {
        services
            .AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Add("/{0}.cshtml");
                })
            .AddRazorRuntimeCompilation();

        return services;
    }
}