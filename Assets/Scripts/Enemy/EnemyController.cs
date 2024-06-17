using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Assigning via code
    public GridTile playerTile;
    public GridTile neighbourTile;
    [System.NonSerialized] public GridTile currentEnemyTile;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject attackIndicator;
    public float DistanceFromPlayer;
    private float timeBetweenMoves;


    [SerializeField] private int damage;
    private bool attackPerperation;

    [System.NonSerialized] public int numberOfMovesPerBeat = 1;

    private PathFinding pathFinding;
    private GetGridTile getGridTile = new GetGridTile();

    private void Start()
    {
        pathFinding = new PathFinding();
        currentEnemyTile = (GridTile)getGridTile.GetTile(transform.position);
    }

    public void ExecuteEnemyAction()
    {
        if (currentEnemyTile != null && playerTile != null)
        {
            var nextTile = pathFinding.GetNextTileOnPathToPlayer(currentEnemyTile);

            if (nextTile.DistanceFromPlayer > 0 && !attackPerperation)
            {
                ExecuteMoveTo(nextTile);
                BlockNextPositionForOthers(nextTile);
            }
            else if (attackPerperation)
            {
                ExecuteAttackOn(nextTile);
            }
            else if (!attackPerperation)
                PrepareAttack(nextTile.transform.position);

        }
    }
    private void BlockNextPositionForOthers(GridTile nextTile)
    {
        nextTile.DistanceFromPlayer = 10000;
        if (nextTile.transform.childCount != 0)
            nextTile.GetComponentInChildren<TMPro.TextMeshPro>().text = nextTile.DistanceFromPlayer.ToString();
    }

    #region Movement
    private void ExecuteMoveTo(GridTile nextTile)
    {
        //getting time between moves matching to time between beats
        timeBetweenMoves = GameManager.Instance.GetTimeBetweenHeafbeats() / 10;
        
        if (nextTile != null)
            StartCoroutine(Extns.SmoothTweeng(timeBetweenMoves,
            (p) => transform.position = p,
            currentEnemyTile.transform.position,
            nextTile.transform.position
           ));

        currentEnemyTile = nextTile;

        if (pathFinding.GetNextTileOnPathToPlayer(nextTile) == playerTile)
            PrepareAttack(nextTile.transform.position);
    }
    #endregion
    private void PrepareAttack(Vector3 indicatorPosition)
    {
        attackPerperation = true;
        attackIndicator.transform.position = indicatorPosition;
        attackIndicator.SetActive(true);
    }
    private void ExecuteAttackOn(GridTile tileToAttack)
    {
        attackPerperation = false;
        attackIndicator.SetActive(false);
        if (tileToAttack.DistanceFromPlayer == 0)
            DealDamageToPlayer();
    }
    private void DealDamageToPlayer()
    {
            GameManager.Instance.player.GetComponent<HealthController>().TakeDamage(damage);
    }
}
