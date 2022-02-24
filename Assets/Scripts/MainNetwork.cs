using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNetwork : HimeLib.SingletonMono<MainNetwork>
{
    public SignalServer posSender;
    public SignalClient posReciever;
}
