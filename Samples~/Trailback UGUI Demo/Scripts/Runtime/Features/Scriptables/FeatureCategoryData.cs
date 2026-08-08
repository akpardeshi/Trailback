using System.Collections.Generic;
using UnityEngine;

namespace ModularForge.Trailback.Demo.Features
{
    [CreateAssetMenu(fileName = "FeatureCategory", menuName = "Trailback/Demo/Feature Category")]
    public class FeatureCategoryData : ScriptableObject
    {
        [Header("Category Info")]
        [SerializeField]
        private string categoryName;

        public string CategoryName => categoryName;
        
        [SerializeField]
        [TextArea(2, 5)]
        private string description;

        public string Description => description;
        
        [Header("Features")]
        [SerializeField]
        private List<FeatureDemoData> features = new();

        public IReadOnlyList<FeatureDemoData> Features => features;
    }
}