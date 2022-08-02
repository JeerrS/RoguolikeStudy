using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            return _instance;
        }
    }
    public int level = 1;
    public int satiety = 100;
    public int hp = 100;
    private bool _enemyMoveRest = false;

    public List<Enemy> enemies = new List<Enemy>();
    
    // Start is called before the first frame update
    void Awake() {
        _instance = this;
    }

    public void ReduceFood(int count) {
        satiety -= count;
        if (satiety < 0) satiety = 0;
    }
    public void AddFood(int count) {
        satiety += count;
        if (satiety > 100 ) satiety = 100;
    }
    public void ReduceHp(int count) {
        hp -= count;
        if (hp < 0) hp = 0;
    }
    public void AddHp(int count) {
        hp += count;
        if (hp > 150 ) hp = 150;
    }

    public void onPlayerMove() {
        if (_enemyMoveRest) {
            _enemyMoveRest = false;
        } else {
            foreach (var enemy in enemies) {
                enemy.Move();
            }
            _enemyMoveRest = true;
        }
    }
}