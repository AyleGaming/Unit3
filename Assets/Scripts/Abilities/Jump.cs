using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Gravity myGravity;
    [SerializeField] private float jumpForce;
    
    public void JumpAbility()
    {
        if (myGravity.IsOnGround())
        {
            myGravity.AddForce(Vector3.up * jumpForce);
        }
    }
}
