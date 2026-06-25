using UnityEngine;

namespace ModularForge.Trailback.Demo
{
    public class DemoBackInput : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                ModularForge.Trailback.Core.Trailback.Back();
            }
        }
    }
}