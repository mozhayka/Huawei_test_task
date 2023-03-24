using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public interface IVisibilityTester
    {
        public Task<VisibilityTestShortAnswer> VisibilityTestAsync();
        public void ScrollHorizontally(double distanceToTheRight);
        public void ScrollVertically(double distanceToTheBot);
    }
}
