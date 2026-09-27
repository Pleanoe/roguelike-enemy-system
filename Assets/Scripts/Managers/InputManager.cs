using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> OnMove;
    public static event Action OnMouseHeld;
    
    private Vector2 previousInput;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            OnMouseHeld?.Invoke();
        }
        
        float moveHor = Input.GetAxisRaw("Horizontal");
        float  moveVer = Input.GetAxisRaw("Vertical");
        Vector2 currentInput= new Vector2(moveHor, moveVer).normalized; //Направление 
        
        if (currentInput != Vector2.zero || previousInput != Vector2.zero)
        {
            OnMove?.Invoke(currentInput); 
            previousInput = currentInput;
        }
    }
}
