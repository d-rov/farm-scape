using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.01f;

    private Vector3 targetPos;
    private bool moving = false;

    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

            if (mousePos.x < -6.3) { mousePos.x = -6.3f;}
            if (mousePos.x > 6.3) { mousePos.x = 6.3f; }
            if (mousePos.y < -3.1) { mousePos.y = -3.1f; }
            if (mousePos.y > 3.5) { mousePos.y = 3.5f; }

            targetPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);
            moving = true;
        }

        if (moving)
        {
            anim.SetBool("isRunning", true);
            if (transform.position.x >= targetPos.x)
            {
                // FlipSprite(true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // FlipSprite(false);
                transform.localScale = new Vector3(1, 1, 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed + Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                moving = false;
                anim.SetBool("isRunning", false);
            }
        }
        Debug.Log("x pos: " + transform.position.x);
        Debug.Log("y pos: " + transform.position.y);
    }
}
