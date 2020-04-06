using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace A_Player
{
    public static class Helpers
    {
        public static IEnumerator LoadMovementTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("MovementTests");
            while (!operation.isDone)
            {
                yield return null;
            }
        }
        
        public static IEnumerator LoadEntityStateMachineTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("EntityStateMachineTests");
            while (!operation.isDone)
            {
                yield return null;
            }
        }
        
        public static IEnumerator LoadMenuScene()
        {
            var operation = SceneManager.LoadSceneAsync("Menu");
            while (!operation.isDone)
            {
                yield return null;
            }
        }
        
        public static IEnumerator LoadItemTestScene()
        {
            var operation = SceneManager.LoadSceneAsync("ItemTests");
            while (!operation.isDone)
            {
                yield return null;
            }
            
            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (!operation.isDone)
            {
                yield return null;
            }
        }

        public static Player GetPlayer()
        {
            Player player = GameObject.FindObjectOfType<Player>();
            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion newRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, newRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }

    }
}