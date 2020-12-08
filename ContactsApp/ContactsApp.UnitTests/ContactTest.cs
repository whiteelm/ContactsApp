using System;
using NUnit.Framework;

namespace ContactsApp.UnitTests
{
    //using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestFixture]
    public class ContactTest
    {
        [Test]
        public void Name_GoodName_ReturnsSameName()
        {
            //Setup
            var contact = new Contact();
            var sourceName = "John";
            var expectedName = sourceName;

            //Act
            contact.Name = sourceName;
            var actualName = contact.Name;

            //Assert
            NUnit.Framework.Assert.AreEqual(expectedName, actualName);
        }
    }
}
