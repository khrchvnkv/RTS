using System;
using UnityEngine;

namespace Common.Ecs.Components.Movement
{
    [Serializable]
    public struct MoveStateComponent
    {
        public bool MoveRequired;
        public Vector3 Direction;
    }
}