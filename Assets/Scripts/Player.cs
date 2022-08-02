using UnityEngine;

public class Player : MonoBehaviour {
    private static float rest = 0.2f;
    public float restTimer = 0f;
    
    private Vector2 _pos = new Vector2(1, 1);
    private Rigidbody2D _rb;
    private BoxCollider2D _collider;
    private Animator _animator;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        restTimer += Time.deltaTime;
        _rb.MovePosition(_pos);
        if (restTimer < rest) {
            return;
        }
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h > 0) {
            v = 0;
        }
        
        if (h != 0 || v != 0) {
            _collider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(_pos, _pos + new Vector2(h, v));
            _collider.enabled = true;
            if (GameManager.Instance.satiety == 0) {
                GameManager.Instance.ReduceHp(5);
            }
            if (hit.transform == null) {
                GameManager.Instance.ReduceFood(5);
                _pos += new Vector2(h, v);
            } else {
                switch (hit.collider.tag) {
                    case "Wall":
                        break;
                    case "Barrier":
                        _animator.SetTrigger("Attack");
                        GameManager.Instance.ReduceFood(5);
                        hit.collider.SendMessage("TakeDamage");
                        break;
                    case "Food":
                        switch (hit.collider.name) {
                            case "Food1(Clone)":
                                GameManager.Instance.ReduceFood(5);
                                GameManager.Instance.AddHp(15);
                                break;
                            case "Food2(Clone)":
                                GameManager.Instance.AddFood(15);
                                break;
                        }
                        _pos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Enemy":
                        break;
                }
            }
            GameManager.Instance.onPlayerMove();
            restTimer = 0;
        }
    }
}