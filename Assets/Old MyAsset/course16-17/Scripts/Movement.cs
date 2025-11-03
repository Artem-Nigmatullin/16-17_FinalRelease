using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public void Move(Transform source,Vector3 position, float _speed)
    {
        source.transform.position = Vector3.MoveTowards(source.transform.position, position, _speed * Time.deltaTime);
    }
}
