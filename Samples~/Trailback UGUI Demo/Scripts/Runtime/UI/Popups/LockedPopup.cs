using ModularForge.Trailback.Core;
using UnityEngine;

namespace ModularForge.Trailback.Demo.UI
{
    public class LockedPopup : UIBase, IBackNavigationBlocker
    {
        [field: Header("Back Navigation Blocker")]
        [field: SerializeField]
        public BackNavigationMode BackNavigationMode { get; private set; } = BackNavigationMode.Block;

        public void ClosePopup()
        {
            DemoNavigationController.Instance.HidePopup(DemoPopupType.Locked);
        }
    }
}   