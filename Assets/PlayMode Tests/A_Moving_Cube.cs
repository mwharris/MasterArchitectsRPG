using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class A_Moving_Cube
    {
        [UnityTest]
        public IEnumerator moving_forward_changes_position()
        {
            // Arrange
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;
            
            // Act
            for (int i = 0; i < 10; i++)
            {
                cube.transform.position += Vector3.forward;
                yield return null;
                // Assert
                Assert.AreEqual(i + 1, cube.transform.position.z);
            }
            
        }

        [UnityTest]
        public IEnumerator look_towards_objects()
        {
            // Arrange
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;
            
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(4, 4, 2);
            
            // Act
            Vector3 lookDir = sphere.transform.position - cube.transform.position;
            cube.transform.rotation = Quaternion.LookRotation(lookDir);

            // Assert
            Assert.True(Mathf.Approximately(cube.transform.forward.x, lookDir.normalized.x));
            Assert.True(Mathf.Approximately(cube.transform.forward.y, lookDir.normalized.y));
            Assert.True(Mathf.Approximately(cube.transform.forward.z, lookDir.normalized.z));
            
            yield return null;
        }
        
    }
}