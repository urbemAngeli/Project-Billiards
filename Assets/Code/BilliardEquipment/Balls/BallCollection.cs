using System;
using Code.Exceptions;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.BilliardEquipment.Balls
{
    public class BallCollection : MonoBehaviour, IService
    {
        
        [SerializeField]
        private Ball[] _balls;
        
        
        public Ball[] Balls => _balls;
        
        public Ball CueBall => _balls[0];
        
        public Ball[] PyramidBalls 
        {
            get
            {
                Ball[] ballsBuffer = new Ball[_balls.Length - 1];
                int j = 0;
                
                for (int i = 0; i < _balls.Length; i++)
                {
                    if(_balls[i].Number == 0)
                        continue;

                    ballsBuffer[j++] = _balls[i];
                }

                return ballsBuffer;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < _balls.Length; i++) 
                _balls[i].Deactivate();
        }

        public Ball FindBallByCollider(Collider targetCollider)
        {
            for (int i = 0; i < _balls.Length; i++)
            {
                if (_balls[i].Collider == targetCollider)
                    return _balls[i];
            }

            throw new AbsenceOfBallWithSuchCollider(
                $"The object of {targetCollider.name} is not registered among the balls!");
        }

        public Ball GetBlackBall()
        {
            byte ballNumber = 8;

            for (int i = 0; i < _balls.Length; i++)
            {
                if(_balls[i].Number == ballNumber)
                    return _balls[i];
            }

            throw new NullReferenceException($"Black ball not found!");
        }
    }
}