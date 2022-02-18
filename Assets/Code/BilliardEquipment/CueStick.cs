using Code.GameLogic;
using Code.Infrastructure.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.BilliardEquipment
{
    public class CueStick : MonoBehaviour, IService
    {
        [SerializeField]
        private Transform _root;
        
        [SerializeField]
        private Transform _view;
        
        private AimControl _aimControl;
        private GameConfig _config;


        public Transform Root => _root;
        public Transform View => _view;


        public void Construct(AimControl aimControl, GameConfig config)
        {
            _aimControl = aimControl;
            _config = config;
            
            _aimControl.onBegin += UpdateStick;
            _aimControl.onMoved += UpdateStick;
            _aimControl.onEnded += Deactivate;
        }
        
        private void Awake() => 
            Deactivate();

        private void Activate()
        {
            if(!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        private void Deactivate()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        private void UpdateStick()
        {
            Activate();
            
            transform.position = _aimControl.Pivot;
            _root.rotation = Quaternion.LookRotation(_aimControl.Direction, Vector3.up);
        
            float offset = Mathf.Clamp(_aimControl.Delta, 0.1f, _config.MaxOffsetCue);
            _view.position = _aimControl.Pivot - _aimControl.Direction * offset;
        }
    }
}