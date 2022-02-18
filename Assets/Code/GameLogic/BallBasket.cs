using System.Collections.Generic;
using Code.BilliardEquipment.Balls;
using Code.Infrastructure.Services;

namespace Code.GameLogic
{
    public class BallBasket : IService
    {

        private List<Ball> _collectedBalls = new List<Ball>();

        public bool TryAdd(Ball ball)
        {
            ball.Deactivate();
            _collectedBalls.Add(ball);

            return !IsOverflow();
        }

        public void Cleanup() => 
            _collectedBalls.Clear();

        private bool IsOverflow() => 
            _collectedBalls.Count == 15;
    }
}