using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(Animator))]
public class TriggerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [YarnCommand("PlayAnimation")]
    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }
}
