#if UNITY_ANDROID
using UnityEngine;
using System.Collections;

namespace ElicitIce {

    public class Example2gui : MonoBehaviour {
        public Rect screenRect= new Rect(0, 0.12f, 100, 80);

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
            GUILayoutOption[] opts = new GUILayoutOption[] {
                GUILayout.Height(Screen.height * 0.1f),
            };
            calcRect();

            GUILayout.BeginArea( calc );
            {
                GUILayout.BeginVertical();
                {

                    if( GUILayout.Button( "Launch Default:\n" + ImagePickerExample2.useDefault, opts ) ) {
                        ImagePickerExample2.useDefault = !ImagePickerExample2.useDefault;
                        BroadcastMessage( "OnSettingChange" );
                    }

                    if( ImagePickerExample2.useDefault ) {
                        if( GUILayout.Button( ImagePickerExample2.showCam ? "Use\nCamera" : "Use\nImage Picker", opts ) ) {
                            ImagePickerExample2.showCam = !ImagePickerExample2.showCam;
                            BroadcastMessage( "OnSettingChange" );
                        }
                    } else {
                        if( GUILayout.Button( "Show Camera:\n" + ImagePickerExample2.showCam, opts ) ) {
                            ImagePickerExample2.showCam = !ImagePickerExample2.showCam;
                            BroadcastMessage( "OnSettingChange" );
                        }
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }
    }
}

#else
#warning ImagePickerExamples contain no code on non-Android Platform
#endif