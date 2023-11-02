namespace Nivandria.Battle
{
    using Nivandria.Battle.Grid;
    using TMPro;
    using UnityEngine;

    public class DamagePopUp : MonoBehaviour
    {
        private float moveSpeed = 5.5f;
        private float disappearTimer = 0.7f;
        private float disappearSpeed = 5f;

        private TextMeshPro textMesh;
        private Color textColor;

        private Vector3 normalPosition;
        private bool movingUp;
        private bool movingDown;

        private static int sortingOrder;

        public static DamagePopUp Create(Vector3 position, int damageAmount, bool isCriticalHit)
        {
            position.x += Random.Range(-0.5f, 0.5f);
            position.y = Random.Range(0.5f, 1.0f);
            Transform damagePopUpTransform = Instantiate(LevelGrid.Instance.GetDamagePopUpPrefabs(), position, Quaternion.identity);
            DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
            damagePopUp.Setup(damageAmount, isCriticalHit);
            return damagePopUp;
        }

        private Transform mainCamera;

        void Awake()
        {
            textMesh = GetComponent<TextMeshPro>();
            textColor = textMesh.color;

            mainCamera = Camera.main.transform;
        }

        void Setup(int damage, bool isCriticalHit)
        {
            if (damage <= 0) textMesh.text = "MISS";
            else textMesh.text = damage.ToString();

            normalPosition = transform.position;

            if (isCriticalHit)
            {
                textMesh.fontSize = 10;
            }

            sortingOrder++;
            textMesh.sortingOrder = sortingOrder;
        }

        private void Update()
        {
            if (transform.position.y < normalPosition.y + 0.5f && !movingUp)
            {
                transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

            }
            else if (transform.position.y > normalPosition.y && !movingDown)
            {
                transform.position -= new Vector3(0, moveSpeed) * Time.deltaTime;
                movingUp = true;

            }
            else
            {
                movingDown = true;
                if (textColor.a > 0.8f)
                {
                    textColor.a -= 1f * Time.deltaTime;
                    textMesh.color = textColor;
                }
            }

            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0) Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
            LookAtBackwards(mainCamera.position);
        }

        private void LookAtBackwards(Vector3 targetPos)
        {
            Vector3 offset = transform.position - targetPos;
            transform.LookAt(transform.position + offset);
        }
    }

}