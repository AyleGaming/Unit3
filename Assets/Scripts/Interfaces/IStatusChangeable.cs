using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusChangeable
{
    void SetStatus(bool status);
    bool GetStatus();
}
