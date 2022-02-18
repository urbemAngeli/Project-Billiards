using UnityEngine;

namespace Code.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        public TouchPhase Phase
        {
            get
            {
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    return TouchPhase.Began;
            
                if(UnityEngine.Input.GetMouseButton(0))
                    return TouchPhase.Moved;

                if (UnityEngine.Input.GetMouseButtonUp(0))
                    return TouchPhase.Ended;

                return TouchPhase.Canceled;
            }
        }
    
        public Vector2 ScreenPosition => UnityEngine.Input.mousePosition;
    }
}