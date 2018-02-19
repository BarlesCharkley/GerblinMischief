using UnityEngine;
using System.Collections;

public class ShakeScript : MonoBehaviour
{
	[SerializeField] private Transform target;
    [SerializeField] private Vector3 shakeAmount;
    [Space]
	[SerializeField] private float fadeInSpeed = 1f;
    [SerializeField] private float fadeOutSpeed = 1f;
    [Space]
    [SerializeField] private bool automatic;
    [SerializeField] private float autoDelay = 0f;
    [Space]
    [SerializeField] private bool timed;
	[SerializeField] private float shakeTime = 3f;

    private bool isShaking;
    private Vector3 originalPos;
    private Vector3 updatedShakeAmount;

    private void OnEnable()
    {
        originalPos = target.localPosition;

        if (automatic)
        {
            Invoke("StartShake", autoDelay);
        }
    }

    public void StartShake()
    {
        originalPos = target.localPosition;
        updatedShakeAmount *= 0f;
        isShaking = true;

        if (timed)
        {
            StartCoroutine(AutoStop());
        }
    }

    public void StopShake()
    {
        if (!automatic)
        {
            isShaking = false;
        }
    }

    private IEnumerator AutoStop()
    {
        yield return new WaitForSeconds(shakeTime);
        isShaking = false;
    }

	void FixedUpdate()
	{
		if (isShaking)
		{
            updatedShakeAmount = Vector3.Lerp(updatedShakeAmount, shakeAmount * 0.5f, fadeInSpeed * Time.fixedDeltaTime);

            target.localPosition = originalPos + new Vector3
                (Random.insideUnitSphere.x * updatedShakeAmount.x,
                Random.insideUnitSphere.y * updatedShakeAmount.y,
                Random.insideUnitSphere.z * updatedShakeAmount.z);
		}

		else if (!isShaking && updatedShakeAmount.magnitude > 0.003f)
		{
            updatedShakeAmount = Vector3.Lerp(updatedShakeAmount, new Vector3(0f, 0f, 0f), fadeOutSpeed * Time.fixedDeltaTime);

            target.localPosition = originalPos + new Vector3
                (Random.insideUnitSphere.x * updatedShakeAmount.x,
                Random.insideUnitSphere.y * updatedShakeAmount.y,
                Random.insideUnitSphere.z * updatedShakeAmount.z);
		}

        //else
        //{
        //    target.localPosition = originalPos;
        //}
	}
}