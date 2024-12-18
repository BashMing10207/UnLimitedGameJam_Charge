using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GolemBoss : MonoBehaviour
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform chest;
    
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float bulletSpeed = 5f;
        
    [SerializeField] private Transform TestTarget;

    [SerializeField] private CinemachineImpulseSource ImpulseSource;
    
    private bool isLeftHandMoving;
    private bool isRightHandMoving;
        
    #region Bullet
    public void CircleShot(int bulletCount)
    {
        ApplyCircleShot(leftHand,bulletCount);
        ApplyCircleShot(rightHand,bulletCount);
    }
    private void ApplyCircleShot(Transform _firePos, int bulletCount, float _angle = 360)
    {
        float angleStep = _angle / bulletCount;
        float angle = _firePos.rotation.eulerAngles.z; 

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletMoveDirection = new Vector3(bulletDirX, bulletDirY, 0f).normalized;

            GameObject bullet = Instantiate(bulletPrefab, _firePos.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletMoveDirection * bulletSpeed;
            }

            angle += angleStep; 
        }
    }
    
    public void SpinShot(int bulletCount)
    {
        StartCoroutine(ApplySpinShot(leftHand, bulletCount,360));
    }
    private IEnumerator ApplySpinShot(Transform _firePos,int bulletCount,int _bulletAmount)
    {
        float angleStep = 360f / bulletCount; 
        float angle = 0f;
        
        for (int i = 0; i < _bulletAmount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletMoveDirection = new Vector3(bulletDirX, bulletDirY, 0f).normalized;

            GameObject bullet = Instantiate(bulletPrefab, _firePos.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletMoveDirection * bulletSpeed;
            }

            angle += angleStep;
            yield return new WaitForSeconds(0.01f);
        }        
    }
    
    public void SectorFormShot(float _angle , int _bulletCount,float _time) 
    {
        if(chest != null)
            StartCoroutine(ApplySectorFormShot(chest , _angle ,_bulletCount ,_time));
    } 
    private IEnumerator ApplySectorFormShot(Transform _firePos, float _angle, int _bulletCount,float _shotTime)
    {
        float elapsedTime = 0f;
        float lastShotTime = 0f;
        
        float startAngle = 270;
        float endAngle = 180;      
        
        _firePos.rotation = Quaternion.Euler(0, 0, startAngle);

        while (elapsedTime < _shotTime)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / _shotTime; 
            float rotationAngle = Mathf.Lerp(startAngle, endAngle, progress);

            _firePos.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

            if (elapsedTime - lastShotTime >= 0.15f)
            {
                ApplyCircleShot(_firePos, _bulletCount, _angle);
                lastShotTime = elapsedTime;
            }
            
            yield return null;
        }
    }
    #endregion
    
    #region Punch
    private void TakeDown()
    {
        StartCoroutine(TakDownLeftHandRoutine(TestTarget));
        StartCoroutine(TakDownRightHandRoutine(TestTarget));
    }
    
    private IEnumerator TakDownLeftHandRoutine(Transform _target)
    {
        if(isLeftHandMoving) yield break;
        
        isLeftHandMoving = true;
        
        Vector3 originalPosition = leftHand.position;
        Vector3 liftTarget = _target.position + Vector3.up * 1.3f;
        Vector3 strikeTarget = _target.position;

        yield return StartCoroutine(MoveToPosition(leftHand, liftTarget, 1f));
    
        yield return new WaitForSeconds(0.3f);
    
        yield return StartCoroutine(MoveToPosition(leftHand, strikeTarget, 20f));
        ImpulseSource.GenerateImpulse(2);
                
        yield return new WaitForSeconds(0.3f);
    
        yield return StartCoroutine(MoveToPosition(leftHand, originalPosition, 1f));
        
        isLeftHandMoving = false;
    }
    
    private IEnumerator TakDownRightHandRoutine(Transform _target)
    {
        if(isRightHandMoving) yield break;
        
        isRightHandMoving = true;
        
        Vector3 originalPosition = rightHand.position;
        Vector3 liftTarget = _target.position + Vector3.up * 2f;
        Vector3 strikeTarget = _target.position;

        yield return StartCoroutine(MoveToPosition(rightHand, liftTarget, 1f));
    
        yield return new WaitForSeconds(0.3f);
    
        yield return StartCoroutine(MoveToPosition(rightHand, strikeTarget, 20f));
        ImpulseSource.GenerateImpulse(2);
        
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(MoveToPosition(rightHand, originalPosition, 1f));
        
        isRightHandMoving = false;
    }
    
    private IEnumerator MoveToPosition(Transform obj, Vector3 target, float speed)
    {
        float journeyLength = Vector3.Distance(obj.position, target);
        float startTime = Time.time;

        while (obj.position != target)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;

            obj.position = Vector3.Lerp(obj.position, target, fractionOfJourney);
        
            yield return null;
        }
    }

    private void CrossHand()
    {
        StartCoroutine(CrossHandRoutine(TestTarget));
    }
    
    private IEnumerator CrossHandRoutine(Transform _target)
    {
        if(isLeftHandMoving || isRightHandMoving) yield break;

        isLeftHandMoving = true;
        isRightHandMoving = true;

        Vector3 originalLeftPosition = leftHand.position;
        Vector3 originalRightPosition = rightHand.position;

        Vector3 leftSidePosition = _target.position + _target.right * -3f;
        Vector3 rightSidePosition = _target.position + _target.right * 3f;
    
        // Move to side positions simultaneously
        Coroutine leftMove = StartCoroutine(MoveToPosition(leftHand, leftSidePosition, 3f));
        Coroutine rightMove = StartCoroutine(MoveToPosition(rightHand, rightSidePosition, 3f));
    
        yield return leftMove;
        yield return rightMove;

        yield return new WaitForSeconds(0.2f);
    
        leftMove = StartCoroutine(MoveToPosition(leftHand, _target.position, 10f));
        rightMove = StartCoroutine(MoveToPosition(rightHand, _target.position, 10f));
        
        yield return leftMove;
        yield return rightMove;
    
        ImpulseSource.GenerateImpulse(3);
        
        yield return new WaitForSeconds(0.15f);
        
        leftMove = StartCoroutine(MoveToPosition(leftHand, originalLeftPosition, 3f));
        rightMove = StartCoroutine(MoveToPosition(rightHand, originalRightPosition, 3f));
    
        yield return leftMove;
        yield return rightMove;
    
        isLeftHandMoving = false;
        isRightHandMoving = false;
    }
        
    #endregion
}
