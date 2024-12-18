using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BashAstar : MonoBehaviour //나는 이게 Astar가 아니라는 것에 만원을 걸다 -배승현
{
    public Vector3 target;
    private Vector3 _maxtmp = new Vector3(10, 1, 10);
    [SerializeField] 
    private LayerMask _whatisObstacle;

    private List<Vector3> _dirs = new List<Vector3>();
    private List<float> _distances = new List<float>();

    public Vector3 PathDir => _maxtmp;

    private void Awake()
    {
        InvokeRepeating(nameof(Pathfind), 0f, 0.5f);
    }
    private void Pathfind()
    {
        {
            _dirs.Clear();
            _distances.Clear();

            for (int i = -1; i <= 1; i++)
            {
                for (int i2 = -1; i2 <= 1; i2++)
                {
                    if (((i != 0 || i2 != 0) && (i * i2 == 0)))
                        if (!(Physics.Raycast(transform.position, Vector3.forward * i2 + Vector3.right * i, 1f, _whatisObstacle, QueryTriggerInteraction.Ignore)))
                        {
                            _dirs.Add((Vector3.right * i + Vector3.forward * i2).normalized * 0.6f);
                        }
                }
            }
            for (int i = 0; i < _dirs.Count; i++)
            {
                _distances.Add(Vector3.Distance(transform.position + _dirs[i], target));
                if (_dirs[i] == _maxtmp * -1)
                {
                    _distances[i] = 1024;
                }
            }
            _maxtmp = _dirs[_distances.IndexOf(_distances.Min())];
        }
    }
}
