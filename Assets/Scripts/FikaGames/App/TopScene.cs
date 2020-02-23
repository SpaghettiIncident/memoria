using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScene 
{
	enum STEP
	{
		INITIALIZE,
		UPDATE,
		FINALIZE,
	}

	STEP _step;

	// コンストラクタ
	public TopScene()
	{
		_step = STEP.INITIALIZE;
	}

	public void Update()
	{

	}
}
