using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public IEnumerator SceneChange(string currentSceneName, string nextSceneName) {

		// 非同期で現在のシーンを破棄
		yield return StartCoroutine(UnloadScene(currentSceneName));

		// 非同期で読み込み開始
		yield return StartCoroutine(LoadScene(nextSceneName));

	}

	// 非同期でシーンを破棄
	IEnumerator UnloadScene(string sceneName) 
	{
		AsyncOperation async_unload = SceneManager.UnloadSceneAsync(sceneName);
		async_unload.allowSceneActivation = false;

		while (!async_unload.isDone) {
			if (async_unload.progress >= 0.9f) {
				async_unload.allowSceneActivation = true;
			}

			yield return null;
		}
	}

	// 非同期で読み込み開始
	IEnumerator LoadScene(string sceneName)
	{
		AsyncOperation async_load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		async_load.allowSceneActivation = false;

		while (!async_load.isDone) {
			if (async_load.progress >= 0.9f) {
				async_load.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}
