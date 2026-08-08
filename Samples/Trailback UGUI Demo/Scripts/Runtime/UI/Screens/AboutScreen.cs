using UnityEngine;

namespace ModularForge.Trailback.Demo.UI
{
    public class AboutScreen : UIBase
    {
        public void OpenHome()
        {
            DemoNavigationController.Instance.OpenRootScreen();
        }
        
        public void OpenDocumentation()
        {
            Application.OpenURL("https://github.com/akpardeshi/Trailback");
        }

        protected override void OnShown()
        {
            base.OnShown();
            Debug.Log($"About screen on show");
        }
    }
}