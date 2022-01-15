using System;
using UnityEngine;
using UnityEngine.UI;
namespace FrameworkDesign.Example
{
    public class GameOverPanel : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}