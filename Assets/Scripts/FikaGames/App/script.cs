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

	public void SetWH(float width, float height)
	{
		var rect = _image.GetComponent<RectTransform>().rect;
		rect.width = width;
		rect.height = height;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
