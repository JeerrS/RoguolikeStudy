using UnityEngine;

public class Enemy : MonoBehaviour {
    private Transform _player;
    private Vector2 _pos;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _pos = transform.position;
        GameManager.Instance.enemies.Add(this);
    }

    void Update() {
        _rb.MovePosition(_pos);
    }

    public void Move() {
        Vector2 offset = _player.position - transform.position;
        if (offset.magnitude < 1.1) {
            //
        } else {
            float x = 0, y = 0;
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x)) {
                if (offset.y < 0) {
                    y = -1;
                } else {
                    y = 1;
                }
            } else {
                if (offset.x < 0) {
                    x = -1;
                } else {
                    x = 1;
                }
            }
            _collider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(_pos, _pos + new Vector2(x, y));
            _collider.enabled = true;
            if (hit.transform == null) {
                _pos += new Vector2(x, y);
            } else {
                switch (hit.collider.tag) {
                    case "Wall":
                        break;
                    case "Barrier":
                        break;
                    case "Food":
                        _pos += new Vector2(x, y);
                        break;
                    case "Player":
                        break;
                }
            }
        }
    }
}