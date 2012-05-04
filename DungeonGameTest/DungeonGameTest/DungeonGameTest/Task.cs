using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonGameTest
{
    //task interface in case we want to add more task types later
    public interface Task
    {
        bool isCompleted();
        int getValue();
    }
}
