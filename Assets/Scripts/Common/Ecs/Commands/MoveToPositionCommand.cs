using System;
using UnityEngine;

namespace Common.Ecs.Commands
{
    [Serializable]
    public struct MoveToPositionCommand
    {
        public Vector3 Destination;
    }
}