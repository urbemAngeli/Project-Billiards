using UnityEngine;

namespace Code.BilliardEquipment.Balls
{
    [RequireComponent(typeof(Collider))]
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private byte _number;

        [SerializeField]
        private Collider _collider;
        
        [SerializeField]
        private Rigidbody _rigidbody;


        public byte Number => _number;
        public Collider Collider => _collider;
        public Rigidbody Rigidbody => _rigidbody;

        
        [ContextMenu("Validate")]
        private void OnValidate()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
            
            if (_number < 0 || _number > 15)
                _number = 0;
        }

        public void Put(Vector3 at, Quaternion rotation)
        {
            Activate();
            
            transform.position = at;
            transform.rotation = rotation;
            
            ResetPhysics();
        }

        public void Deactivate()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        public void ResetPhysics()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        private void Activate()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
    }
}