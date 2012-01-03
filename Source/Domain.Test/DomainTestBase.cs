using Domain.Util;
using NUnit.Framework;

namespace Domain.Test
{
    public class DomainTestBase
    {
        public static Repository Repository { get; set; }

        [SetUp]
        public void SetUpTestBase()
        {
            Registry.InitialiseDefaults();

            // clean repository for each test
            Repository = new Repository();
            Registry.Repository = Repository;
        }

        protected void AssertIsPersisted(Entity entity)
        {
            Assert.That(entity, Is.Not.Null);
            Assert.That(entity.Id, Is.Not.EqualTo(0));
            Assert.That(Repository.Load<Entity>(entity.Id), Is.EqualTo(entity));
        }
    }
}
