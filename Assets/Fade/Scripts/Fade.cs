/*
 The MIT License (MIT)

Copyright (c) 2013 yamamura tatsuhiko

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Fade : MonoBehaviour
{
	IFade fade;
	public bool isFinished;
	float cutoutRange = 1.0f;

	void Start() {
		Init();
		fade.Range = cutoutRange;
	}

	void Init() {
		//		cutoutRange = 1.0f;
		fade = GetComponent<IFade>();
	}

	void OnValidate() {
		Init();
		fade.Range = cutoutRange;
	}

	public IEnumerator FadeOutAndWait(float fadeOutTime)
	{
		// フェードアウト開始
		FadeOut(fadeOutTime);

		// フェードアウト処理終了を待つ
		while (!isFinished) {
			yield return null;
		}
	}

	public IEnumerator FadeInAndWait(float fadeInTime)
	{
		FadeIn(fadeInTime);

		while (!isFinished) {
			yield return null;
		}
	}

	public IEnumerator FadeInCoroutine(float time, System.Action action) {
		isFinished = false;

		float endTime = Time.timeSinceLevelLoad + time * (cutoutRange);

		var endFrame = new WaitForEndOfFrame();

		while (Time.timeSinceLevelLoad <= endTime) {
			cutoutRange = (endTime - Time.timeSinceLevelLoad) / time;
			fade.Range = cutoutRange;
			yield return endFrame;
		}
		cutoutRange = 0;
		fade.Range = cutoutRange;

		if (action != null) {
			action();
		}

		isFinished = true;
	}

	IEnumerator FadeOutCoroutine(float time, System.Action action) {
		isFinished = false;
		float endTime = Time.timeSinceLevelLoad + time * (1 - cutoutRange);

		var endFrame = new WaitForEndOfFrame();

		while (Time.timeSinceLevelLoad <= endTime) {
			cutoutRange = 1 - ((endTime - Time.timeSinceLevelLoad) / time);
			fade.Range = cutoutRange;
			yield return endFrame;
		}
		cutoutRange = 1;
		fade.Range = cutoutRange;

		if (action != null) {
			action();
		}

		isFinished = true;
	}

	public Coroutine FadeIn(float time, System.Action action) {
		StopAllCoroutines();
		return StartCoroutine(FadeInCoroutine(time, action));
	}

	public Coroutine FadeIn(float time) {
		return FadeIn(time, null);
	}

	public Coroutine FadeOut(float time, System.Action action) {
		StopAllCoroutines();
		return StartCoroutine(FadeOutCoroutine(time, action));
	}

	public Coroutine FadeOut(float time) {
		return FadeOut(time, null);
	}

	private void OnGUI()
	{
		GUI.BeginGroup(new Rect(10, 300, 200, 100));

		GUI.Box(new Rect(0, 0, 200, 100), "FadeInfo");
		GUI.Label(new Rect(25, 25, 200, 30), "Fade Range : " + fade.Range);

		GUI.EndGroup();
	}
}
