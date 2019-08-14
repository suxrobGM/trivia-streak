using UnityEngine;
using System.Collections;
#if UNITY_ANDROID
namespace ElicitIce {
public class getImageforsetting : ImagePicker {

	
		public Texture2D image;
		public dfTextureSprite te;
		
		GUIContent[] sizeGUI = new GUIContent[]{
			new GUIContent("256"),
			new GUIContent("512"),
			new GUIContent("1024"),
			new GUIContent("2048"),
			new GUIContent("4096"),
			new GUIContent("4100"),
		};
		
		int[] sizes = new int[]{
			400,512,1024,2048,4096, 4100
		};
		int[] sizesy = new int[]{
			400,512,1024,2048,4096, 4100
		};
		int sizeX = 0;
		int sizeY = 0;
		
		public bool useCamera  = false;
		public bool useDefault = false;
		public bool bestFit    = true;
		
		//public new GUITexture guiTexture;
		
		internal void Start() {
			data = new ImagePickerData();
			{
				data.loadImage = image;
				data.fileName = null;
				data.gameObject = gameObject.name;
				data.callback = ImagePickerCallback;
			}


		}
		
		ImagePickerData data = null;
		
		public GUIStyle boxStyle = new GUIStyle();



		public void OnClick()
		{
			
			data.bestFit = bestFit;
			data.showCamera = useCamera;
			data.useDefault = useDefault;
			data.fileSubDir = "xxd";
			data.maxWidth = sizes[sizeX];
			data.maxHeight = sizesy[sizeY];
			
			StartImagePicker(data);
			//Example();
		}

			
		private bool GUILayoutToggleButton(string prefix, bool me, params GUILayoutOption[] opts) {
			if(GUILayout.Button(prefix + (me ? "On" : "Off"), opts)) {
				me = !me;
			}
			return me;
		}
		
		public override void ImagePickerCallback(string result) {
			base.ImagePickerCallback(result);
			//image = data.loadImage;
			te.Texture = data.loadImage as Texture;
			//upload.Start(image);
	
		}
	
	}
}
#endif