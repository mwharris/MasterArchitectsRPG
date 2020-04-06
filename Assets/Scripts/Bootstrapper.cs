using UnityEngine;

public class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Initialize()
    {
        GameObject inputGameObject = new GameObject("[INPUT SYSTEM]");
        inputGameObject.AddComponent<PlayerInput>();
        GameObject.DontDestroyOnLoad(inputGameObject);
    }
}