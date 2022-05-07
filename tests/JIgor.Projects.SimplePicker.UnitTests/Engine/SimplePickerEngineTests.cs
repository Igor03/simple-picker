using FluentAssertions;
using FluentAssertions.Execution;
using JIgor.Projects.SimplePicker.Engine;
using JIgor.Projects.SimplePicker.Engine.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JIgor.Projects.SimplePicker.UnitTests.Engine
{
    [TestClass]
    public class SimplePickerEngineTests
    {
        [TestMethod]
        public void PickElements_1_ShouldReturnExpectedResult()
        {
            // Arrange
            var seed = new List<string>()
            {
                "Breeze",
                "Brady",
                "Manning",
                "Brady"
            };
            var numberOfPicks = 2;
            var sourceListInitialSize = seed.Count;

            // Act
            var result = ListPicker.PickElements(seed, numberOfPicks);

            // Assert
            using var assertionScope = new AssertionScope();
            _ = result.Should().NotBeNull()
                .And.BeOfType(typeof(List<string>))
                .And.HaveCount(e => e == numberOfPicks);

            _ = seed.Should().NotBeNull()
                .And.BeOfType(typeof(List<string>))
                .And.HaveCount(e => e == (sourceListInitialSize - numberOfPicks));
        }

        [TestMethod]
        public void PickElements_1_ShouldThrowEmptyListException()
        {
            // Arrange
            var seed = new List<string>() { };
            var numberOfPicks = 2;

            // Act
            Action result = () => _ = ListPicker.PickElements(seed, numberOfPicks);

            // Assert
            result.Should().ThrowExactly<EmptyListException>();
        }

        [TestMethod]
        public void PickElements_1_ShouldThrowInvalidNumberOfPicksException_1()
        {
            // Arrange
            var sourceList = new List<string>()
            {
                "Breeze",
                "Montana",
                "Elway"
            };
            var numberOfPicks = 0;

            #region Act

            Action result = () => _ = ListPicker.PickElements(sourceList, numberOfPicks);

            #endregion Act

            // Assert
            result.Should().ThrowExactly<InvalidNumberOfPicksException>(because:
                "The number of picks was 0.");
        }

        [TestMethod]
        public void PickElements_1_ShouldThrowInvalidNumberOfPicksException_2()
        {
            // Arrange
            var sourceList = new List<string>()
            {
                "Breeze",
                "Montana",
                "Elway"
            };
            var numberOfPicks = 4;

            // Act
            Action result = () => _ = ListPicker.PickElements(sourceList, numberOfPicks);

            // Assert
            result.Should().ThrowExactly<InvalidNumberOfPicksException>(because:
                "The number of picks was greater than the total of elements of the source list.");
        }

        [TestMethod]
        public void PickElements_2_ShouldReturnExpectedResult()
        {
            // Arrange
            var seed = new List<string>()
            {
                "Breeze",
                "Brady",
                "Manning",
                "Brady"
            };
            var numberOfPicks = 2;
            var sourceListInitialSize = seed.Count;
            Func<string, bool> pickCondition = p => p == "Breeze";
            var matchedElementsCount = seed.Where(pickCondition).ToList().Count;

            // Act
            var result = ListPicker.PickElements(seed, numberOfPicks, pickCondition);

            // Assert
            using var assertionScope = new AssertionScope();
            _ = result.Should().NotBeNull()
                .And.BeOfType(typeof(List<string>))
                .And.HaveCount(e => e == (numberOfPicks) || e == (matchedElementsCount));

            _ = seed.Should().NotBeNull()
                .And.BeOfType(typeof(List<string>))
                .And.HaveCount(e => e == (sourceListInitialSize - numberOfPicks) || e == (sourceListInitialSize - matchedElementsCount));
        }

        [TestMethod]
        public void PickElements_2_ShouldThrowEmptyListException()
        {
            // Arrange
            var seed = new List<string>() { };
            var numberOfPicks = 2;
            Func<string, bool> pickCondition = p => p == "Breeze";

            // Act
            Action result = () => _ = ListPicker.PickElements(seed, numberOfPicks, pickCondition);

            // Assert
            _ = result.Should().ThrowExactly<EmptyListException>(because:
                "The source list has no elements");
        }

        [TestMethod]
        public void PickElements_2_ShouldThrowInvalidNumberOfPicksException_1()
        {
            // Arrange
            var seed = new List<string>()
            {
                "Mahomes",
                "Wilson"
            };
            var numberOfPicks = 0;
            Func<string, bool> pickCondition = p => p == "Breeze";

            // Act
            Action result = () => _ = ListPicker.PickElements(seed, numberOfPicks, pickCondition);

            // Assert
            _ = result.Should().ThrowExactly<InvalidNumberOfPicksException>(because:
                "The number of picks was 0.");
        }

        [TestMethod]
        public void PickElements_2_ShouldThrowInvalidNumberOfPicksException_2()
        {
            // Arrange
            var seed = new List<string>()
            {
                "Mahomes",
                "Wilson"
            };
            var numberOfPicks = 3;
            Func<string, bool> pickCondition = p => p == "Breeze";

            // Act
            Action result = () => _ = ListPicker.PickElements(seed, numberOfPicks, pickCondition);

            // Assert
            _ = result.Should().ThrowExactly<InvalidNumberOfPicksException>(because:
                "The number of picks was greater than the total of elements of the source list.");
        }
    }
}