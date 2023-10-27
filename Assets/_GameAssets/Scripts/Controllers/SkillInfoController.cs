using System;
using UnityEngine;

public class SkillInfoController : MonoBehaviour
{
    public event Action<SkillModel> OnSkillChoiceChanged;
    public event Action<Character> OnCharacterChanged;

    private SkillModel _chosenSkill;
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

    public void LearnChosenSkill() => _character.LearnSkill(_chosenSkill);
    public void ForgetChosenSkill() => _character.ForgetSkill(_chosenSkill);
    public void ForgetAllSkills() => _character.ForgetAllSkills();
    public bool CanLearnChosenSkill() => _character != null && _character.CanLearnSkill(_chosenSkill);
    public bool CanForgetChosenSkill() => _character != null && _character.CanForgetSkill(_chosenSkill);

    public void SetChosenSkill(SkillModel skillModel)
    {
        _chosenSkill = skillModel;
        OnSkillChoiceChanged?.Invoke(_chosenSkill);
    }
}