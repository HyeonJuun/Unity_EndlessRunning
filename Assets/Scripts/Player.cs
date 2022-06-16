using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forward_speed;
    public float max_speed;

    private int desired_lane = 1; // 0 : Left 1 : Middle 2: Right
    public float lane_distance = 2.5f;
    public bool is_grounded;

    public float jump_force;
    public float gravity = -20;
    private Vector3 velocity;

    public Animator animator;
    private bool is_sliding;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        
        if(forward_speed < max_speed)
            forward_speed += 0.1f * Time.deltaTime;

        direction.z = forward_speed;

        animator.SetBool("isGameStarted", true);

        //is_grounded = Physics.CheckSphere(ground_check.position, 0.17f, ground_layer);

        animator.SetBool("isGrounded", is_grounded);

        if (is_grounded && velocity.y < 0)
            velocity.y = -1f;

        if (controller.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || SwipeManager.swipe_up)
            {
                Jump();
            }
            
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || SwipeManager.swipe_down && !is_sliding)
        {
            StartCoroutine(Slide());
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.swipe_right)
        {
            desired_lane++;
            if (desired_lane == 3)
                desired_lane = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipe_left)
        {
            desired_lane--;
            if (desired_lane == -1)
                desired_lane = 0;

        }
        Vector3 target_position = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desired_lane == 0)
        {
            target_position += Vector3.left * lane_distance;
        }
        else if (desired_lane == 2)
        {
            target_position += Vector3.right * lane_distance;
        }
        //transform.position = target_position;
        //transform.position = Vector3.Lerp(transform.position, target_position, Time.fixedDeltaTime * 80);
        if (transform.position == target_position)
            return;
        Vector3 diff = target_position - transform.position;
        Vector3 move_dir = diff.normalized * 25 * Time.deltaTime;
        if (move_dir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(move_dir);
        else
            controller.Move(diff);
    }
    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jump_force;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameover = true;
        }
    }

    private IEnumerator Slide()
    {
        is_sliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);
        
        controller.center = new Vector3(0, 0f, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        is_sliding = false;
    }
}
