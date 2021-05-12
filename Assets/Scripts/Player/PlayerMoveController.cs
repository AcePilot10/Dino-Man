using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSoundController;

public partial class PlayerController : MonoBehaviour
{
    public float speed = 0.4f;
    public GameObject colliderObject;

    private Vector2 _targetDirection = Vector2.zero;
    private Vector2 _currentDirection = Vector2.zero;
    private bool _hasMadeFirstMove = false;
    private bool _canMove = false;

    public void Move()
    {
        CheckForFirstMove();
        if (!_canMove)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        //GameSoundController.Instance.PlaySound(GameSoundTypes.CHOMP);
        _rb.velocity = _currentDirection * speed;
        Rotate();

        // set taregt direction
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _targetDirection = Vector2.up;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _targetDirection = Vector2.right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _targetDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _targetDirection = Vector2.left;
        }

        if (_targetDirection == Vector2.zero)
            return;
        // set current direction
        if (_targetDirection == Vector2.up && Valid(Vector2.up))
        {
            _currentDirection = Vector2.up;
        }
        if (_targetDirection == Vector2.right && Valid(Vector2.right))
        {
            _currentDirection = Vector2.right;

        }
        if (_targetDirection == Vector2.down && Valid(Vector2.down))
        {
            _currentDirection = Vector2.down;

        }
        if (_targetDirection == Vector2.left && Valid(Vector2.left))
        {
            _currentDirection = Vector2.left;

        }
    }
    public void WakeupComplete()
    {
        _canMove = true;
    }

    public void Reset()
    {
        transform.position = _startPos;
        _hasMadeFirstMove = false;
        _anim.Play("Wake Up");
    }

    private void CheckForFirstMove()
    {
        if (_hasMadeFirstMove)
            return;
        if (Input.GetKey(KeyCode.UpArrow) && Valid(Vector2.up))
        {
            _currentDirection = Vector2.up;
            _hasMadeFirstMove = true;
            _anim.SetTrigger("Start");
        }
        if (Input.GetKey(KeyCode.RightArrow) && Valid(Vector2.right))
        {
            _currentDirection = Vector2.right;
            _hasMadeFirstMove = true;
            _anim.SetTrigger("Start");
        }
        if (Input.GetKey(KeyCode.DownArrow) && Valid(-Vector2.up))
        {
            _currentDirection = Vector2.down;
            _hasMadeFirstMove = true;
            _anim.SetTrigger("Start");
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Valid(-Vector2.right))
        {
            _currentDirection = Vector2.left;
            _hasMadeFirstMove = true;
            _anim.SetTrigger("Start");
        }
    }

    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 2);

        if (hit.collider == null)
            return true;
        else
            return hit.collider.gameObject != colliderObject;
    }

    private void Rotate()
    {
        Quaternion rotation = transform.rotation;
        if (_currentDirection == Vector2.up || _currentDirection == Vector2.down)
        {
            if (_spriteRenderer.flipX)
            {
                rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else
            {
                rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }

        }
        if (_currentDirection == Vector2.down)
        {
            if (_spriteRenderer.flipX)
            {
                rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }
            else
            {
                rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
        }
        if (_currentDirection == Vector2.right || _currentDirection == Vector2.left)
        {
            rotation = Quaternion.Euler(Vector3.zero);
            _spriteRenderer.flipX = (_currentDirection == Vector2.right);
        }

        transform.rotation = rotation;
    }
}