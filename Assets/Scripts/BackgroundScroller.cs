using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class BackgroundScroller : MonoBehaviour
    {
        private static readonly int PROPERTY_POSITION = Shader.PropertyToID("_Position");
        
        [SerializeField]
        private Image targetImage;
        [SerializeField]
        private Vector2 speed;

        private Vector2 currentValue;
        private Material materialInstance;
        
        private void Start()
        {
            materialInstance = new(targetImage.material);
            targetImage.material = materialInstance;
            materialInstance.name += " BackgroundScroller";
        }

        private void OnDestroy()
        {
            if (materialInstance != null)
            {
                Destroy(materialInstance);
                materialInstance = null;
            }
        }

        private void Update()
        {
            currentValue += speed * Time.deltaTime;
            materialInstance.SetVector(PROPERTY_POSITION, currentValue);
        }
    }
}