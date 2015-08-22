using UnityEngine;
using System.Collections;
using Nodify.Runtime;

namespace Nodify.Runtime.Nodes
{
	[CreateMenu("Variables/UnityEngine/GameObject", "GameObject", "Icons/unity_gameobject_variable_icon")]
	public class UnityVariablesGameObject : GlobalVariable<GameObject> { }
}
