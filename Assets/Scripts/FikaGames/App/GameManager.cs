using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	TouchManager _touch_manager;
//	private Define.SCENE_KIND _scene_kind;
	private string _scene_name = "test";
	new void Awake() {
		// 既に存在すれば処理を抜ける
		if (this != Instance) {
			Destroy(this);
			return;
		}

		// シーンをまたいでも破棄されないように設定
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
    {
		Debug.Log(" ----- gm start ----- ");

		this._touch_manager = new TouchManager();
	}

    void Update()
    {
//		this._touch_manager.update();

		// タッチ取得
//		TouchManager touch_state = this._touch_manager.getTouch();

		// タッチされていたら処理
/*		if (touch_state._touch_flag) {
			if (touch_state._touch_phase == TouchPhase.Began) {
				// タッチした瞬間の処理
			}
			if (touch_state._touch_phase == TouchPhase.Ended) {
				if (SceneManager.GetActiveScene().name == Define.SCENE_TOP) {
					SceneManager.LoadScene(Define.SCENE_MAIN);
				}
				if (SceneManager.GetActiveScene().name == Define.SCENE_MAIN) {
//					Util.LoadScene(Define.SCENE_KIND.SCENE_TOP);
					SceneManager.LoadScene(Define.SCENE_TOP);
				}
			}


		}*/
	}

	private void OnGUI() {

		// シーン情報表示
		GUI.BeginGroup(new Rect(10, 10, 200, 100));
		GUI.Box(new Rect(0, 0, 200, 100), "SceneInfo");
		GUI.Label(new Rect(25, 25, 200, 30), "Current Scene : " + SceneManager.GetActiveScene().name);
		GUI.EndGroup();

		// テスト用シーン遷移ボタン
		GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 100));
		if (GUI.Button(new Rect(0, 0, 100, 30), "Change Scene")) {
			if (SceneManager.GetActiveScene().name == Define.SCENE_TOP) {
				SceneManager.LoadScene(Define.SCENE_MAIN);
			}
			if (SceneManager.GetActiveScene().name == Define.SCENE_MAIN) {
				SceneManager.LoadScene(Define.SCENE_TOP);
			}
		}
		GUI.EndGroup();
	}
}
