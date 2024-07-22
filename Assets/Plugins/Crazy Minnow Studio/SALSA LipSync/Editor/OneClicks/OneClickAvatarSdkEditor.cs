using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace CrazyMinnow.SALSA.OneClicks
{
    public class OneClickAvatarSdkEditor : Editor
    {
        private delegate void SalsaOneClickChoice(GameObject gameObject);
        private static SalsaOneClickChoice _salsaOneClickSetup = OneClickAvatarSdk.Setup;

        private delegate void EyesOneClickChoice(GameObject gameObject);
        private static EyesOneClickChoice _eyesOneClickSetup = OneClickAvatarSdkEyes.Setup;

        [MenuItem("GameObject/Crazy Minnow Studio/SALSA LipSync/One-Clicks/Avatar SDK")]
        public static void OneClickSetup_AvatarSDK()
        {
            _salsaOneClickSetup = OneClickAvatarSdk.Setup;
            _eyesOneClickSetup = OneClickAvatarSdkEyes.Setup;

            OneClickSetup();
        }

        public static void OneClickSetup()
        {
            GameObject go = Selection.activeGameObject;
            if (go == null)
            {
                Debug.LogWarning(
                    "NO OBJECT SELECTED: You must select an object in the scene to apply the OneClick to.");
                return;
            }

            ApplyOneClick(go);
        }

        private static void ApplyOneClick(GameObject go)
        {
            _salsaOneClickSetup(go);
            _eyesOneClickSetup(go);

            // add QueueProcessor
            OneClickBase.AddQueueProcessor(go);

            // config AudioSource
            var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(OneClickBase.RESOURCE_CLIP);
            OneClickBase.ConfigureSalsaAudioSource(go, clip, true);
        }
    }
}