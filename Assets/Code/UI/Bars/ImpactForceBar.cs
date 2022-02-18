using Code.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Bars
{
    public class ImpactForceBar : MonoBehaviour
    {
        [SerializeField]
        private Image _scale;

        private AimControl _aimControl;
        

        public void Construct(AimControl aimControl)
        {
            _aimControl = aimControl;
            
            _aimControl.onBegin += UpdateScale;
            _aimControl.onMoved += UpdateScale;
            _aimControl.onEnded += Hide;
        }

        private void Awake() => 
            Hide();

        public void Hide()
        {
            if(gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        private void UpdateScale()
        {
            Show();
            
            _scale.fillAmount = _aimControl.Delta;
        }

        private void Show()
        {
            if(!gameObject.activeSelf) 
                gameObject.SetActive(true);
        }
    }
}