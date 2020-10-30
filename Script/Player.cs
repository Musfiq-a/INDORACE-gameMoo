using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ControlType { HumanInput, AI}
    public ControlType controlType = ControlType.HumanInput;

    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } = 0;
    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private Mobil mobilController;

    void Awake()
    {
        checkpointsParent = GameObject.Find ("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Water");
        mobilController = GetComponent<Mobil>();
    }

    void StartLap()
    {
        Debug.Log("StartLap!!!");
        CurrentLap++;
        lastCheckpointPassed =1;
        lapTimerTimestamp = Time.time;
    }

    void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log("EndLap - LapTime was " + LastLapTime + " seconds");
        //Debug.Log("FINISH!!!");
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.layer != checkpointLayer)
        {
            return;
        }

        // If this cekpoint 1...
        if (collider.gameObject.name == "1")
        {
            //... and we've completed a lap, end the current lap
            if (lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }

            // If we 
            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            return;
        }

        // If we've
        if (collider.gameObject.name == (lastCheckpointPassed+1).ToString())
        {
            lastCheckpointPassed++;
        }


    } 

    void Update()
    {
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;

        if (controlType == ControlType.HumanInput)
        {
            mobilController.Steer = GameManager.Instance.InputController.SteerInput;
            mobilController.Throttle = GameManager.Instance.InputController.ThrottelInput;    
        }
    }     
}
