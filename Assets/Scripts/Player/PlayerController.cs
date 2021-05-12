using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSoundController;

public partial class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    private Vector3 _startPos;
    private bool _isDying = false;
    public bool IsDying
    {
        get
        {
            return _isDying;
        }
        set 
        {
            _isDying = value;
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _startPos = transform.position;
    }

    private void Start()
    {
        GameSoundController.Instance.PlaySound(GameSoundTypes.BEGIN);
        GameObject.FindGameObjectWithTag("Player Move Sound").GetComponent<AudioSource>().PlayDelayed(5f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Die()
    {
        GameSoundController.Instance.PlaySound(GameSoundTypes.DIE);
        _canMove = false;
        _anim.SetTrigger("Die");
        _targetDirection = Vector2.zero;
        _currentDirection = Vector2.zero;
        _spriteRenderer.flipX = false;
        _spriteRenderer.flipY = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        FindObjectOfType<EnemyManager>().StopAllEnemies();
        _isDying = true;
    }

    public void DeathAnimationComplete()
    {
        FindObjectOfType<GameManager>().Respawn();
    }
}