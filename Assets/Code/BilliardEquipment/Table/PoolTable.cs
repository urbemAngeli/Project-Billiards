using Code.BilliardEquipment.Table.Pockets;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.BilliardEquipment.Table
{
    public class PoolTable : MonoBehaviour, IService
    {
        [SerializeField]
        private Transform _headSpot;
        
        [SerializeField]
        private Transform _footSpot;
        
        [SerializeField]
        private TablePockets _pockets;

        
        public Transform HeadSpot => _headSpot;
        public Transform FootSpot => _footSpot;
        public TablePockets Pockets => _pockets;
    }
}