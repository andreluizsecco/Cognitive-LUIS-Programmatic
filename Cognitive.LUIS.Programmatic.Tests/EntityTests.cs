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
        public const string SimpleEntityName = "SimpleEntityTest";
        public const string CompositeEntityName = "CompositeEntityTest";
        public const string ClosedListEntityName = "ClosedListEntityTest";
        public const string RegexEntityName = "RegexEntityTest";
        public const string PatternAnyEntityName = "PatternAnyEntityTest";
        public const string SimpleEntityNameChanged = "SimpleEntityTestChanged";

        public EntityTests() =>
            Initialize();

        [Fact]
        public async Task ShouldGetSimpleEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllSimpleEntitiesAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<Entity>>(entities);
            }
        }

        [Fact]
        public async Task ShouldGetCompositeEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllCompositeEntitiesAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<CompositeEntity>>(entities);
            }
        }

        [Fact]
        public async Task ShouldGetClosedListEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllClosedListEntitiesAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<ClosedListEntity>>(entities);
            }
        }

        [Fact]
        public async Task ShouldGetRegexEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllRegexEntitiesAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<RegexEntity>>(entities);
            }
        }

        [Fact]
        public async Task ShouldGetPatternAnyEntityList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllPatternAnyEntitiesAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<PatternAnyEntity>>(entities);
            }
        }

        [Fact]
        public async Task ShouldGetExistSimpleEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllSimpleEntitiesAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);
                    entities = await client.Entities.GetAllSimpleEntitiesAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.Entities.GetSimpleEntityByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.Equal(firstEntity.Name, entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetExistCompositeEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllCompositeEntitiesAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);
                    await client.Entities.AddCompositeEntityAsync(CompositeEntityName, new[] { "SimpleEntityTest" }, appId, appVersion);
                    entities = await client.Entities.GetAllCompositeEntitiesAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.Entities.GetCompositeEntityByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.Equal(firstEntity.Name, entity.Name);
                Assert.Equal("SimpleEntityTest", firstEntity.Children.ElementAt(0).Name);
            }
        }

        [Fact]
        public async Task ShouldGetExistClosedListEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllClosedListEntitiesAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    var subList = new List<ClosedListItem>
                    {
                        new ClosedListItem() { CanonicalForm = "Test", List = new[] { "value1", "value2" } }
                    };

                    await client.Entities.AddClosedListEntityAsync(ClosedListEntityName, subList, appId, appVersion);
                    entities = await client.Entities.GetAllClosedListEntitiesAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.Entities.GetClosedListEntityByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.Equal(firstEntity.Name, entity.Name);
                Assert.Single(firstEntity.SubLists);
            }
        }

        [Fact]
        public async Task ShouldGetExistRegexEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllRegexEntitiesAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    await client.Entities.AddRegexEntityAsync(RegexEntityName, "[0-9]-[0-9]", appId, appVersion);
                    entities = await client.Entities.GetAllRegexEntitiesAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.Entities.GetRegexEntityByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.Equal(firstEntity.Name, entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetExistPatternAnyEntityById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entities = await client.Entities.GetAllPatternAnyEntitiesAsync(appId, appVersion);
                if (entities.Count == 0)
                {
                    await client.Entities.AddPatternAnyEntityAsync(PatternAnyEntityName, new[] { "value1", "value2" }, appId, appVersion);
                    entities = await client.Entities.GetAllPatternAnyEntitiesAsync(appId, appVersion);
                }

                var firstEntity = entities.FirstOrDefault();

                var entity = await client.Entities.GetPatternAnyEntityByIdAsync(firstEntity.Id, appId, appVersion);
                Assert.Equal(firstEntity.Name, entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsEntityId()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.Entities.GetSimpleEntityByIdAsync(InvalidId, appId, appVersion);
                Assert.Null(entity);
            }
        }

        [Fact]
        public async Task ShouldGetEntityByName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion) == null)
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);

                var entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                Assert.NotNull(entity);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsEntityName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                if (entityTest != null)
                    await client.Entities.DeleteSimpleEntityAsync(entityTest.Id, appId, appVersion);

                var entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                Assert.Null(entity);
            }
        }

        [Fact]
        public async Task ShouldAddNewEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                if (entityTest != null)
                    await client.Entities.DeleteSimpleEntityAsync(entityTest.Id, appId, appVersion);

                var newId = await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);
                Assert.NotNull(newId);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnEntityNewEntityTestWhenAlreadyExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entityTest = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                if (entityTest == null)
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion));

                Assert.Equal("BadArgument - The models: { SimpleEntityTest } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRenameEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                var entityChanged = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityNameChanged, appId, appVersion);

                if (entity == null)
                {
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);
                    entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                }

                if (entityChanged != null)
                    await client.Entities.DeleteSimpleEntityAsync(entityChanged.Id, appId, appVersion);

                await client.Entities.RenameAsync(entity.Id, SimpleEntityNameChanged, appId, appVersion);

                entity = await client.Entities.GetSimpleEntityByIdAsync(entity.Id, appId, appVersion);
                Assert.Equal(SimpleEntityNameChanged, entity.Name);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenExistsEntityWithSameName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                var entityChanged = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityNameChanged, appId, appVersion);
                string entityChangedId = null;

                if (entity == null)
                {
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);
                    entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                }
                if (entityChanged == null)
                    entityChangedId = await client.Entities.AddSimpleEntityAsync(SimpleEntityNameChanged, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.RenameAsync(entity.Id, SimpleEntityNameChanged, appId, appVersion));

                Assert.Equal("BadArgument - The models: { SimpleEntityTestChanged } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameEntityTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.RenameAsync(InvalidId, SimpleEntityName, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 51593248-363e-4a08-b946-2061964dc690 in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldDeleteEntityTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion) == null)
                    await client.Entities.AddSimpleEntityAsync(SimpleEntityName, appId, appVersion);

                var entity = await client.Entities.GetSimpleEntityByNameAsync(SimpleEntityName, appId, appVersion);
                await client.Entities.DeleteSimpleEntityAsync(entity.Id, appId, appVersion);
                entity = await client.Entities.GetSimpleEntityByIdAsync(entity.Id, appId, appVersion);

                Assert.Null(entity);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnDeleteEntityTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Entities.DeleteSimpleEntityAsync(InvalidId, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 51593248-363e-4a08-b946-2061964dc690 in the specified application version.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
