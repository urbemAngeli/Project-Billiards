using Code.BilliardEquipment;
using Code.BilliardEquipment.Balls;
using Code.BilliardEquipment.Pyramid;
using Code.BilliardEquipment.Table;
using Code.GameLogic;
using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachine;
using Code.Other;
using Code.Services.Input;
using Code.Services.Ticks;
using Code.StaticData;
using Code.Trajectory;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly SceneData _sceneData;
        private readonly AllServices _services;
        private readonly Transform _gameRoot;
        private readonly Camera _camera;
        private readonly GameConfig _config;

        public BootstrapState(
            SceneData sceneData,
            AllServices services,
            Transform gameRoot, 
            Camera camera, 
            GameConfig config)
        {
            _sceneData = sceneData;
            _services = services;
            _gameRoot = gameRoot;
            _camera = camera;
            _config = config;
        }
        
        public void Enter()
        {
            RegisterServices();
            InjectDependencies();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<PoolTable>(_sceneData.Table);

            _services.RegisterSingle<BallCollection>(_sceneData.BallCollection);
            
            RegisterBallPyramid();
            
            RegisterBallDropTrigger();
            
            _services.RegisterSingle<Player>(
                new Player(
                    _services.Single<PoolTable>(),
                    _services.Single<BallCollection>(), 
                    _services.Single<BallPyramid>()));

            RegisterTickProcessor();
            
            _services.RegisterSingle<IInputService>(InputService());
            
            _services.RegisterSingle<BallBasket>(new BallBasket());
            
            _services.RegisterSingle<AimControl>(
                new AimControl(
                    _services.Single<IInputService>(), 
                    _services.Single<TickProcessor>(),
                    _camera,
                    _services.Single<BallCollection>().CueBall.transform,
                    _config));
            
            _services.RegisterSingle<СueStickHittingPhysics>(
                new СueStickHittingPhysics(
                    _services.Single<AimControl>(),
                    _services.Single<BallCollection>().CueBall, 
                    _config));
            
            _services.RegisterSingle<TrajectoryDrawing>(new TrajectoryDrawing());
            
            _services.RegisterSingle<SimulationTrajectory>(
                new SimulationTrajectory(
                    _services.Single<AimControl>(),
                    _services.Single<BallCollection>(),
                    _services.Single<TrajectoryDrawing>()));
            
            _services.RegisterSingle<CueStick>(_sceneData.CueStick);
            _sceneData.CueStick.Construct(_services.Single<AimControl>(), _config);

            _services.RegisterSingle<GameProcess>(
                new GameProcess(
                    _sceneData.Table.Pockets,
                    _services.Single<BallDropTrigger>(),
                    _services.Single<Player>(),
                    _services.Single<AimControl>(),
                    _services.Single<GameStateMachine>(),
                    _services.Single<BallBasket>()));
        }

        private void InjectDependencies()
        {
            _sceneData.UIRoot.ImpactForceBar.Construct(_services.Single<AimControl>());
        }

        private void RegisterTickProcessor()
        {
            GameObject processor = new GameObject("TickProcessor");
            processor.transform.parent = _gameRoot;
            _services.RegisterSingle<TickProcessor>(processor.AddComponent<TickProcessor>());
        }

        private void RegisterBallPyramid()
        {
            _services.RegisterSingle<BallPyramid>(_sceneData.BallPyramid);
            _sceneData.BallPyramid.Construct(_sceneData.BallCollection);
        }

        private void RegisterBallDropTrigger()
        {
            _services.RegisterSingle<BallDropTrigger>(_sceneData.BallDropTrigger);
            _sceneData.BallDropTrigger.Construct(_sceneData.BallCollection);
        }

        private IInputService InputService()
        {
            return new StandaloneInputService();

            // return Application.isEditor
            //     ? (IInputService) new StandaloneInputService()
            //     : new MobileInputService();
        }
    }
}