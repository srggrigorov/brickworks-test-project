using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeView : MonoBehaviour
{
    [SerializeField] private SkillInfoController _skillInfoController;
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private List<SkillView> _skillViews;
    private Dictionary<Toggle, SkillView> _skillViewsToggleDict;
    private Toggle _activeToggle;

    private void Awake()
    {
        _skillViewsToggleDict = new Dictionary<Toggle, SkillView>();
        foreach (var skillView in _skillViews)
        {
            _skillViewsToggleDict.Add(skillView.Toggle, skillView);
        }

        _skillViews.Clear();
        _skillInfoController.OnCharacterChanged += UpdateSkillViews;
    }

    private void Start()
    {
        _activeToggle = _skillViewsToggleDict.First().Key;
        _skillInfoController.SetChosenSkill(_skillViewsToggleDict[_activeToggle].Skill);
        _activeToggle.onValueChanged.AddListener(ActiveToggleChanged);
    }

    private void UpdateSkillViews(Character character)
    {
        foreach (var toggleViewPair in _skillViewsToggleDict)
        {
            var view = toggleViewPair.Value;
            if (view.Skill.IsBase)
            {
                view.SetColorAsBase();
                continue;
            }

            if (character.IsSkillLearned(view.Skill))
            {
                view.SetColorAsLearned();
            }
            else
            {
                view.SetColorAsNotLearned();
            }
        }
    }

    private void ActiveToggleChanged(bool _ = true)
    {
        if (_activeToggle != null)
        {
            _activeToggle.onValueChanged.RemoveListener(ActiveToggleChanged);
        }

        _activeToggle = _toggleGroup.GetFirstActiveToggle();
        _activeToggle.onValueChanged.AddListener(ActiveToggleChanged);
        _skillInfoController.SetChosenSkill(_skillViewsToggleDict[_activeToggle].Skill);
    }
}