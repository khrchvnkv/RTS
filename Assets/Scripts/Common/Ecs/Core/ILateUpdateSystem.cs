namespace Common.Ecs.Core
{
    public interface ILateUpdateSystem : ISystem
    {
        void OnLateUpdate(in int entity);
    }
}