
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

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

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText {
		get {
			return Time.time > timeElapsed + timeUntilDisplay;
		}
	}

	void Start() {
		string filePath = Util.GetScenarioFilePath("test.csv");
		LoadScenario(filePath);
		SetNextLine();
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
				uiText.text = currentText.Substring(0, displayCharacterCount);
			}
			else
			{
				uiText.text = "";
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