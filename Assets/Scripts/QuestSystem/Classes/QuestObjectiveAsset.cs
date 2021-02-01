using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ObjectiveAsset", menuName = "Quests/Objective")]
public class QuestObjectiveAsset : ScriptableObject
{
    [SerializeField] private string _objectiveTitle;

    [SerializeField, TextArea(5, 10)] private string[] _objectiveTexts;

    [SerializeField] private string _objectiveShort;

    [SerializeField] private GameObject _prefabToInstantiate;

    public string ConfirmText = "Auf auf und davon!";

//~ todo(Hati) Structure for Prefab Parameters ?
    [SerializeField] private Sprite _objectiveTeller;

    public List<QuestTextProcessor> TextProcessors;
    
    public string ObjectiveTitle => _objectiveTitle;

    public string ObjectiveShort => _objectiveShort;

    public string[] ObjectiveTexts => _objectiveTexts;

    public GameObject PrefabToInstantiate => _prefabToInstantiate;

    public Sprite ObjectiveTeller => _objectiveTeller;
    
    
}