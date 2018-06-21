using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class EntityTests : BaseTest
    {
        public const string EntityName = "EntityTest";
        public const string EntityNameChanged = "EntityTestChanged";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) =>
            Initialize();

        [ClassCleanup]
        public static void ClassCleanup() =>
            Cleanup();

        [TestMethod]
        public async Task ShouldGetEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.GetAllEntitiesAsync(appId, appVersion);
                Assert.IsInstanceOfType(entities, typeof(IEnumerable<Entity>));
            }
        }

        [TestMethod]
        public async Task ShouldGetExistEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.GetAllEntitiesAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    await client.AddEntityAsync(EntityName, appId, appVersion);
                    entities = await client.GetAllEntitiesAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.GetEntityByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.AreEqual(firstEntity.Name, entity.Name);
            }
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsEntityId()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.GetEntityByIdAsync(InvalidId, appId, appVersion);
                Assert.IsNull(entity);
            }
        }

        [TestMethod]
        public async Task ShouldGetEntityByName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetEntityByNameAsync(EntityName, appId, appVersion) == null)
                    await client.AddEntityAsync(EntityName, appId, appVersion);

                var entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                Assert.IsNotNull(entity);
            }
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsEntityName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                if (entityTest != null)
                    await client.DeleteEntityAsync(entityTest.Id, appId, appVersion);

                var entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                Assert.IsNull(entity);
            }
        }

        [TestMethod]
        public async Task ShouldAddNewEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                if (entityTest != null)
                    await client.DeleteEntityAsync(entityTest.Id, appId, appVersion);

                var newId = await client.AddEntityAsync(EntityName, appId, appVersion);
                Assert.IsNotNull(newId);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnEntityNewEntityTestWhenAlreadyExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                if (entityTest == null)
                    await client.AddEntityAsync(EntityName, appId, appVersion);

                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.AddEntityAsync(EntityName, appId, appVersion));

                Assert.AreEqual(ex.Message, "BadArgument - The models: { EntityTest } already exist in the specified application version.");
            }
        }

        [TestMethod]
        public async Task ShouldRenameEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                var entityChanged = await client.GetEntityByNameAsync(EntityNameChanged, appId, appVersion);

                if (entity == null)
                {
                    await client.AddEntityAsync(EntityName, appId, appVersion);
                    entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                }

                if (entityChanged != null)
                    await client.DeleteEntityAsync(entityChanged.Id, appId, appVersion);

                await client.RenameEntityAsync(entity.Id, EntityNameChanged, appId, appVersion);

                entity = await client.GetEntityByIdAsync(entity.Id, appId, appVersion);
                Assert.AreEqual(EntityNameChanged, entity.Name);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenExistsEntityWithSameName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                var entityChanged = await client.GetEntityByNameAsync(EntityNameChanged, appId, appVersion);
                string entityChangedId = null;

                if (entity == null)
                {
                    await client.AddEntityAsync(EntityName, appId, appVersion);
                    entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                }
                if (entityChanged == null)
                    entityChangedId = await client.AddEntityAsync(EntityNameChanged, appId, appVersion);

                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.RenameEntityAsync(entity.Id, EntityNameChanged, appId, appVersion));

                Assert.AreEqual(ex.Message, "BadArgument - The models: { EntityTestChanged } already exist in the specified application version.");
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.RenameEntityAsync(InvalidId, EntityName, appId, appVersion));

                Assert.AreEqual(ex.Message, "BadArgument - Cannot find the input model in the specified application version.");
            }
        }

        [TestMethod]
        public async Task ShouldDeleteEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetEntityByNameAsync(EntityName, appId, appVersion) == null)
                    await client.AddEntityAsync(EntityName, appId, appVersion);

                var entity = await client.GetEntityByNameAsync(EntityName, appId, appVersion);
                await client.DeleteEntityAsync(entity.Id, appId, appVersion);
                entity = await client.GetEntityByIdAsync(entity.Id, appId, appVersion);

                Assert.IsNull(entity);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnDeleteEntityTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.DeleteEntityAsync(InvalidId, appId, appVersion));

                Assert.AreEqual(ex.Message, "BadArgument - Cannot find the input model in the specified application version.");
            }
        }
    }
}
