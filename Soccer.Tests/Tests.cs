namespace Soccer
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void HappyPath()
        {
            Assert.AreEqual("Aston_Villa", Program.Execute());
        }
    }
}