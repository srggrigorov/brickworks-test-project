using System.Collections.Generic;
using UnityEngine;

public abstract class SkillModel : ScriptableObject
{
    [field: SerializeField] public int Price { get; protected set; }
    [field: SerializeField] public bool IsBase { get; protected set; }
    [field: SerializeField] public List<SkillModel> PreviousSkills { get; protected set; }
    [field: SerializeField] public List<SkillModel> NextSkills { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }
}