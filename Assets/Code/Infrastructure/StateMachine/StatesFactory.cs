using System;
using Code.BilliardEquipment.Table;
using Code.GameLogic;
using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine
{
    public class StatesFactory
    {
        private readonly SceneData _sceneData;
        private readonly Camera _camera;
        private readonly AllServices _services;
        private readonly Transform _gameRoot;
        private readonly GameConfig _config;


        public StatesFactory(
            SceneData sceneData,
            Camera camera,
            AllServices services,
            Transform gameRoot,
            GameConfig config)
        {
            _camera = camera;
            _services = services;
            _gameRoot = gameRoot;
            _config = config;
            _sceneData = sceneData;
        }

        public TState Create<TState>() where TState : class, IState
        {
            if (typeof(TState) == typeof(BootstrapState))
                return new BootstrapState(_sceneData, _services, _gameRoot, _camera, _config) as TState;

            if (typeof(TState) == typeof(GameLoopState))
                return new GameLoopState(_services.Single<GameProcess>()) as TState;

            if (typeof(TState) == typeof(RestartState))
                return new RestartState(_services.Single<GameStateMachine>()) as TState;

            return default;
        }
    }
}