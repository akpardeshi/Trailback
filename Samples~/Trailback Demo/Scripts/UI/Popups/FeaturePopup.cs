using System.Linq;
using TMPro;
using ModularForge.Trailback.Demo.Features;
using UnityEngine;
using UnityEngine.UI;

namespace ModularForge.Trailback.Demo.UI
{
    public class FeaturePopup : UIBase
    {
        [System.Serializable]
        private struct DeactivationComponents
        {
            public string groupName;
            public GameObject[] gameObjects;
        }

        [SerializeField] private TextMeshProUGUI headingText;

        [SerializeField] private TextMeshProUGUI shortDescriptionText;

        [SerializeField] private TextMeshProUGUI descriptionText;

        [SerializeField] private TextMeshProUGUI counterText;

        [SerializeField] private DeactivationComponents[] deactivationComponents;

        private FeatureDemoData _featureData;

        public void SetFeatureData(FeatureDemoData featureData)
        {
            _featureData = featureData;

            InitializeFeatureData();

            ManageVideoGroup();
            ManageScreenshotGroup();

            SetInitialScreenshot();
            
            Invoke(nameof(UpdateLayout), 0.01f);
        }

        private void InitializeFeatureData()
        {
            headingText.text = _featureData.FeatureName;
            shortDescriptionText.text = _featureData.ShortDescription;
            descriptionText.text = _featureData.FullDescription;
        }

        private bool HasVideo => !string.IsNullOrEmpty(_featureData?.VideoUrl);

        private bool HasScreenShot => _featureData != null &&
                                      _featureData.Screenshots != null &&
                                      _featureData.Screenshots.Length > 0;

        private void ManageVideoGroup()
        {
            ManageGroup("Video", HasVideo);
        }

        private void ManageGroup(string groupName, bool active)
        {
            GameObject[] gameObjects = deactivationComponents.FirstOrDefault(x => x.groupName == groupName).gameObjects;

            foreach (GameObject go in gameObjects)
            {
                go.SetActive(active);
            }
        }

        private void ManageScreenshotGroup()
        {
            ManageGroup("Screenshot", HasScreenShot);
        }

        public void OpenDocuments()
        {
            Application.OpenURL(_featureData?.DocumentationUrl);
        }

        public void OpenVideo()
        {
            Application.OpenURL(_featureData.VideoUrl);
        }

        public void Close()
        {
            DemoNavigationController.Instance.HidePopup(DemoPopupType.Feature);
        }

        public void ShowPreviousScreenshot()
        {
            if (_featureData == null)
            {
                return;
            }

            var screenshots = _featureData.Screenshots;

            if (screenshots == null || screenshots.Length == 0)
            {
                return;
            }

            _currentScreenshotIndex--;

            if (_currentScreenshotIndex < 0)
            {
                _currentScreenshotIndex = screenshots.Length - 1;
            }

            RefreshScreenshot();
        }

        public void ShowNextScreenshot()
        {
            if (_featureData == null)
            {
                return;
            }

            var screenshots = _featureData.Screenshots;

            if (screenshots == null || screenshots.Length == 0)
            {
                return;
            }

            _currentScreenshotIndex++;

            if (_currentScreenshotIndex >= screenshots.Length)
            {
                _currentScreenshotIndex = 0;
            }

            RefreshScreenshot();
        }

        private int _currentScreenshotIndex;

        [SerializeField] private Image screenshotImage;

        [SerializeField] private GameObject screenshotContainer;

        private void SetInitialScreenshot()
        {
            _currentScreenshotIndex = 0;

            RefreshScreenshot();
        }

        private void RefreshScreenshot()
        {
            var screenshots = _featureData.Screenshots;

            if (screenshots == null || screenshots.Length == 0)
            {
                screenshotContainer.SetActive(false);

                return;
            }

            screenshotContainer.SetActive(true);

            screenshotImage.sprite = screenshots[_currentScreenshotIndex];

            counterText.text = $"{_currentScreenshotIndex + 1} / {screenshots.Length}";
        }
        
        [SerializeField]
        private ContentSizeFitter innerFitter; 
        [SerializeField]
        private ContentSizeFitter outerFitter; 

        private void UpdateLayout()
        {
            if (innerFitter != null)
            {
                innerFitter.SetLayoutHorizontal();
                innerFitter.SetLayoutVertical();
            }

            if (outerFitter != null)
            {
                outerFitter.SetLayoutHorizontal();
                outerFitter.SetLayoutVertical();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(innerFitter.GetComponent<RectTransform>());
            LayoutRebuilder.ForceRebuildLayoutImmediate(outerFitter.GetComponent<RectTransform>());
        }
    }
}