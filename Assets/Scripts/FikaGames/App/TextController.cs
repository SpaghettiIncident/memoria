
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

using UnityEngine.U2D;
public class TextController : MonoBehaviour
{
	// public string[] scenarios;
	[SerializeField] Text uiText;

	[SerializeField]
	[Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int currentLine = 0;
	private int lastUpdateCharacter = -1;
	public Canvas _canvas;
	GameObject _windowObj;
	private Text _windowText;
	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText {
		get {
			return Time.time > timeElapsed + timeUntilDisplay;
		}
	}
	SpriteAtlas _spriteAtlas;
	private GameObject _chara_0;
	script _script;
	void Start() {
		string filePath = Util.GetScenarioFilePath("test.csv");
		LoadScenario(filePath);
		SetNextLine();

		_chara_0 = new GameObject("SpeakerCharacter");
		var script = _chara_0.AddComponent<SpeakerCharacter>();
		//_chara_0.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

		GameObject obj = (GameObject)Resources.Load("Prefab/window");
		GameObject instance = (GameObject)Instantiate(obj,
													  new Vector3(0.0f, 0.0f, 0.0f),
													  Quaternion.identity);
		instance.transform.SetParent(_canvas.transform, false);
		_script = instance.GetComponent<script>();
//		instance.transform.position = new Vector3(350.0f, 150.0f, 0.0f);
		_script.SetWH(300.0f, 100.0f);
		

		//_windowObj = new GameObject("window");
		//_windowObj.AddComponent<Window>();
		//_windowObj.transform.SetParent(_canvas.transform);
//		_spriteAtlas = Resources.Load<SpriteAtlas>("Character/kohaku");
//		script.SetSpriteAtlas(_spriteAtlas);
		//_chara_0 = new GameObject("SpeakerCharacter");
		//var script = _chara_0.AddComponent<SpeakerCharacter>();
		//script.SetSpriteAtlas(spriteAtlas);

		//		StartCoroutine(load());
	}

	IEnumerator load()
	{
		var spriteAtlas = Resources.LoadAsync<SpriteAtlas>("Character/kohaku");

		while (!spriteAtlas.isDone)
		{
			if (spriteAtlas.progress >= 0.9f)
			{
				_chara_0 = new GameObject("SpeakerCharacter");
				var script = _chara_0.AddComponent<SpeakerCharacter>();
				script.SetSpriteAtlas(spriteAtlas.asset as SpriteAtlas);
			}

			yield return null;
		}
	}
	void Update() {
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if (IsCompleteDisplayText) {
			if (currentLine < _scenarioDataList.Count && Input.GetMouseButtonDown(0))
				{
					SetNextLine();
			}
		}
		else {
			// 完了してないなら文字をすべて表示する
			if (Input.GetMouseButtonDown(0)) {
				timeUntilDisplay = 0;
			}
		}

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if (displayCharacterCount != lastUpdateCharacter) {
			if (currentText != "")
			{
//				uiText.text = currentText.Substring(0, displayCharacterCount);
				_script.SetText(currentText.Substring(0, displayCharacterCount));
			}
			else
			{
//				uiText.text = "";
				_script.SetText("");
			}

			lastUpdateCharacter = displayCharacterCount;
		}
	}


	void SetNextLine() {
		currentText = _scenarioDataList[currentLine].Text;
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine++;
		lastUpdateCharacter = -1;
	}

	List<ScenarioDataLine> _scenarioDataList = new List<ScenarioDataLine>();
	void LoadScenario(string path)
	{
		_scenarioDataList.Clear();

		try
		{
			using (StreamReader sr = new StreamReader(path))
			{
				while (!sr.EndOfStream)
				{
					//1行づつ読み取る。カンマも読み取っている。
					string line = sr.ReadLine();

					//カンマで区切った文の塊を格納する
					string[] values = line.Split(',');

					ScenarioDataLine Scenarioline = new ScenarioDataLine(values);
					_scenarioDataList.Add(Scenarioline);
				}
			}
		}
		catch (System.Exception e)
		{
			Debug.Log("シナリオファイルが見つかりません");
		}

	}

	class ScenarioDataLine
	{
		public string Command;
		public string Arg1;
		public string Arg2;
		public string Text;
		public string Speaker;

		// キーワード「params」で可変長引数になる
		public ScenarioDataLine(params string[] ary)
		{
			for (int i = 0; i < ary.Length; i++)
			{
				switch (i)
				{
					case 0:
						Command = ary[i];
						break;

					case 1:
						Arg1 = ary[i];
						break;

					case 2:
						Arg2 = ary[i];
						break;

					case 3:
						Text = ary[i];
						break;

					case 4:
						Speaker = ary[i];
						break;

					default:
						break;
				}
			}
		}
	}
}