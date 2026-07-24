namespace ModularForge.Trailback.Demo.UI
{
    public class ConfirmationPopup: UIBase
    {
        public void OnYesClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }

        public void OnNoClick()
        {
            DemoNavigationController.Instance.HidePopup(DemoPopupType.Confirmation);
        }
    }
}