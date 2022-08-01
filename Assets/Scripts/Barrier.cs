using UnityEngine;

public class Barrier : MonoBehaviour {

    public int health = 2;
    public Sprite brokenBarrier;

    public void TakeDamage() {
        health -= 1;
        GetComponent<SpriteRenderer>().sprite = brokenBarrier;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
