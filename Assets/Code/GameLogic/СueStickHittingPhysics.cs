using Code.BilliardEquipment.Balls;
using Code.Infrastructure.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.GameLogic
{
    public class СueStickHittingPhysics : IService
    {
        private readonly AimControl _aimControl;
        private readonly Ball _cueBall;
        private readonly GameConfig _config;
        

        public СueStickHittingPhysics(AimControl aimControl, Ball cueBall, GameConfig config)
        {
            _aimControl = aimControl;
            _cueBall = cueBall;
            _config = config;

            SubscribeUpdates();
        }

        public Vector3 CalculateForce() => 
            _aimControl.Direction * Mathf.Lerp(0, _config.MaxForce, _aimControl.Delta);

        private void Hit() => 
            _cueBall.Rigidbody.AddForce(CalculateForce(), ForceMode.Impulse);

        // private float CalculateForceDelta()
        // {
        //     float distance = Vector3.Distance(_aimControl.FingerPosition, _cueBall.transform.position);
        //     float distanceDelta = Mathf.InverseLerp(0, _config.MaxOffsetCue, distance);
        //     return Mathf.Lerp(0, _config.MaxForce, distanceDelta);
        // }

        private void SubscribeUpdates() => 
            _aimControl.onEnded += Hit;
    }
}