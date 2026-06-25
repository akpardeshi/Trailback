namespace ModularForge.Trailback.Demo.UI
{
    public class ConfirmationPopup: UIBase
    {
        public void OnExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }

        public void OnStayClick()
        {
            DemoNavigationController.Instance.HidePopup(DemoPopupType.Confirmation);
        }
    }
}