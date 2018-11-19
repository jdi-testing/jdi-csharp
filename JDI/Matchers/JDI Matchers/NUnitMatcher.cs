﻿using System;
using Epam.JDI.Core.Settings;
using JDI_Commons;
using NUnit.Framework;

namespace JDI_Matchers
{
    public class NUnitMatcher : IAssert
    {
        public Exception Exception(string message, Exception ex)
        {
            JDISettings.Logger.Exception(ex);
            return ex;
        }

        public Exception Exception(string message)
        {
            JDISettings.Logger.Error(message);
            Assert.Fail(message);
            return new Exception(message);
        }

        public void AreEqual<T>(T actual, T expected)
        {
            Assert.AreEqual(actual, expected);
        }

        public void Matches(string actual, string regEx)
        {
            IsTrue(actual.Matches(regEx));
        }

        public void Contains(string actual, string expected)
        {
            IsTrue(actual.Contains(expected));
        }

        public void IsTrue(bool actual)
        {
            Assert.IsTrue(actual);
        }

        public void IsTrue(Func<bool> actual)
        {
            Assert.IsTrue(actual.ForceDone());
        }
    }
}
