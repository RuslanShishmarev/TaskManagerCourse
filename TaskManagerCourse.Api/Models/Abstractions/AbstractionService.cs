using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerCourse.Api.Models.Abstractions
{
    public abstract class AbstractionService
    {
        public bool DoAction(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
