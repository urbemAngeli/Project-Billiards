using System;
using Code.Infrastructure.Services;
using Code.Services.Input;
using Code.Services.Ticks;
using Code.StaticData;
using Code.Tools;
using UnityEngine;

namespace Code.GameLogic
{
    public class AimControl : IService, ITick
    {
        public event Action onBegin;
        public event Action onMoved;
        public event Action onEnded;
        
        
        private readonly IInputService _crossInput;
        private readonly TickProcessor _tickProcessor;
        private readonly Camera _camera;
        private readonly Transform _cueBall;
        private readonly GameConfig _config;

        private Vector3 _fingerPosition;
        private Vector3 _direction;
        private float _delta;
        private Vector3 _cueBallPosition;

        public Vector3 FingerPosition => _fingerPosition;
        public Vector3 Direction => _direction;
        public Vector3 Pivot => _cueBallPosition;
        public float Delta => _delta;

        
        public AimControl(
            IInputService crossInput, 
            TickProcessor tickProcessor,
            Camera camera, Transform cueBall, 
            GameConfig config)
        {
            _crossInput = crossInput;
            _tickProcessor = tickProcessor;
            _camera = camera;
            _cueBall = cueBall;
            _config = config;
        }

        public void StartAiming() => 
            _tickProcessor.Add(this);

        public void ProcessAiming()
        {
            if (_crossInput.Phase == TouchPhase.Began)
            {
                CalculateAim();
                onBegin?.Invoke();
            }
            else if(_crossInput.Phase == TouchPhase.Moved)
            {
                CalculateAim();
                onMoved?.Invoke();
            }
            else if(_crossInput.Phase == TouchPhase.Ended)
            {
                onEnded?.Invoke();
            }
        }
        
        private void CalculateAim()
        {
            _cueBallPosition = MathUtils.RoundHeight(_cueBall.position);
            _fingerPosition = MathUtils.RoundHeight(GetWorldFingerPosition());
            _direction = GetLookDirection();
            _delta = GetDelta();
        }

        public void StopAiming()
        {
            onEnded?.Invoke();
            _tickProcessor.Remove(this);
        }

        void ITick.Tick() => 
            ProcessAiming();
        
        private Vector3 GetLookDirection() => 
            (_cueBallPosition - _fingerPosition).normalized;

        private float GetDelta() =>
            Mathf.InverseLerp(0, _config.MaxOffsetFinger, Vector3.Distance(_fingerPosition, _cueBallPosition));

        private Vector3 GetWorldFingerPosition()
        {
            Vector2 screenPosition = _crossInput.ScreenPosition;
        
            _fingerPosition = _camera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, _camera.transform.position.y));

            _fingerPosition.y = 0.05f;

            return _fingerPosition;
        }
    }
}