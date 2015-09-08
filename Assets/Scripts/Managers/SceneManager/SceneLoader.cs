using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    [SerializeField]
    private GameObject loadingScreenPrefab;

    [SerializeField]
    private string loadingScreenScene;

    private GameObject loadingScreenInstance;
    private string destinationSceneName;

    private void Awake () {
		loadingScreenInstance = null;
		destinationSceneName = null;
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator OnLevelWasLoaded () {
        if (Application.loadedLevelName == loadingScreenScene) {
            if(string.IsNullOrEmpty(destinationSceneName)) {
                Debug.LogError("You forgot to set the destination scene for the scene loader.");
				GameObject.Destroy(gameObject);
				yield break;
            }

            loadingScreenInstance = GameObject.Instantiate<GameObject>(loadingScreenPrefab);
			DontDestroyOnLoad(loadingScreenInstance);

			//	It doesn't look good without a small delay.
			yield return new WaitForSeconds(0.25f);

			Application.LoadLevelAsync(destinationSceneName);
        } else {
			GameObject.Destroy(loadingScreenInstance);
			GameObject.Destroy(gameObject);
        }
    }

    public void Load (string destinationSceneName) {
        this.destinationSceneName = destinationSceneName;
		Application.LoadLevel(loadingScreenScene);
    }
}