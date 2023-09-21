namespace Common.Ecs.Core
{
    public interface IFixedUpdateSystem : ISystem
    {
        void OnFixedUpdate(in int entity);
    }
}