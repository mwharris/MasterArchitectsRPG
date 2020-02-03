using System.Collections;
using A_Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace State_Machine
{
    public class Game_State_Machine
    {
        [UnityTest]
        public IEnumerator Switches_To_Loading_When_Level_Is_Selected()
        {
            yield return Helpers.LoadMenuScene();

            GameStateMachine stateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            // Check that we are in Menu state by default
            Assert.AreEqual(typeof(Menu), stateMachine.CurrentStateType);
            
            // Mimic clicking a load level button
            PlayButton.LevelToLoad = "Level1";
            yield return null;

            // Check that we moved to Loading state
            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentStateType);
        }
    }
}