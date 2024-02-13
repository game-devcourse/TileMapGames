using System;
using System.Collections.Generic;
using UnityEngine;


using State = UnityEngine.MonoBehaviour;
using Transition = System.Tuple<UnityEngine.MonoBehaviour, System.Func<bool>, UnityEngine.MonoBehaviour>;

/**
 * This component represents a generic state-machine.
 * Each state is represented by a component installed on the same object as the StateMachine component.
 * At each transition, the machine enables the active state and disables all other states.
 * The first state added is the first active state.
 */
public class StateMachine: MonoBehaviour {

    private List<State>      states      = new List<State>();
    private List<Transition> transitions = new List<Transition>();

    private State activeState = null;

    //we added this to make sure each time the enemy goes to a different state it will be visible since we hide him in the ambusher state
    //and there is a chance the enemy will switch to the next state without even returning to be visible.
    //this is usually happening in the ExitState and EnterState each state has but since the state machine dosent wor that way we
    //nedded to come up with another solution.
    private SpriteRenderer _image;

    public void GoToState(State newActiveState) {
        _image.enabled = true;
        if (activeState == newActiveState) return;
        if (activeState!=null) activeState.enabled = false;
        activeState = newActiveState;
        activeState.enabled = true;
        Debug.Log("Going to state " + activeState);
    }

    public StateMachine AddState(State newState) {
        states.Add(newState);
        return this;
    }

    public StateMachine AddTransition(State fromState, Func<bool> condition, State toState) {
        transitions.Add(new Transition(fromState,condition,toState));
        return this;
    }

    private void Start() {
        foreach (State state in states) {
            state.enabled = false;
        }
        _image = transform.gameObject.GetComponent<SpriteRenderer>();
        GoToState(states[0]);
    }

    private void Update() {
        foreach (Transition transition in transitions) {
            if (transition.Item1==activeState) {
                if (transition.Item2()==true) {
                    GoToState(transition.Item3);
                    break;
                }
            }
        }
    }
}
 