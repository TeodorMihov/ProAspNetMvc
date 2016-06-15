using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using EssentialTools.Models.Substract;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void Discount_Above_100()
        {
            //arrange 
            IDiscountHelper target = getTestObject();
            decimal total = 200;

            //act
            var discountTotal = target.ApplyDiscount(total);

            //arrange
            Assert.AreEqual(total * 0.9m, discountTotal);

        }

        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            IDiscountHelper target = getTestObject();

            var tenDiscount = target.ApplyDiscount(10);
            var hundredDiscount = target.ApplyDiscount(100);
            var inTheMiddle = target.ApplyDiscount(50);

            Assert.AreEqual(5, tenDiscount, "Ten discount is wrong");
            Assert.AreEqual(95, hundredDiscount, "100 discount is wrong");
            Assert.AreEqual(45, inTheMiddle, "50 discount is wrong");
        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            IDiscountHelper target = getTestObject();

            var five = target.ApplyDiscount(5);
            var zero = target.ApplyDiscount(0);

            Assert.AreEqual(5, five);
            Assert.AreEqual(0, zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Discount_Negative_Total()
        {
            IDiscountHelper target = getTestObject();

            target.ApplyDiscount(-1);
        }
    }
}
