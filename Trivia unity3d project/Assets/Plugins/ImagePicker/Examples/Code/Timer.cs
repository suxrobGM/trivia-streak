using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Timer {
    public float duration  = 1;
    public float start     = -1;
    public float end       = -1;
    public bool  done       = false;
    public bool  notStartedValue = false;

    public Timer() { }
    public Timer(float aDuration) { duration = aDuration; }
    public Timer(float aDuration, bool aNotStartedValue) { duration = aDuration; notStartedValue = aNotStartedValue; }

    public float LerpValue { get { return Mathf.InverseLerp(start, end, Time.time); } }
    public bool  IsRunning { get { return start >= 0; } }
    
    public bool IsDone {
        get {
            if (!done) {
                if (start >= 0) {
                    if (end < Time.time)
                        done = true;
                } else { //not started
                    return notStartedValue;
                }
            }
            return done;
        }
    }

    public static implicit operator bool (Timer a)
    {
        return a.IsDone;
    }


    public static implicit operator float(Timer a)
    {
        return a.LerpValue;
    }

	// Use this for initialization
	public void Start () {
        start = Time.time;
        end = Time.time + duration;
        done = false;
	}

    public void StartOnce(float aDuration)
    {
        start = Time.time;
        end = Time.time + aDuration;
        done = false;
    }

    public void Stop()
    {
        start = -1;
        end   = -1;
        done = false;
    }

    public string ToString(int precision = 0,string extra ="")
    {
        return string.Format("{0:F" + precision + "}{1}", end - Time.time, extra);
    }
}
