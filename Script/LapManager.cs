using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public float CurrentLapTime
    {
        get
        {
            if ( m_IsLapStarted == false)
            {
                return 0f;
            }
            return Time.realtimeSinceStartup - m_CurrentLapStartTime;
        }
    }

    public float LastLapTime { get; private set; }
    public float BestLapTime { get; private set; }

    bool m_IsLapStarted = false;
    float m_CurrentLapStartTime;
    int m_LastLapLineIndex = 0;
    int m_HighestLapLine;

    private void Start()
    {
        m_HighestLapLine = GetHighestLapLine();
    }
    
    int GetHighestLapLine()
    {
        int highestLapLine = 0;
        LapLine[] lapLines = GetComponentsInChildren<LapLine>();
        for( int i=0; i < lapLines.Length; ++i )
        {
            highestLapLine = Mathf.Max( m_HighestLapLine, lapLines[ i ].Index );
        }
        return highestLapLine;
    }
    
    public void OnLapLinePassed( int index )
    {
        Debug.Log( "OnLapLinePassed" + index );
        
        if ( index == 0)
        {
            if( m_IsLapStarted == false || m_LastLapLineIndex == m_HighestLapLine )
            {
                OnFinishLinePassed();
            }
        }
        else
        {
            if( index == m_LastLapLineIndex + 1 )
            {
                m_LastLapLineIndex = index;
                Debug.Log( "Passed Line" + index );
            }
        }
    }

    void OnFinishLinePassed()
    {
        if ( m_IsLapStarted == true )
        {
            LastLapTime = Time.realtimeSinceStartup - m_CurrentLapStartTime;
            if ( LastLapTime < BestLapTime || BestLapTime == 0f)
            {
                BestLapTime = LastLapTime;
            }
        }

        m_IsLapStarted = true;
        m_CurrentLapStartTime = Time.realtimeSinceStartup;
    }

}
