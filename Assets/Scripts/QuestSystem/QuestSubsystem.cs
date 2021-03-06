﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QuestSubsystem : MonoBehaviour
{
    [SerializeField] private bool _allowDoubleQuests;

    [Header("Runtime)")] public List<QuestObjective> FinishedQuests;
    public List<Tuple<QuestObjectiveAsset, QuestCompletionParameters>> FinishedQuestAssets;

    public UnityEvent<bool> onJournalOpened;

    public UnityEvent<Vector3> onNewQuestFinished;

    public UnityEvent<QuestObjective> OnQuestObjectiveChange = new UnityEvent<QuestObjective>();

    public QuestObjective NextObjective;
    public bool NoNextObjectiveMeansAny = true;

    BoatControl controller;

    public TMPro.TMP_Text text;

    public QuestPopup journal;

    public AudioSource soundFX;
    public AudioClip journalSFX;
    public AudioClip objectiveSFX;

    private void Awake()
    {
        controller = GetComponent<BoatControl>();
        journal.CloseButton.onClick.AddListener(() => onJournalOpened.Invoke(false));
        soundFX = GetComponent<AudioSource>();
    }

    private void Start()
    {
        FinishedQuestAssets = new List<Tuple<QuestObjectiveAsset, QuestCompletionParameters>>();

        FinishedQuests.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //~ todo(Hati) This should be attached to the player
        QuestObjective objective = other.GetComponent<QuestObjective>();
        if (objective && (NextObjective == objective || (NoNextObjectiveMeansAny && NextObjective == null)))
        {
            if (!_allowDoubleQuests)
            {
                /// Abort Handling this quest when we already had it
                if (FinishedQuests.Contains(objective)) return;
            }

            FinishedQuests.Add(objective);
            objective.onQuestReached.Invoke();
            //Play sound to feedback objective reached (this is anyoing fast, so I removed it for now)
            //soundFX.clip = objectiveSFX;
            //soundFX.Play();


            objective.ToggleArrow(false);
            NextObjective = objective.NextObjective;
            if(NextObjective)
                NextObjective.ToggleArrow(true);
            OnQuestObjectiveChange.Invoke(NextObjective);

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

                AddToJournal(objective.GetAsset(), questCompletionParameters);

                onNewQuestFinished.Invoke(playerTransform.position);

                FinishedQuestAssets.Add(Tuple.Create(objective.GetAsset(), questCompletionParameters));
                text.text = "- " + objective.GetAsset().ObjectiveShort;
            }
            if(objective.m_isLast)
                FindObjectOfType<FadeOutManager>().StartFade();
        }
    }


    public void OpenJournal()
    {
        if(journal)
        {
            journal.OpenJournal();
            soundFX.clip = journalSFX;
            soundFX.Play();
        }
    }

    public void CloseJournal()
    {
        if(journal)
        {
            journal.Close();
        }
    }

    public void SkipTyping()
    {
        if(journal)
        {
            journal.contentTyper.SkipToEnd();
        }
    }

    public void AddToJournal(QuestObjectiveAsset Asset, QuestCompletionParameters Params)
    {
        if (journal)
        {
            journal.gameObject.SetActive(true);
            onJournalOpened.Invoke(true);
            journal.AddToJournalAndShow(Asset, Params);
        }
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