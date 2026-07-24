using UnityEngine;

namespace ModularForge.Trailback.Demo.Features
{
    [CreateAssetMenu(fileName = "FeatureDemoData", menuName = "Trailback/Demo/Feature Demo Data")]
    public class FeatureDemoData : ScriptableObject
    {
        [Header("Basic Info")]
        [SerializeField]
        private string featureName;
        
        public string FeatureName => featureName;

        [SerializeField]
        [TextArea(2, 5)]
        private string shortDescription;
        
        public string ShortDescription => shortDescription;

        [SerializeField]
        [TextArea(5, 15)]
        private string fullDescription;
        
        public string FullDescription => fullDescription;
        
        [Header("Media")]
        [SerializeField]
        private Sprite [] screenshots;
        
        public Sprite [] Screenshots => screenshots;
        
        [Header("URL")]
        [SerializeField]
        private string videoUrl;
        
        public string VideoUrl => videoUrl;

        [SerializeField]
        private string documentationUrl;
        
        public string DocumentationUrl => documentationUrl;
        
        [Header("Organization")]
        [SerializeField]
        private FeatureStatus featureStatus;
        
        public FeatureStatus FeatureStatus => featureStatus;

        [SerializeField] 
        private FeatureVersion featureVersion;
        
        public FeatureVersion FeatureVersion => featureVersion;

        [SerializeField]
        private int featureImportance;
        
        public int FeatureImportance => featureImportance;
    }
}