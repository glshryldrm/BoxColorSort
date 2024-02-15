using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    float xStartPos = -2.5f;
    float xFinishPos = 1.7f;
    float yStartPos = 4.6f;
    float yFinishPos = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 vector1 = new Vector3();
        Vector3 vector2 = new Vector3(0.6f, 0.6f, 0.6f);

        
        for (float i = yFinishPos; i < yStartPos; i += 0.6f)
        {
            for (float j = xStartPos; j < xFinishPos; j += 0.6f)
            {
                vector1.x = j;
                vector1.y = i;
                vector1.z = -0.5f;
                Gizmos.DrawWireCube(transform.position + vector1, vector2);
            }
        }
    }

}
