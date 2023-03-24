﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class SimpleVisibilityTester : IVisibilityTester
    {
        private UIMonitor Monitor;
        private VisibilityResult CurrentAnswer;

        public SimpleVisibilityTester(UIMonitor monitor)
        {
            Monitor = monitor;
            CurrentAnswer = new VisibilityResult();
        }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            Monitor.ScrollHorizontally(distanceToTheRight);
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            Monitor.ScrollVertically(distanceToTheBot);
        }

        public Task<VisibilityResult> TestVisibilityAsync()
        {
            CurrentAnswer.Clear();
            foreach (var elem in Monitor.RootElements)
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
                foreach (var element in elem.Subelements)
                {
                    RecurentVisibilityTest(element);
                }
            }
        }
    }
}
