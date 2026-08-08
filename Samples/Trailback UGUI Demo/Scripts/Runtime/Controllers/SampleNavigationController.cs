using ModularForge.Trailback.Core;
using ModularForge.Trailback.Demo.UI;
using UnityEngine;

namespace ModularForge.Trailback.Demo
{
    /// <summary>
    /// Simple navigation controller used by the Quick Start sample.
    ///
    /// This implementation keeps the navigation flow explicit and easy to follow.
    /// For larger projects, see the Complete UGUI Demo and the Developer Guide.
    /// </summary>
    public class SampleNavigationController : MonoBehaviour
    {

        [SerializeField] private HomeScreen homeScreen;
        [SerializeField] private AboutScreen aboutScreen;
        
        [SerializeField] private InfoPopup infoPopup;
        
        [SerializeField] private BackInputSource backInputSource;
        
        private void Awake()
        {
            // Reset the navigation history to start new session
            TrailbackFacade.ResetHistory();

            TrailbackFacade.SetNavigationHandler(new DemoBackNavigationHandler());

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
            homeScreen.Hide();
            aboutScreen.Hide();
            infoPopup.Hide();
            
            ShowHomeScreen();
        }

        public void ShowHomeScreen()
        {
            homeScreen.Show();
            TrailbackFacade.ReportShown(homeScreen);
        }

        private void HideHomeScreen()
        {
            // IMPORTANT: Do NOT call TrailbackFacade.ReportHidden(homeScreen) here.
            // To keep Home in history so pressing Back returns to it.
            // Only call TrailbackFacade.ReportHidden(homeScreen); if you want to completely remove 
            // an element from history.
            
            homeScreen.Hide();
        }

        public void ShowAboutScreen()
        {
            HideHomeScreen();
            
            aboutScreen.Show();
            TrailbackFacade.ReportShown(aboutScreen);
        }

        public void HideAboutScreen()
        {
            aboutScreen.Hide();
            TrailbackFacade.ReportHidden(aboutScreen);
        }
        
        public void ShowInfoPopup()
        {
            infoPopup.Show();
            TrailbackFacade.ReportShown(infoPopup);
        }

        public void HideInfoPopup()
        {
            infoPopup.Hide();
            TrailbackFacade.ReportHidden(infoPopup);
        }
        
        private void HandleBackRequested()
        {
            TrailbackFacade.Back();
        }
        
        public void OpenRootScreen()
        {
            aboutScreen.Hide();
            infoPopup.Hide();
            
            // Clear the history when opening Root Screen
            // In this demo HomeScreen is the Root Screen 
            TrailbackFacade.ResetHistory();

            ShowHomeScreen();
        }
    }
}