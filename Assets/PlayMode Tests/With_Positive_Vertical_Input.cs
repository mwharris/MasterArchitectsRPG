using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace A_Player
{
    public static class Helpers
    {
        public static void CreateFloor()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(50, 0.1f, 50);
            floor.transform.position = Vector3.zero;
        }

        public static Player CreatePlayer()
        {
            var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.AddComponent<NavMeshAgent>();
            playerGameObject.transform.position = new Vector3(0, 1.3f, 0);
            
            Player player = playerGameObject.AddComponent<Player>();
            
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
            Helpers.CreateFloor();

            var player = Helpers.CreatePlayer();
            
            player.PlayerInput.Vertical.Returns(1f);
            player.PlayerInput.Horizontal.Returns(4f);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(5f);
            
            float endingZPosition = player.transform.position.z;

            Assert.Greater(endingZPosition, startingZPosition);
        }
    }

    public class With_Negative_Mouse_X
    {
        [UnityTest]
        public IEnumerator Turns_Left()
        {
            Helpers.CreateFloor();

            var player = Helpers.CreatePlayer();
            
            player.PlayerInput.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
}