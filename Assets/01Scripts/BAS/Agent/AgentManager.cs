using System;
using System.Collections.Generic;
using UnityEngine;

    public class AgentManager : GetCompoParent // : Manager<AgentManager>
    {
    public List<Unit> Units = new List<Unit>();
    public int SelectedUnitIdx = 0;
    public Unit SelectedUnit => Units[SelectedUnitIdx];
    public  Action<ActSO> ActionExcutor;



}

