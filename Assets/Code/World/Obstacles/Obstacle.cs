using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.World.Obstacle
{
    class Obstacle : MonoBehaviour, IObstacle
    {
        Vector2 size;
        BoxCollider2D collider;
        private int score = 100;
        public int Score { get => score; set => score = value; }

        private void Awake()
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(2,2);
            Text t = gameObject.AddComponent<Text>();
            t.text = score.ToString();
            Font TextFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            t.font = TextFont;
            t.material = TextFont.material;
        }

        public void UpdateObstacle()
        {
            Debug.Log("ObstacleUpdate");
        }

        GameObject IObstacle.GetGameObject()
        {
            return this.gameObject;
        }

        string IObstacle.GetType()
        {
            throw new NotImplementedException();
        }
    }
}
