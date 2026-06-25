using TMPro;
using ModularForge.Trailback.Demo.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ModularForge.Trailback.Demo.Features
{
    public class FeatureCard : MonoBehaviour
    {
        [Header("Text")] [SerializeField] private TextMeshProUGUI featureName;
        [SerializeField] private TextMeshProUGUI shortDescription;

        private FeatureDemoData _featureData;

        private Button _button;

        private void Awake()
        {
            CacheComponents();
        }

        private void CacheComponents()
        {
            if (!TryGetComponent(out _button))
            {
                _button = gameObject.AddComponent<Button>();
            }
        }

        private void SetButton()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => OnClick(_featureData));
        }

        private void OnClick(FeatureDemoData data)
        {
            FeaturePopup popup = DemoNavigationController.Instance.GetPopup(DemoPopupType.Feature) as FeaturePopup;

            if (!popup)
            {
                Debug.LogWarning($"The feature pop did not found", this);
                return;
            }

            popup.SetFeatureData(data);
            DemoNavigationController.Instance.ShowPopup(DemoPopupType.Feature);
        }

        public void SetCardData(FeatureDemoData featureData)
        {
            _featureData = featureData;

            SetButton();
            InitializeFeatureCard();
        }

        private void InitializeFeatureCard()
        {
            name = _featureData.FeatureName;

            featureName.text = _featureData.FeatureName;
            shortDescription.text = _featureData.ShortDescription;
        }
    }
}