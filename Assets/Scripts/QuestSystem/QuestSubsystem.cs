using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestSubsystem : MonoBehaviour
{
    [Header("(Runtime)")] public List<QuestObjective> FinishedQuests;

    private void Start()
    {
        FinishedQuests.Clear();
    }

    private void Update()
    {
        this.transform.position += Vector3.forward * Time.deltaTime;
    }


    //~ todo(Hati) Publish Event for finished quests here
    //~ todo(Hati) Query finished Quests

    public void FinishQuest(QuestObjective Objective)
    {
        FinishedQuests.Add(Objective);
    }

    private void OnTriggerEnter(Collider other)
    {
        //~ todo(Hati) This should be attached to the player
        QuestObjective objective = other.GetComponent<QuestObjective>();
        if (objective)
        {
            /// Abort Handling this quest when we already had it
            if (FinishedQuests.Contains(objective)) return;
            
            FinishedQuests.Add(objective);
            
            Debug.LogFormat("QuestSubsystem found Objective: {0}", other.gameObject.name);

            GameObject prefabToInstantiate = objective.GetAsset().PrefabToInstantiate;
            if (prefabToInstantiate)
            {
                var go = GameObject.Instantiate(objective.GetAsset().PrefabToInstantiate);

                var popup = go.GetComponent<QuestPopup>();

                Transform playerTransform = this.transform;
                popup.Setup(objective.GetAsset(), new QuestCompletionParameters()
                {
                    CompletedObjective = objective,
                    PlayerGameObject = this.gameObject,

                    CompletionPlayerLocation = playerTransform.position,
                    CompletionRotation = playerTransform.rotation,
                    NextObjective = objective.NextObjective,
                });
            }
        }
    }
}