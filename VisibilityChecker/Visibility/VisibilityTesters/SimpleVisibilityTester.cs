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

        public VisibilityTestShortAnswer VisibilityTest()
        {
            CurrentAnswer = new();
            foreach (var elem in Monitor.ParentElements)
            {
                RecurentVisibilityTest(elem);
            }
            return CurrentAnswer;
        }

        private void RecurentVisibilityTest(UIElement elem)
        {
            var visibility = ElementVisibility.IsVisible(elem, Monitor.Window);
            switch (visibility)
            {
                case Visibility_.Partially:
                    CurrentAnswer.PartiallyIds.Add(elem.Id);
                    foreach (var element in elem.GetSubelements())
                    {
                        RecurentVisibilityTest(element);
                    }
                    break;
                case Visibility_.Visible:
                    CurrentAnswer.VisibleIds.Add(elem.Id);
                    break;
                case Visibility_.Invisible:
                    CurrentAnswer.InvisibleIds.Add(elem.Id);
                    break;
            }
        }
    }
}
