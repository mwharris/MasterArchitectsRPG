﻿using System.Collections;
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
        
        [UnityTest]
        public IEnumerator Switches_To_Play_When_Level_Loading_Completed()
        {
            GameObject.Destroy(GameObject.FindObjectOfType<GameStateMachine>());
            
            yield return Helpers.LoadMenuScene();

            GameStateMachine stateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            // Check that we are in Menu state by default
            Assert.AreEqual(typeof(Menu), stateMachine.CurrentStateType);
            
            // Mimic clicking a load level button
            PlayButton.LevelToLoad = "Level1";
            yield return null;

            // Check that we moved to Loading state
            Assert.AreEqual(typeof(LoadLevel), stateMachine.CurrentStateType);
            
            // Wait until LoadLevel asynchronously loads the scene and moves us to Play state
            yield return new WaitUntil(()=> stateMachine.CurrentStateType == typeof(Play));

            // Check that we moved to Play state
            Assert.AreEqual(typeof(Play), stateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator Only_Allows_One_Instance_To_Exist()
        {
            // Create two GameStateMachines
            GameStateMachine firstGameStateMachine = new GameObject("First GSM").AddComponent<GameStateMachine>();
            GameStateMachine secondGameStateMachine = new GameObject("Second GSM").AddComponent<GameStateMachine>();
            yield return null;
            
            // Get the amount of GameStateMachines in the scene
            GameStateMachine[] stateMachines = GameObject.FindObjectsOfType<GameStateMachine>();
            
            // Make sure there's only 1 (one should have been deleted automatically)
            Assert.IsTrue(secondGameStateMachine == null);
            Assert.IsTrue(firstGameStateMachine != null);
            Assert.IsTrue(stateMachines.Length == 1);
        }
    }
}