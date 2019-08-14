using UnityEngine;

/// <summary>
/// this class is free to use, change, replicate, share, tweet, etc.
/// </summary>
public class Exit : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
