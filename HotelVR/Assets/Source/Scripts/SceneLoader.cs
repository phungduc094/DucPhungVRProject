using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /*public AssetReference scence;*/
    public string scene;

    private AsyncOperationHandle<SceneInstance> handle;

    public void LoadScene()
    {
        StartCoroutine(DoLoadScene());
    }

    private IEnumerator DoLoadScene()
    {
        var loadScene = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive);
        loadScene.Completed += SceneLoadComplete;

        Debug.Log("Starting loading scene");
        while(!loadScene.IsDone)
        {
            var status = loadScene.GetDownloadStatus();
            float progress = status.Percent;
            SelectActivitiesUI.instance.UpdateProgressLoadingScene(status.Percent);
            yield return null;
        }
    }

    private void SceneLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(obj.Result.Scene.name + "successfully loaded.");
            handle = obj;

            CameraController.instance.DeactiveCamUI();
            SelectActivitiesUI.instance.Deactive();
            //CameraController.instance.IntoVRScene();
        }
    }

    public void UnloadScene()
    {
        Addressables.UnloadSceneAsync(handle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Successfully unloaded scence.");
            }
        };
    }
}