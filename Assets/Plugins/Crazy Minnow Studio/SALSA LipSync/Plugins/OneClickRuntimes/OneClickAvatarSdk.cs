using UnityEngine;

namespace CrazyMinnow.SALSA.OneClicks
{
    public class OneClickAvatarSdk : OneClickBase
    {
        public static float BlendshapeScale = 100.0f;
        
        /// <summary>
        /// RELEASE NOTES:
        ///		: Initial release.
        /// ==========================================================================
        /// PURPOSE: This script provides simple, simulated lip-sync input to the
        ///		Salsa component from text/string values. For the latest information
        ///		visit crazyminnowstudio.com.
        /// ==========================================================================
        /// DISCLAIMER: While every attempt has been made to ensure the safe content
        ///		and operation of these files, they are provided as-is, without
        ///		warranty or guarantee of any kind. By downloading and using these
        ///		files you are accepting any and all risks associated and release
        ///		Crazy Minnow Studio, LLC of any and all liability.
        /// ==========================================================================
        /// </summary>
        public static void Setup(GameObject gameObject)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //	SETUP Requirements:
            //		use NewExpression("expression name") to start a new viseme/emote expression.
            //		use AddShapeComponent to add blendshape configurations, passing:
            //			- string array of shape names to look for.
            //			  : string array can be a single element.
            //			  : string array can be a single regex search string.
            //			    note: useRegex option must be set true.
            //			- optional string name prefix for the component.
            //			- optional blend amount (default = 1.0f).
            //			- optional regex search option (default = false).

            Init();

            #region SALSA-Configuration
            NewConfiguration(OneClickConfiguration.ConfigType.Salsa);
            {
                ////////////////////////////////////////////////////////
                // SMR regex searches (enable/disable/add as required).
                AddSmrSearch("^.+(Head)$");
                AddSmrSearch("^.+TeethLower$");

                ////////////////////////////////////////////////////////
                // Adjust SALSA settings to taste...
                // - data analysis settings
                autoAdjustAnalysis = true;
                autoAdjustMicrophone = false;
                audioUpdateDelay = 0.089798f;

                // - advanced dynamics settings
                loCutoff = 0.02f;
                hiCutoff = 0.38f;
                useAdvDyn = true;
                advDynPrimaryBias = 0.5f;
                useAdvDynJitter = false;
                advDynJitterAmount = 0.25f;
                advDynJitterProb = 0.25f;
                advDynSecondaryMix = 0f;
                emphasizerTrigger = 0f;

                ////////////////////////////////////////////////////////
                // Viseme setup...

                NewExpression("f");
                AddShapeComponent(new[] { "FF" }, 0.08f, 0f, 0.08f, "viseme_FF", 0.006f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "FF" }, 0.08f, 0f, 0.08f, "viseme_FF_teeth", 0.008f * BlendshapeScale, false, "^.+TeethLower$");
                AddShapeComponent(new[] { "mouthRollLower" }, 0.08f, 0f, 0.08f, "viseme_FF", 0.00772f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen", 0.00112f * BlendshapeScale, false, "^.+Head$");

                NewExpression("w");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen", 0.0015f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen_teeth", 0.0010f * BlendshapeScale, false, "^.+TeethLower$");
                AddShapeComponent(new[] { "mouthFunnel" }, 0.08f, 0f, 0.08f, "mouthFunnel", 0.0074f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthPucker" }, 0.08f, 0f, 0.08f, "mouthPucker", 0.0099f * BlendshapeScale, false, "^.+Head$");
                /*AddShapeComponent(new[] { "mouthUpperUpLeft" }, 0.08f, 0f, 0.08f, "mouthUpperUpLeft", 0.0075f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthUpperUpRight" }, 0.08f, 0f, 0.08f, "mouthUpperUpRight", 0.0075f * BlendshapeScale, false, "^.+Head$");*/

                NewExpression("t");
                AddShapeComponent(new[] { "DD" }, 0.08f, 0f, 0.08f, "viseme_DD", 0.0054f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "DD" }, 0.08f, 0f, 0.08f, "viseme_DD_teeth", 0.007f * BlendshapeScale, false, "^.+TeethLower$");

                NewExpression("th");
                AddShapeComponent(new[] { "TH" }, 0.08f, 0f, 0.08f, "viseme_TH", 0.0095f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen", 0.0015f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen_teeth", 0.0015f * BlendshapeScale, false, "^.+TeethLower$");

                NewExpression("ow");
                AddShapeComponent(new[] { "mouthFunnel" }, 0.08f, 0f, 0.08f, "mouthFunnel", 0.00077f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthPucker" }, 0.08f, 0f, 0.08f, "mouthPucker", 0.004f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "oh" }, 0.08f, 0f, 0.08f, "viseme_O", 0.004f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen", 0.0029f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen_teeth", 0.0029f * BlendshapeScale, false, "^.+TeethLower$");

                NewExpression("ee");
                AddShapeComponent(new[] { "^mouthLeft$" }, 0.08f, 0f, 0.08f, "mouthLeft", 0.00422f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "^mouthRight$" }, 0.08f, 0f, 0.08f, "mouthRight", 0.00422f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthSmileLeft" }, 0.08f, 0f, 0.08f, "mouthSmileL", 0.0082f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthSmileRight" }, 0.08f, 0f, 0.08f, "mouthSmileR", 0.0082f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen", 0.0011f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen_teeth", 0.0011f * BlendshapeScale, false, "^.+TeethLower$");
                AddShapeComponent(new[] { "mouthStretchLeft" }, 0.08f, 0f, 0.08f, "mouthStretchLeft", 0.0080f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthStretchRight" }, 0.08f, 0f, 0.08f, "mouthStretchRight", 0.0080f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthLowerDownLeft" }, 0.08f, 0f, 0.08f, "mouthLowerDownLeft", 0.0013f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthLowerDownRight" }, 0.08f, 0f, 0.08f, "mouthLowerDownRight", 0.0013f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthUpperUpLeft" }, 0.08f, 0f, 0.08f, "mouthUpperUpLeft", 0.0023f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "mouthUpperUpRight" }, 0.08f, 0f, 0.08f, "mouthUpperUpRight", 0.0022f * BlendshapeScale, false, "^.+Head$");

                NewExpression("oo");
                //AddShapeComponent(new[] { "mouthOpen" }, 0.08f, 0f, 0.08f, "mouthOpen", 0.00751f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "oh" }, 0.08f, 0f, 0.08f, "viseme_O", 0.0014f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "ou" }, 0.08f, 0f, 0.08f, "viseme_U", 0.0010f * BlendshapeScale, false, "^.+Head$");

                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen", 0.0045f * BlendshapeScale, false, "^.+Head$");
                AddShapeComponent(new[] { "jawOpen" }, 0.08f, 0f, 0.08f, "jawOpen_teeth", 0.0045f * BlendshapeScale, false, "^.+TeethLower$");

            }
            #endregion // SALSA-configuration

            #region EmoteR-Configuration
            NewConfiguration(OneClickConfiguration.ConfigType.Emoter);
            {
                ////////////////////////////////////////////////////////
                // SMR regex searches (enable/disable/add as required).
                AddSmrSearch("^.+Head$");

                useRandomEmotes = false;
                isChancePerEmote = true;
                numRandomEmotesPerCycle = 0;
                randomEmoteMinTimer = 1f;
                randomEmoteMaxTimer = 2f;
                randomChance = 0.5f;
                useRandomFrac = false;
                randomFracBias = 0.5f;
                useRandomHoldDuration = false;
                randomHoldDurationMin = 0.1f;
                randomHoldDurationMax = 0.5f;

                ////////////////////////////////////////////////////////
                // Emote setup...

                NewExpression("soften");
                AddEmoteFlags(false, true, false, 1f);
                AddShapeComponent(new[] { "mouthSmile" }, 0.25f, 0.1f, 0.2f, "mouthSmile", 0.00257f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browDownLeft" }, 0.25f, 0.1f, 0.2f, "browDownLeft", 0.00218f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browInnerUp" }, 0.25f, 0.1f, 0.2f, "browInnerUp", 0.00289f * BlendshapeScale, true);

                NewExpression("browsUp");
                AddEmoteFlags(false, true, false, 0.427f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browDownRight" }, 0.25f, 0.1f, 0.2f, "browDownRight", 0.0015f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browInnerUp" }, 0.25f, 0.1f, 0.2f, "browInnerUp", 0.00777f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browOuterUpLeft" }, 0.25f, 0.1f, 0.2f, "browOuterUpLeft", 0.00569f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browOuterUpRight" }, 0.25f, 0.1f, 0.2f, "browOuterUpRight", 0.00473f * BlendshapeScale, true);

                NewExpression("browUp");
                AddEmoteFlags(false, true, false, 1f);
                AddShapeComponent(new[] { "browDownRight" }, 0.25f, 0.1f, 0.2f, "browDownRight", 0.006469f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browInnerUp" }, 0.25f, 0.1f, 0.2f, "browInnerUp", 0.00623f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browOuterUpLeft" }, 0.25f, 0.1f, 0.2f, "browOuterUpLeft", 0.00128f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browOuterUpRight" }, 0.25f, 0.1f, 0.2f, "browOuterUpRight", 0.00051f * BlendshapeScale, true);

                NewExpression("puff");
                AddEmoteFlags(false, true, false, 1f);
                AddShapeComponent(new[] { "mouthPucker" }, 0.25f, 0.1f, 0.2f, "mouthPucker", 0.00231f * BlendshapeScale, true);
                AddShapeComponent(new[] { "mouthDimpleLeft" }, 0.25f, 0.1f, 0.2f, "mouthDimpleLeft", 0.0009f * BlendshapeScale, true);
                AddShapeComponent(new[] { "mouthDimpleRight" }, 0.25f, 0.1f, 0.2f, "mouthDimpleRight", 0.0018f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekPuff" }, 0.25f, 0.1f, 0.2f, "cheekPuff", 0.0023f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintLeft" }, 0.25f, 0.1f, 0.2f, "cheekSquintLeft", 0.00424f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintRight" }, 0.25f, 0.1f, 0.2f, "cheekSquintRight", 0.00629f * BlendshapeScale, true);
                AddShapeComponent(new[] { "noseSneerLeft" }, 0.25f, 0.1f, 0.2f, "noseSneerLeft", 0.00128f * BlendshapeScale, true);
                AddShapeComponent(new[] { "noseSneerRight" }, 0.25f, 0.1f, 0.2f, "noseSneerRight", 0.00199f * BlendshapeScale, true);

                NewExpression("scrunch");
                AddEmoteFlags(false, true, false, 1f);
                AddShapeComponent(new[] { "eyeSquintLeft" }, 0.25f, 0.1f, 0.2f, "eyeSquintLeft", 0.00282f * BlendshapeScale, true);
                AddShapeComponent(new[] { "eyeSquintRight" }, 0.25f, 0.1f, 0.2f, "eyeSquintRight", 0.00244f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browDownLeft" }, 0.25f, 0.1f, 0.2f, "browDownLeft", 0.0027f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browDownRight" }, 0.25f, 0.1f, 0.2f, "browDownRight", 0.00212f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintLeft" }, 0.25f, 0.1f, 0.2f, "cheekSquintLeft", 0.00501f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintRight" }, 0.25f, 0.1f, 0.2f, "cheekSquintRight", 0.00417f * BlendshapeScale, true);
                AddShapeComponent(new[] { "noseSneerLeft" }, 0.25f, 0.1f, 0.2f, "noseSneerLeft", 0.00205f * BlendshapeScale, true);
                AddShapeComponent(new[] { "noseSneerRight" }, 0.25f, 0.1f, 0.2f, "noseSneerRight", 0.0027f * BlendshapeScale, true);

                NewExpression("squint");
                AddEmoteFlags(false, true, false, 1f);
                AddShapeComponent(new[] { "eyeSquintLeft" }, 0.25f, 0.1f, 0.2f, "eyeSquintLeft", 0.00295f * BlendshapeScale, true);
                AddShapeComponent(new[] { "eyeSquintRight" }, 0.25f, 0.1f, 0.2f, "eyeSquintRight", 0.00346f * BlendshapeScale, true);
                AddShapeComponent(new[] { "mouthDimpleLeft" }, 0.25f, 0.1f, 0.2f, "mouthDimpleLeft", 0.0018f * BlendshapeScale, true);
                AddShapeComponent(new[] { "mouthDimpleRight" }, 0.25f, 0.1f, 0.2f, "mouthDimpleRight", 0.00148f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintLeft" }, 0.25f, 0.1f, 0.2f, "cheekSquintLeft", 0.00475f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintRight" }, 0.25f, 0.1f, 0.2f, "cheekSquintRight", 0.00366f * BlendshapeScale, true);

                NewExpression("focus");
                AddEmoteFlags(false, true, false, 1f);
                AddShapeComponent(new[] { "eyeSquintLeft" }, 0.25f, 0.1f, 0.2f, "eyeSquintLeft", 0.00481f * BlendshapeScale, true);
                AddShapeComponent(new[] { "eyeSquintRight" }, 0.25f, 0.1f, 0.2f, "eyeSquintRight", 0.00109f * BlendshapeScale, true);
                AddShapeComponent(new[] { "browDownRight" }, 0.25f, 0.1f, 0.2f, "browDownRight", 0.00295f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintLeft" }, 0.25f, 0.1f, 0.2f, "cheekSquintLeft", 0.00424f * BlendshapeScale, true);
                AddShapeComponent(new[] { "cheekSquintRight" }, 0.25f, 0.1f, 0.2f, "cheekSquintRight", 0.0034f * BlendshapeScale, true);
            }
            #endregion // EmoteR-configuration

            DoOneClickiness(gameObject);

            if (selectedObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>() == null)
                selectedObject.AddComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();

            //Darrin's Tweaks
            salsa.useTimingsOverride = true;
            salsa.globalDurON = 0.19f;
            salsa.globalDurOffBalance = -0.09f;
            salsa.globalNuanceBalance = -0.239f;

            emoter.NumRandomEmphasizersPerCycle = 4;
            EmoteExpression emote;
            emote = emoter.FindEmote("soften");
            if (emote != null)
                emote.frac = 1f;
            emote = emoter.FindEmote("browsUp");
            if (emote != null)
                emote.frac = 0.427f;
            emote = emoter.FindEmote("browUp");
            if (emote != null)
                emote.frac = 1f;
            emote = emoter.FindEmote("puff");
            if (emote != null)
                emote.frac = 1f;
            emote = emoter.FindEmote("scrunch");
            if (emote != null)
                emote.frac = 1f;
            emote = emoter.FindEmote("squint");
            if (emote != null)
                emote.frac = 1f;
            emote = emoter.FindEmote("focus");
            if (emote != null)
                emote.frac = 1f;

            var silenceAnalyzer = selectedObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();
            if (silenceAnalyzer)
            {
                silenceAnalyzer.silenceThreshold = 0.9f;
                silenceAnalyzer.timingStartPoint = 0.4f;
                silenceAnalyzer.timingEndVariance = 0.8f;
                silenceAnalyzer.silenceSampleWeight = 0.95f;
                silenceAnalyzer.bufferSize = 512;
            }
        }

        private static void UpdateExpression(string visemeName)
        {
            var currentConfiguration = oneClickConfigurations[oneClickConfigurations.Count - 1];
        }
    }
}
