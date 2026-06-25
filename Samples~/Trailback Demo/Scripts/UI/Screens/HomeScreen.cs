namespace ModularForge.Trailback.Demo.UI
{
    public class HomeScreen : UIBase
    {
        public void OpenFeaturesScreen()
        {
            DemoNavigationController.Instance.ShowScreen(DemoScreenType.Features);
        }
        
        public void OpenAboutScreen()
        {
            DemoNavigationController.Instance.ShowScreen(DemoScreenType.About);
        }
        
        public void OpenInfoPopup()
        {
            DemoNavigationController.Instance.ShowPopup(DemoPopupType.Info);
        }
        
        public void OpenLockedPopup()
        {
            DemoNavigationController.Instance.ShowPopup(DemoPopupType.Locked);
        }
    }
}