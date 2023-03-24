using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class OptimizedVisibilityTester : IVisibilityTester
    {
        private readonly UIMonitor Monitor;
        private VisibilityTestShortAnswer CurrentAnswer;
        private List<Visibility_X> CurrentVisibilityByX;
        private List<Visibility_Y> CurrentVisibilityByY;
        private List<Visibility_> CurrentMergetVisibility;
        private bool UpdatedSinceLastCalculationByX, UpdatedSinceLastCalculationByY;

        public OptimizedVisibilityTester(UIMonitor monitor)
        {
            Monitor = monitor;
            CurrentAnswer = new VisibilityTestShortAnswer();
            int length = Monitor.AllElements.Count;
            CurrentVisibilityByX = new(Enumerable.Repeat(Visibility_X.Partially, length).ToArray());
            CurrentVisibilityByY = new(Enumerable.Repeat(Visibility_Y.Partially, length).ToArray());
            CurrentMergetVisibility = new(Enumerable.Repeat(Visibility_.Partially, length).ToArray());
            UpdatedSinceLastCalculationByX = true;
            UpdatedSinceLastCalculationByY = true;
        }

        public void ScrollHorizontally(double distanceToTheRight)
        {
            UpdatedSinceLastCalculationByX = true;
            Monitor.ScrollHorizontally(distanceToTheRight);
        }

        public void ScrollVertically(double distanceToTheBot)
        {
            UpdatedSinceLastCalculationByY = true;
            Monitor.ScrollVertically(distanceToTheBot);
        }

        public VisibilityTestShortAnswer VisibilityTest()
        {
            if (!UpdatedSinceLastCalculationByX && !UpdatedSinceLastCalculationByY)
                return CurrentAnswer;
            CurrentAnswer.Clear();

            if (UpdatedSinceLastCalculationByX)
            {
                RecalculateVisibilityByX();
                UpdatedSinceLastCalculationByX = false;
            }

            if (UpdatedSinceLastCalculationByY)
            {
                RecalculateVisibilityByY();
                UpdatedSinceLastCalculationByY = false;
            }
            MergeVisibility();

            return CurrentAnswer;
        }

        private void RecalculateVisibilityByX()
        {
            foreach (var elem in Monitor.ParentElements)
            {
                RecurentVisibilityByX(elem);
            }
        }

        private void RecurentVisibilityByX(UIElement elem)
        {
            var visibility = ElementVisibility.IsVisibleByX(elem, Monitor.Window);
            CurrentVisibilityByX[elem.Id] = visibility;
            if (visibility == Visibility_X.Partially)
            {
                foreach (var element in elem.GetSubelements())
                {
                    RecurentVisibilityByX(element);
                }
            }
        }

        private void RecalculateVisibilityByY()
        {
            foreach (var elem in Monitor.ParentElements)
            {
                RecurentVisibilityByY(elem);
            }
        }

        private void RecurentVisibilityByY(UIElement elem)
        {
            var visibility = ElementVisibility.IsVisibleByY(elem, Monitor.Window);
            CurrentVisibilityByY[elem.Id] = visibility;
            if (visibility == Visibility_Y.Partially)
            {
                foreach (var element in elem.GetSubelements())
                {
                    RecurentVisibilityByY(element);
                }
            }
        }

        private void MergeVisibility()
        {
            foreach (var elem in Monitor.ParentElements)
            {
                RecurentVisibilityMerge(elem);
            }
        }

        private void RecurentVisibilityMerge(UIElement elem)
        {
            var visibility_x = CurrentVisibilityByX[elem.Id];
            var visibility_y = CurrentVisibilityByY[elem.Id];
            var visibility = VisibilityMerger.MergeVisibility(visibility_x, visibility_y);
            CurrentMergetVisibility[elem.Id] = visibility;
            CurrentAnswer.Add(elem.Id, visibility);

            if (visibility == Visibility_.Partially)
            {
                if (visibility_x != Visibility_X.Partially)
                {
                    foreach (var element in elem.GetSubelements())
                    {
                        CurrentVisibilityByX[element.Id] = visibility_x;
                    }
                }

                if (visibility_y != Visibility_Y.Partially)
                {
                    foreach (var element in elem.GetSubelements())
                    {
                        CurrentVisibilityByY[element.Id] = visibility_y;
                    }
                }
            
                foreach (var element in elem.GetSubelements())
                {
                    RecurentVisibilityMerge(element);
                }
            }
        }
    }
}
