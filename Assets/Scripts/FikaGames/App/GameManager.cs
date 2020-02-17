using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	TouchManager _touch_manager;
//	private Define.SCENE_KIND _scene_kind;
	private string _scene_name = "test";

	[SerializeField]
	Fade fade = null;

	new void Awake() {
		// 既に存在すれば処理を抜ける
		if (this != Instance) {
			Destroy(this);
			return;
		}

		// シーンをまたいでも破棄されないように設定
		DontDestroyOnLoad(this.gameObject);
		DontDestroyOnLoad(fade);
	}

	void Start()
    {
		Debug.Log(" ----- gm start ----- ");

		this._touch_manager = new TouchManager();
	}

    void Update()
    {


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
//				group.blocksRaycasts = false;
				fade.FadeIn(0.5f, () =>
				{
					//					image.color = (isMainColor) ? color1 : color2;
					//					isMainColor = !isMainColor;
					//					fade.FadeOut(1, () => {
					//						group.blocksRaycasts = true;
					//					});
					//					fade.FadeOut(1);
					SceneManager.LoadScene(Define.SCENE_MAIN);
				});
			}
			if (SceneManager.GetActiveScene().name == Define.SCENE_MAIN) {
				SceneManager.LoadScene(Define.SCENE_TOP);
			}
		}
		GUI.EndGroup();
	}
}
