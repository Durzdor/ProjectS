using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public void MoveLeft() { print("llego"); transform.position += Vector3.left;}
    public void MoveRight() => transform.position += Vector3.right;
    public void MoveUp() => transform.position += Vector3.up;
    public void MoveDown() => transform.position += Vector3.down;
}
