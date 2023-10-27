using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [field: SerializeField] public Toggle Toggle { get; private set; }
    [field: SerializeField] public SkillModel Skill { get; private set; }
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;

    [Header("Colors")] [SerializeField] private Color _baseSkillColor;
    [SerializeField] private Color _learnedSkillColor;
    [SerializeField] private Color _notLearnedSkillColor;


    private void Awake()
    {
        _icon.sprite = Skill?.Icon;
    }

    public void SetColorAsBase() => _background.color = _baseSkillColor;
    public void SetColorAsLearned() => _background.color = _learnedSkillColor;
    public void SetColorAsNotLearned() => _background.color = _notLearnedSkillColor;
}