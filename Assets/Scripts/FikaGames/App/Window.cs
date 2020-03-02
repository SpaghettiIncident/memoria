using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
	GameObject _frameObj;
	GameObject _textObj;
	Image _frame;
	Text _text;

    // Start is called before the first frame update
    void Start()
    {
		_frameObj = new GameObject("Frame");
		_frame = _frameObj.AddComponent<Image>();
		_frameObj.transform.SetParent(this.gameObject.transform);

		_textObj = new GameObject("Text");
		_text = _textObj.AddComponent<Text>();
		_textObj.transform.SetParent(this.gameObject.transform);

		var s = _frame.GetComponent<RectTransform>();
		var rect = s.rect;
		rect.width = 700.0f;
		rect.height = 150.0f;
    }

	public void SetText(string text)
	{
		_text.text = text;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
