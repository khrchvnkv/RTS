using System;

namespace Common.Infrastructure.Services.UpdateBehaviour
{
    public interface IUpdateBehaviour
    {
        event Action OnUpdate;
        event Action OnFixedUpdate;
        event Action OnLateUpdate;
    }
}