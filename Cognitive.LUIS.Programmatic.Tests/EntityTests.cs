using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class EntityTests
    {
        private const string SUBSCRIPTION_KEY = "{YourSubscriptionKey}";
        private readonly string _appId;

        public EntityTests()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                _appId = app.Id;
            else
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
        }

        [TestMethod]
        public async Task ShouldGetEntityList()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var entities = await client.GetAllEntitiesAsync(_appId, "1.0");
            Assert.IsInstanceOfType(entities, typeof(IEnumerable<Entity>));
        }

        [TestMethod]
        public async Task ShouldGetExistEntityById()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var entities = await client.GetAllEntitiesAsync(_appId, "1.0");
            if (entities.Count == 0)
            {
                await client.AddEntityAsync("EntityTest", _appId, "1.0");
                entities = await client.GetAllEntitiesAsync(_appId, "1.0");
            }

            var firstEntity = entities.FirstOrDefault();

            var entity = await client.GetEntityByIdAsync(firstEntity.Id, _appId, "1.0");
            Assert.AreEqual(firstEntity.Name, entity.Name);
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsEntityId()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);

            var entity = await client.GetEntityByIdAsync("51593248-363e-4a08-b946-2061964dc690", _appId, "1.0");
            Assert.IsNull(entity);
        }

        [TestMethod]
        public async Task ShouldGetEntityByName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            if (await client.GetEntityByNameAsync("EntityTest", _appId, "1.0") == null)
                await client.AddEntityAsync("EntityTest", _appId, "1.0");

            var entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsEntityName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var entityTest = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            if (entityTest != null)
                await client.DeleteEntityAsync(entityTest.Id, _appId, "1.0");

            var entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            Assert.IsNull(entity);
        }

        [TestMethod]
        public async Task ShouldAddNewEntityTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);

            var entityTest = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            if (entityTest != null)
                await client.DeleteEntityAsync(entityTest.Id, _appId, "1.0");

            var newId = await client.AddEntityAsync("EntityTest", _appId, "1.0");
            Assert.IsNotNull(newId);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnEntityNewEntityTestWhenAlreadyExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.AddEntityAsync("EntityTest", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "An entity extractor with the name EntityTest already exists in the application");
        }

        [TestMethod]
        public async Task ShouldRenameEntityTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            var entityChanged = await client.GetEntityByNameAsync("EntityTestChanged", _appId, "1.0");

            if (entity == null)
            {
                await client.AddEntityAsync("EntityTest", _appId, "1.0");
                entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            }

            if (entityChanged != null)
                await client.DeleteEntityAsync(entityChanged.Id, _appId, "1.0");

            await client.RenameEntityAsync(entity.Id, "EntityTestChanged", _appId, "1.0");

            entity = await client.GetEntityByIdAsync(entity.Id, _appId, "1.0");
            Assert.AreEqual("EntityTestChanged", entity.Name);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenExistsEntityWithSameName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            var entityChanged = await client.GetEntityByNameAsync("EntityTestChanged", _appId, "1.0");
            string entityChangedId = null;

            if (entity == null)
            {
                await client.AddEntityAsync("EntityTest", _appId, "1.0");
                entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            }
            if (entityChanged == null)
                entityChangedId = await client.AddEntityAsync("EntityTestChanged", _appId, "1.0");

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.RenameEntityAsync(entity.Id, "EntityTestChanged", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "The application already contains an entity extractor with the same name");
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.RenameEntityAsync("51593248-363e-4a08-b946-2061964dc690", "EntityTest", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "The specified model does not exist in the specified application.");
        }

        [TestMethod]
        public async Task ShouldDeleteEntityTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            if (await client.GetEntityByNameAsync("EntityTest", _appId, "1.0") == null)
                await client.AddEntityAsync("EntityTest", _appId, "1.0");

            var entity = await client.GetEntityByNameAsync("EntityTest", _appId, "1.0");
            await client.DeleteEntityAsync(entity.Id, _appId, "1.0");
            entity = await client.GetEntityByIdAsync(entity.Id, _appId, "1.0");

            Assert.IsNull(entity);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnDeleteEntityTestWhenNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.DeleteEntityAsync("51593248-363e-4a08-b946-2061964dc690", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "The specified model does not exist in the application");
        }

    }
}
