using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossAttack_Phase2 : Attack
{
    [SerializeField] private List<Hand> hands;
    [Range(0, 100)]
    [SerializeField] private float speed;
    [SerializeField] private int waitTime;
    [SerializeField] private List<Transform> clapPos;
    public int WaitTime => waitTime;

    private GameObject player;
    private bool isAttacking;
    private bool isClap;

    private int punchCounter = -1;
    private int PunchCounter
    {
        get { return punchCounter; }
        set
        {
            punchCounter = value;
            if (punchCounter > 3)
            {
                punchCounter = 0;
            }
        }
    }

    private bool isAttackDone;
    private bool isRetractDone;

    public Action ResetVaribles;
    public Action<bool, Animator> AnimSwitch;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;

        ResetVaribles += () =>
        {
            isAttackDone = false;
            isRetractDone = false;
            isAttacking = false;
            isClap = false;
        };

        AnimSwitch += (boolean, animator) => animator.enabled = boolean;
    }

    public override void UseAttack()
    {
        if (currentWaitTime <= 0 && !isAttacking && player)
        {
            isAttacking = true;
            isAttackDone = false;
            isRetractDone = false;
            PunchCounter = UnityEngine.Random.Range(0, 4);

            currentWaitTime = WaitForNextAttack;

            if (PunchCounter < 2)
            {
                hands[PunchCounter].startPos = hands[PunchCounter].GetStartPos();
                hands[PunchCounter].TargetPos = GetCurrentPlayerPos();
                AnimSwitch?.Invoke(false, hands[PunchCounter]._Animator);
            }
            else if(PunchCounter == 2)
            {
                hands[0].startPos = hands[0].GetStartPos();
                hands[1].startPos = hands[1].GetStartPos();

                Vector2 pos = GetCurrentPlayerPos();

                hands[0].TargetPos = new Vector2(pos.x - 1, pos.y);
                hands[1].TargetPos = new Vector2(pos.x + 1, pos.y);

                AnimSwitch?.Invoke(false, hands[0]._Animator);
                AnimSwitch?.Invoke(false, hands[1]._Animator);
            }
            else if(PunchCounter > 2)
            {
                hands[0].startPos = hands[0].GetStartPos();
                hands[1].startPos = hands[1].GetStartPos();

                AnimSwitch?.Invoke(false, hands[0]._Animator);
                AnimSwitch?.Invoke(false, hands[1]._Animator);
            }

        }
    }

    private void Update()
    {
        if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            var step = speed * Time.deltaTime;

            switch (PunchCounter)
            {
                case 2:
                    SpecialAttack(step);
                    break;

                case 3:
                    ClapAttack(step);
                    break;

                default:
                    NormalAttack(step);
                    break;
            }
        }
    }

    private void ClapAttack(float step)
    {
        Hand handToPunchLeft = hands[0];
        Hand handToPunchRight = hands[1];

        if (!isAttackDone)
        {
            StartCoroutine(PunchPlayer(new List<Hand>() { handToPunchLeft, handToPunchRight }, () =>
            {
                handToPunchLeft.TargetPos = clapPos[0].position;
                handToPunchRight.TargetPos = clapPos[1].position;

                handToPunchLeft._HandTransform.position = Vector2.MoveTowards(handToPunchLeft._HandTransform.position, handToPunchLeft.TargetPos, step);
                handToPunchRight._HandTransform.position = Vector2.MoveTowards(handToPunchRight._HandTransform.position, handToPunchRight.TargetPos, step);
            }));
        }

        if(isAttackDone && !isClap)
        {
            StartCoroutine(ClapPlayer(new List<Hand>() { handToPunchLeft, handToPunchRight }, () =>
            {

                handToPunchLeft.TargetPos = new Vector2(-1, clapPos[0].position.y);
                handToPunchRight.TargetPos = new Vector2(1, clapPos[1].position.y);


                handToPunchLeft._HandTransform.position = Vector2.MoveTowards(handToPunchLeft._HandTransform.position, handToPunchLeft.TargetPos, step);
                handToPunchRight._HandTransform.position = Vector2.MoveTowards(handToPunchRight._HandTransform.position, handToPunchRight.TargetPos, step);
            }));
        }

        if (isAttackDone && isClap && !isRetractDone)
        {
            StartCoroutine(RetractPunch(new List<Hand>() { handToPunchLeft, handToPunchRight }, () =>
            {
                handToPunchLeft._HandTransform.position = ReturnToDefaultPosition(handToPunchLeft._HandTransform.position, handToPunchLeft.startPos, step);
                handToPunchRight._HandTransform.position = ReturnToDefaultPosition(handToPunchRight._HandTransform.position, handToPunchRight.startPos, step);
            }));
        }

        if (isAttackDone && isRetractDone)
        {
            ResetVaribles?.Invoke();
            AnimSwitch?.Invoke(true, handToPunchLeft._Animator);
            AnimSwitch?.Invoke(true, handToPunchRight._Animator);

        }
    }

    private void SpecialAttack(float step)
    {
        Hand handToPunchLeft = hands[0];
        Hand handToPunchRight = hands[1];

        if (!isAttackDone)
        {
            StartCoroutine(PunchPlayer(new List<Hand>() { handToPunchLeft, handToPunchRight }, () =>
            {
                handToPunchLeft._HandTransform.position = Vector2.MoveTowards(handToPunchLeft._HandTransform.position, handToPunchLeft.TargetPos, step);
                handToPunchRight._HandTransform.position = Vector2.MoveTowards(handToPunchRight._HandTransform.position, handToPunchRight.TargetPos, step);
            }));
        }

        if (isAttackDone && !isRetractDone)
        {
            StartCoroutine(RetractPunch(new List<Hand>() { handToPunchLeft, handToPunchRight }, () =>
            {
                handToPunchLeft._HandTransform.position = ReturnToDefaultPosition(handToPunchLeft._HandTransform.position, handToPunchLeft.startPos, step);
                handToPunchRight._HandTransform.position = ReturnToDefaultPosition(handToPunchRight._HandTransform.position, handToPunchRight.startPos, step);
            }));
        }

        if (isAttackDone && isRetractDone)
        {
            ResetVaribles?.Invoke();
            AnimSwitch?.Invoke(true, handToPunchLeft._Animator);
            AnimSwitch?.Invoke(true, handToPunchRight._Animator);

        }
    }

    private void NormalAttack(float step)
    {
        Hand handToPunch = hands[PunchCounter];
        if (!isAttackDone)
        {
            StartCoroutine(PunchPlayer(new List<Hand>() { handToPunch }, () =>
            {
                Vector2 pos = handToPunch._HandTransform.position;
                handToPunch._HandTransform.position = Vector2.MoveTowards(pos, handToPunch.TargetPos, step);
            }));
        }

        if (isAttackDone && !isRetractDone)
        {
            StartCoroutine(RetractPunch(new List<Hand>() { handToPunch }, () =>
            {
                Vector2 pos = handToPunch._HandTransform.position;
                handToPunch._HandTransform.position = ReturnToDefaultPosition(pos, handToPunch.startPos, step);
            }));
        }

        if (isAttackDone && isRetractDone)
        {
            ResetVaribles?.Invoke();
            AnimSwitch?.Invoke(true, handToPunch._Animator);
        }
    }

    private IEnumerator PunchPlayer(List<Hand> handToPunch, Action punchAction)
    {
        punchAction?.Invoke();
        bool check = false;
        while (!check)
        {
            foreach (Hand hand in handToPunch)
            {
                if (!TargetReached(hand._HandTransform.position, hand.TargetPos))
                {
                    check = false;
                    break;
                }
                else
                    check = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(WaitTime);

        isAttackDone = true;
    }

    private IEnumerator ClapPlayer(List<Hand> handToPunch, Action punchAction)
    {
        punchAction?.Invoke();
        bool check = false;
        while (!check)
        {
            foreach (Hand hand in handToPunch)
            {
                if (!TargetReached(hand._HandTransform.position, hand.TargetPos))
                {
                    check = false;
                    break;
                }
                else
                    check = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(WaitTime);

        isClap = true;
    }

    private IEnumerator RetractPunch(List<Hand> handToPunch, Action retractAction)
    {
        retractAction?.Invoke();

        bool check = false;
        while (!check)
        {
            foreach (Hand hand in handToPunch)
            {
                if (!TargetReached(hand._HandTransform.position, hand.startPos))
                {
                    check = false;
                    break;
                }
                else
                    check = true;
            }

            yield return null;
        }

        isRetractDone = true;
    }

    private Vector2 ReturnToDefaultPosition(Vector2 self, Vector2 target, float step)
    {
        return Vector2.MoveTowards(self, target, step);
    }

    #region checking conditions
    private Vector2 GetCurrentPlayerPos() { return player.transform.position; }
    private bool TargetReached(Vector2 self, Vector2 target) { return self == target; }
    #endregion
}
