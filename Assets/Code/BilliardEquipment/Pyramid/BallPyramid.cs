using System.Collections.Generic;
using System.Linq;
using Code.BilliardEquipment.Balls;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.BilliardEquipment.Pyramid
{
    public class BallPyramid : MonoBehaviour, IService
    {
        
        [SerializeField]
        private PyramidPosition[] _positions;
        private BallCollection _ballCollection;


        [ContextMenu("Collect")]
        private void OnValidate() => 
            _positions = GetComponentsInChildren<PyramidPosition>();

        public void Construct(BallCollection ballCollection) => 
            _ballCollection = ballCollection;

        public void Put(Vector3 at)
        {
            ArrangePyramid(at);
            ArrangeBalls();
        }

        private void ArrangePyramid(Vector3 at) => 
            transform.position = at;

        private void ArrangeBalls()
        {
            List<Ball> ballsBuffer = _ballCollection.PyramidBalls.ToList();
            
            Ball blackBall = _ballCollection.GetBlackBall();
            ballsBuffer.Remove(blackBall);
            
            for (int i = 0; i < _positions.Length; i++)
            {
                if (ballsBuffer.Count == 0)
                    break;

                if (IsBlackBallPosition(i, blackBall))
                {
                    UpdatePosition(blackBall, i);
                    continue;
                }

                int selectedBall = Random.Range(0, ballsBuffer.Count);
                UpdatePosition(ballsBuffer[selectedBall], i);
                ballsBuffer.RemoveAt(selectedBall);
            }
        }

        private bool IsBlackBallPosition(int positionIndex, Ball blackBall) =>
             _positions[positionIndex].Number == 5 && blackBall != null;

        private void UpdatePosition(Ball ball, int positionIndex) => 
            ball.Put(_positions[positionIndex].transform.position, Quaternion.Euler(-90, 0, 180));
    }
}