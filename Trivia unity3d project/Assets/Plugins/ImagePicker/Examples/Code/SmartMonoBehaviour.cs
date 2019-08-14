using System;
using UnityEngine;

/// <summary>
/// Highly recommended workaround for an ancient Unity3D issue, causing hundreds of programmers to add code like this to any monobehaviour script that requires a lot of moving the transform
/// 
/// The problem is that transform is a get function that calls a GetComponent, which is more expensive then a buffered version
/// 
/// In this class I use a override on transform, change any script that uses transform into extending SmartMonoBehaviour to gain better performance (noticible if you have hundreds of objects in the scene that run around).
/// Update the scripts to not contain a Start, or make sure they override and call the base class and you could use _transform to skip the if null check
/// 
/// Just copy the behaviour for any of the GetComponent properties of MonoBehaviour, like renderer, collider, etc.
/// If you call these multiple times per frame on lots of objects you will lots of needless instructions from being called.
/// 
/// The reason this behaviour is not standard, is because a reference would take up memory where as the GetComponent method does not, thus saving memory
/// The trick is extending selectivly and buffer those components you use in many if not all your scripts.
/// If you have ten thousand objects all calling transform 5 times a frame, then buffering transform is smart
/// If the same script calls to transform when a certain interaction happens that only occurs once in a few minutes, buffering would use up 10000 * 32 bits (or 64 bits on x64 platforms) = 40 KB ( or 80 KB on x64) to only save a couple of instructions every few minutes.
/// 
/// You might consider to use the buffer on:
/// renderer, animation, rigidbody, collider, gameObject
/// 
/// Create Smart subclasses if you notice a large group of scripts with the same code example: 
/// RigidBehaviour for different physics objects, that only buffers rigidbody and collider
/// MaterialChanger that buffers material and perhaps renderer
/// 
/// this class is free to use, change, replicate, share, tweet, etc.
/// </summary>
public class SmartMonoBehaviour : MonoBehaviour
{
    public Transform _transform = null;
    public new Transform transform { get { if (_transform == null) _transform = base.transform; return _transform; } }

    public virtual void Start()
    {
        if(_transform == null)
            _transform = base.transform;
    }

    /// <summary>
    /// Instance pattern helper function, finds or creates an instance within the scene
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T FindOrCreate<T>() where T : Component
    {
        T result = (T)FindObjectOfType(typeof(T));
        if (result == null)
        {
            GameObject go = new GameObject();
            result = go.AddComponent<T>();
            result.name = typeof(T).ToString();
        }

        return result;
    }

    //For some reason this does not want to work in firstpass as a proper extension
    public static T GetOrCreateComponent<T>( MonoBehaviour me ) where T : Component {
        T ret = me.GetComponent<T>();
        if( ret == null ) {
            ret = me.gameObject.AddComponent<T>();
        }
        return ret;
    }
}