using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ModularForge.Trailback.Demo.Features
{
    public class FeatureCategoryCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headingText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        [SerializeField] private Transform featuresParent;

        private FeatureCategoryData _featureCategoryData;
        
        [SerializeField] private FeatureCard featureCardPrefab;
        
        private readonly List<FeatureCard> featureCards = new ();

        [SerializeField]
        private Transform featureParent;
        
        public void SetCategory(FeatureCategoryData featureCategoryData)
        {
            _featureCategoryData = featureCategoryData;

            InitializeFeatureCategoryCard();
        }

        private void InitializeFeatureCategoryCard()
        {
            name = _featureCategoryData.CategoryName;
            
            headingText.text = _featureCategoryData.CategoryName;
            descriptionText.text = _featureCategoryData.Description;

            InitializeFeatureCards();
        }

        private void InitializeFeatureCards()
        {
            foreach (var feature in _featureCategoryData.Features)
            {
                FeatureCard featureCard = Instantiate(featureCardPrefab, featureParent);
                featureCard.SetCardData(feature);
                featureCards.Add(featureCard);
            }
        }
    }
}