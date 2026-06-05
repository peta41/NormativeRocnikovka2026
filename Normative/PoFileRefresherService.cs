using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

public class PoFileRefresherService : BackgroundService
{
    private const string cacheKeyPrefix = "CultureDictionary-";

    private const string poFileWileCard = "*.po";

    private readonly IServiceProvider provider = null!;

    private readonly IWebHostEnvironment environment = null!;

    private readonly LocalizationOptions options = null!;

    private IChangeToken changeToken = null!;

    private readonly object state = new();

    private readonly string poFilesPath = null!;
    
    public PoFileRefresherService(IOptions<LocalizationOptions> options, IServiceProvider provider, IWebHostEnvironment environment)
    {
        this.provider = provider;

        this.environment = environment;

        this.options = options.Value;

        this.poFilesPath = Path.Combine(this.options.ResourcesPath, poFileWileCard);
    }
    
    private static string GetCacheKey(string culture) => cacheKeyPrefix + culture;

    private IDirectoryContents DirectoryContent => environment.ContentRootFileProvider.GetDirectoryContents(this.options.ResourcesPath);

    private void SetChangeToken()
    {
        changeToken = environment.ContentRootFileProvider.Watch(poFilesPath);

        changeToken.RegisterChangeCallback(ChangeCallBack, state);
    }

    private void ChangeCallBack(object _)
    {
        // Stop change call back being called multiple times.
        Thread.Sleep(5000);

        ProcessChanged();

        SetChangeToken();
    }

    private void ProcessChanged()
    {
        using IServiceScope scope = provider.CreateScope();

        IMemoryCache cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

        foreach (IFileInfo fileInfo in DirectoryContent)
        {
            string name = Path.GetFileNameWithoutExtension(fileInfo.Name);

            cache.Remove(GetCacheKey(name));
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetChangeToken();

        return Task.CompletedTask;
    }
}