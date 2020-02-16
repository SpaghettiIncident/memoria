using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Util
{

	static public Define.SCENE_KIND GetSceneKind() {
		return (Define.SCENE_KIND)SceneManager.GetActiveScene().buildIndex;
	}

	static public void LoadScene(Define.SCENE_KIND scene_kind) {
		SceneManager.LoadScene((int)scene_kind);
	}
}
