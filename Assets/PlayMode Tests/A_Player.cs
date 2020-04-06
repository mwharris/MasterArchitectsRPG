using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace A_Player
{
    public class Player_Input_Test
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
    }
    
    public class With_Positive_Vertical_Input : Player_Input_Test
    {
        [UnityTest]
        public IEnumerator Moves_Forward()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.Vertical.Returns(1f);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(0.5f);
            
            float endingZPosition = player.transform.position.z;

            Assert.Greater(endingZPosition, startingZPosition);
        }
    }
    
    public class With_Negative_Vertical_Input : Player_Input_Test
    {
        [UnityTest]
        public IEnumerator Moves_Backward()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.Vertical.Returns(-1f);

            float startingZPosition = player.transform.position.z;
            
            yield return new WaitForSeconds(0.5f);
            
            float endingZPosition = player.transform.position.z;

            Assert.Less(endingZPosition, startingZPosition);
        }
    }
    
    public class With_Positive_Horizontal_Input : Player_Input_Test
    {
        [UnityTest]
        public IEnumerator Moves_Right()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.Horizontal.Returns(1f);

            float startingXPosition = player.transform.position.x;
            
            yield return new WaitForSeconds(0.5f);
            
            float endingXPosition = player.transform.position.x;

            Assert.Greater(endingXPosition, startingXPosition);
        }
    }
    
    public class With_Negative_Horizontal_Input : Player_Input_Test
    {
        [UnityTest]
        public IEnumerator Moves_Left()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.Horizontal.Returns(-1f);

            float startingXPosition = player.transform.position.x;
            
            yield return new WaitForSeconds(0.5f);
            
            float endingXPosition = player.transform.position.x;

            Assert.Less(endingXPosition, startingXPosition);
        }
    }

    public class With_Negative_Mouse_X : Player_Input_Test
    {
        [UnityTest]
        public IEnumerator Turns_Left()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.MouseX.Returns(-1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Less(turnAmount, 0);
        }
    }
    
    public class With_Positive_Mouse_X : Player_Input_Test
    {
        [UnityTest]
        public IEnumerator Turns_Right()
        {
            yield return Helpers.LoadMovementTestScene();

            var player = Helpers.GetPlayer();
            
            PlayerInput.Instance.MouseX.Returns(1f);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            float turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            Assert.Greater(turnAmount, 0);
        }
    }
}