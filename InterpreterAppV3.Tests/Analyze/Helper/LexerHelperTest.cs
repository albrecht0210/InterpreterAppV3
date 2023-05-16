using InterpreterAppV3.Library.Analyze.Helper;
using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Tests.Analyze.Helper
{
    public class LexerHelperTest
    {
        [TestCase("19")]
        [TestCase(".7")]
        [TestCase("1.6")]
        public void RegexMatcher_ShouldMatchText(string text)
        {
            // Arrange
            string pattern = @"^\d*\.?\d+$";

            // Act
            var result = LexerHelper.RegexMatcher(pattern, text);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestCase(".")]
        [TestCase("9.")]
        [TestCase("a")]
        [TestCase("*")]
        public void RegexMatcher_ShouldNotMatchText(string text)
        {
            // Arrange
            string pattern = @"^\d*\.?\d+$";

            // Act
            var result = LexerHelper.RegexMatcher(pattern, text);

            // Assert
            Assert.AreEqual(false, result);
        }

        [TestCase("BEGIN")]
        [TestCase("END")]
        [TestCase("CODE")]
        [TestCase("IF")]
        [TestCase("ELSE")]
        [TestCase("WHILE")]
        [TestCase("DISPLAY")]
        [TestCase("SCAN")]
        [TestCase("AND")]
        [TestCase("OR")]
        [TestCase("NOT")]
        [TestCase("INT")]
        [TestCase("FLOAT")]
        [TestCase("CHAR")]
        [TestCase("BOOL")]
        public void IsReserveKeyword_ReturnsTrue(string text)
        {
            // Act and Assert
            Assert.IsTrue(LexerHelper.IsReserveKeyword(text));
        }

        [TestCase("begin")]
        [TestCase("int")]
        public void IsReserveKeyword_ReturnsFalse(string text)
        {
            // Act and Assert
            Assert.IsFalse(LexerHelper.IsReserveKeyword(text));
        }

        [Test]
        public void GetReserveKeywordSymbol_WithValidKeyword_ReturnSymbol()
        {
            // Arrange
            string valid_keyword = "BEGIN";

            // Act
            var symbol = LexerHelper.GetReserveKeywordSymbol(valid_keyword);

            // Assert
            Assert.AreEqual(Symbol.BEGIN, symbol);
        }

        [TestCase("begin")]
        [TestCase("int")]
        public void GetReserveKeywordSymbol_Throws_Exception(string text)
        {
            // Assert
            Assert.Throws<Exception>(() => LexerHelper.GetReserveKeywordSymbol(text));
        }

    }
}
