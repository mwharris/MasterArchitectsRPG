using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
            
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            return player;
        }

        public static float CalculateTurn(Quaternion originalRotation, Quaternion newRotation)
        {
            var cross = Vector3.Cross(originalRotation * Vector3.forward, newRotation * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }
    }
    
    public class With_Positive_Vertical_Input
    {
        [UnityTest]
        public IEnumerator Moves_Forward()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(1f);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);
            
            float endingZPosition = player.transform.position.z;

            Assert.Greater(endingZPosition, startingZPosition);
        }
    }
    
    public class With_Negative_Vertical_Input
    {
        [UnityTest]
        public IEnumerator Moves_Backward()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Vertical.Returns(-1f);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);
            
            float endingZPosition = player.transform.position.z;

            Assert.Less(endingZPosition, startingZPosition);
        }
    }
    
    public class With_Positive_Horizontal_Input
    {
        [UnityTest]
        public IEnumerator Moves_Right()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Horizontal.Returns(1f);

            float startingXPosition = player.transform.position.x;
            
            yield return new WaitForSeconds(5f);
            
            float endingXPosition = player.transform.position.x;

            Assert.Greater(endingXPosition, startingXPosition);
        }
    }
    
    public class With_Negative_Horizontal_Input
    {
        [UnityTest]
        public IEnumerator Moves_Left()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.Horizontal.Returns(-1f);

            float startingXPosition = player.transform.position.x;
            
            yield return new WaitForSeconds(5f);
            
            float endingXPosition = player.transform.position.x;

            Assert.Less(endingXPosition, startingXPosition);
        }
    }

    public class With_Negative_Mouse_X
    {
        [UnityTest]
        public IEnumerator Turns_Left()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
    
    public class With_Positive_Mouse_X
    {
        [UnityTest]
        public IEnumerator Turns_Right()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            player.PlayerInput.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmount, 0);
        }
    }
}