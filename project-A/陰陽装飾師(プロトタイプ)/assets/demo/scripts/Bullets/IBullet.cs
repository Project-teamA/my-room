/***
 * 
 *    Title: すべての弾丸のインターフェイス
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void SetDamage(int damage);
	int GetDamage();
    void Fire(Transform target);
}
