using UnityEngine;

/**
 * This component patrols between given points, chases a given target object when it sees it, and rotates from time to time.
 */
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Ambusher))]
[RequireComponent(typeof(Capturer))]
[RequireComponent(typeof(BeingLazy))]
public class EnemyControllerStateMachine: StateMachine {
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] float radiusToAmbush = 2f;
    [SerializeField] float probabilityToRotate = 0.2f;
    [SerializeField] float probabilityToStopRotating = 0.2f;

    private Chaser chaser;
    private Patroller patroller;
    private Rotator rotator;
    private Ambusher ambusher;
    private Capturer capturer;
    private BeingLazy lazy;

    private float DistanceToTarget() {
        return Vector3.Distance(transform.position, chaser.TargetObjectPosition());
    }

    private void Awake() {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<Patroller>();
        rotator = GetComponent<Rotator>();
        ambusher = GetComponent<Ambusher>();
        capturer = GetComponent<Capturer>();
        lazy = GetComponent<BeingLazy>();
        base
        .AddState(patroller)     // This would be the first active state.
        .AddState(chaser)
        .AddState(rotator)
        .AddState(ambusher)
        .AddState(capturer)
        .AddState(lazy)
        .AddTransition(patroller, () => DistanceToTarget()<=radiusToWatch,   chaser)
        .AddTransition(rotator,   () => DistanceToTarget()<=radiusToWatch,   chaser)
        .AddTransition(chaser,    () => DistanceToTarget() > radiusToWatch,  patroller)
        .AddTransition(rotator,   () => Random.Range(0f, 1f) < probabilityToStopRotating * Time.deltaTime, patroller)
        .AddTransition(patroller, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, rotator)
        .AddTransition(rotator, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, ambusher)
        .AddTransition(patroller, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, ambusher)
        .AddTransition(ambusher, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, patroller)
        .AddTransition(ambusher, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, rotator)
        .AddTransition(patroller, () => DistanceToTarget()<=radiusToWatch,   capturer)
        .AddTransition(rotator,   () => DistanceToTarget()<=radiusToWatch,   capturer)
        .AddTransition(capturer,  () => DistanceToTarget()<=radiusToAmbush, ambusher)
        .AddTransition(capturer,  () => DistanceToTarget() > radiusToWatch,  patroller)
        .AddTransition(capturer,  () => DistanceToTarget() > radiusToWatch,  rotator)
        .AddTransition(patroller, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, lazy)
        .AddTransition(rotator, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, lazy)
        .AddTransition(ambusher, () => Random.Range(0f, 1f) < probabilityToRotate * Time.deltaTime, lazy)
        .AddTransition(lazy, () => DistanceToTarget()<=radiusToWatch,   capturer)
        .AddTransition(lazy, () => DistanceToTarget()<=radiusToWatch,   chaser)
        .AddTransition(lazy, lazy.GetStateSleep,   patroller)
        .AddTransition(lazy, lazy.GetStateSleep,   rotator)
        .AddTransition(lazy, lazy.GetStateSleep,   ambusher)
        ;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }    
}
 