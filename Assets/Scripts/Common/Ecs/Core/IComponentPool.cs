namespace Common.Ecs.Core
{
    public interface IComponentPool
    {
        void AllocateComponentsArray();
        void RemoveComponent(in int entity);
    }
}