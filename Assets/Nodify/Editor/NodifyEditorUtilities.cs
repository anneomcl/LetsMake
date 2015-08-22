/**
Copyright (c) <2014>, <Devon Klompmaker>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the <organization> nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**/
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Nodify.Runtime;
using Nodify.Rendering;

namespace Nodify.Editor
{
	public class NodifyEditorUtilities
    {
        #region Public Static Fields
        public static Node currentManipulatingNode = null;
        public static Vector2 currentManipulatingNodeOffset;
        public static Anchor currentConnectingAnchor = null;
        public static Anchor currentDraggingAnchor = null;
        public static NodeGroup currentSelectedGroup = null;
        #endregion

        #region Cached Private Lookup Dictionaries
        private static Dictionary<Type, Type> cachedNodeRendererTypes = new Dictionary<Type, Type>();
		private static Dictionary<Type, object> cachedNodeRendererObjects = new Dictionary<Type, object>();

		private static Dictionary<Type, Type> cachedAnchorRendererTypes = new Dictionary<Type, Type>();
		private static Dictionary<Type, object> cachedAnchorRendererObjects = new Dictionary<Type, object>();
        #endregion

        #region Private Reflection Methods
        private static List<Type> nodeRendererTypes
		{
			get
			{
				List<Type> types = new List<Type>();

				foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach(Type type in assembly.GetTypes())
					{
						if(type.BaseType != null)
                        {
                            if (type.BaseType.GetGenericArguments().Length > 0) 
                            {
                                if (typeof(Rendering.NodeRendererBase).IsAssignableFrom(type)) 
                                {
                                    types.Add(type);
                                }
                            }
						}
					}
				}

				return types;
			}
		}

		private static List<Type> anchorRendererTypes
		{
			get
			{
				List<Type> types = new List<Type>();

				foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					foreach(Type type in assembly.GetTypes())
					{
						if(type.BaseType != null)
						{
							if(type.BaseType.GetGenericArguments().Length > 0)
							{
                                if (typeof(Rendering.AnchorRendererBase).IsAssignableFrom(type))
                                {
                                    types.Add(type);
                                }
							}
						}
					}
				}

				return types;
			}
		}
        #endregion

        /// <summary>
		/// Find a Rendering.NodeRenderer that matches the type of node to render.
		/// </summary>
		/// <returns>The node renderer.</returns>
		/// <param name="nodeType">Node type.</param>
		public static object FindNodeRenderer(Type nodeType)
		{
			if(!cachedNodeRendererTypes.ContainsKey(nodeType))
			{
                CustomRenderer[] renderers = (CustomRenderer[])nodeType.GetCustomAttributes(typeof(CustomRenderer), true);

                Type nodeRendererType = null;

                if (renderers.Length > 0)
                {
                    nodeRendererType = Type.GetType(renderers[0].rendererType);
                }

                if(nodeRendererType == null)
                {
                    nodeRendererType = typeof(Nodify.Rendering.DefaultNodeRenderer);
                }

				cachedNodeRendererTypes.Add(nodeType, nodeRendererType);
			}

			if(!cachedNodeRendererObjects.ContainsKey(cachedNodeRendererTypes[nodeType]))
			{
				cachedNodeRendererObjects.Add(cachedNodeRendererTypes[nodeType], Activator.CreateInstance(cachedNodeRendererTypes[nodeType]));
			}

			return cachedNodeRendererObjects[cachedNodeRendererTypes[nodeType]];
		}

		/// <summary>
		/// Find the anchor renderer that best matches the anchor type.
		/// </summary>
		/// <returns>The anchor renderer.</returns>
		/// <param name="anchorType">Anchor type.</param>
		public static object FindAnchorRenderer(Type anchorType)
		{
			if(!cachedAnchorRendererTypes.ContainsKey(anchorType))
			{
				Type anchorRendererType = typeof(Rendering.DefaultAnchorRenderer);

				foreach(Type rendererType in anchorRendererTypes)
				{
					if(rendererType.BaseType.GetGenericArguments()[0] == anchorType)
					{
						anchorRendererType = rendererType;
					}
				}

				cachedAnchorRendererTypes.Add(anchorType, anchorRendererType);
			}

			if(!cachedAnchorRendererObjects.ContainsKey(cachedAnchorRendererTypes[anchorType]))
			{
				cachedAnchorRendererObjects.Add(cachedAnchorRendererTypes[anchorType], Activator.CreateInstance(cachedAnchorRendererTypes[anchorType]));
			}

			return cachedAnchorRendererObjects[cachedAnchorRendererTypes[anchorType]];
		}

		/// <summary>
		/// Draws a bezier with a specified bendiness.
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		/// <param name="color">Color.</param>
		/// <param name="size">Size.</param>
		/// <param name="bendiness">Bendiness.</param>
		public static void DrawBezier(Vector2 start, Vector2 end, Color color, float size, float bendiness = 50)
		{
			Vector2 directionTo = end - start;
			directionTo.y = 0;

			Handles.BeginGUI();

			Handles.DrawBezier(start, end, start + (directionTo.normalized * bendiness), end - (directionTo.normalized * bendiness), color, null, size);

			Handles.EndGUI();
		}

		/// <summary>
		/// Finds a point on the Bezier curve at time T based on bendiness.
		/// </summary>
		/// <returns>The on bezier.</returns>
		/// <param name="s">S.</param>
		/// <param name="e">E.</param>
		/// <param name="t">T.</param>
		/// <param name="bendiness">Bendiness.</param>
		public static Vector2 PointOnBezier(Vector2 s, Vector2 e, float t, float bendiness = 50)
		{
			Vector2 directionTo = e - s;
			directionTo.y = 0;

			Vector2 st = s + (directionTo.normalized * bendiness);
			Vector2 et = e - (directionTo.normalized * bendiness);

			return PointOnBezier(s, e, st, et, t);
		}

		/// <summary>
		/// Finds the point on the bezier curve at T.
		/// </summary>
		/// <returns>The on bezier.</returns>
		/// <param name="s">S.</param>
		/// <param name="e">E.</param>
		/// <param name="st">St.</param>
		/// <param name="et">Et.</param>
		/// <param name="t">T.</param>
		public static Vector2 PointOnBezier(Vector2 s, Vector2 e, Vector2 st, Vector2 et, float t)
	    {
	        return (((-s + 3*(st-et) + e)* t + (3*(s+et) - 6*st))* t + 3*(st-s)) * t + s;
	    }

        /// <summary>
        /// Destroys the gameobject in both Run mode and edit mode safely.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void SafeDestroy(GameObject gameObject)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }

        /// <summary>
        /// Duplicates the gameObject the same as the Unity way.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static GameObject Duplicate(GameObject obj)
        {
            UnityEngine.Object prefabRoot = PrefabUtility.GetPrefabParent(obj);

            if (prefabRoot != null)
            {
                GameObject result = (GameObject)PrefabUtility.InstantiatePrefab(prefabRoot);
                result.name = obj.name;
                result.transform.parent = obj.transform.parent;

                return result;
            }
            else
            {
                GameObject result = (GameObject)GameObject.Instantiate(obj);
                result.name = obj.name;
                result.transform.parent = obj.transform.parent;

                return result;
            }
        }

        /// <summary>
        /// Iterates through the custom styles of a GUISkin
        /// and finds a custom style by name.
        /// </summary>
        /// <param name="skin"></param>
        /// <param name="styleName"></param>
        /// <returns></returns>
        public static GUIStyle FindStyleByName(GUISkin skin, string styleName)
        {
            for(int i = 0; i < skin.customStyles.Length; i++)
            {
                if(skin.customStyles[i].name == styleName)
                {
                    return skin.customStyles[i];
                }
            }

            Debug.LogWarning("Could not find style => " + styleName);

            return null;
        }

        /// <summary>
        /// Returns a truncated version of a 
        /// string based on additional parameters supplied.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <param name="trail"></param>
        /// <returns></returns>
        public static string Truncate(string source, int length, string trail = "...")
        {
            if (source != null && source.Length > length)
            {
                source = source.Substring(0, length) + trail;
            }
            return source;
        }

        /// <summary>
        /// Finds all types that have the CreateMenu
        /// attribute. These should be type of Node.
        /// </summary>
        /// <returns></returns>
        public static List<CreateMenu> FindNodeTypes()
        {
            List<CreateMenu> menuItems = new List<CreateMenu>();

            foreach (Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (System.Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(CreateMenu), false).Length > 0)
                    {
                        CreateMenu menuItem = (CreateMenu)type.GetCustomAttributes(typeof(CreateMenu), false)[0];
                        menuItem.type = type;

                        menuItems.Add(menuItem);
                    }
                }
            }

            menuItems.Sort(NodifyEditorUtilities.CreateMenuSort);
            
            return menuItems;
        }

        public static Vector2 ClosestPointOnRectangle(Vector2 point, Rect rect)
        {
            Vector2 pointA = new Vector2(rect.xMin, rect.center.y);
            Vector2 pointB = new Vector2(rect.xMax, rect.center.y);
            Vector2 pointC = new Vector2(rect.center.x, rect.yMax);
            Vector2 pointD = new Vector2(rect.center.x, rect.yMin);

            Vector2 closest = pointA;

            if (Vector2.Distance(point, closest) > Vector2.Distance(pointB, point)) { closest = pointB; }
            if (Vector2.Distance(point, closest) > Vector2.Distance(pointC, point)) { closest = pointC; }
            if (Vector2.Distance(point, closest) > Vector2.Distance(pointD, point)) { closest = pointD; }

            return closest;
        }

        /// <summary>
        /// Sorts the create menu using the Uri scheme.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CreateMenuSort(CreateMenu a, CreateMenu b)
        {
            Uri p1 = new Uri(Uri.EscapeUriString("file://" + a.path.Replace(" ", string.Empty)));
            Uri p2 = new Uri(Uri.EscapeUriString("file://" + b.path.Replace(" ", string.Empty)));

            return Uri.Compare(p1, p2, UriComponents.Path, UriFormat.Unescaped, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the current asset path.
        /// </summary>
        /// <returns>The current asset path.</returns>
        public static string GetCurrentAssetPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if(path == "") {
                // No path found. Fallback to assets root
                path = "Assets/";
            } else if(Path.GetExtension(path) != "") {
                // Get path from file
                path = path.Replace(Path.GetFileName(path), "") + "/";
            } else {
                path += "/";
            }
            return path;
        }

        #region Editor Zoom Methods

        private const float kEditorWindowTabHeight = 21.0f;
        private static Matrix4x4 preZoomGUIMatrix;

        public static Rect BeginZoomArea(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup();

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.TopLeft());
            clippedArea.y += kEditorWindowTabHeight;
            GUI.BeginGroup(clippedArea);

            preZoomGUIMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

            return clippedArea;
        }

        public static void EndZoomArea()
        {
            GUI.matrix = preZoomGUIMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
        }

        #endregion
    }

    public static class RectExtensions
    {
        public static Vector2 TopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }
        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }
        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }
        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale.x;
            result.xMax *= scale.x;
            result.yMin *= scale.y;
            result.yMax *= scale.y;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
        public static Rect GetRectFromPoints(Vector2 corner1, Vector2 corner2)
        {
            Rect rect = new Rect(corner1.x, corner1.y, corner2.x - corner1.x, corner2.y - corner1.y);
            if (rect.width < 0f)
            {
                rect.x += rect.width;
                rect.width = -rect.width;
            }
            if (rect.height < 0f)
            {
                rect.y += rect.height;
                rect.height = -rect.height;
            }
            return rect;
        }
    }

}

