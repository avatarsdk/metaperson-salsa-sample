using CrazyMinnow.SALSA;
using CrazyMinnow.SALSA.OneClicks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AvatarSdkSalsaTools
{
    public static void Configure(GameObject avatarObj, GameObject parentObj, AudioClip audioClip = null)
    {
        OneClickAvatarSdkEyes.BlendshapeScale = OneClickAvatarSdk.BlendshapeScale = AvatarSdkSalsaTools.GetMaxBlendshapesValue(avatarObj);
        OneClickAvatarSdk.Setup(parentObj);
        OneClickAvatarSdkEyes.Setup(parentObj);

        if (parentObj.GetComponent<QueueProcessor>() == null)
        {
            // add QueueProcessor
            OneClickBase.AddQueueProcessor(parentObj);
        }

        if (audioClip != null)
        {
            OneClickBase.ConfigureSalsaAudioSource(parentObj, audioClip, true);
        }

        var eyes = parentObj.GetComponent<Eyes>();
        eyes.queueProcessor = parentObj.GetComponent<QueueProcessor>();
        var salsa = parentObj.GetComponent<Salsa>();
        salsa.emoter.queueProcessor = parentObj.GetComponent<QueueProcessor>();
        salsa.queueProcessor = parentObj.GetComponent<QueueProcessor>();

        salsa.audioSrc = parentObj.GetComponent<AudioSource>();
        salsa.DistributeTriggers(LerpEasings.EasingType.SquaredIn);
        salsa.AdjustAnalysisSettings();
        salsa.UpdateExpressionControllers();
        salsa.queueProcessor.ResetQueues();
        salsa.configReady = true;
        salsa.Initialize();
    }
    public static float GetMaxBlendshapesValue(GameObject gameObject)
    {
        SkinnedMeshRenderer[] meshRenderes = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        var headMesh = meshRenderes.FirstOrDefault(loader => loader.name == "AvatarHead");
        if (headMesh == null)
        {
            return 100.0f;
        }
        int blenshapeIdx = headMesh.sharedMesh.GetBlendShapeIndex("FF");
        if (blenshapeIdx > -1)
        {
            var res = meshRenderes.FirstOrDefault(mr => mr.name == "AvatarHead").sharedMesh.GetBlendShapeFrameWeight(blenshapeIdx, 0);
            return res;
        }
        return 100.0f;
    }
}
