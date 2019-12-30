public interface IState
{
    // Handle State logic
    void Tick();
    
    // Handle any actions needed before this State becomes active
    void OnEnter();
    
    // Handle any actions needed after this State is deactivated
    void OnExit();
}