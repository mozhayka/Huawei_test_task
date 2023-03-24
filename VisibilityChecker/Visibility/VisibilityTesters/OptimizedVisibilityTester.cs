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
        private readonly VisibilityResult CurrentAnswer;
        private readonly List<Visibility_X> CurrentVisibilityByX;
        private readonly List<Visibility_Y> CurrentVisibilityByY;
        private readonly List<Visibility_> CurrentMergetVisibility;
        private bool UpdatedSinceLastCalculationByX, UpdatedSinceLastCalculationByY;

        public OptimizedVisibilityTester(UIMonitor monitor)
        {
            Monitor = monitor;
            CurrentAnswer = new VisibilityResult();
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

        public async Task<VisibilityResult> TestVisibilityAsync()
        {
            if (!UpdatedSinceLastCalculationByX && !UpdatedSinceLastCalculationByY)
                return CurrentAnswer;
            CurrentAnswer.Clear();

            var taskX = Task.Run(() =>
            {
                if (UpdatedSinceLastCalculationByX)
                {
                    RecalculateVisibilityByX();
                    UpdatedSinceLastCalculationByX = false;
                }
            });

            var taskY = Task.Run(() =>
            {
                if (UpdatedSinceLastCalculationByY)
                {
                    RecalculateVisibilityByY();
                    UpdatedSinceLastCalculationByY = false;
                }
            });

            await Task.WhenAll(taskX, taskY);

            MergeVisibility();

            return CurrentAnswer;
        }

        private void RecalculateVisibilityByX()
        {
            // var tasks = new List<Task>();
            foreach (var elem in Monitor.ParentElements)
            {
                // tasks.Add(new Task(() => RecurentVisibilityByX(elem)));
                RecurentVisibilityByX(elem);
            }
            // tasks.ForEach(x => x.Wait());
        }

        private void RecurentVisibilityByX(UIElement elem)
        {
            // var tasks = new List<Task>();
            var visibility = ElementVisibility.IsVisibleByX(elem, Monitor.Window);
            CurrentVisibilityByX[elem.Id] = visibility;
            if (visibility == Visibility_X.Partially)
            {
                foreach (var element in elem.GetSubelements())
                {
                    // tasks.Add(new Task(() => RecurentVisibilityByX(element)));
                    RecurentVisibilityByX(element);
                }
            }
            // tasks.ForEach(x => x.Wait());
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
