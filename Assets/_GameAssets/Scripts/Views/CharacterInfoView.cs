using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsCountText;
    [SerializeField] private Button _addPointsButton;
    [SerializeField] private CharacterInfoController _characterInfoController;

    public void UpdateView(Character character)
    {
        _pointsCountText.text = character.SkillPoints.ToString();
    }

    private void AddSkillPoints(int pointsCount)
    {
        _characterInfoController.AddSkillPoints(pointsCount);
    }

    private void OnEnable()
    {
        _characterInfoController.OnCharacterChanged += UpdateView;
        _addPointsButton.onClick.AddListener(() => AddSkillPoints(1));
    }

    private void OnDisable()
    {
        _characterInfoController.OnCharacterChanged -= UpdateView;
        _addPointsButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        _characterInfoController.OnCharacterChanged -= UpdateView;
        _addPointsButton.onClick.RemoveAllListeners();
    }
}