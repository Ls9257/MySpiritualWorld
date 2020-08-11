using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Com.PerlinNoise.Fumiki
{
    public class RockMaterialMonitor : MonoBehaviour
    {
        /// <summary>
        /// 图片的宽度
        /// </summary>
        [SerializeField] private int pictureWidth = 100;
        /// <summary>
        /// 图片的高度
        /// </summary>
        [SerializeField] private int pictrueHeight = 100;
        /// <summary>
        /// 用于柏林噪声的X采样偏移量（仿伪随机）
        /// </summary>
        [SerializeField] private float xOrg = .0f;
        /// <summary>
        /// 用于柏林噪声的Y采样偏移量（仿伪随机）
        /// </summary>
        [SerializeField] private float yOrg = .0f;
        /// <summary>
        /// 柏林噪声的缩放值（值越大，柏林噪声计算越密集）
        /// </summary>
        [SerializeField] private float scale = 20.0f;
        /// <summary>
        /// 最终生成的柏林噪声图
        /// </summary>
        private Texture2D noiseTex;
        /// <summary>
        /// 颜色数组
        /// </summary>
        private Color[] pix;
        /// <summary>
        /// 方块的材质
        /// </summary>
        private MeshRenderer meshRend;

        private void Start()
        {
            meshRend = GetComponent<MeshRenderer>();
            noiseTex = new Texture2D(pictureWidth, pictrueHeight);
            // 根据图片的宽高填充颜色数组
            pix = new Color[noiseTex.width * noiseTex.height];
            // 将生成的柏林噪声图赋值给方块的材质
            meshRend.material.mainTexture = noiseTex;
        }

        private void Update()
        {
            // 计算柏林噪声
            CalcNoise();
        }

        /// <summary>
        /// 计算柏林噪声
        /// </summary>
        private void CalcNoise()
        {
            float y = .0f;
            while (y < noiseTex.height)
            {
                float x = .0f;
                while (x < noiseTex.width)
                {
                    // 计算出X的采样值
                    float xCoord = xOrg + x / noiseTex.width * scale;
                    // 计算出Y的采样值
                    float yCoord = yOrg + y / noiseTex.height * scale;
                    // 用计算出的采样值计算柏林噪声
                    float sample = Mathf.PerlinNoise(xCoord, yCoord);
                    // 填充颜色数组
                    pix[Convert.ToInt32(y * noiseTex.width + x)] = new Color(sample, sample, sample);
                    x++;
                }
                y++;
            }
            noiseTex.SetPixels(pix);
            noiseTex.Apply();
        }
    }
}