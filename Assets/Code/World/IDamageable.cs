using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.World
{
    interface IDamageable
    {        
        bool GetDamage(float dmg);
        bool Death();
    }
}
