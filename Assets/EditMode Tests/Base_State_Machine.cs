using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class Base_State_Machine
    {
        [Test]
        public void Initial_Set_State_Switches_To_State()
        {
            StateMachine stateMachine = new StateMachine();

            IState firstState = Substitute.For<IState>();
            stateMachine.SetState(firstState);
            
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
        
        [Test]
        public void Subsequent_Set_State_Switches_To_State()
        {
            StateMachine stateMachine = new StateMachine();

            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.SetState(secondState);
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        
        [Test]
        public void Transition_Switches_State_When_Condition_Met()
        {
            StateMachine stateMachine = new StateMachine();

            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();

            bool ShouldTransitionState() => true;
            stateMachine.AddTransition(firstState, secondState, ShouldTransitionState);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.Tick();
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        
        [Test]
        public void Transition_Doesnt_Switch_State_When_Condition_Not_Met()
        {
            StateMachine stateMachine = new StateMachine();

            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();

            bool ShouldTransitionState() => false;
            stateMachine.AddTransition(firstState, secondState, ShouldTransitionState);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.Tick();
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
        
        [Test]
        public void Transition_From_Any_Switches_State_When_Condition_Met()
        {
            StateMachine stateMachine = new StateMachine();

            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();

            bool ShouldTransitionState() => true;
            stateMachine.AddAnyTransition(secondState, ShouldTransitionState);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.Tick();
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        
        
        [Test]
        public void Transition_Doesnt_Switch_State_Not_In_Correct_Source_State()
        {
            StateMachine stateMachine = new StateMachine();

            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            IState thirdState = Substitute.For<IState>();

            bool ShouldTransitionState() => true;
            stateMachine.AddTransition(secondState, thirdState, ShouldTransitionState);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.Tick();
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
    }
}