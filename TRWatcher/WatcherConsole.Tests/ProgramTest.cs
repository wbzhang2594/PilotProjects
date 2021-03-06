using Newtonsoft.Json.Linq;
using System.Collections.Generic;
// <copyright file="ProgramTest.cs">Copyright ©  2015</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatcherConsole;

namespace WatcherConsole.Tests
{
    /// <summary>This class contains parameterized unit tests for Program</summary>
    [PexClass(typeof(Program))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ProgramTest
    {

        /// <summary>Test stub for QueryFrom(JArray)</summary>
        [PexMethod]
        internal IEnumerable<string> QueryFromTest(JArray jArray)
        {
            IEnumerable<string> result = Program.QueryFrom(jArray);
            return result;
            // TODO: add assertions to method ProgramTest.QueryFromTest(JArray)
        }
    }
}
