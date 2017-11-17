using UnityEngine;
/*
 * this class handles all the input the game needs.
 * it's basically a wrapper for input. 
 */
public class InputManager : MonoBehaviour
{
	public float zMov(){
        return Input.GetAxisRaw(Strings.Movement.VERTICAL);
    }
	public float xMov()
	{
        return Input.GetAxisRaw(Strings.Movement.HORIZONTAL);
	}
    /* 
     * functions that return values for the mouse position
     */
    public float yRot(){
        return Input.GetAxis(Strings.Movement.MOUSE_X);
    }
    public float xRot()
	{
        return Input.GetAxis(Strings.Movement.MOUSE_Y);
	}
    public float jump()
    {
        return Input.GetAxis(Strings.Movement.JUMP);
    }
    public float shoot()
    {
        return Input.GetAxisRaw(Strings.Movement.FIRE_1);
    }

    
}
