using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ModularForge.Trailback.Demo.Features;

namespace ModularForge.Trailback.Demo.UI
{
    public class FeaturesScreen: UIBase
    {
        [SerializeField]
        private FeatureCategoryData [] featureCategoryData;
        
        [SerializeField]
        private FeatureCategoryCard featureCategoryCardPrefab;
        
        private readonly List<FeatureCategoryCard> _featureCategoryCards = new ();

        [SerializeField]
        private Transform categoryParent;

        public override void Show()
        {
            base.Show();
            
            InitializeCategories();
        }

        public override void Hide()
        {
            base.Hide();

            DestroyCategoryCards();
        } 

        private void InitializeCategories()
        {
            foreach (var featureCategory in featureCategoryData)
            {
                FeatureCategoryCard category = Instantiate(featureCategoryCardPrefab, categoryParent);
                category.SetCategory(featureCategory);
                _featureCategoryCards.Add(category);
            }
            
            Invoke(nameof(UpdateLayout), 0.01f);
        }

        private void DestroyCategoryCards()
        {
            foreach (var categoryCard in _featureCategoryCards)
            {
                Destroy(categoryCard.gameObject);
            }
            
            _featureCategoryCards.Clear();
        }

        public void OpenHome()
        {
            DemoNavigationController.Instance
                .OpenRootScreen();
        }

        public void OpenDocumentation()
        {
            Debug.Log($"Open Documentation");
            Application.OpenURL("https://github.com/akpardeshi");
        }

        public void OpenGitHub()
        {
            Debug.Log($"Open GitHub");
            Application.OpenURL("https://github.com/akpardeshi");
        }

        public void OpenAboutScreen()
        {
            Debug.Log($"Open About");
            DemoNavigationController.Instance
                .ShowScreen(DemoScreenType.About);
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