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
    private IEnumerator ApplySectorFormShot(Transform _firePos, float _angle, int _bulletCount, float _shotTime)
    {
        float elapsedTime = 0f;
        float lastShotTime = 0f;

        float downAngle = 270f;
        float halfAngle = _angle / 2f;
        
        float startAngle = downAngle + halfAngle;
        float endAngle = downAngle - halfAngle;
        
        _firePos.rotation = Quaternion.Euler(0, 0, startAngle);
        
        while (elapsedTime <= _shotTime)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / _shotTime;
            float rotationAngle = Mathf.Lerp(startAngle , endAngle, progress);

            _firePos.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        
            if (elapsedTime - lastShotTime >= 0.2f)
            {
                ApplyCircleShot(_firePos, _bulletCount, halfAngle);
                lastShotTime = elapsedTime;
            }
            
            yield return null;
        }
    }
    
    #endregion
    
    #region Punch
    public void TakeDownLeft(float _speed , float _downSpeed)
    {
        StartCoroutine(TakDownLeftHandRoutine(TestTarget,_speed,_downSpeed));
        
    }
    public void TakeDownRight(float _speed , float _downSpeed)
    {
        StartCoroutine(TakDownRightHandRoutine(TestTarget, _speed, _downSpeed));
    }
    
    
    private IEnumerator TakDownLeftHandRoutine(Transform _target,float _speed,float _downSpeed)
    {
        if(isLeftHandMoving) yield break;
        
        isLeftHandMoving = true;
        
        Vector3 originalPosition = leftHand.position;
        Vector3 liftTarget = _target.position + Vector3.up * 1.3f;
        Vector3 strikeTarget = _target.position;

        yield return StartCoroutine(MoveToPosition(leftHand, liftTarget, _speed));
    
        yield return new WaitForSeconds(0.3f);
    
        yield return StartCoroutine(MoveToPosition(leftHand, strikeTarget, _downSpeed));
        ShakeCamera(2);
        
        
        yield return new WaitForSeconds(0.3f);
    
        yield return StartCoroutine(MoveToPosition(leftHand, originalPosition, _speed));
        
        isLeftHandMoving = false;
    }
    
    private IEnumerator TakDownRightHandRoutine(Transform _target,float _speed,float _downSpeed)
    {
        if(isRightHandMoving) yield break;
        
        isRightHandMoving = true;
        
        Vector3 originalPosition = rightHand.position;
        Vector3 liftTarget = _target.position + Vector3.up * 2f;
        Vector3 strikeTarget = _target.position;

        yield return StartCoroutine(MoveToPosition(rightHand, liftTarget, _speed));
    
        yield return new WaitForSeconds(0.3f);
    
        yield return StartCoroutine(MoveToPosition(rightHand, strikeTarget, _downSpeed));
        ShakeCamera(2);
        
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(MoveToPosition(rightHand, originalPosition, _speed));
        
        isRightHandMoving = false;
    }
    
    

    public void CrossHand(float _speed , float _crossSpeed)
    {
        StartCoroutine(CrossHandRoutine(TestTarget,_speed,_crossSpeed));
    }
    private IEnumerator CrossHandRoutine(Transform _target,float _speed ,float _crossSpeed)
    {
        if(isLeftHandMoving || isRightHandMoving) yield break;

        isLeftHandMoving = true;
        isRightHandMoving = true;

        Vector3 originalLeftPosition = leftHand.position;
        Vector3 originalRightPosition = rightHand.position;

        Vector3 leftSidePosition = _target.position + _target.right * -3f;
        Vector3 rightSidePosition = _target.position + _target.right * 3f;
                
        Coroutine leftMove = StartCoroutine(MoveToPosition(leftHand, leftSidePosition, _speed));
        Coroutine rightMove = StartCoroutine(MoveToPosition(rightHand, rightSidePosition, _speed));
    
        yield return leftMove;
        yield return rightMove;
        
        yield return new WaitForSeconds(0.2f);
        
        Vector3 leftEndSidePosition = _target.position + _target.right * -1f;
        Vector3 rightEndSidePosition = _target.position + _target.right * 1f;
        
        leftMove = StartCoroutine(MoveToPosition(leftHand, leftEndSidePosition, _crossSpeed));
        rightMove = StartCoroutine(MoveToPosition(rightHand, rightEndSidePosition, _crossSpeed));
        
        yield return leftMove;
        yield return rightMove;
    
        ShakeCamera(2);
        
        yield return new WaitForSeconds(0.15f);
        
        leftMove = StartCoroutine(MoveToPosition(leftHand, originalLeftPosition, _speed));
        rightMove = StartCoroutine(MoveToPosition(rightHand, originalRightPosition, _speed));
    
        yield return leftMove;
        yield return rightMove;
    
        isLeftHandMoving = false;
        isRightHandMoving = false;
    }
            
    #endregion
    
    public void TakeDownLeftAndCircleShot(int bulletCount, float _speed,float _downSpeed)
    {
        StartCoroutine(TakeDownLeftAndShootCoroutine(bulletCount, _speed,_downSpeed));
    }
    
    public void TakeDownRightAndCircleShot(int bulletCount, float _speed,float _downSpeed)
    {
        StartCoroutine(TakeDownRightAndShootCoroutine(bulletCount, _speed,_downSpeed));
    }

    private IEnumerator TakeDownLeftAndShootCoroutine(int bulletCount, float _speed,float _downSpeed)
    {
        Vector3 leftHandOriginPosition = leftHand.position;
        
        yield return StartCoroutine(MoveToPosition(leftHand, leftHand.position + new Vector3(0, 2, 0), _speed));
                    
        yield return StartCoroutine(MoveToPosition(leftHand, leftHandOriginPosition, _downSpeed));
        ShakeCamera(1.4f);
        
        ApplyCircleShot(leftHand, bulletCount);
    }
    
    private IEnumerator TakeDownRightAndShootCoroutine(int bulletCount, float _speed,float _downSpeed)
    {
        Vector3 rightHandOriginPosition = rightHand.position;
        
        yield return StartCoroutine(MoveToPosition(rightHand, rightHand.position + new Vector3(0, 2, 0), _speed));
        
        yield return StartCoroutine(MoveToPosition(rightHand, rightHandOriginPosition, _downSpeed));
        ShakeCamera(1.4f);
        ApplyCircleShot(rightHand, bulletCount);
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

    private void ShakeCamera(float power) =>ImpulseSource.GenerateImpulse(power);
   
}
