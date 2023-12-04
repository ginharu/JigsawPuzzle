using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Script
{
    public class LoadingRoot : MonoBehaviour
    {
        public static LoadingRoot Instance;

        [FormerlySerializedAs("Progress")] public Image progress;

        [FormerlySerializedAs("LoadingText")] public TMP_Text loadingText;

        [FormerlySerializedAs("LoadScene")] public string loadScene = "Login";

        public GameObject OnLoadingWaiter;

       // public static SystemManager sysManager;
        [FormerlySerializedAs("LoadFinish")] [HideInInspector] public bool loadFinish;

        private float _lastProgress = 0;

        private static readonly int Counter = 0;

        private void Awake()
        {
            Instance = this;
            StartCoroutine(ProgressData.Init());
        }
        
        
        private void Start()
        {
            _lastProgress = 0;
            StartCoroutine(LoadAsynchronously());
        }

        public void SetLoadScene(string scene = "Game")
        {
            loadScene = scene;
        }


        private IEnumerator InitSdk()
        {
            SRDebug.Init();
            //TKGSDKManager.Instance.InitSDK();
            Application.targetFrameRate = 60;
            yield break;
        }

        IEnumerator LoadAsynchronously()
        {
            float startProgress = _lastProgress;

            yield return InitSdk();
            LogUtil.Log("SDK 初始化完成");
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
            while (!operation.isDone)
            {
                float endProgress = startProgress + Mathf.Clamp01(operation.progress / 0.9f) * (1 - startProgress);
                yield return GoProgress(endProgress);
                if (progress.fillAmount >= 1)
                {
                    LogUtil.Log($"场景加载完成 {progress.fillAmount}");
                    break;
                }
                
            }
        }
    
        private IEnumerator GoProgress(float endProgress)
        {
            while (_lastProgress < endProgress || !loadFinish)
            {
                UpdateProgress(_lastProgress + 0.5f);
                yield return null;
            }
        }

        private void UpdateProgress(float lastProgress) {
            if (lastProgress >=1)
            {
                lastProgress = 1;
            }
            _lastProgress = lastProgress;
            progress.fillAmount = this._lastProgress*1f;
            loadingText.text = $"{this._lastProgress * 100:0}%";
        }
    }
}
