using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Unity/UI/Button/OnClick", "UI.Button.OnClick" )]
	public class UnityUIButtonOnClick : Node 
	{
		[Expose]
		public UnityEngine.UI.Button button;

		[Expose]
		public void OnClick() { }

		private void Awake()
		{
			if(button != null)
				button.onClick.AddListener(HandleButtonOnClick);
		}

		private void OnDestroy()
		{
			if(button != null)
				button.onClick.RemoveListener(HandleButtonOnClick);
		}

		private void HandleButtonOnClick()
		{
			Fire("OnClick");
		}
	}
}
