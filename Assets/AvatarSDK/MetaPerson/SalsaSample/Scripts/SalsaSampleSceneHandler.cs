/* Copyright (C) Itseez3D, Inc. - All Rights Reserved
* You may not use this file except in compliance with an authorized license
* Unauthorized copying of this file, via any medium is strictly prohibited
* Proprietary and confidential
* UNLESS REQUIRED BY APPLICABLE LAW OR AGREED BY ITSEEZ3D, INC. IN WRITING, SOFTWARE DISTRIBUTED UNDER THE LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR
* CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED
* See the License for the specific language governing permissions and limitations under the License.
* Written by Itseez3D, Inc. <support@avatarsdk.com>, July 2024
*/

using AvatarSDK.MetaPerson.Loader;
using CrazyMinnow.SALSA;
using CrazyMinnow.SALSA.OneClicks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SalsaSampleSceneHandler : MonoBehaviour
{
    public Button button;
    public MetaPersonLoader loader;
    public GameObject dstObject;
    public Salsa salsa;
    public Text progressText;
    public AudioSource audioSource;
    public GameObject existingAvatar;
    const string avatarUri = "https://metaperson.avatarsdk.com/avatars/b255d298-7644-48ec-85ef-4a2200668458/model.glb";
    // Start is called before the first frame update
    void Start()
    {
        progressText.gameObject.SetActive(false);   
        button.onClick.AddListener(OnButtonClick);
    }
    void ProgressReport(float progress)
    {
        progressText.text = string.Format("Downloading avatar: {0}%", (int)(progress * 100));
    }
    void ReleaseSalsa() {
        salsa.TurnOffAll();
        salsa.enabled = false;
        salsa.emoter.enabled = false;

        salsa.queueProcessor = null;
        salsa.audioSrc = null;
        salsa.visemes.Clear();
        salsa.emoter.emotes.Clear();        
        salsa.emoter.configReady = false;
        salsa.configReady = false;
        
        var silenceAnalyzer = dstObject.GetComponent<SalsaAdvancedDynamicsSilenceAnalyzer>();
        silenceAnalyzer.enabled = false;

        DestroyImmediate(silenceAnalyzer);
        DestroyImmediate(salsa);
        DestroyImmediate(salsa.emoter);
    }
    void ReleaseSalsaEyes() {
        var eyes = dstObject.GetComponent<Eyes>();
        eyes.enabled = false;
        eyes.eyes.Clear();

        DestroyImmediate(eyes);
    }
    async void OnButtonClick()
    {
        button.gameObject.SetActive(false);
        progressText.gameObject.SetActive(true);

        await loader.LoadModelAsync(avatarUri, ProgressReport);        
        progressText.gameObject.SetActive(false);        
        SkinnedMeshRenderer[] meshRenderes = loader.avatarObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        ReleaseSalsa();
        ReleaseSalsaEyes();        

        MetaPersonUtils.ReplaceAvatar(loader.avatarObject, existingAvatar);

        OneClickAvatarSdkEyes.BlendshapeScale = OneClickAvatarSdk.BlendshapeScale = 1.0f;
        OneClickAvatarSdk.Setup(dstObject);
        
        OneClickAvatarSdkEyes.Setup(dstObject);
        var eyes = dstObject.GetComponent<Eyes>();
        eyes.queueProcessor = dstObject.GetComponent<QueueProcessor>();        
        salsa = dstObject.GetComponent<Salsa>();
        salsa.emoter.queueProcessor = dstObject.GetComponent<QueueProcessor>();
        salsa.queueProcessor = dstObject.GetComponent<QueueProcessor>();

        salsa.audioSrc = audioSource;
        salsa.DistributeTriggers(LerpEasings.EasingType.SquaredIn);
        salsa.AdjustAnalysisSettings();
        salsa.UpdateExpressionControllers();
        salsa.queueProcessor.ResetQueues();
        salsa.configReady = true;
        salsa.Initialize();
    }
    // Update is called once per frame
    void Update()
    {

    }

}
