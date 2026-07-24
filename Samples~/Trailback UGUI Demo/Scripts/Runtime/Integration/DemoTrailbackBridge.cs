using System;
using ModularForge.Trailback.Core;

namespace ModularForge.Trailback.Demo
{
    public sealed class DemoTrailbackBridge : TrailbackIntegrationBridge
    {
        public override void InitializeSession()
        {
            TrailbackFacade.ResetHistory();
        }
        
        public override event Action OnNavigationRootReached
        {
            add =>
                TrailbackFacade.OnNavigationRootReached += value;

            remove =>
                TrailbackFacade.OnNavigationRootReached -= value;
        }

        public override void SetNavigationHandler(IBackNavigationHandler handler)
        {
            TrailbackFacade.SetNavigationHandler(handler);
        }

        public override void Show(IBackNavigable element)
        {
            TrailbackFacade.ReportShown(element);
        }

        public override void Hide(IBackNavigable element)
        {
            // ReportHidden removes the element from Trailback's history.
            // If you only want to visually hide without removing from history,
            // don't call this method.
            TrailbackFacade.ReportHidden(element);
        }

        public override bool Back()
        {
            return TrailbackFacade.Back();
        }

        public override void ResetHistory()
        {
            TrailbackFacade.ResetHistory();
        }
    }
}