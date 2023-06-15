using UnityEngine;

namespace Egliss
{
    [DisallowMultipleComponent]
    public class FanViewer : MonoBehaviour
    {
        [SerializeField, Range(0, 360.0f)]
        private float _fovX = 90.0f;
        [SerializeField, Range(0, 360.0f)]
        private float _fovY = 90.0f;
        [SerializeField, Range(0, 100)]
        private float _distance = 7.0f;
        [SerializeField]
        private Color _color = new Color(1.0f, 0.5f, 0.5f, 0.5f);

        
        [SerializeField, Range(1, 255), HideInInspector]
        private int _quality = 16;
        [HideInInspector]
        private bool _isIgnoreYFan = false;

        public float fovX { get { return _fovX; } }
        public float fovY { get { return _fovY; } }
        public float distance { get { return _distance; } }
        public Color color { get { return _color; } }
        public int quality { get { return _quality; } }
        public bool isIgnoreYFan { get { return _isIgnoreYFan; } }

    }

    
}
