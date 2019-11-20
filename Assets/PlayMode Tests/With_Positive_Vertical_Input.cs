using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace A_Player
{
    public class With_Positive_Vertical_Input
    {
        [UnityTest]
        public IEnumerator Moves_Forward()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(50, 0.1f, 50);
            floor.transform.position = Vector3.zero;

            var playerGameObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerGameObject.AddComponent<CharacterController>();
            playerGameObject.transform.position = new Vector3(0, 1.3f, 0);
            
            Player player = playerGameObject.AddComponent<Player>();
            
            var testPlayerInput = Substitute.For<IPlayerInput>();
            player.PlayerInput = testPlayerInput;
            
            testPlayerInput.Vertical.Returns(1f);
            testPlayerInput.Horizontal.Returns(4f);

            float startingZPosition = playerGameObject.transform.position.z;
            
            yield return new WaitForSeconds(5f);
            
            float endingZPosition = player.transform.position.z;

            Assert.Greater(endingZPosition, startingZPosition);
        }
    }
}