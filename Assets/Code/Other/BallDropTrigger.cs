using System;
using Code.BilliardEquipment.Balls;
using Code.Extensions;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Other
{
    public class BallDropTrigger : MonoBehaviour, IService
    {
        public event Action<Ball> onBallFell;

        [SerializeField]
        private bool _isGizmos;

        [SerializeField]
        private BallCollection _ballCollection;
        
        private LayerMask _targetMak;

        private void Awake() => 
            _targetMak = LayerMask.NameToLayer("Ball");

        public void Construct(BallCollection ballCollection) => 
            _ballCollection = ballCollection;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.IsInLayerMask(_targetMak))
            {
                Ball foundBall = _ballCollection.FindBallByCollider(other);
                onBallFell?.Invoke(foundBall);
            }
        }
        
        private void OnDrawGizmos()
        {
            if (!_isGizmos)
                return;
            
            Color oldColor = Gizmos.color;
            Gizmos.color = new Color(Color.red.r,Color.red.g, Color.red.b, 0.5f);
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.color = oldColor;
        }
    }
}