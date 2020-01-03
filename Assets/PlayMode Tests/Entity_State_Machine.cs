using System.Collections;
using A_Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace State_Machine
{
    public class Entity_State_Machine
    {
        [UnityTest]
        public IEnumerator Starts_In_Idle_State()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();

            EntityStateMachine stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator Switches_To_Chase_Player_When_In_Range()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();

            Player player = Helpers.GetPlayer();
            EntityStateMachine stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            
            // Check that we are in Idle state when out of chase range
            player.transform.position = stateMachine.transform.position + new Vector3(5.1f, 0f, 0f);
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);

            // Check that we transition to Chase state when within chase range
            player.transform.position = stateMachine.transform.position + new Vector3(4.9f, 0f, 0f);
            yield return null;
            Assert.AreEqual(typeof(ChasePlayer), stateMachine.CurrentStateType);
        }
        
        [UnityTest]
        public IEnumerator Switches_To_Dead_Only_When_HP_Is_Zero()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();

            EntityStateMachine stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();
            Entity entity = stateMachine.GetComponent<Entity>();

            // Check that we start in the Idle state
            entity.TakeHit(entity.Health - 1);
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);
            
            // Check that we transition to Dead state when taking lethal damage
            entity.TakeHit(entity.Health);
            yield return null;
            Assert.AreEqual(typeof(Dead), stateMachine.CurrentStateType);
        }
    }
}