using System;
using Code.BilliardEquipment;
using Code.BilliardEquipment.Balls;
using Code.BilliardEquipment.Pyramid;
using Code.BilliardEquipment.Table;
using Code.Other;
using Code.UI;
using UnityEngine;

namespace Code.Infrastructure
{
    [Serializable]
    public class SceneData
    {
        public Camera MainCamera;
        public UIRoot UIRoot;
        public PoolTable Table;
        public CueStick CueStick;
        public BallCollection BallCollection;
        public BallPyramid BallPyramid;
        public BallDropTrigger BallDropTrigger;
    }
}