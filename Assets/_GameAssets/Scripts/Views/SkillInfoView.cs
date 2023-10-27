using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoView : MonoBehaviour
{
    [SerializeField] private SkillInfoController _skillInfoController;
    [SerializeField] private TMP_Text _skillNameText;
    [SerializeField] private TMP_Text _skillPriceText;
    [SerializeField] private Button _learnButton;
    [SerializeField] private Button _forgetButton;
    [SerializeField] private Button _resetButton;

    private SkillModel _chosenSkill;

    private void Awake()
    {
        _skillInfoController.OnSkillChoiceChanged += UpdateSkillInfo;
        _skillInfoController.OnCharacterChanged += UpdateView;
        _learnButton.onClick.AddListener(() => _skillInfoController.LearnChosenSkill());
        _forgetButton.onClick.AddListener(() => _skillInfoController.ForgetChosenSkill());
        _resetButton.onClick.AddListener(() => _skillInfoController.ForgetAllSkills());
    }

    private void UpdateView(Character character)
    {
        UpdateSkillInfo(_chosenSkill);
    }

    private void OnDestroy()
    {
        _skillInfoController.OnSkillChoiceChanged -= UpdateSkillInfo;
        _skillInfoController.OnCharacterChanged -= UpdateView;
        _learnButton.onClick.RemoveAllListeners();
        _forgetButton.onClick.RemoveAllListeners();
        _resetButton.onClick.RemoveAllListeners();
    }

    private void UpdateSkillInfo(SkillModel skillModel)
    {
        _chosenSkill = skillModel;
        _skillNameText.text = skillModel.name;
        _skillPriceText.text = skillModel.Price.ToString();
        _learnButton.interactable = _skillInfoController.CanLearnChosenSkill();
        _forgetButton.interactable = _skillInfoController.CanForgetChosenSkill();
    }
}