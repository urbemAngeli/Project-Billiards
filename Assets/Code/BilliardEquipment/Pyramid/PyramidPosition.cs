using UnityEngine;

namespace Code.BilliardEquipment.Pyramid
{
    public class PyramidPosition : MonoBehaviour
    {
        [SerializeField]
        private int number;


        public int Number => number;


        // private void OnDrawGizmos()
        // {
        //     Color oldColor = Gizmos.color;
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawSphere(transform.position, 0.03f);
        //     Gizmos.color = oldColor;
        // }
    }
}