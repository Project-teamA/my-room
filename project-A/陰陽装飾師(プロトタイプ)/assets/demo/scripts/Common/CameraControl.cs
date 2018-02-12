/***
 * 
 *    Title: カメラの移動と自動スケーリング
 *   
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    /// <summary>
    /// カメラオートスケーリングのコントロールタイプ
    /// </summary>
    public enum ControlType
    {
        ConstantWidth,  // カメラは一定の幅を保つ
        ConstantHeight	// カメラは一定の高さを維持する
    }

    // カメラコントロールタイプ
    public ControlType controlType;
    // カメラはこのオブジェクトに合わせてオートスケールします
    public SpriteRenderer focusObjectRenderer;
    // フォーカスオブジェクトのエッジからの水平オフセット
    public float offsetX = 0f;
    // フォーカスオブジェクトのエッジからの垂直オフセット
    public float offsetY = 0f;
    // 移動（ドラッグ）時のカメラの速度
    public float dragSpeed = 2f;

    // カメラ移動の制限点
    private float maxX, minX, maxY, minY;
    // 今すぐベクターをドラッグする
    private float moveX, moveY;
    // このゲームオブジェクトのカメラコンポーネント
    private Camera cam;
    // 原点カメラのアスペクト比
    private float originAspect;

    /// <summary>
    /// このインスタンスを開始します
    /// </summary>
    void Start()
	{
		cam = GetComponent<Camera>();
		Debug.Assert (focusObjectRenderer && cam, "間違った初期設定");
		originAspect = cam.aspect;
        // フォーカスオブジェクトのコーナーから制限ポイントを取得する
        maxX = focusObjectRenderer.bounds.max.x;
		minX = focusObjectRenderer.bounds.min.x;
		maxY = focusObjectRenderer.bounds.max.y;
		minY = focusObjectRenderer.bounds.min.y;
		UpdateCameraSize();
	}

    /// <summary>
    /// 最新のインスタンスを更新します
    /// </summary>
    void LateUpdate()
    {
        // カメラのアスペクト比が変更されました
        if (originAspect != cam.aspect)
		{
			UpdateCameraSize();
			originAspect = cam.aspect;
		}
        // カメラを水平に動かす必要がある
        if (moveX != 0f)
        {
            // 水平移動が許可されている
            if (controlType == ControlType.ConstantHeight)
			{
				bool permit = false;
                // 右に移動
                if (moveX > 0f)
				{
                    // 制限ポイントに達していない場合
                    if (cam.transform.position.x + (cam.orthographicSize * cam.aspect) < maxX - offsetX)
					{
						permit = true;
					}
				}
                // 左に移動
                else
                {
                    // 制限ポイントに達していない場合
                    if (cam.transform.position.x - (cam.orthographicSize * cam.aspect) > minX + offsetX)
					{
						permit = true;
					}
				}
				if (permit == true)
				{
                    // カメラを動かす
                    transform.Translate(Vector3.right * moveX * dragSpeed, Space.World);
				}
			}
            moveX = 0f;
        }
        // 垂直方向にカメラを移動する必要がある
        if (moveY != 0f)
        {
            // 許可された垂直方向の移動
            if (controlType == ControlType.ConstantWidth)
			{
				bool permit = false;
                // 上がる
                if (moveY > 0f)
				{
                    // 制限ポイントに達していない場合
                    if (cam.transform.position.y + cam.orthographicSize < maxY - offsetY)
					{
						permit = true;
					}
				}
                // 下に移動
                else
                {
                    // 制限ポイントに達していない場合
                    if (cam.transform.position.y - cam.orthographicSize > minY + offsetY)
					{
						permit = true;
					}
				}
				if (permit == true)
				{
                    // カメラを動かす
                    transform.Translate (Vector3.up * moveY * dragSpeed, Space.World);
				}
			}
            moveY = 0f;
        }
    }

    /// <summary>
    /// カメラを水平に動かす必要があります
    /// </summary>
    /// <param name="distance">距離</param>
    public void MoveX(float distance)
    {
        moveX = distance;
    }

    /// <summary>
    /// カメラを垂直方向に移動する必要があります
    /// </summary>
    /// <param name="distance">距離</param>
    public void MoveY(float distance)
    {
        moveY = distance;
    }

    /// <summary>
    /// フォーカスオブジェクトに合わせてカメラのサイズを更新します
    /// </summary>
    private void UpdateCameraSize()
	{
		switch (controlType)
		{
		case ControlType.ConstantWidth:
			cam.orthographicSize = (maxX - minX - 2 * offsetX) / (2f * cam.aspect);
			break;
		case ControlType.ConstantHeight:
			cam.orthographicSize = (maxY - minY - 2 * offsetY) / 2f;
			break;
		}
	}
}
