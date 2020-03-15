using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class script : MonoBehaviour
{
	public Image _image;
	public Text _text;

	public void SetText(string text)
	{

		_text.text = text;
	}

	public void SetWH()
	{

//		rect.height = height;
	}
    // Start is called before the first frame update
    void Start()
    {
		_image = transform.GetChild(0).gameObject.GetComponent<Image>();
		//		_text.text = text;
		var sd = _image.GetComponent<RectTransform>().sizeDelta;
		//		var t = Screen.width;
		sd.x = Screen.width;

//		_image.GetComponent<RectTransform>().sizeDelta = sd;
	}

    // Update is called once per frame
    void Update()
    {
		var sd = _image.GetComponent<RectTransform>().sizeDelta;
		//		var t = Screen.width;
		sd.x = Screen.width;

//		_image.GetComponent<RectTransform>().sizeDelta = sd;
	}
}
