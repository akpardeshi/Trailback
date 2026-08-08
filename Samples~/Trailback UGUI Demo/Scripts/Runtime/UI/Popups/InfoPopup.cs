namespace ModularForge.Trailback.Demo.UI
{
    public class InfoPopup : UIBase
    {
        public void Close()
        {
            DemoNavigationController.Instance.HidePopup(DemoPopupType.Info);
        }

        public void OpenLockedPopup()
        {
            DemoNavigationController.Instance.ShowPopup(DemoPopupType.Locked);
        }
    }
}