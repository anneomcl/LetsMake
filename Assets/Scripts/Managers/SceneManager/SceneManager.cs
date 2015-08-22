using UnityEngine;
using System.Collections;

public static class SceneManager
{
	private static GameObject sceneLoaderPrefab = null;

    public static void LoadScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}

    public static void LoadSceneAsync(string sceneName)
    {
		if(sceneLoaderPrefab == null)
			FindSceneLoaderPrefab();

		if(sceneLoaderPrefab != null)
		{
	        GameObject sceneLoaderGO = GameObject.Instantiate<GameObject>(sceneLoaderPrefab);
	        SceneLoader sceneLoader = sceneLoaderGO.GetComponent<SceneLoader>();
	        sceneLoader.Load(sceneName);
		}
		else
		{
			Debug.LogError("Failed to load scene! Could not find SceneLoader prefab.");
		}
    }

	private static void FindSceneLoaderPrefab()
	{
		sceneLoaderPrefab = Resources.Load<GameObject>("SceneLoader");
	}
}