using Code.Infrastructure.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Trajectory
{
    public class TrajectoryDrawing : IService
    {
        private const string LineDataPath = "LineStaticData";

        private LineRenderer[] _lines = new LineRenderer[2];
        private SpriteRenderer[] _sprites = new SpriteRenderer[2];
        private float _lineDistance = 0.2f;


        public TrajectoryDrawing() => 
            CreateResources();

        public void DrawTrajectoryHitting(Vector3 position1, Vector3 position2, Vector3 direction)
        {
            Show();
            
            _lines[0].SetPositions(new []{position1, position2, (position2 + direction * _lineDistance)});
            _sprites[0].transform.position = position2;
            
            // Debug.DrawLine(a, b, Color.white);
            // Debug.DrawRay(b, c * 0.5f, Color.white);
        }

        public void DrawTrajectoryReflection(Vector3 position, Vector3 direction)
        {
            Show();
            
            _lines[1].SetPositions(new []{position, (position + direction * _lineDistance)});
            _sprites[1].transform.position = position;
            
            //Debug.DrawRay(position, direction * 0.5f, Color.green);
        }
        
        public void Cleanup()
        {
            for (int i = 0; i < _lines.Length; i++)
            {
                if(_lines[i].enabled)
                {
                    _lines[i].enabled = false;
                    _sprites[i].enabled = false;
                }
            }
        }

        public void ClearTrajectoryReflection()
        {
            if (_lines[1].enabled)
            {
                _lines[1].enabled = false;
                _sprites[1].enabled = false;
            }
        }

        private void Show()
        {
            for (int i = 0; i < 2; i++)
            {
                if (!_lines[i].enabled)
                {
                    _lines[i].enabled = true;
                    _sprites[i].enabled = true;
                }
            }
        }

        private void CreateResources()
        {
            TrajectoryDrawingStaticData datas = Resources.Load<TrajectoryDrawingStaticData>(LineDataPath);

            for (int i = 0; i < 2; i++)
            {
                _lines[i] = Object.Instantiate(datas.LinePrefab).GetComponent<LineRenderer>();
                _sprites[i] = Object.Instantiate(datas.CircleSpotPrefab).GetComponent<SpriteRenderer>();
                _sprites[i].transform.localScale = (i == 0) ? Vector3.one : Vector3.one * 1.5f;
            }

            _lines[0].positionCount = 3;
            _lines[1].positionCount = 2;
            
            Cleanup();
        }
    }
}