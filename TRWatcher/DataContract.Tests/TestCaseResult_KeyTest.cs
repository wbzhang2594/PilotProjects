// <copyright file="TestCaseResult_KeyTest.cs" company="Microsoft">Copyright © Microsoft 2015</copyright>
using System;
using DataContract;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataContract.Tests
{
    /// <summary>This class contains parameterized unit tests for TestCaseResult_Key</summary>
    [PexClass(typeof(TestCaseResult_Key))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class TestCaseResult_KeyTest
    {
        /// <summary>Test stub for Equals(TestCaseResult_Key, TestCaseResult_Key)</summary>
        [PexMethod]
        public bool EqualsTest(
            [PexAssumeUnderTest]TestCaseResult_Key target,
            TestCaseResult_Key x,
            TestCaseResult_Key y
        )
        {
            bool result = target.Equals(x, y);
            return result;
            // TODO: add assertions to method TestCaseResult_KeyTest.EqualsTest(TestCaseResult_Key, TestCaseResult_Key, TestCaseResult_Key)
        }

        /// <summary>Test stub for GetHashCode(TestCaseResult_Key)</summary>
        [PexMethod]
        public int GetHashCodeTest([PexAssumeUnderTest]TestCaseResult_Key target, TestCaseResult_Key obj)
        {
            int result = target.GetHashCode(obj);
            return result;
            // TODO: add assertions to method TestCaseResult_KeyTest.GetHashCodeTest(TestCaseResult_Key, TestCaseResult_Key)
        }
    }
}
