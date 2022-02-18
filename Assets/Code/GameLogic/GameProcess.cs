using Code.BilliardEquipment.Balls;
using Code.BilliardEquipment.Table.Pockets;
using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.States;
using Code.Other;

namespace Code.GameLogic
{
    public class GameProcess : IService
    {
        
        private readonly TablePockets _pockets;
        private readonly BallDropTrigger _flightTrigger;
        private readonly Player _player;
        private readonly AimControl _aimControl;
        private readonly GameStateMachine _stateMachine;
        private readonly BallBasket _ballBasket;


        public GameProcess(
            TablePockets pockets,
            BallDropTrigger flightTrigger, 
            Player player,
            AimControl aimControl, 
            GameStateMachine stateMachine, 
            BallBasket ballBasket)
        {
            _pockets = pockets;
            _flightTrigger = flightTrigger;
            _player = player;
            _aimControl = aimControl;
            _stateMachine = stateMachine;
            _ballBasket = ballBasket;
        }

        public void Run()
        {
            _pockets.onBallFell += OnBallFell;
            _flightTrigger.onBallFell += OnBallFell;
            
            _ballBasket.Cleanup();
            
            _player.PutBallPyramid();
            StartNewBreakdown();
        }

        public void Stop()
        {
            _pockets.onBallFell -= OnBallFell;
            _flightTrigger.onBallFell -= OnBallFell;
            
            _aimControl.StopAiming();
        }

        private void OnBallFell(Ball ball)
        {
            if (ball.Number == 0)
                StartNewBreakdown();
            
            else if (ball.Number == 8)
                EndSession();
            
            else
            {
                if(_ballBasket.TryAdd(ball))
                    ContinueSession();
                else
                    EndSession();
            }
        }
        
        private void EndSession() => 
            _stateMachine.ChangeState<RestartState>();

        private void StartNewBreakdown()
        {
            _player.PutCueBall();
            _aimControl.StartAiming();
        }

        private void ContinueSession() => 
            _aimControl.StartAiming();
    }
}