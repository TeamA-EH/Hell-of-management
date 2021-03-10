using System;
using UnityEngine;

namespace HOM
{
    public interface ISKill
    {

        ///<summary> Activates the skill functionality </summary>
        ///<param name="OnSkillCompleted"> Callback called when this skill has been completed </param>
        void Execute(Action OnSkillCompleted = null);

        ///<summary> Stops skill execution at any time of execution </summary>
        ///<param name="OnSkillStopped"> Callback called when this skill has been stopped </param>
        void Stop(Action OnSkillStopped = null);
    }
}
