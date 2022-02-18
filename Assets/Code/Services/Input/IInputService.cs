using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService : IService
    {
        TouchPhase Phase { get; }
        Vector2 ScreenPosition { get; }
    }
}