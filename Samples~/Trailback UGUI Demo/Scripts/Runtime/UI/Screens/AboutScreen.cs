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
            Debug.Log($"Open Documentation");
            Application.OpenURL("https://github.com/akpardeshi/Trailback");
        }
    }
}