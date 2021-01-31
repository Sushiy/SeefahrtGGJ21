﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class QuestSubsystem : MonoBehaviour
{
    [SerializeField] private bool _allowDoubleQuests;

    [Header("Runtime)")] public List<QuestObjective> FinishedQuests;
    public List<Tuple<QuestObjectiveAsset, QuestCompletionParameters>> FinishedQuestAssets;

    public UnityEvent<bool> onJournalOpened;


    private void Start()
    {
        FinishedQuestAssets = new List<Tuple<QuestObjectiveAsset, QuestCompletionParameters>>();

        FinishedQuests.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //~ todo(Hati) This should be attached to the player
        QuestObjective objective = other.GetComponent<QuestObjective>();
        if (objective)
        {
            if (!_allowDoubleQuests)
            {
                /// Abort Handling this quest when we already had it
                if (FinishedQuests.Contains(objective)) return;
            }

            FinishedQuests.Add(objective);


            Debug.LogFormat("QuestSubsystem found Objective: {0}", other.gameObject.name);

            GameObject prefabToInstantiate = objective.GetAsset().PrefabToInstantiate;
            if (prefabToInstantiate)
            {
                Transform playerTransform = this.transform;
                QuestCompletionParameters questCompletionParameters = new QuestCompletionParameters()
                {
                    CompletedObjective = objective,
                    PlayerGameObject = this.gameObject,

                    CompletionPlayerLocation = playerTransform.position,
                    CompletionRotation = playerTransform.rotation,
                    NextObjective = objective.NextObjective,
                };

                OpenJournal(objective.GetAsset(), questCompletionParameters);


                FinishedQuestAssets.Add(Tuple.Create(objective.GetAsset(), questCompletionParameters));
            }
        }
    }

    public UnityEvent OpenJournal(QuestObjectiveAsset Asset, QuestCompletionParameters Params)
    {
        var go = GameObject.Instantiate(Asset.PrefabToInstantiate);
        var popup = go.GetComponent<QuestPopup>();
        if (popup)
        {
            onJournalOpened.Invoke(true);
            popup.Setup(Asset, Params);
            popup.ConfirmButton.onClick.AddListener(() => onJournalOpened.Invoke(false));

            return (UnityEvent) popup.ConfirmButton.onClick;
        }

        Destroy(go);

        return null;
    }

    public bool GetLastQuest(out Tuple<QuestObjectiveAsset, QuestCompletionParameters> FinishedQuest)
    {
        FinishedQuest = null;
        if (FinishedQuestAssets.Count > 0)
        {
            FinishedQuest = FinishedQuestAssets.Last();
        }

        return FinishedQuest != null;
    }
}