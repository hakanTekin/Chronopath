using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Assets.Code.World.Obstacle
{
    class Obstacle : MonoBehaviour, IObstacle
    {

        SpriteShape shape;
        
        Vector2 size;
        BoxCollider2D collider;
        private int score = 0;
        private SpriteRenderer renderer;
        [SerializeField] private Sprite sprite;
        public int Score { get => score; set => score = value; }

        private void Awake()
        {
            sprite = Resources.Load<Sprite>("HandPainted Lava Textures/Textures/03");
            collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(2,2); //TODO: Why?
            collider.offset = Vector2.zero;
            renderer = gameObject.AddComponent<SpriteRenderer>();
            shape = Resources.Load<SpriteShape>("ObstacleShapeProfile");
            renderer.sprite = sprite;
            renderer.drawMode = SpriteDrawMode.Tiled;
            renderer.tileMode = SpriteTileMode.Continuous;
        }

        public Spline CreateSpline()
        {
            //Spline box = shapeController.spline;
            renderer.size = collider.size;
            Vector3 lt, rt, lb, rb; //left-top, right-top, left-bottom, right-bottom;
            lt = new Vector3(
                -1 * (collider.size.x / 2),
                collider.size.y / 2,
                gameObject.transform.position.z
                );

            rt = new Vector3(
                collider.size.x / 2,
                collider.size.y / 2,
                gameObject.transform.position.z
                );

            lb = new Vector3(
                -1 * (collider.size.x / 2),
                -1 * (collider.size.y / 2),
                gameObject.transform.position.z
                );

            rb = new Vector3(
                collider.size.x / 2,
               -1 * (collider.size.y / 2),
                gameObject.transform.position.z
                );

            // box.InsertPointAt(0, lt);
            // box.InsertPointAt(1, rt);
            // box.InsertPointAt(2, rb);
            // box.InsertPointAt(3, lb);

            //shapeController.splineDetail = 1;
            //shapeController.RefreshSpriteShape();
            //return box;
            return null;
        }
        public void UpdateObstacle()
        {
        }

        GameObject IObstacle.GetGameObject()
        {
            return this.gameObject;
        }
        public World GetWorld()
        {
            return FindObjectOfType<World>();
        }

        string IObstacle.GetType()
        {
            throw new NotImplementedException();
        }

        bool AssignCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
            return true;
        }
    }
}
