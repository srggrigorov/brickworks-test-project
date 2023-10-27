using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class Character : ScriptableObject
{
    protected int _skillPoints;

    [field: SerializeField] public SkillModel BaseSkill { get; protected set; }
    [SerializeField] protected List<SkillModel> _availableSkills;
    public HashSet<SkillModel> AvailableSkills { get; protected set; }
    public HashSet<SkillModel> LearnedSkills { get; private set; }
    public event Action<Character> OnChanged;

    public int SkillPoints
    {
        get => _skillPoints;
        private set => _skillPoints = value > 0 ? value : 0;
    }

    public void Initialize()
    {
        AvailableSkills = _availableSkills.ToHashSet();
        LearnedSkills = new HashSet<SkillModel> { BaseSkill };
        OnChanged?.Invoke(this);
    }

    public virtual void AddSkillPoints(int pointsCount)
    {
        SkillPoints += pointsCount;
        OnChanged?.Invoke(this);
    }

    public void LearnSkill(SkillModel skillModel)
    {
        if (!CanLearnSkill(skillModel))
        {
            return;
        }

        LearnedSkills.Add(skillModel);
        AddSkillPoints(-skillModel.Price);
        OnChanged?.Invoke(this);
    }


    public void ForgetSkill(SkillModel skillModel)
    {
        if (!CanForgetSkill(skillModel))
        {
            return;
        }

        AddSkillPoints(skillModel.Price);
        LearnedSkills.Remove(skillModel);
        OnChanged?.Invoke(this);
    }

    public bool CanLearnSkill(SkillModel skillModel)
    {
        bool isEnoughPoints = SkillPoints >= skillModel.Price;
        bool isNotLearned = !LearnedSkills.Contains(skillModel);
        bool previousSkillLearned = skillModel.PreviousSkills.Any(x => LearnedSkills.Contains(x));
        return isEnoughPoints && isNotLearned && previousSkillLearned;
    }

    public bool CanForgetSkill(SkillModel skillModel)
    {
        bool isLearned = LearnedSkills.Contains(skillModel);
        if (!isLearned || skillModel.IsBase)
        {
            return false;
        }

        var nextLearnedSkills = skillModel.NextSkills.Where(x => LearnedSkills.Contains(x)).ToList();
        return nextLearnedSkills.All(x =>
            x.PreviousSkills.Any(y => y != skillModel && LearnedSkills.Contains(y)));
    }

    public void ForgetAllSkills()
    {
        int totalSkillPoints = 0;
        foreach (var skill in LearnedSkills)
        {
            if (!skill.IsBase)
            {
                totalSkillPoints += skill.Price;
            }
        }

        LearnedSkills.RemoveWhere(x => !x.IsBase);
        AddSkillPoints(totalSkillPoints);
        OnChanged?.Invoke(this);
    }

    public bool IsSkillLearned(SkillModel skillModel) => LearnedSkills.Contains(skillModel);
}