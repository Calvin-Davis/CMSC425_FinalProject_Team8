using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class ZoneBot : MonoBehaviour
{
    public float botSpeed = 5, floatHeight = 0.4F, floatAnimationSpeed = 3F;
    public Vector3 Center = Vector3.zero;
    public bool useInitialPosition = false;
    public float radius = 1;
    private NavMeshAgent agent;
    public GameObject player;
    public Material neutralEyeColor, pursuitEyeColor;
    public AudioSource alertSound;
    private Renderer eyeRenderer;
    private bool pursuiting, prevPursuiting = false;
    private float animationTime = 0, restingYPos = 0;
    private Vector3 prevVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed= botSpeed;
        //Sets point for bot to rest at either at current position or at given vector3
        if (useInitialPosition)
        {
            Center = GetComponent<Transform>().position;
        }
        agent.destination = Center;
        
        eyeRenderer = transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        setEyeColor(neutralEyeColor);
    }

    // Update is called once per frame
    void Update()
    {
        //detects if player in range
        float distance = (player.transform.position - Center).magnitude;
        if (distance < radius) {
            pursuiting = true;
            agent.destination = player.transform.position;
        } else {
            pursuiting = false;
            agent.destination = Center;
        }
        //setting eyecolor based on if chasing player or not
        if (pursuiting && !prevPursuiting) {
            setEyeColor(pursuitEyeColor);
            restingYPos = transform.position.y;
            if (alertSound != null)
            {
                alertSound.PlayDelayed(0);
            }
        }

        else if (!pursuiting && prevPursuiting) {
            setEyeColor(neutralEyeColor);
        }
    

        animateFloating();

        prevPursuiting = pursuiting;
        prevVelocity = agent.velocity;
    }

    void setEyeColor(Material color) {
        eyeRenderer.material = color;
    }

    //used to make robot animation more dynamic, allowing them to float up and down
    //instead of just hovering in at same y. Also allows robot to lower to ground
    //and rest when not chasing
    void animateFloating() {
        if (agent.velocity != Vector3.zero || (agent.velocity == Vector3.zero && prevVelocity != Vector3.zero)) {

            float yPos = floatHeight * (Mathf.Cos(floatAnimationSpeed*animationTime + MathF.PI)+1);
            transform.position = new Vector3(transform.position.x, restingYPos + yPos, transform.position.z);

            if (animationTime >= 2*MathF.PI / floatAnimationSpeed)
                animationTime = 0;

            animationTime += Time.deltaTime;

            if (agent.velocity == Vector3.zero && prevVelocity != Vector3.zero) {
                StartCoroutine(ReturnToRestCoroutine());
                StartCoroutine(SmoothRotateCoroutine());
            }
        }
    }

    //returns bot to its original height before it started moving  
    private IEnumerator ReturnToRestCoroutine()
    {
        Vector3 direction = Vector3.zero;
        direction.y = Mathf.Sign(restingYPos - transform.position.y);
        Vector3 position;

        while (Math.Abs(restingYPos - transform.position.y) >= 0.01 && Math.Abs(restingYPos - transform.position.y) <= 5)
        {
            // Interpolate movement smoothly over time
            transform.position += direction * Time.deltaTime * floatAnimationSpeed;
            position = transform.position;
            yield return null;
            transform.position = position;
        }

        transform.position = new Vector3(transform.position.x, restingYPos, transform.position.z);
        animationTime = 0;
    }

    //used to rotate bot when bot to face player after returning to resting spot
    //otherwise robot almost always faces away from player (it faces the direction
    //opposite of the chase upon return)
    private IEnumerator SmoothRotateCoroutine() {
        float elapsedTime = 0.0f;
        float rotationTime = 1f;
        //find angle of robot to player using their locations
        Vector3 playerPosition = player.transform.position;
        Vector3 currentPosition = transform.position;

        Vector3 directionToPlayer = playerPosition - currentPosition;

        float yAngle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        //smoothly roatate bot based on calculated angle
        Quaternion targetRotation = Quaternion.Euler(0f, yAngle, 0f);

        Quaternion startRotation = transform.rotation;

        while (elapsedTime < rotationTime)
        {
            // Interpolate rotation smoothly over time
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = targetRotation;
    }
}

