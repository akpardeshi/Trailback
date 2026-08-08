using ModularForge.Trailback.Demo.UI;
using ModularForge.Trailback.Core;

namespace ModularForge.Trailback.Demo
{
    public class DemoBackNavigationHandler : IBackNavigationHandler
    {
        public void NavigateBackTo(BackContext context)
        {
            if (context.Current is UIBase currentUI)
            {
                currentUI.Hide();
            }

            if (context.BackTarget is UIBase backTargetUI)
            {
                backTargetUI.Show();
            }
        }
    }
}