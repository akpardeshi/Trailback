using ModularForge.Trailback.Demo.UI;
using UnityEngine.Serialization;

namespace ModularForge.Trailback.Demo
{
    [System.Serializable]
    public struct DemoPopupEntry
    {
        [FormerlySerializedAs("type")] public DemoPopupType Type;
        [FormerlySerializedAs("popup")] public UIBase Popup;
    }
}