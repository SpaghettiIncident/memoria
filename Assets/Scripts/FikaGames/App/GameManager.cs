using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	TouchManager _touch_manager;

	[SerializeField]
	Fade fade = null;

	string _current_scene_name;

	enum STATE
	{
		INITIALIZE,
		UPDATE,
		FINALIZE,
	}

	STATE _main_state;

	new void Awake() {
		_main_state = STATE.INITIALIZE;

		// 既に存在すれば処理を抜ける
		if (this != Instance) {
			Destroy(this);
			return;
		}

		_current_scene_name = Define.SCENE_TOP;
	}

	void Start()
    {
		Debug.Log(" ----- gm start ----- ");

		this._touch_manager = new TouchManager();

		// 通常トップシーン読み込み
		// 開発中はデバッグトップへ
		StartCoroutine(SetStartScene(_current_scene_name, () =>
				{
					_main_state = STATE.UPDATE;
				})
			);
	}

    void Update()
    {
		switch (_main_state)
		{
			case STATE.INITIALIZE:
				break;

			case STATE.UPDATE:
				break;

			case STATE.FINALIZE:
				break;
		}

	}

	// デバッグ情報
	private void OnGUI() {

		// シーン情報表示
		GUI.BeginGroup(new Rect(10, 10, 200, 100));
		GUI.Box(new Rect(0, 0, 200, 100), "SceneInfo");
		GUI.Label(new Rect(25, 20, 200, 20), "Current State : " + _main_state);
		GUI.Label(new Rect(25, 40, 200, 20), "Current Scene : " + _current_scene_name);
		//float aup = (async_unload == null) ? 0 : async_unload.progress;
		//GUI.Label(new Rect(25, 60, 200, 20), "Unload progress : " + aup);
		//float alp = (async_load == null) ? 0 : async_load.progress;
		//GUI.Label(new Rect(25, 80, 200, 20), "load progress : " + alp);

		GUI.EndGroup();

		// テスト用シーン遷移ボタン
		GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 100));

		if (GUI.Button(new Rect(0, 0, 100, 30), "Change Scene")) {

			// トップシーンからメインシーンへ
			if (_current_scene_name == Define.SCENE_TOP) {
				//				group.blocksRaycasts = false;
				StartCoroutine(SceneChange(Define.SCENE_GAME));
			}


			if (_current_scene_name == Define.SCENE_GAME) {
				StartCoroutine(SceneChange(Define.SCENE_TOP));
			}
		}

		if (GUI.Button(new Rect(0, 30, 100, 30), "Fade In")) {
			fade.FadeIn(1.0f, () => {


			});
		}

		if (GUI.Button(new Rect(0, 60, 100, 30), "Fade Out")) {
			fade.FadeOut(1.0f, () => {


			});
		}
		GUI.EndGroup();
	}

	// 初めのシーンへ遷移
	IEnumerator SetStartScene(string startSceneName, System.Action action) 
	{
		yield return StartCoroutine(LoadScene(startSceneName));

		yield return StartCoroutine(fade.FadeInAndWait(1.0f));

		if (action != null) {
			action();
		}
	}

	// シーン遷移
	IEnumerator SceneChange(string nextSceneName) 
	{
		// フェードアウト開始して待つ
		yield return StartCoroutine(fade.FadeOutAndWait(1.0f));

		// 現在のシーンを破棄、次のシーンを読み込み
		yield return StartCoroutine(SceneChange(_current_scene_name, nextSceneName));

		// 読み込んだシーン名を覚えておく
		_current_scene_name = nextSceneName;

		//　フェードイン開始して待つ
		yield return StartCoroutine(fade.FadeInAndWait(1.0f));


		// 初期化完了を待つ
	}

	public IEnumerator SceneChange(string currentSceneName, string nextSceneName) {

		// 非同期で現在のシーンを破棄
		yield return StartCoroutine(UnloadScene(currentSceneName));

		// 非同期で読み込み開始
		yield return StartCoroutine(LoadScene(nextSceneName));

	}

	AsyncOperation async_unload = null;

	// 非同期でシーンを破棄
	IEnumerator UnloadScene(string sceneName) {
		async_unload = SceneManager.UnloadSceneAsync(sceneName);
		async_unload.allowSceneActivation = false;

		while (!async_unload.isDone) {
			if (async_unload.progress >= 0.9f) {
				async_unload.allowSceneActivation = true;
			}

			yield return null;
		}
	}


	AsyncOperation async_load = null;
	// 非同期で読み込み開始
	IEnumerator LoadScene(string sceneName) {
		async_load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		async_load.allowSceneActivation = false;

		while (!async_load.isDone) {
			if (async_load.progress >= 0.9f) {
				async_load.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}

