using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestSubsystem : MonoBehaviour
{
    [Header("Runtime)")] public List<QuestObjective> FinishedQuests;

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
        QuestObjective objectives = other.GetComponent<QuestObjective>();
        if (objectives)
        {
            Debug.LogFormat("QuestSubsystem found Objective: {0}", other.gameObject.name);

            GameObject prefabToInstantiate = objectives.GetAsset().PrefabToInstantiate;
            if (prefabToInstantiate)
            {
                var go = GameObject.Instantiate(objectives.GetAsset().PrefabToInstantiate);

                var popup = go.GetComponent<QuestPopup>();

                popup.Setup(objectives.GetAsset());
            }
        }
    }
}