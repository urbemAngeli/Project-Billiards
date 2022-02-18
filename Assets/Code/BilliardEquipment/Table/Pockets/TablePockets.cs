using System;
using Code.BilliardEquipment.Balls;
using Code.Extensions;
using Code.Other;
using UnityEngine;

namespace Code.BilliardEquipment.Table.Pockets
{
    public class TablePockets : MonoBehaviour
    {
        public event Action<Ball> onBallFell;
        

        [SerializeField]
        private BallCollection _ballCollection;

        private ObserverTrigger[] _pockets;
        private LayerMask _targetMak;


        private void Awake()
        {
            _targetMak = LayerMask.NameToLayer("Ball");
            _pockets = GetComponentsInChildren<ObserverTrigger>();

            for (int i = 0; i < _pockets.Length; i++) 
                _pockets[i].onEnter += OnBallFell;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _pockets.Length; i++) 
                _pockets[i].onEnter -= OnBallFell;
        }

        private void OnBallFell(Collider other)
        {
            if (other.gameObject.IsInLayerMask(_targetMak))
            {
                Ball foundBall = _ballCollection.FindBallByCollider(other);
                onBallFell?.Invoke(foundBall);
            }
        }
    }
}