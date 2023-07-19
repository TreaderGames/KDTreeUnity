//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using System;
//using UnityEngine.ResourceManagement.AsyncOperations;
//using System.Collections;

//public enum ScreenType
//{
//    GameOver,
//}

//public class ScreenLoader : Singleton<ScreenLoader>
//{
//    [Serializable]
//    public struct ScreenData
//    {
//        public ScreenType screenType;
//        public AssetReferenceGameObject assetRef;
//    }

//    public struct ResourceData
//    {
//        public ScreenType screenType;
//        public Action<object> callback;
//        public AsyncOperationHandle<GameObject> opHandle;

//        public ResourceData(ScreenType inScreenType, Action<object> inCallback, AsyncOperationHandle<GameObject> operationHandle)
//        {
//            screenType = inScreenType;
//            callback = inCallback;
//            opHandle = operationHandle;
//        }
//    }

//    //private List<ResourceData> resourceQueue = new List<ResourceData>();

//    [SerializeField] List<ScreenData> screenDataList = new List<ScreenData>();

//    #region Public

//    public void LoadScreen(ScreenType screenType, Action<object> callback)
//    {
//        ScreenData screenData = GetScreenData(screenType);
//        AsyncOperationHandle<GameObject> opHandle = Addressables.LoadAssetAsync<GameObject>(screenData.assetRef);
//        ResourceData resourceData = new ResourceData(screenType, callback, opHandle);
//        StartCoroutine(WaitForResourceLoaded(resourceData));
//    }

//    #endregion

//    #region Private
//    private ScreenData GetScreenData(ScreenType screenType)
//    {
//        return screenDataList.Find(e => e.screenType.Equals(screenType));
//    }

//    private IEnumerator WaitForResourceLoaded(ResourceData resourceData)
//    {
//        yield return resourceData.opHandle;

//        if (resourceData.opHandle.Status == AsyncOperationStatus.Succeeded)
//        {
//            GameObject obj = resourceData.opHandle.Result;
//            Instantiate(obj);
//            resourceData.callback?.Invoke(resourceData.opHandle.Result);
//        }
//    }
//    #endregion
//}
