﻿using VisibilityChecker;

namespace Tests.Answers
{
    internal class VisibilityTestAnswers
    {
        public static VisibilityResult Test1Answer = new() {
            VisibleIds = new List<int>() { 0 },
            PartiallyIds = new List<int>(), 
            InvisibleIds = new List<int>(), 
        };

        public static VisibilityResult Test2Answer = new()
        {
            VisibleIds = new List<int>() { 2 },
            PartiallyIds = new List<int>() { 1 },
            InvisibleIds = new List<int>() { 0 },
        };

        public static VisibilityResult TestWithScrolling1Answer = new()
        {
            VisibleIds = new List<int>(),
            PartiallyIds = new List<int>() { 1 },
            InvisibleIds = new List<int>() { 0, 2 },
        };

        public static VisibilityResult TestWithScrolling2Answer = new()
        {
            VisibleIds = new List<int>() { 0 },
            PartiallyIds = new List<int>(),
            InvisibleIds = new List<int>() { 1 },
        };

        public static void CompareAnswers(VisibilityResult expected, VisibilityResult actual)
        {
            CollectionAssert.AreEqual(expected.VisibleIds, actual.VisibleIds);
            CollectionAssert.AreEqual(expected.PartiallyIds, actual.PartiallyIds);
            CollectionAssert.AreEqual(expected.InvisibleIds, actual.InvisibleIds);
        }
    }
}
