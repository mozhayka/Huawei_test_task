using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class SimpleVisibilityTester : IVisibilityTester
    {
        private UIMonitor Monitor;
        private VisibilityTestShortAnswer CurrentAnswer;

        public SimpleVisibilityTester(UIMonitor monitor)
        {
            Monitor = monitor;
            CurrentAnswer = new VisibilityTestShortAnswer();
        }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            Monitor.ScrollHorizontally(distanceToTheRight);
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            Monitor.ScrollVertically(distanceToTheBot);
        }

        public Task<VisibilityTestShortAnswer> VisibilityTestAsync()
        {
            CurrentAnswer.Clear();
            foreach (var elem in Monitor.ParentElements)
            {
                RecurentVisibilityTest(elem);
            }
            return Task.FromResult(CurrentAnswer);
        }

        private void RecurentVisibilityTest(UIElement elem)
        {
            var visibility = ElementVisibility.IsVisible(elem, Monitor.Window);
            CurrentAnswer.Add(elem.Id, visibility);

            if (visibility == Visibility_.Partially)
            {
                foreach (var element in elem.GetSubelements())
                {
                    RecurentVisibilityTest(element);
                }
            }
        }
    }
}
