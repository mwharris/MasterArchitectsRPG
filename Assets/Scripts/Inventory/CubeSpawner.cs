using UnityEngine;

public class CubeSpawner : ItemComponent
{
    public override void Use()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        cube.GetComponent<Renderer>().material.color = Color.red;
    }
}