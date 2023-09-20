namespace Nivandria.Explore.Puzzle
{    
    using UnityEngine;

    public class Laser : MonoBehaviour
    {
        private int _maxBounce = 20;

        private int _count;
        private LineRenderer _laser;

        [SerializeField]
        private Vector3 _offSet;
       
        private void Start()
        {
            _laser = GetComponent<LineRenderer>();
        }
        private void Update()
        {
           _count = 0;            
           castLaser(transform.position + _offSet, transform.forward);            
        }
        private void castLaser(Vector3 position, Vector3 direction)
        {
            _laser.SetPosition(0, transform.position + _offSet);
            _count = 0; // Mulailah dengan _count = 0 di setiap pemanggilan castLaser

            for (int i = 0; i < _maxBounce; i++)
            {
                Ray ray = new Ray(position, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 300))
                {
                    position = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);

                    if (_count < _maxBounce - 1)
                        _count++;

                    _laser.positionCount = _count + 1; // Atur jumlah posisi LineRenderer

                    _laser.SetPosition(_count, hit.point);

                    if (hit.transform.tag == "ObjekLazer")
                    {
                        hit.collider.gameObject.GetComponent<DurasiLaser>().SetDurasi(+0.1f);
                    }

                    if (hit.transform.tag != "PantulanLazer")
                    {
                        break;
                    }
                }
                else
                {
                    position = ray.GetPoint(300);

                    if (_count < _maxBounce - 1)
                        _count++;

                    _laser.positionCount = _count + 1; // Atur jumlah posisi LineRenderer

                    _laser.SetPosition(_count, position);
                }
            }
        }
        private void OnDisable()
        {
            // Ketika skrip Laser dimatikan (disabled), matikan juga LineRenderer
            _laser.enabled = false;
        }
        private void OnEnable()
        {
            // Ketika skrip Laser dinyalakan (enabled), nyalakan juga LineRenderer
            _laser.enabled = true;
        }
    }
}