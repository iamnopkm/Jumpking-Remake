/* camera follow player (1st version)

// using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     public Transform player;
//     public float cameraOffset = 0;
//     public float cameraTopOffset = 0;
//     public float cameraBottomOffset = 8;

//     private void LateUpdate()
//     {
//         Vector3 playerPosition = player.position;

//         // Move the camera horizontally with the player
//         transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z);

//         // Move the camera vertically within the specified range
//         float cameraY = Mathf.Clamp(playerPosition.y + cameraOffset, playerPosition.y + cameraBottomOffset, playerPosition.y + cameraTopOffset);
//         transform.position = new Vector3(transform.position.x, cameraY, transform.position.z);
//     }
// }

*/

// 

/* camera 2nd version */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Jump King");
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.gameObject.transform.position.y + 7, 1f), -10);
    }
}
