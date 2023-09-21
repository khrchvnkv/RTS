namespace Common.Ecs.Core
{
    public interface IUpdateSystem : ISystem
    {
        void OnUpdate(in int entity);
    }
}