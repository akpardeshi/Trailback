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
        
        [SerializeField]
        private ContentSizeFitter innerFitter; 
        [SerializeField]
        private ContentSizeFitter outerFitter;

        protected override void OnShown()
        {
            InitializeCategories();
        }

        protected override void OnHidden()
        {
            DestroyCategoryCards();
        } 

        // Initialize the categories only after the screen becomes visible.
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

        // Destroy the categories only after the screen becomes hidden.
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
            DemoNavigationController.Instance.OpenRootScreen();
        }

        public void OpenDocumentation()
        {
            Application.OpenURL("https://github.com/akpardeshi/Trailback");
        }
        
        public void OpenAboutScreen()
        {
            DemoNavigationController.Instance.ShowScreen(DemoScreenType.About);
        }
        
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