using UnityEngine;

namespace CrazyMinnow.SALSA.OneClicks
{
    public class OneClickAvatarSdkEyes : MonoBehaviour
    {
        public static float BlendshapeScale = 100.0f;

        public static void Setup(GameObject go)
        {
            string head = "^head$";
            string[] body = new string[] { "^.+(Head)$" };
            string[] eyeL = new string[] { "LeftEye$" };
            string[] eyeR = new string[] { "RightEye$" };
            string[] blinkL = new string[] { "eyeBlinkLeft" };
            string[] blinkR = new string[] { "eyeBlinkRight" };

            if (go)
            {
                Eyes eyes = go.GetComponent<Eyes>();
                if (eyes == null)
                {
                    eyes = go.AddComponent<Eyes>();
                }
                else
                {
                    DestroyImmediate(eyes);
                    eyes = go.AddComponent<Eyes>();
                }

                // System Properties
                eyes.characterRoot = go.transform;

                // Heads - Bone_Rotation
                eyes.BuildHeadTemplate(Eyes.HeadTemplates.Bone_Rotation_XY);
                eyes.heads[0].expData.name = "head";
                eyes.heads[0].expData.components[0].name = "head";
                eyes.heads[0].expData.controllerVars[0].bone = Eyes.FindTransform(eyes.characterRoot, head);
                eyes.headTargetOffset.y = 0.065f;
                eyes.CaptureMin(ref eyes.heads);
                eyes.CaptureMax(ref eyes.heads);

                // Eyes - Bone_Rotation
                eyes.BuildEyeTemplate(Eyes.EyeTemplates.Bone_Rotation);
                eyes.eyes[0].expData.controllerVars[0].bone = Eyes.FindTransform(eyes.characterRoot, eyeL);
                eyes.eyes[0].expData.name = "eyeL";
                eyes.eyes[0].expData.components[0].name = "eyeL";
                eyes.eyes[1].expData.controllerVars[0].bone = Eyes.FindTransform(eyes.characterRoot, eyeR);
                eyes.eyes[1].expData.name = "eyeR";
                eyes.eyes[1].expData.components[0].name = "eyeR";
                eyes.CaptureMin(ref eyes.eyes);
                eyes.CaptureMax(ref eyes.eyes);

                // Eyelids - Bone_Rotation
                eyes.BuildEyelidTemplate(Eyes.EyelidTemplates.BlendShapes, Eyes.EyelidSelection.Upper); // includes left/right eyelid
                float blinkMax = 0.01f * BlendshapeScale;
                // Left eyelid
                eyes.blinklids[0].expData.controllerVars[0].smr = Eyes.FindTransform(eyes.characterRoot, body).GetComponent<SkinnedMeshRenderer>();
                eyes.blinklids[0].expData.controllerVars[0].blendIndex = Eyes.FindBlendIndex(eyes.blinklids[0].expData.controllerVars[0].smr, blinkL);
                eyes.blinklids[0].expData.controllerVars[0].maxShape = blinkMax;
                eyes.blinklids[0].expData.name = "eyelidL";
                // Right eyelid
                eyes.blinklids[1].expData.controllerVars[0].smr = eyes.blinklids[0].expData.controllerVars[0].smr;
                eyes.blinklids[1].expData.controllerVars[0].blendIndex = Eyes.FindBlendIndex(eyes.blinklids[0].expData.controllerVars[0].smr, blinkR);
                eyes.blinklids[1].expData.controllerVars[0].maxShape = blinkMax;
                eyes.blinklids[1].expData.name = "eyelidR";

                // Track lids
                eyes.CopyBlinkToTrack();
                if (eyes.tracklids.Count > 1)
                {
                    eyes.tracklids[0].referenceIdx = 0; // left eye
                    eyes.tracklids[1].referenceIdx = 1; // right eye
                }

                if (eyes.tracklids.Count > 3)
                {
                    eyes.tracklids[2].referenceIdx = 0; // left eye
                    eyes.tracklids[3].referenceIdx = 1; // right eye
                }

                // Initialize the Eyes module
                eyes.Initialize();
            }
        }
    }
}