using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JIgor.Projects.SimplePicker.Engine.Exceptions;

namespace JIgor.Projects.SimplePicker.Engine
{
    // TODO: Turn this into a Nuget Package
    public static class ListPicker
    {
        public static IEnumerable<T> PickElements<T>([NotNull] IEnumerable<T> list,
            int numberOfPicks)
            where T : notnull
        {
            if (numberOfPicks <= 0)
            {
                throw new InvalidNumberOfPicksException(
                    "Number of picks should be greater than 0");
            }

            List<T> castedList = (List<T>)list;

            if (castedList.Count == default)
            {
                throw new EmptyListException(
                    "List cannot be empty in this scenario");
            }

            if (castedList.Count < numberOfPicks)
            {
                throw new InvalidNumberOfPicksException(
                    "Number of picks should not be greater than the number of elements the list object has");
            }

            Random rnd = new Random();
            List<T> removedElements = new List<T>();

            for (int i = 0; i < numberOfPicks; i++)
            {
                T element = castedList[rnd.Next(0,
                    castedList.Count)];

                removedElements.Add(element);

                castedList.Remove(element);

            }

            return removedElements;
        }

        public static IEnumerable<T> PickElements<T>([NotNull] IEnumerable<T> list,
            int numberOfPicks,
            [NotNull] Func<T, bool> pickCondition)
            where T : notnull
        {
            if (numberOfPicks <= 0)
            {
                throw new InvalidNumberOfPicksException(
                    "Number of picks should be greater than 0");
            }

            List<T> castedList = (List<T>)list;

            if (castedList.Count == default)
            {
                throw new EmptyListException(
                    "List cannot be empty in this scenario");
            }

            if (castedList.Count < numberOfPicks)
            {
                throw new InvalidNumberOfPicksException(
                    "Number of picks should not be greater than the number of elements the list object has");
            }

            Random rnd = new Random();
            List<T> pickedElements = new List<T>();
            List<T> visitedElements = (List<T>)CopyList(castedList);

            while (visitedElements.Count > 0 && numberOfPicks != pickedElements.Count)
            {
                T element = castedList[rnd.Next(0,
                    castedList.Count)];

                if (pickCondition(element))
                {
                    pickedElements.Add(element);
                    castedList.Remove(element);
                }

                visitedElements.Remove(element);
            }

            return pickedElements;
        }

        private static IEnumerable<T> CopyList<T>(List<T> sourceList)
        {
            List<T> elements = new List<T>();

            foreach (T element in sourceList)
            {
                elements.Add(element);
            }

            return elements;
        }
    }
}
