using ModularForge.Trailback.Demo.UI;
using UnityEngine.Serialization;

namespace ModularForge.Trailback.Demo
{
    [System.Serializable]
    public struct DemoScreenEntry
    {
        [FormerlySerializedAs("type")] public DemoScreenType Type;
        [FormerlySerializedAs("screen")] public UIBase Screen;
    }
}