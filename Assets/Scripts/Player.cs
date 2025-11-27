using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Floats")]
    [SerializeField] float maxLife;
    float _life;

    [Header("Bools")]
    private bool running;

    private void Start()
    {
        _life = maxLife;
    }

    private void Update()
    {
        if (_life > maxLife && !running)
        {
            running = true;
            StartCoroutine(GraduallyReduceHP(_life - maxLife, 5f));
        }
    }

    public float GetLife()
    {
        return _life;
    }

    public float GetMaxLife()
    {
        return maxLife;
    }

    public void Damage(float d)
    {

        _life -= d;
        Debug.Log(_life);

        if (_life <= 0)
        {
            Death();
        }
    }

    public void Heal(float amount)
    {
        _life += amount;
    }

    public IEnumerator GraduallyReduceHP(float damage, float rate)
    {
        while (damage > 0)
        {
            float delta = rate * Time.deltaTime;
            if(_life <= 0)
            {
                Death();
            }
            if(delta > damage)
            {
                _life -= damage;
                break;
            }
            _life -= delta;
            damage -= delta;
            yield return null;
        }

        running = false;
    }

    public void Death()
    {
        Debug.Log("Me fulminaron wachin que carajo hiciste pedazo de aweonado");
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("Nivel 1 (Re-Design)");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
