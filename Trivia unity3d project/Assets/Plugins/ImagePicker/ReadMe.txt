=============================================================================================================
	Image Picker for Android
		Created by: Elicit Ice
		ImagePicker@ElicitIce.com
=============================================================================================================

=============================================================================================================
	How to use:
=============================================================================================================

Step 1: Open Player settings, select Android, in Other Settings, Select Write Access External (SDCard)
or if you manually create the manifest, add the following just below the end of the application block:
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

Step 2: Add the following into the Application portion of your manifest, you may change anything of this activity except the android:name part.
	<activity android:name="com.ElicitIce.Plugin.ImagePicker" android:label="@string/app_name" > </activity>
	
Step 3: Open an Example, set the scene to load and build

Step 4: Alter the examples or create a custom class based on ImagePicker.cs to suit your needs

If you have any questions or encounter problems with the usage of the plugin, please contact:
	ImagePicker@ElicitIce.com

Or write a review with what you like/dislike about the plugin in the asset store.

=============================================================================================================
	Notes
=============================================================================================================

This plugin uses the Unity3D Android Plugin callback method, it is therefore required that any game object that should receive the callback has a unique name in order to function properly.

You can create scripts to call the plugin in any supported language, although we reserve the right to limit support to C#.

The C# code is just the interface, you can leave out any parts that are not used or implement an entirely different design.
The ImagePickerData.cs contains a region of code which could be removed "ImagePicker Default Implementation variables" if you implement your own interface.

A single game object can be used to load selected textures (ImagePickesExample1.cs), or multiple game objects each with unique names (ImagePickesExample2.cs)
An event system can be created to handle all the callbacks (ImagePickerExample5.cs)

Proper use of preprocessor directives is highly recommended, Example5 is a good starting point.

=============================================================================================================
	Receive Files Notes		&		Select Multiple
=============================================================================================================
The plugin maintains a single list of received files, which is not stored anywhere (except temporarily in memory). 
Every time the application is launched the list is empty.
If the application is launched as part of receiving files, the list will contain those files.
If the application is running while receiving files, the new files are added to the end of the list.

There are two recommended ways to process the entries:
- Load all                  using receiveAllFiles (see ImagePicker.cs and ImagePickerExample3.cs)
- Load images one by one    using receiveFile (see ImagePicker.cs, ImagePickerExample3.cs and ImagePickerExample4.cs)

The plugin can also clear the list automatically, set removeReceived to true in the ImagePickerData

Load All and allow the plugin to clear the list, the callback will return the new resized and cached filepath and prepend the index number of the file, for example:
        0|/data/sdcard/Android/com.yourname.yourapp/received/animage.jpg
        1|/data/sdcard/Android/com.yourname.yourapp/received/anotherimage.jpg
        2|/data/sdcard/Android/com.yourname.yourapp/received/yeahmore.jpg

If you choose to load images one by one with removeReceived true, you would need to wait for each result to come back.
After the first result comes back, keep using index 0 as long as there are images.
Be sure to set the receiver to null or use a System.Action<Texture2D> otherwise the same texture would be replaced over and over again.

Here is some 5+1 step pseudo code for the flow:
step 0: while(receivedFiles > 0)
{
    step 1: receiveFile(data, 0)
    step 2: wait for callback (optionally add a timeout of a couple of seconds)
    step 3: load received path into texture
    step 4: use loaded texture
    step 5: make the data ready for the next file
}

=============================================================================================================
	AndroidManifest Extended Notes
=============================================================================================================

Add this to the Application section of the manifest.
		<activity android:name="com.ElicitIce.Plugin.ImagePicker" android:label="@string/app_name" > </activity>

Or, if you would like to be able to receive files from external applications as well, use this instead:

		<!-- You only need to add this activity to the manifest -->
        <activity android:name="com.ElicitIce.Plugin.ImagePicker" android:label="@string/app_name" >
            <!-- remove the intent filters if you do not wish to add the ability to receive images from other apps -->
            <!-- Remove Receiving -->
            <intent-filter>
                <action android:name="android.intent.action.SEND" />
                <category android:name="android.intent.category.DEFAULT" />
                <data android:mimeType="image/*" />
            </intent-filter>
            <intent-filter>
                <action android:name="android.intent.action.SEND_MULTIPLE" />
                <category android:name="android.intent.category.DEFAULT" />
                <data android:mimeType="image/*" />
            </intent-filter>
            <!-- End Remove Receiving -->
        </activity>

External write permissions are no longer required, please send me an e-mail if it is required.
In case you do need it, add the following just below the application section:

  <!-- We only need Read permissions as of version 8, if the plugin runs into write permission errors, please send an e-mail to me detailing the device, android version and logcat -->
  <!-- To add write permissions, you can delete the READ permission and enable: <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" /> -->
  <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />

Add the following if you want to demand the presence of a camera
uses-feature with required="true" informs the Google Play Store that your application DEMANDS a camera (optional autofocus), and may will not be visible in the store for devices without a camera.

		<!-- Optional -->
		<uses-permission android:name="android.permission.CAMERA" />    
		<uses-feature android:name="android.hardware.camera" android:required="false" />
		<uses-feature android:name="android.hardware.camera.autofocus" android:required="false" />
		

=============================================================================================================
	Version History
=============================================================================================================
Version 11 - work in progress:

Chrash fix:
- Added more error catching while opening the picker dialog (this hopefully mitigates misbehaving applications causing exceptions and closing)

Changes:
- Changed ImagePicker LoadImage to public static so it is easier to use in other classes
- Minor changes to signature of GetOrCreateComponent in SmartMonoBehaviour (CSharp first-pass does not seem to like extension classes)

Additional mime-types support:
- Added picker mime-type support for video
- Added GetVideoPreview
- Added text receiving and sharing (GetReceivedTextCount, GetReceivedText, ShareText, ShareTextWithApp)
- Updated Example 3 to include text receiving
- Updated Example 6 with video support and tweaked the GUI a bit
- Added text receive intent to the example manifest (with XML comments for easy removal)

Already works as expected:
- Changing the mime type (showCamera false) loads the default or shows a picker of applications that can handle "video/*" mime-type
- playing the video using Handheld.PlayFullScreenMovie( receivedFilePath ); //Note this is a Unity3D pro only feature 
- sharing the video with other applications
- 3rd party camera application for video recording
- access to the file bytes

Not yet implemented:
- Example code for sharing text
- Plugin handler for sending multiple texts in a single call (is this even needed? very few applications seem to support receiving multiple texts at once)


Version 10:
- Rebuild to solve issues on some devices

Version 9:
Bug Fixes:
- Fixed out of memory exceptions when using extremely large files (a regression caused by version 8)
- Subdirs are now only appended once (again version 8 code revisions)
- in the C# examples the pragma warnings of version 8 don't throw errors anymore

New:
- Added a first implementation of a share with X feature, it's not as polished as I would like but the above bugs needed squishing

Changed:
- minor GUI change: offset the scene index by one
- the example manifest has write enabled again, read the notes in the manifest for details.

Future:
- What would you like to see implemented?
- I would love to see my work in action, if you have a application or game on the market that uses ImagePicker for Android, send me a link


Version 8:
Bug fixes:
- ImagePicker.cs functions marked now as public
- Helpful Warnings instead of Unhelpful errors when loading the examples on the wrong platform

New:
- Ability to retrieve raw image path before the plug-in does it's magic
- Added AndroidManifest Extended Notes in ReadMe
- Select multiple images (modified example 4)*
- Progress indicator when the plugin is processing files

Changed:
- Major optimizations, completely reworked several code paths
- Changed code so that Write External permissions are now no longer required **
- Manifest updated to match the latest default as used by Unity
- Updated the ReadMe and manifest to include more explicit SD card permissions (in case the previous bit fails)

*:  only with Applications that implement EXTRA_ALLOW_MULTIPLE correctly, might be available on HoneyComb, but officially Android implemented this on 4.3 JellyBean and later.
**: needs further testing to make sure it works on all devices, still need READ_EXTERNAL_STORAGE to access camera files.

Version 7:

Changed:
- The debug output is slightly more informative now when processing images, showing 

Bugfixes:
- maxWidth and maxHeight will now correctly limit the returned texture
- less memory used for downscaling images

Note:
- out of all the options bestFit uses more memory when used, but returns an image that match at least one axis to the requested maximum image. If memory usage is still an issue, please lower the resolution when using bestFit, or disable bestFit and request a higher resolution image.
- Check the description/comments in ImagePickerData.cs bestFit for more information

Version 6:

New:
- Demo has been updated to include ImagePickerExample5 (scene 4- ) as well as the bugfixes and new functionalities listed below.
- ImagePickerExample5.cs a cross platform aware, event based image loader (note ImagePicker for Android is still Android only).
- Image rotation corrected on devices that store rotated images
- Add to Gallery and a method to get the public Pictures folder for saving images (see Example5).
- Optional callback when user cancels, if no callback is specified Unity will receive null in the image receiver callback.
- Additional debug callbacks (verbose, warning and error) which work seperate from the debug setting.
Each callback will return that specific level of information (see Example5).
Note: Due to certain limitations, not all logcat information is sent (receiving files and adding to gallery is logcat only as there is no Unity link available at that stage).

Bugfixes:
- images not loading on specific devices (Samsung Galaxy S3 and Galaxy Nexus)
- Example 4 now correctly sets useDefault for the buttons added in the previous version
- changes made in ImagePickerData.cs made MonoDevelop cry
- ImagePickerData.cs now public to fix namespace issues

Changed:
- Extra debug line to test if JNI was succesfull in ImagePicker
- ImagePicker wrapper now also has awake
- Example 1 with a much cleaner GUI
- Polished out all warnings from the import of this plugin, except for one that reminds you this is an Android only plugin
- Updated various comments and readme


Version 5:
Based on user feedback (this could be you!) I added a new feature:
New: 
- Launch default camera or gallery, or show a dialog that allows default to be set
The photo cube demo has been updated with two additional buttons to switch between the different scenario's.

Changed:
- Example 2 and 4 (cube and gallery) can now either open the default or show a image picker

Fixed:
- inconsistent filenames when directly calling openFile directly
- Example 2 used a wrong implementaiton of the imageSetter action in ScanForFile
- Corrected Namespace for Example4

Derp:
- Manifest does not need camera permissions, commented out in the manifest just in case I am wrong.


Version 4:
- Added System.Action<Texture2D> alternatives to most API funcitons
- Added the ability to Receive Files from external apps (requires some additional lines in the AndroidManifest)
- Added function to process files manually (scale down and cache a known external file)
- Added Example 3 (options) and Example 4 (basic 3d Gallery) to showcase the new functionalities.
- Example 3 also contains a example of how to scan for files using C# File and Directory
- Changed Example instructions to black for readability
- Added Second Example scene to the APK
- Fixed a bug that caused .jpg.jpg and .jpeg.jpg

Version 3:
- Added comments and code to example for Logcat debugging
- Added showCamera, please help test this on as many devices as possible
- Added optional Camera permissions (needed to show/use Camera apps)
- Changed example code to make supplying the currentActivity optional

- Added a request for review in the version history ( I want to hear from you! )

Version 2:
- Multiple scaling options
- Stores a copy in your apps resource path, from there you can load, remove, etc. 
- Prevents Unity from crashing when loading huge files by letting you set size restrictions
- Does not block other plug-ins, by being just an Activity instead of a UnityPlayerActivity

Version 1:
- Initial internal release