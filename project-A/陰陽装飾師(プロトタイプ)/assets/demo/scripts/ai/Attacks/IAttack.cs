﻿/***
 * 
 *    Title: すべての攻撃タイプのインタフェース
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Attack(Transform target);
}
