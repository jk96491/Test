using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Character : MonoBehaviour 
{
    public enum CameraPosType : int
    {
        NONE = -1,
        VIEW1,
        VIEW2,
        VIEW3,
        VIEW4,
    }


    [System.Serializable]
    public class CameraTrans
    {
       public Transform trans = null;
    }
    [SerializeField]
    private Transform viewTrans = null;
    [SerializeField]
    private Transform modelTrans = null;
    [SerializeField]
    private NavMeshAgent Nav = null;
	public Animator animator;
    [SerializeField]
    private CameraTrans[] cameraTrans = null;

    float rotationSpeed = 30;

	Vector3 inputVec;
	Vector3 targetDirection;
	
	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress, Knight, Mage, Archer, TwoHanded, Swordsman, Spearman, Hammer, Crossbow};

	public Warrior warrior;

    public void SetCameraPos(CameraPosType type_, Transform cameraTrans_ ,bool movingCorutine, float speed_ ,System.Action MovingByUpdateEndCallback_ = null)
    {
        int index = (int)type_;

        CameraManager.Instance.FollowTarget(gameObject.transform, cameraTrans_);

        if (null == cameraTrans)
            return;

        if(null != cameraTrans[index])
        {
            if (false == movingCorutine)
                CameraManager.Instance.CameraMovingByUpdate(cameraTrans_, cameraTrans[index].trans, speed_, MovingByUpdateEndCallback_);
            else
                CameraManager.Instance.CameraMovingByCoroutine(cameraTrans_.position, cameraTrans[index].trans, cameraTrans_.transform, speed_, MovingByUpdateEndCallback_);
            CameraManager.Instance.CameraRotatingByCoroutine(cameraTrans_.rotation, cameraTrans[index].trans.rotation, cameraTrans_.transform, speed_);
        }
    }

    public void GoToWayPoint(Transform TargetTras_)
    {
        if(null != Nav)
        {
            Nav.SetDestination(TargetTras_.position);
        }
    }

    public void UpdateCharacter()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = -(Input.GetAxisRaw("Vertical"));
        inputVec = new Vector3(x, 0, z);

        //Apply inputs to animator
        animator.SetFloat("Input X", z);
        animator.SetFloat("Input Z", -(x));

        if (x != 0 || z != 0)  //if there is some input
        {
            //set that character is moving
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
        }
        else
        {
            //character is not moving
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }

        UpdateMovement();  //update character position and facing
        viewTrans.localPosition = modelTrans.localPosition;
    }

	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}

	//converts control input vectors into camera facing vectors
	void GetCameraRelativeMovement()
	{  
		Transform cameraTransform = Camera.main.transform;

		// Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right= new Vector3(forward.z, 0, -forward.x);

		//directional inputs
		float v= Input.GetAxisRaw("Vertical");
		float h= Input.GetAxisRaw("Horizontal");

		// Target direction relative to the camera
		targetDirection = h * right + v * forward;
	}

	//face character along input direction
	void RotateTowardMovementDirection()  
	{
		if (inputVec != Vector3.zero)
		{
            modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
		}
	}

	void UpdateMovement()
	{
		//get movement input from controls
		Vector3 motion = inputVec;

		//reduce input for diagonal movement
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? 0.7f:1;

		RotateTowardMovementDirection();  
		GetCameraRelativeMovement();  
	}
}