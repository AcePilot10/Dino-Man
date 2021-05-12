using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Transform waypointHolder;
    public float speed;
    public float breakoutTime = 3f;
    public bool isStopped = false;
    public Sprite whiteSprite;

    private Transform[] _waypoints;
    private int _currentWaypoint = -1;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _feetSpriteRenderer;
    private Sprite _originalSprite;
    private Vector3 _prisonLocation;
    private bool _isRetreating = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _prisonLocation = transform.position;
        _originalSprite = _spriteRenderer.sprite;
        _feetSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _waypoints = new Transform[waypointHolder.childCount];
        for (int i = 0; i < waypointHolder.childCount; i++)
        {
            _waypoints[i] = waypointHolder.GetChild(i);
        }
        Breakout();
    }

    public void Breakout()
    {
        StartCoroutine(nameof(BreakoutCoroutine));
    }

    void Update()
    {
        if (_currentWaypoint == -1)
        {
            MoveToPrison();
        }
        if (isStopped)
        {
            return;
        }
        if (_currentWaypoint != -1)
            Move();
    }

    #region Movement

    private void Move()
    {
        var targetPos = _waypoints[_currentWaypoint].position;
        var distance = Vector2.Distance(transform.position, targetPos);
        if (distance < 0.1)
        {
            if (!_isRetreating)
            {
                if (_currentWaypoint == _waypoints.Length - 1)
                {
                    _currentWaypoint = 1;
                    ReverseWaypoints();
                }
                else
                    _currentWaypoint++;
            }
            else
            {
                if (_currentWaypoint == 1)
                {
                    _currentWaypoint = _waypoints.Length - 1;
                    ReverseWaypoints();
                }
                else
                    _currentWaypoint--;
            }
        }

        Vector2 p = Vector2.MoveTowards(transform.position,
                                        targetPos,
                                        speed);
        Rotate();
        _rb.MovePosition(p);

    }

    private void ReverseWaypoints()
    {
        _waypoints.Reverse();
    }

    private void Rotate()
    {
        var direction = _waypoints[_currentWaypoint].position - transform.position;
        if (direction.x < 0)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }

    private void MoveToPrison()
    {
        transform.Translate(((_prisonLocation - transform.position).normalized * speed));
    }

    #endregion

    public void ReturnToPrison()
    {
        _currentWaypoint = -1;
        isStopped = true;
    }

    public void Reset()
    {
        ReturnToPrison();
        StartCoroutine(nameof(BreakoutCoroutine));
    }

    private IEnumerator BreakoutCoroutine()
    {
        yield return new WaitForSeconds(breakoutTime);
        isStopped = false;
        _currentWaypoint = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            FindObjectOfType<GameManager>().PlayerHit(this);
        }
    }

    #region Retreating

    int _currentColorIndex = 0;

    public void Retreat()
    {
        _isRetreating = true;
        if (_currentWaypoint == 0)
            _currentWaypoint = _waypoints.Length - 1;
        else
            _currentWaypoint--;
        StartCoroutine(nameof(FlashRetreatColors));
    }

    public void FinishRetreating()
    {
        _isRetreating = false;
        isStopped = false;
    }

    private IEnumerator FlashRetreatColors()
    {
        Color originalColor = new Color(255, 255, 255);
        _spriteRenderer.sprite = whiteSprite;
        while (_isRetreating)
        {
            yield return new WaitForSeconds(0.1f);
            if (_currentColorIndex == 0)
            {
                _spriteRenderer.color = Color.blue;
                _feetSpriteRenderer.color = Color.blue;
                _currentColorIndex = 1;
            }
            else
            {
                _spriteRenderer.color = Color.white;
                _feetSpriteRenderer.color = Color.white;
                _currentColorIndex = 0;
            }
        }
        _spriteRenderer.color = originalColor;
        _feetSpriteRenderer.color = originalColor;
        _spriteRenderer.sprite = _originalSprite;
        StartCoroutine(nameof(BreakoutCoroutine));
    }

    #endregion
}