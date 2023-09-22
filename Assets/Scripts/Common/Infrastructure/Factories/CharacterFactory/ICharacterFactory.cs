using UnityEngine;

namespace Common.Infrastructure.Factories.CharacterFactory
{
    public interface ICharacterFactory
    {
        GameObject CreateCharacter(Transform point);
    }
}