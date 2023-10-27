using UnityEngine;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private Mage _magePrefab;
    [SerializeField] private CharacterInfoController _characterInfoController;
    [SerializeField] private SkillInfoController _skillInfoController;

    public void OpenWindow(Character character)
    {
        // Possibility to make different skill trees for different characters in the future
        // Need to add skill tree view change
        _characterInfoController.SetCharacter(character);
        _skillInfoController.SetCharacter(character);
    }

    private void Start()
    {
        Mage mage = Instantiate(_magePrefab);
        mage.Initialize();
        OpenWindow(mage);
    }
}