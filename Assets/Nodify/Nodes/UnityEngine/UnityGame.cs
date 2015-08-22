using UnityEngine;
using System.Collections;

namespace Nodify.Runtime.Nodes
{
    [CreateMenu("Unity/Game", "Unity.Game", "Icons/unity_game_icon")]
    public class UnityGame : Node
    {
        #region Exposed Methods
        [Expose]
        public void OnAwake()
        {
            
        }

        [Expose]
        public void OnStart()
        {
            
        }

        [Expose]
        public void OnUpdate()
        {
            
        }

        [Expose]
        public void OnLateUpdate()
        {
            
        }

        [Expose]
        public void OnFixedUpdate()
        {
            
        }

        [Expose]
        public void OnApplicationDidQuit()
        {
            
        }

        #endregion

        private void Awake()
        {
            this.Fire("OnAwake");
        }

        private void Start()
        {
            this.Fire("OnStart");
        }

        private void Update()
        {
            this.Fire("OnUpdate");
        }

        private void LateUpdate()
        {
            this.Fire("OnLateUpdate");
        }

        private void FixedUpdate()
        {
            this.Fire("OnFixedUpdate");
        }

        private void OnApplicationQuit()
        {
            this.Fire("OnApplicationDidQuit");
        }
    }
}