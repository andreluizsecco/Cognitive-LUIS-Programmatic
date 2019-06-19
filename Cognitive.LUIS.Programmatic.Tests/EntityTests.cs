using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class EntityTests : BaseTest
    {
        public const string EntityName = "EntityTest";
        public const string EntityNameChanged = "EntityTestChanged";

        public EntityTests() =>
            Initialize();

        [Fact]
        public async Task ShouldGetEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<Entity>>(entities);
            }
        }

        [Fact]
        public async Task ShouldGetExistEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    await client.Entities.AddAsync(EntityName, appId, appVersion);
                    entities = await client.Entities.GetAllAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.Entities.GetByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.Equal(firstEntity.Name, entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsEntityId()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.Entities.GetByIdAsync(InvalidId, appId, appVersion);
                Assert.Null(entity);
            }
        }

        [Fact]
        public async Task ShouldGetEntityByName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Entities.GetByNameAsync(EntityName, appId, appVersion) == null)
                    await client.Entities.AddAsync(EntityName, appId, appVersion);

                var entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                Assert.NotNull(entity);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsEntityName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                if (entityTest != null)
                    await client.Entities.DeleteAsync(entityTest.Id, appId, appVersion);

                var entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                Assert.Null(entity);
            }
        }

        [Fact]
        public async Task ShouldAddNewEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                if (entityTest != null)
                    await client.Entities.DeleteAsync(entityTest.Id, appId, appVersion);

                var newId = await client.Entities.AddAsync(EntityName, appId, appVersion);
                Assert.NotNull(newId);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnEntityNewEntityTestWhenAlreadyExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                if (entityTest == null)
                    await client.Entities.AddAsync(EntityName, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.AddAsync(EntityName, appId, appVersion));

                Assert.Equal("BadArgument - The models: { EntityTest } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRenameEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                var entityChanged = await client.Entities.GetByNameAsync(EntityNameChanged, appId, appVersion);

                if (entity == null)
                {
                    await client.Entities.AddAsync(EntityName, appId, appVersion);
                    entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                }

                if (entityChanged != null)
                    await client.Entities.DeleteAsync(entityChanged.Id, appId, appVersion);

                await client.Entities.RenameAsync(entity.Id, EntityNameChanged, appId, appVersion);

                entity = await client.Entities.GetByIdAsync(entity.Id, appId, appVersion);
                Assert.Equal(EntityNameChanged, entity.Name);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenExistsEntityWithSameName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                var entityChanged = await client.Entities.GetByNameAsync(EntityNameChanged, appId, appVersion);
                string entityChangedId = null;

                if (entity == null)
                {
                    await client.Entities.AddAsync(EntityName, appId, appVersion);
                    entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                }
                if (entityChanged == null)
                    entityChangedId = await client.Entities.AddAsync(EntityNameChanged, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.RenameAsync(entity.Id, EntityNameChanged, appId, appVersion));

                Assert.Equal("BadArgument - The models: { EntityTestChanged } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.RenameAsync(InvalidId, EntityName, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 51593248-363e-4a08-b946-2061964dc690 in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldDeleteEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Entities.GetByNameAsync(EntityName, appId, appVersion) == null)
                    await client.Entities.AddAsync(EntityName, appId, appVersion);

                var entity = await client.Entities.GetByNameAsync(EntityName, appId, appVersion);
                await client.Entities.DeleteAsync(entity.Id, appId, appVersion);
                entity = await client.Entities.GetByIdAsync(entity.Id, appId, appVersion);

                Assert.Null(entity);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnDeleteEntityTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.DeleteAsync(InvalidId, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 51593248-363e-4a08-b946-2061964dc690 in the specified application version.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
