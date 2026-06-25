using System.Collections.Generic;
using ModularForge.Trailback.Input;
using ModularForge.Trailback.Demo.UI;
using Trailback.Demo.Scripts.Integration;
using UnityEngine;

namespace ModularForge.Trailback.Demo
{
    public class DemoNavigationController : MonoBehaviour
    {
        public static DemoNavigationController Instance;

        private readonly DemoTrailbackBridge _bridge = new();

        [SerializeField] private DemoScreenEntry[] screens;

        [SerializeField] private DemoPopupEntry[] popups;

        [SerializeField] private DemoScreenType startupScreen;

        private Dictionary<DemoScreenType, UIBase> _screenLookup;
        private Dictionary<DemoPopupType, UIBase> _popupLookup;
        
        [SerializeField] private BackInputSource backInputSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            BuildScreenLookup();
            BuildPopupLookup();

            ModularForge.Trailback.Core.Trailback.SetNavigationHandler(
                new DemoBackNavigationHandler());

            CacheComponents();
        }

        private void CacheComponents()
        {
            if (backInputSource)
            {
                return;
            }

            backInputSource = FindAnyObjectByType<BackInputSource>();
        }

        private void OnEnable()
        {
            if (backInputSource == null) return;

            backInputSource.BackRequested += HandleBackRequested;
        }

        private void OnDisable()
        {
            if (backInputSource == null) return;

            backInputSource.BackRequested -= HandleBackRequested;
        }

        private void Start()
        {
            HideAllScreens();
            HideAllPopups();

            ShowScreen(startupScreen);
        }

        private void HideAllScreens()
        {
            foreach (var screen in screens)
            {
                screen.Screen.Hide();
            }
        }

        private void HideAllPopups()
        {
            foreach (var popup in popups)
            {
                popup.Popup.Hide();
            }
        }

        public void ShowScreen(DemoScreenType screenType)
        {
            UIBase targetScreen = GetScreen(screenType);

            if (targetScreen == null)
            {
                Debug.LogError($"Screen not found: {screenType}");
                return;
            }

            HideAllScreens();

            targetScreen.Show();

            _bridge.Show(targetScreen);
        }

        public void ShowPopup(DemoPopupType popupType)
        {
            UIBase targetPopup = GetPopup(popupType);

            if (targetPopup == null)
            {
                Debug.LogError($"Popup not found: {popupType}");
                return;
            }

            targetPopup.Show();

            _bridge.Show(targetPopup);
        }

        public void HidePopup(DemoPopupType popupType)
        {
            UIBase targetPopup = GetPopup(popupType);

            if (targetPopup == null)
            {
                Debug.LogError($"Popup not found: {popupType}");
                return;
            }

            targetPopup.Hide();

            _bridge.Hide(targetPopup);
        }

        private void BuildScreenLookup()
        {
            _screenLookup = new Dictionary<DemoScreenType, UIBase>();

            foreach (var entry in screens)
            {
                _screenLookup[entry.Type] = entry.Screen;
            }
        }

        private void BuildPopupLookup()
        {
            _popupLookup = new Dictionary<DemoPopupType, UIBase>();

            foreach (var entry in popups)
            {
                _popupLookup[entry.Type] = entry.Popup;
            }
        }

        private UIBase GetScreen(DemoScreenType screenType)
        {
            if (_screenLookup.TryGetValue(screenType, out var screen))
            {
                return screen;
            }

            Debug.LogError($"Screen not found: {screenType}");

            return null;
        }

        public UIBase GetPopup(DemoPopupType popupType)
        {
            if (_popupLookup.TryGetValue(popupType, out var popup))
            {
                return popup;
            }

            Debug.LogError($"Popup not found: {popupType}");

            return null;
        }

        public void OnNavigationRootReached()
        {
            ShowPopup(DemoPopupType.Confirmation);
        }

        public void OpenRootScreen()
        {
            ModularForge.Trailback.Core.Trailback.ResetHistory();

            ShowScreen(startupScreen);
        }

        private void HandleBackRequested()
        {
            _bridge.Back();
        }
    }
}