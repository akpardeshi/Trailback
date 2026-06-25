using System;
using ModularForge.Trailback.Core;
using ModularForge.Trailback.Integration;

namespace Trailback.Demo.Scripts.Integration
{
    public sealed class DemoTrailbackBridge : TrailbackIntegrationBridge
    {
        public override event Action RootReached
        {
            add =>
                ModularForge.Trailback.Core.Trailback.OnNavigationRootReached += value;

            remove =>
                ModularForge.Trailback.Core.Trailback.OnNavigationRootReached -= value;
        }

        public override void Show(IBackNavigable element)
        {
            ModularForge.Trailback.Core.Trailback.ReportShown(element);
        }

        public override void Hide(IBackNavigable element)
        {
            ModularForge.Trailback.Core.Trailback.ReportHidden(element);
        }

        public override bool Back()
        {
            return ModularForge.Trailback.Core.Trailback.Back();
        }

        public override void ResetHistory()
        {
            ModularForge.Trailback.Core.Trailback.ResetHistory();
        }
    }
}