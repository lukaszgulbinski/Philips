namespace Weather
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void HappyPath()
        {
            Assert.AreEqual("14", Program.Execute());
        }
    }
}