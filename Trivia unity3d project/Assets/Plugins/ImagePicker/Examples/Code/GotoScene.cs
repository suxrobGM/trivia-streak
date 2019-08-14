using UnityEngine;
using System.Collections;

public class GotoScene : MonoBehaviour {
    public int SceneIndex = 0;
    public Rect screenRect;

    Rect calc;
    float last_w = -1, last_h = -1;
    
    void calcRect() {
        if(Screen.width != last_w && Screen.height != last_h) {
            calc = screenRect;
            calc.x *= Screen.width;
            calc.y *= Screen.height;
            calc.height *= Screen.height;
            calc.width *= Screen.width;
        }
    }

    void OnGUI() {
        calcRect();

        if(GUI.Button(calc, "Goto scene: " + (SceneIndex + 1))) {
            Application.LoadLevel(SceneIndex);
        }
    }
}
