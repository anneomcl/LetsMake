#if UNITY_4_6 || UNITY_5
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine.UI/Text", "Text")]
	public class UnityVariablesUIText : GlobalVariable<Text> 
    { 

    }
}
#endif