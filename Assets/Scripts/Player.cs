using UnityEngine;

public class Player : MonoBehaviour
{
    private void Move()
    {
        var deltaX = Input.GetAxis(“Horizontal”); 
        var newXPos = transform.position.x + deltaX;
        transform.position = new Vector2(newXPos, transform.position.y);
    }


}
