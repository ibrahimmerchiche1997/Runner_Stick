using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Coins_Manager : MonoBehaviour
{
    public Text coin_text;
    private int c = 0;
    [SerializeField] GameObject coin_prefab;

    [Space]
    [Header("Pooling System")]
    [SerializeField] GameObject coin_animated_prefab;
    [SerializeField] Transform target_coin;
    Queue<GameObject> coinQueu = new Queue<GameObject>();


    [Space]
    [Header("Animation settting")]
    [SerializeField] int maxCoin = 5;
    [SerializeField] [Range(.5f, .9f)] float minAnimDuration;
    [SerializeField] [Range(.9f, 2f)] float maxAnimDuration;

    Vector3 targetposition;


    private void Awake()
    {
        targetposition = target_coin.position;
        PrepareCoins();

    }

    void PrepareCoins()
    {
        GameObject coin;
        for (int i = 0; i < maxCoin; i++)
        {
            
            coin = Instantiate(coin_animated_prefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinQueu.Enqueue(coin);

        }
    }
    public void Collect_Coins(Transform other)
    {
        GameObject z = Instantiate(coin_prefab, other.transform.position, Quaternion.identity);
        z.transform.parent = transform;
        Destroy(z,1);
    }

    void Animate(Vector3 collectCoinPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (coinQueu.Count > 0)
            {
                GameObject coin = coinQueu.Dequeue();
                coin.SetActive(true);
                coin.transform.position = collectCoinPosition;
                float dur = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetposition, dur)
                    .SetEase(Ease.InOutBack)
                    .OnComplete(() =>
                                    {
                                        coin.SetActive(false);
                                        coinQueu.Enqueue(coin);
                                        _Coin++;
                                    })

                    ;
            }
        }
    }



    public void AddCoin(Vector3 collectCoinPosition, int amount)
    {
        Animate(collectCoinPosition, amount);
    }

    public int _Coin
    {
        get { return c; }
        set
        {
            c = value;
            coin_text.text = _Coin.ToString();
        }
    }
}
