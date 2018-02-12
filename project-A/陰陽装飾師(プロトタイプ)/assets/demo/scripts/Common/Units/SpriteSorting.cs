/***
 * 
 *    Title: スプライトの順序はyの位置に依存します
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    // Staticは更新時には順序を変更しません。開始時にのみ
    public bool isStatic;
    // 精度向上のための乗算器
    public float rangeFactor = 100f;

    // このオブジェクトと子のスプライトリスト
    private Dictionary<SpriteRenderer, int> sprites = new Dictionary<SpriteRenderer, int>();

    void Awake()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprites.Add(sprite, sprite.sortingOrder);
        }
    }

    void Start()
    {
        UpdateSortingOrder();
    }

    void Update()
    {
        if (isStatic == false)
        {
            UpdateSortingOrder();
        }
    }

    /// <summary>
    /// スプライトの並べ替え順序を更新します
    /// </summary>
    private void UpdateSortingOrder()
    {
        foreach (KeyValuePair<SpriteRenderer, int> sprite in sprites)
        {
            sprite.Key.sortingOrder = sprite.Value - (int)(transform.position.y * rangeFactor);
        }
    }
}
