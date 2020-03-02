using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.U2D;

public class SpeakerCharacter : MonoBehaviour
{
	SpriteAtlas _spriteAtlas;
	SpriteRenderer _body;
	SpriteRenderer _face;

	// Start is called before the first frame update
	void Start()
    {
		GameObject body = new GameObject("body");
		_body = body.AddComponent<SpriteRenderer>();
		GameObject face = new GameObject("face");
		_face = face.AddComponent<SpriteRenderer>();

		body.transform.parent = this.gameObject.transform;
		face.transform.parent = this.gameObject.transform;

		_spriteAtlas = Resources.Load<SpriteAtlas>("Character/kohaku");
		_body.sprite = _spriteAtlas.GetSprite("portrait_kohaku_01");

		this.gameObject.transform.position = new Vector3(3.0f, -1.5f, 5.0f);
		this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

	}

	public void SetSpriteAtlas(SpriteAtlas spriteAtlas)
	{
		_spriteAtlas = spriteAtlas;
		_body.sprite = _spriteAtlas.GetSprite("portrait_kohaku_01");
	}

	public enum Face
	{
		DEFAULT = 0,
		PROUD,
		ANGRY,
		SUPRISE,
		SMILE,
		DOUBT,
	}
	public void ChangeFace(Face face)
	{
		switch (face)
		{
			case Face.DEFAULT:
				_face.sprite = null;
				break;
			case Face.PROUD:
				_face.sprite = _spriteAtlas.GetSprite("portrait_kohaku_02");
				break;
			case Face.ANGRY:
				_face.sprite = _spriteAtlas.GetSprite("portrait_kohaku_03");
				break;
			case Face.SUPRISE:
				_face.sprite = _spriteAtlas.GetSprite("portrait_kohaku_04");
				break;
			case Face.SMILE:
				_face.sprite = _spriteAtlas.GetSprite("portrait_kohaku_05");
				break;
			case Face.DOUBT:
				_face.sprite = _spriteAtlas.GetSprite("portrait_kohaku_06");
				break;
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnGUI()
	{
		if (GUI.Button(new Rect(300, 30, 100, 30), "Face.DEFAULT"))
		{
			ChangeFace(Face.DEFAULT);
		}
		if (GUI.Button(new Rect(300, 60, 100, 30), "Face.PROUD"))
		{
			ChangeFace(Face.PROUD);
		}
		if (GUI.Button(new Rect(300, 90, 100, 30), "Face.ANGRY"))
		{
			ChangeFace(Face.ANGRY);
		}
		if (GUI.Button(new Rect(300, 120, 100, 30), "Face.SUPRISE"))
		{
			ChangeFace(Face.SUPRISE);
		}
		if (GUI.Button(new Rect(300, 150, 100, 30), "Face.SMILE"))
		{
			ChangeFace(Face.SMILE);
		}
		if (GUI.Button(new Rect(300, 180, 100, 30), "Face.DOUBT"))
		{
			ChangeFace(Face.DOUBT);
		}
	}
}
