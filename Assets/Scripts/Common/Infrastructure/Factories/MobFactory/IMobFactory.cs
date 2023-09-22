using UnityEngine;

namespace Common.Infrastructure.Factories.MobFactory
{
    public interface IMobFactory
    {
        GameObject CreateMob(Transform point);
    }
}