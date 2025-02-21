using UnityEngine;

namespace egmp7.Game.Sensors
{
    public class ToggleSize : MonoBehaviour
    {
        [Header("Main Settings")]
        [SerializeField] Vector2 ExpandSize = new (1f,1f);

        [Header ("Debug Settings")]
        public Color Color1 = Color.white;
        public Color Color2 = Color.white;

        private CollisionSensor _collisionSensor;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _initLocalScale;

        private bool _isToggled = false;

        void Start()
        {
            _initLocalScale = transform.localScale;
        }

        void Update()
        {
            // when inside
            if (_collisionSensor.GetState())
            {
                _isToggled = !_isToggled; // Toggle the state

                if (_isToggled)
                {
                    _isToggled = !_isToggled;
                    // Expand
                    transform.localScale = ExpandSize;
                    _spriteRenderer.color = new Color(Color1.r, Color1.g, Color1.b, 0.3f);
                }
                
            }
            // when outside
            else
            {
                _isToggled = !_isToggled; // Toggle the state

                if (_isToggled)
                {
                    _isToggled = !_isToggled;
                    // Contract
                    transform.localScale = _initLocalScale;
                    _spriteRenderer.color = new Color(Color2.r, Color2.g, Color2.b, 0.3f);
                }
            }
        }

        private void Awake()
        {
            if (!TryGetComponent(out _collisionSensor))
            {
                ErrorManager.LogMissingComponent<CollisionSensor>(gameObject);
            }

            if (!TryGetComponent(out _spriteRenderer))
            {
                ErrorManager.LogMissingComponent<SpriteRenderer>(gameObject);
            }
        }
    }
}
