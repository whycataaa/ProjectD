using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ParallaxBackground : MonoBehaviour
{
    private const string CameraName = "CameraMain";
    // 主摄像机的Transform组件
    private Transform mainCameraTrans;
    // 上一帧摄像机的位置
    private Vector3 lastCameraPosition;
    // 背景图单位尺寸
    private float textureUnitSizeX;
    // 跟随摄像机的权重
    public Vector2 FollowWeight;
    // 距离越远的背景权重越高，如 天空、云、太阳等 设置0.8-1范围效果尚可
    // 距离越近的背景权重越低，如 身边的树木、花草、房子等等
    void Start()
    {
        // 获取主摄像机的Transform组件
        mainCameraTrans = GameObject.Find(CameraName).transform;
        // 初始化上一帧摄像机的位置为当前摄像机的位置
        lastCameraPosition = mainCameraTrans.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        // 获取Sprite的纹理
        Texture2D texture = sprite.texture;
        // 计算背景图在游戏场景里的单位尺寸
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void FixedUpdate()
    {
        ImageFollowCamera();
        ResetImageX();
        lastCameraPosition = mainCameraTrans.position; // 更新上一帧摄像机的位置
    }

    private void ResetImageX()
    {
        // 检查是否需要移动背景
        if (Mathf.Abs(mainCameraTrans.position.x - transform.position.x) >= textureUnitSizeX)
        {
            // 重置背景位置
            transform.position = new Vector3(mainCameraTrans.position.x , transform.position.y, transform.position.z);
        }
    }

    private void ImageFollowCamera()
    {
        // 计算摄像机位置的偏移量
        Vector3 offsetPosition = mainCameraTrans.position - lastCameraPosition;
        // 根据权重调整背景图片的位置
        transform.position += new Vector3(offsetPosition.x * FollowWeight.x, offsetPosition.y * FollowWeight.y, 0);
    }

}