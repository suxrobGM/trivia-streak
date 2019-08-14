#if UNITY_ANDROID

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ElicitIce {
    public class ImagePickerExample3 : ImagePicker {
        public Timer refresh = new Timer(5f, true);

        public int received = 0;
        public int receivedTexts = 0;
        public string[] receivedText;

        public List<Texture2D> displayAll = new List<Texture2D>();
        public string[] files = new string[0];
        public Texture2D[] receivedImages;

        public int filesInDir = 0;

        GUIContent[] sizeGUI = new GUIContent[]{
            new GUIContent("256"),
            new GUIContent("512"),
            new GUIContent("1024"),
            new GUIContent("2048")
        };

        int[] sizes = new int[]{
            256,512,1024,2048
        };

        int sizeX = 0;
        int sizeY = 0;
        public bool bestFit = false;
        public bool autoRemove = true;

        public  Vector2 scrollDumbnails;

        public string firstPath = null;
        public string receiveDir = "/Receive/";
        
        private  Vector2 scrollFiles;
        
        #region TOUCH_SCROLL_VIEW_VARIABLES
        /// <summary>
        /// Block button behaviour while dragging
        /// </summary>
        public bool consumeMouseUp = false;
        /// <summary>
        /// are we dragging?
        /// </summary>
        public bool isDrag = false;
        /// <summary>
        /// determine speed based on difference with this one
        /// </summary>
        public Vector2 lastPos;
        /// <summary>
        /// multiply speed with this (3 = 3x as fast, 0.1 is one tenth)
        /// </summary>
        public float dragFactor = 1f;
        /// <summary>
        /// Value of the imgsDistance we need to cross before we consider the touch a drag
        /// </summary>
        public float sqrThreshhold = 10f;
        #endregion TOUCH_SCROLL_VIEW_VARIABLES

        // Update is called once per frame
        void Update() {
            if(refresh) {
                received = GetReceivedCount();
                if(received > 0)
                    firstPath = GetRawEntry(0);
                else
                    firstPath = "--ready to receive--";

                receivedTexts = GetReceivedTextCount();
                if( receivedTexts > 0 ) {
                    receivedText = GetReceivedText( 0 );
                }

                if( Directory.Exists( GetInternalDir() + receiveDir ) ) {
                    files = Directory.GetFiles( GetInternalDir() + receiveDir );
                    filesInDir = files.Length;
                } else {
                    files = new string[] { "--none--" };
                    filesInDir = 0;
                }

                refresh.Start();
            }
        }

        public GUIStyle imageBox;
        private  Texture2D fullscreen;

        void OnGUI() {
            GUILayoutOption height = GUILayout.Height(Screen.height * 0.1f);
            GUILayoutOption width  = GUILayout.Width((Screen.width / 2f) - 2f);

            GUI.skin.button.wordWrap = true;

            if(fullscreen != null) {
                height = GUILayout.Height(Screen.height);
                width = GUILayout.Width(Screen.width);
                if(GUILayout.Button(fullscreen, height, width)) {
                    fullscreen = null;
                }
                return;
            }

            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    if(GUILayout.Button("Load all Receive Images: " + received, width, height)) {

                        if(received > 0) {
                            ReceiveAllFiles(texture => displayAll.Add(texture), null, receiveDir, sizes[sizeX], sizes[sizeY], bestFit, autoRemove);
                        } else {
                            received = GetReceivedCount();
                            firstPath = null;
                        }
                    }

                    if(received > 0) {
                        if(firstPath == null)
                            firstPath = GetRawEntry(0);
                        if(firstPath != null) {
                            GUILayout.Box("first entry:\n" + firstPath, width);
                            GUILayout.BeginHorizontal(width);
                            {
                                if(GUILayout.Button("Remove first entry", height)) {
                                    RemoveReceivedEntry(0);
                                    received = GetReceivedCount();
                                    if(received > 0)
                                        firstPath = GetRawEntry(0);
                                }

                                if(!autoRemove) {
                                    if(GUILayout.Button("Load first entry", height)) {
                                        ReceiveFile(texture => displayAll.Add(texture), "img", receiveDir, sizes[sizeX], sizes[sizeY], bestFit);
                                    }
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                    }

                    if( receivedTexts > 0 ) {
                        GUILayout.BeginHorizontal( width );
                        GUILayout.Box( "Subject: " + receivedText[1], height );
                        GUILayout.Box( "Text: " + receivedText[0], height );
                        GUILayout.EndHorizontal();
                        if(GUILayout.Button("Remove text"))
                        {
                            RemoveReceivedTextEntry( 0 );
                            receivedTexts--;
                            if( receivedTexts > 0 ) {
                                receivedText = GetReceivedText( 0 );
                            }
                        }
                    }

                    if(GUILayout.Button("Open picker", height)) {
                        StartImagePicker(texture => displayAll.Add(texture), null, receiveDir, sizes[sizeX], sizes[sizeY], bestFit, true);
                    }

                    scrollFiles = GUILayout.BeginScrollView(scrollFiles, GUI.skin.window, GUILayout.Height(Screen.height * 0.2f));
                    {
                        GUILayout.Label( "Files in received directory: " + filesInDir );
                        GUILayout.BeginVertical();
                        {
                            foreach(string file in files)
                                GUILayout.Label(file);
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndScrollView();
                }
                GUILayout.EndVertical();

                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                {
                    GUILayout.Space(Screen.height * 0.1f);
                    GUILayout.BeginHorizontal(width);
                    {
                        if(GUILayout.Button("Best fit " + (bestFit ? "On" : "Off"), height)) {
                            bestFit = !bestFit;
                        }

                        if(GUILayout.Button("Remove after processing " + (autoRemove ? "On" : "Off"), height)) {
                            autoRemove = !autoRemove;
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Label("X max: ");
                    sizeX = GUILayout.SelectionGrid(sizeX, sizeGUI, 4, height, width);

                    GUILayout.Label("Y max: ");
                    sizeY = GUILayout.SelectionGrid(sizeY, sizeGUI, 4, height, width);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            scrollDumbnails = GUILayout.BeginScrollView(scrollDumbnails, GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            {
                imageBox.fixedHeight = imageBox.fixedWidth = Screen.width / 7.2f;

                int cnt = Screen.width > Screen.height ? 7 : 5;

                if(receivedImages.Length != displayAll.Count) {
                    receivedImages = displayAll.ToArray();
                }

                int x = GUILayout.SelectionGrid(-1, receivedImages, cnt, imageBox, GUILayout.Width(Screen.width * 0.9f), GUILayout.ExpandHeight(true));
                if(x >= 0 && x < receivedImages.Length) {
                    if(consumeMouseUp)
                        consumeMouseUp = false;
                    else
                        fullscreen = receivedImages[x];
                }
            }
            GUILayout.EndScrollView();

            Rect lr = GUILayoutUtility.GetLastRect();
            if(Input.GetMouseButton(0)) {
                if(isDrag == false) {
                    if(lr.Contains(Event.current.mousePosition)) {
                        isDrag = true;
                        lastPos = Event.current.mousePosition;
                    }
                } else {
                    //you could flip this 
                    Vector2 pos = lastPos - Event.current.mousePosition;
                    if(pos.sqrMagnitude > sqrThreshhold)
                        consumeMouseUp = true;
                    scrollDumbnails += pos * dragFactor * Time.deltaTime;

                }
            } else {
                isDrag = false;
                consumeMouseUp = false;
            }
        }
    }
}

#else
#warning ImagePickerExamples contain no code on non-Android Platform
#endif