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
    async void OnButtonClick()
    {
        button.gameObject.SetActive(false);
        progressText.gameObject.SetActive(true);

        await loader.LoadModelAsync(avatarUri, ProgressReport);        
        progressText.gameObject.SetActive(false);        
        SkinnedMeshRenderer[] meshRenderes = loader.avatarObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        MetaPersonUtils.ReplaceAvatar(loader.avatarObject, existingAvatar);
        Dictionary<string, SkinnedMeshRenderer> meshes = meshRenderes.ToDictionary(m => m.name, m => m);
        Dictionary<string, string> blendshapes = new Dictionary<string, string>() {
            {"saySmall","DD" },
            {"sayMedium","oh" },
            {"sayLarge","aa" },
        };

        foreach (var viseme in salsa.visemes)
        {
            string blenshapeName = blendshapes[viseme.expData.name];
            if (viseme.expData.controllerVars.Count == 2)
            {
                viseme.expData.controllerVars[0].maxShape = 0.01f;
                viseme.expData.controllerVars[0].smr = meshes["AvatarHead"];
                viseme.expData.controllerVars[0].blendIndex = meshes["AvatarHead"].sharedMesh.GetBlendShapeIndex(blenshapeName);
                viseme.expData.controllerVars[1].maxShape = 0.01f;
                viseme.expData.controllerVars[1].smr = meshes["AvatarTeethLower"];
                viseme.expData.controllerVars[1].blendIndex = meshes["AvatarTeethLower"].sharedMesh.GetBlendShapeIndex(blenshapeName);
            }
        }

        foreach(var emote in salsa.emoter.emotes)
        {
            for(int i = 0; i < emote.expData.controllerVars.Count; i++)
            {
                var controllerVar = emote.expData.controllerVars[i];
                controllerVar.smr = meshes["AvatarHead"];
                controllerVar.maxShape = 0.008f;
                controllerVar.blendIndex = meshes["AvatarHead"].sharedMesh.GetBlendShapeIndex(emote.expData.components[i].name);
            }
        }
        
        salsa.emoter.UpdateEmoteLists();
        salsa.emoter.UpdateExpressionControllers();
        salsa.emoter.configReady = true;
        salsa.emoter.Initialize();

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
