using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastQuestPointer : MonoBehaviour
{
    public QuestSubsystem questsub;
    private Vector3 lastQuestPosition;
    private Vector3 lastQuestDirection;

    public Transform indicator;

    public float offsetY = 2.0f;
    public float tPower = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        questsub.onNewQuestFinished.AddListener(SetQuestLocation);
    }

    void SetQuestLocation(Vector3 worldposition)
    {
        lastQuestPosition = worldposition;
    }

    // Update is called once per frame
    void Update()
    {
        lastQuestDirection = lastQuestPosition - transform.position;
        float distance = lastQuestDirection.magnitude;

        float t = Mathf.Clamp(distance / 15.0f, 0,1);
        float mult = Mathf.Lerp(distance * 0.25f, 15.0f, t);
        float y = Mathf.Lerp(offsetY, 0.0f, Mathf.Pow(t, tPower));
        Quaternion rotateTarget = Quaternion.Slerp(Quaternion.LookRotation(Vector3.up, -Vector3.forward), Quaternion.LookRotation(lastQuestDirection), Mathf.Pow(t, tPower));

        indicator.position = transform.position + new Vector3(0,y,0) + lastQuestDirection.normalized * mult;

        indicator.rotation = rotateTarget;

    }
}
