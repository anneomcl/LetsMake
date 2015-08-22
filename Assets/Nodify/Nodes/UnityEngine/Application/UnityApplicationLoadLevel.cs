using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/Application/Load Level", "Application.LoadLevel" )]
	public class UnityApplicationLoadLevel : Node 
	{
        public enum LoadLevelType
        {
            LoadLevel,
            LoadLevelAsync
        }
        
		[Expose]
        public LoadLevelType loadLevelType = LoadLevelType.LoadLevel;

		[Expose]
		public string levelName;

        protected override void OnExecute()
		{
            switch (loadLevelType)
            {
            case LoadLevelType.LoadLevel:
				SceneManager.LoadScene(levelName);
                break;
            case LoadLevelType.LoadLevelAsync:
				SceneManager.LoadSceneAsync(levelName);
                break;
            }

			base.OnExecute();
		}
	}
}
