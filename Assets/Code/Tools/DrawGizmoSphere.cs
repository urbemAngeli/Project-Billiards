#if UNITY_EDITOR

using UnityEngine;

namespace Code.Tools
{
    public class DrawGizmoSphere : MonoBehaviour
    {

        [SerializeField]
        private Color _color = Color.yellow;

        [SerializeField]
        private float _radius = 1;

        private void OnDrawGizmos()
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
            Gizmos.color = oldColor;
        }
    }
}

#endif