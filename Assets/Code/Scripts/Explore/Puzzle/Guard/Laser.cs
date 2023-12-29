namespace Nivandria.Explore.Puzzle
{
    using UnityEngine;

    /// <summary>
    /// Simulates a laser beam that can bounce off surfaces a certain number of times.
    /// </summary>
    public class Laser : MonoBehaviour
    {
        private int _maxBounce = 20;  // The maximum number of times the laser can bounce.
        private int _count;          // The current bounce count.
        private LineRenderer _laser; // The LineRenderer component used to visualize the laser.
        [SerializeField] private Vector3 _offSet; // Offset for the laser's starting position.

        private void Start()
        {
            _laser = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            _count = 0; // Reset the bounce count on each update.
            castLaser(transform.position + _offSet, transform.forward);
        }

        private void castLaser(Vector3 position, Vector3 direction)
        {
            _laser.SetPosition(0, transform.position + _offSet); // Set the initial position of the laser.
            _count = 0; // Start with _count = 0 for each castLaser call.

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

                    _laser.positionCount = _count + 1; // Set the number of positions for the LineRenderer.

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

                    _laser.positionCount = _count + 1; // Set the number of positions for the LineRenderer.

                    _laser.SetPosition(_count, position);
                }
            }
        }

        private void OnDisable()
        {
            // When the Laser script is disabled, also disable the LineRenderer.
            _laser.enabled = false;
        }

        private void OnEnable()
        {
            // When the Laser script is enabled, also enable the LineRenderer.
            _laser.enabled = true;
        }
    }
}