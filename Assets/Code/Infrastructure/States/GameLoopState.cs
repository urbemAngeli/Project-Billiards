using Code.BilliardEquipment.Table;
using Code.GameLogic;
using Code.Infrastructure.StateMachine;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {

        private readonly GameProcess _gameProcess;

        public GameLoopState(GameProcess gameProcess)
        {
            _gameProcess = gameProcess;
        }
        
        public void Enter()
        {
            _gameProcess.Run();
        }

        public void Exit()
        {
            _gameProcess.Stop();
        }
    }
}