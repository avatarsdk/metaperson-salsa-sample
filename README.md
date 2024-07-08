# MetaPerson - Unity SALSA LipSync Sample

This sample demonstrates how to use [MetaPerson](https://metaperson.avatarsdk.com/) avatars in Unity with [SALSA LipSync](https://crazyminnowstudio.com/docs/salsa-lip-sync/modules/overview/) package.

![Sample in Unity](./Documentation/Images/unity_screen.png "SALSA Sample")

### Requirements
- Unity 2021.3.19f1 or newer
- [SALSA LipSync](https://assetstore.unity.com/packages/tools/animation/salsa-lipsync-suite-148442) package.

## Getting Started
1. Clone this repository to your computer
2. Open the project in Unity 2021.3.19f1 or newer.
3. Import the [SALSA LipSync Suite](https://assetstore.unity.com/packages/tools/animation/salsa-lipsync-suite-148442) package.
4. Open the scene MetapersonSalsaSampleScene located in Assets\AvatarSDK\MetaPerson\SalsaSample\Scenes
5. Run the demo project, click on the "Load another avatar" button to see how the avatar can be replaced in runtime.

## How does it work
There is the predefined avatar on the scene that is animated with SALSA when you run the project. When you click the button, another avatar is downloaded. Then it replaces the original one. Audio and facial animation keep playing continuously for the new avatar. The MetapersonAvatar object placed on the scene contains the predefined MetaPerson avatar and has a number of attached components. SALSA component is responsible for LipSync configuration, Audio Source and Queue Processor are responsible for playing and processing the audio. EmoteR component is optional and provides additional avatar emote settings. MetaPerson Loader handles the process of downloading and displaying a new avatar on the scene. MetaPerson Material Generator is required to provide configured materials for the Metaperson skeletal mesh.