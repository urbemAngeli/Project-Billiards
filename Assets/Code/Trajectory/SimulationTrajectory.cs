using Code.BilliardEquipment.Balls;
using Code.GameLogic;
using Code.Infrastructure.Services;
using UnityEditor;
using UnityEngine;

namespace Code.Trajectory
{
    public class SimulationTrajectory : IService
    {
        private readonly AimControl _aimControl;
        private readonly BallCollection _ballCollection;
        private readonly TrajectoryDrawing _trajectoryDrawing;

        private readonly Quaternion rotatation90 = Quaternion.AngleAxis(-90, Vector3.up);

        private RaycastHit[] _hitsBuffer = new RaycastHit[2];
        private int _ballMask;

        public SimulationTrajectory(AimControl aimControl, BallCollection ballCollection, TrajectoryDrawing trajectoryDrawing)
        {
            _aimControl = aimControl;
            _ballCollection = ballCollection;
            _trajectoryDrawing = trajectoryDrawing;

            _aimControl.onBegin += Simulate;
            _aimControl.onMoved += Simulate;
            _aimControl.onEnded += Cleanup;

            _ballMask = 1 << LayerMask.NameToLayer("Ball");
        }

        private void Simulate()
        {
            Vector3 fromSpot = RoundPosition(_ballCollection.CueBall.transform.position);
            
            Vector3 cueBallHitPosition;
            RaycastHit hit;

            if (TryFindCrossBall(fromSpot, _aimControl.Direction, out hit))
            {
                cueBallHitPosition = hit.point + (.03f * hit.normal);
                
                _trajectoryDrawing.DrawTrajectoryHitting(fromSpot,cueBallHitPosition, rotatation90 * hit.normal);
                 _trajectoryDrawing.DrawTrajectoryReflection(hit.collider.transform.position, -1 * hit.normal);
            }
            else if (Physics.Raycast(fromSpot, _aimControl.Direction, out hit, 10))
            {
                Vector3 reflectDirection = Vector3.Reflect((hit.point - fromSpot).normalized, hit.normal);
                cueBallHitPosition = hit.point + (0.03f * hit.normal);
                
                _trajectoryDrawing.DrawTrajectoryHitting(fromSpot,cueBallHitPosition, reflectDirection);
                _trajectoryDrawing.ClearTrajectoryReflection();
            }
            // else
            //     _trajectoryDrawing.Cleanup();
        }
        
        private void Cleanup() => 
            _trajectoryDrawing.Cleanup();

        private bool TryFindCrossBall(Vector3 fromSpot, Vector3 direction, out RaycastHit hit)
        {
            //Debug.DrawRay(fromSpot, direction * 4, Color.magenta);
            
            int hitsCount = Physics.SphereCastNonAlloc(fromSpot, 0.03f, direction, _hitsBuffer, 20, _ballMask);

            for (int i = 0; i < hitsCount; i++)
            {
                if (_hitsBuffer[i].collider.transform == _ballCollection.CueBall.transform) continue;

                hit = _hitsBuffer[i];
                return true;
            }

            hit = default;
            return false;
        }

        private Vector3 RoundPosition(Vector3 position) => 
            new Vector3(position.x, 0.05f, position.z);
    }
}