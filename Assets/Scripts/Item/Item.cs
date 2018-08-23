using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum ItemKind
    {
        SPEED_BOOST,
        OXYGEN,
    }

    public ItemKind kind;
    public float value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Character character = collision.GetComponent<Character>();
            character.GetItem(kind, value);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // TODO : 아이템 별로 이동방향 지정할 수 있게 해주기
        // 지금은 일괄적으로 화면 위로 떠오르는 식으로 처리
        transform.Translate(Vector2.up * Time.deltaTime * 2f);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
