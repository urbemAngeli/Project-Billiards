using Code.BilliardEquipment.Balls;
using Code.BilliardEquipment.Pyramid;
using Code.BilliardEquipment.Table;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code
{
    public class Player : IService
    {

        private readonly PoolTable _table;
        private readonly BallCollection _ballCollection;
        private readonly BallPyramid _ballPyramid;


        public Player(PoolTable table, BallCollection ballCollection, BallPyramid ballPyramid)
        {
            _table = table;
            _ballCollection = ballCollection;
            _ballPyramid = ballPyramid;
        }
        
        public void PutCueBall() => 
            _ballCollection.CueBall.Put(_table.HeadSpot.position, Quaternion.identity);

        public void PutBallPyramid() => 
            _ballPyramid.Put(_table.FootSpot.position);
    }
}