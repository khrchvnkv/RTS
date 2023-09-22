using System;
using UnityEngine.Serialization;

namespace Common.Ecs.Components.Movement
{
    [Serializable]
    public struct MoveSpeedComponent
    {
        public float Value;
    }
}