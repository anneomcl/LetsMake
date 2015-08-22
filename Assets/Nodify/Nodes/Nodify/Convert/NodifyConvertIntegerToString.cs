using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Nodify/Convert/Integer To String", "Convert.IntToString" )]
	public class NodifyConvertIntegerToString : Node 
	{
		[Expose]
		public int source;

		[Expose]
		public string result;

		protected override void OnExecute()
		{
			result = source.ToString();
			base.OnExecute();
		}
	}
}
