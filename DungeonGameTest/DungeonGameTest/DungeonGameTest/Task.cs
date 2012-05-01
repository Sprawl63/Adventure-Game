using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonGameTest
{
    //task interface in case we want to add more task types later
    public interface Task
    {
        bool isCompleted(Bbox b1, Bbox b2, string el);
    }
}
