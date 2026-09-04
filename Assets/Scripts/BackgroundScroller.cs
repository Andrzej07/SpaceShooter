using UnityEngine;

namespace SpaceShooter
{
    public class BackgroundScroller : MonoBehaviour
    {
        private static readonly int PROPERTY_POSITION = Shader.PropertyToID("_Position");
        
        [SerializeField]
        private Skybox target;
        [SerializeField]
        private Vector2 speed;

        private Vector2 currentValue;
        private Material materialInstance;

        private void Start()
        {
            materialInstance = new(target.material);
            materialInstance.name += " BackgroundScroller";
            target.material = materialInstance;
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