using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Util
{
	public static string GetDataPath()
	{
		return Application.dataPath + "/Data";
	}

	public static string GetDataScenarioPath()
	{
		return Application.dataPath + "/Data/Scenario";
//		return Application.dataPath + "/Resources/Scenario";
	}

	public static string GetScenarioFilePath(string fileName)
	{
		return GetDataScenarioPath() + "/" + fileName;
	}
}
