﻿using Drifter.Scrapers;
using Drifter.Worker.DI;
using Microsoft.Practices.Unity;

namespace Drifter.Worker
{
    internal static class UnityConfig
    {
        internal static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            // Log4net
            container.AddNewExtension<UnityBuildTrackingExtension>()
                     .AddNewExtension<UnityLogCreationExtension>();

            container.RegisterType<IScrapePosts, WebPostScraper>();

            return container;
        }
    }
}
