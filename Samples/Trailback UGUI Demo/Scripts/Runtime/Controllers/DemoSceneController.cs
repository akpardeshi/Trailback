using ModularForge.Trailback.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModularForge.Trailback.Demo
{
    public class DemoSceneController : MonoBehaviour
    {
        public void ReloadScene()
        {
            TrailbackFacade.ResetHistory();
            SceneManager.LoadScene("Trailback UGUI Demo");
        }
    }
}