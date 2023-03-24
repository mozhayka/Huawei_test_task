using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityChecker
{
    public class OptimizedVisibilityTester : IVisibilityTester
    {
        private readonly UIMonitor Monitor;
        private readonly VisibilityResult CurrentAnswer;
        private readonly Visibility_X[] VisibilityByX;
        private readonly Visibility_Y[] VisibilityByY;
        private readonly Visibility_[] MergedVisibility;
        private bool UpdatedSinceLastCalculationByX, UpdatedSinceLastCalculationByY;

        public OptimizedVisibilityTester(UIMonitor monitor)
        {
            Monitor = monitor;
            CurrentAnswer = new VisibilityResult();
            int length = Monitor.AllElements.Count;
            VisibilityByX = new Visibility_X[length];
            VisibilityByY = new Visibility_Y[length];
            MergedVisibility = new Visibility_[length];
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

            var taskX = Task.Run(() => {
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
            Parallel.ForEach(
                   Monitor.RootElements,
                   elem => RecurentVisibilityByX(elem)
            );
        }

        private void RecurentVisibilityByX(UIElement elem)
        {
            var visibility = ElementVisibility.IsVisibleByX(elem, Monitor.Window);
            VisibilityByX[elem.Id] = visibility;
            if (visibility == Visibility_X.Partially)
            {
                //Parallel.ForEach(
                //   elem.Subelements,
                //   elem => RecurentVisibilityByX(elem)
                //);
                foreach (var element in elem.Subelements)
                    RecurentVisibilityByX(element);
            }
        }

        private void RecalculateVisibilityByY()
        {
            //Console.WriteLine(Monitor.RootElements.Count);
            Parallel.ForEach(
                   Monitor.RootElements,
                   elem => RecurentVisibilityByY(elem)
            );
        }

        private void RecurentVisibilityByY(UIElement elem)
        {
            var visibility = ElementVisibility.IsVisibleByY(elem, Monitor.Window);
            VisibilityByY[elem.Id] = visibility;
            if (visibility == Visibility_Y.Partially)
            {
                //Parallel.ForEach(
                //   elem.Subelements,
                //   elem => RecurentVisibilityByY(elem)
                //);
                foreach (var element in elem.Subelements)
                    RecurentVisibilityByY(element);
            }
        }

        private void MergeVisibility()
        {
            foreach (var elem in Monitor.RootElements)
            {
                RecurentVisibilityMerge(elem);
            }
        }

        private void RecurentVisibilityMerge(UIElement elem)
        {
            var visibility_x = VisibilityByX[elem.Id];
            var visibility_y = VisibilityByY[elem.Id];
            var visibility = VisibilityMerger.MergeVisibility(visibility_x, visibility_y);
            MergedVisibility[elem.Id] = visibility;
            CurrentAnswer.Add(elem.Id, visibility);

            if (visibility == Visibility_.Partially)
            {
                if (visibility_x != Visibility_X.Partially)
                {
                    foreach (var element in elem.Subelements)
                    {
                        VisibilityByX[element.Id] = visibility_x;
                    }
                }

                if (visibility_y != Visibility_Y.Partially)
                {
                    foreach (var element in elem.Subelements)
                    {
                        VisibilityByY[element.Id] = visibility_y;
                    }
                }
            
                foreach (var element in elem.Subelements)
                {
                    RecurentVisibilityMerge(element);
                }
            }
        }
    }
}
