using ModularForge.Trailback.Demo.UI;
using ModularForge.Trailback.Core;

namespace ModularForge.Trailback.Demo
{
    public sealed class DemoBackNavigationHandler : IBackNavigationHandler
    {
        public void NavigateBackTo(BackContext context)
        {
            if (context.Current is UIBase currentUI)
            {
                currentUI.Hide();
            }

            if (context.Previous is UIBase previousUI)
            {
                previousUI.Show();
            }
        }
    }
}