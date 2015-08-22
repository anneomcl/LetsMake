using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Application/Reload Level", "Application.ReloadLevel")]
    public class UnityApplicationReloadLevel : Node
    {
        public enum LoadLevelType
        {
            LoadLevel,
            LoadLevelAsync
        }

        [Expose]
        public LoadLevelType loadLevelType = LoadLevelType.LoadLevel;

        protected override void OnExecute()
        {
            switch (loadLevelType)
            {
            case LoadLevelType.LoadLevel:
				SceneManager.LoadScene(Application.loadedLevelName);
                break;
            case LoadLevelType.LoadLevelAsync:
				SceneManager.LoadSceneAsync(Application.loadedLevelName);
                break;
            }

            base.OnExecute();
        }
    }
}
