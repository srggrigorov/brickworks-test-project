using System;
using UnityEngine;

public class CharacterInfoController : MonoBehaviour
{
    public event Action<Character> OnCharacterChanged;

    private Character _character;

    public void SetCharacter(Character character)
    {
        if (_character != null)
        {
            _character.OnChanged -= OnCharacterChanged;
        }

        _character = character;
        _character.OnChanged += OnCharacterChanged;
        OnCharacterChanged?.Invoke(_character);
    }


    public void AddSkillPoints(int pointsCount)
    {
        _character?.AddSkillPoints(pointsCount);
    }
}