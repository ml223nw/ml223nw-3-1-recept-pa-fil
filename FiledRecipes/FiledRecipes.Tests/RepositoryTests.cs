using FiledRecipes.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace FiledRecipes.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Load_IfFileNotFound_ShouldThrowFileNotFoundException()
        {
            // arrange
            RecipeRepository repository = new RecipeRepository(@"..\..\App_Data\Recipes_FileNotFound.txt");

            // act
            repository.Load();

            // assert is handled by ExpectedException
        }

        [TestMethod]
        public void Load_IfEmptyRow_ShouldNotThrowException()
        {
            // arrange
            RecipeRepository repository = new RecipeRepository(@"..\..\App_Data\Recipes_EmptyRows.txt");

            try
            {
                // act
                repository.Load();
            }
            catch (Exception ex)
            {
                // assert
                Assert.Fail("Expected no exception, but got: " + ex.GetType());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileFormatException))]
        public void Load_IfBadIngredientFormat_ShouldThrowFileFormatException()
        {
            // arrange
            RecipeRepository repository = new RecipeRepository(@"..\..\App_Data\Recipes_BadIngredientFormat.txt");

            // act
            repository.Load();

            // assert is partly handled by ExpectedException
            Assert.Fail("Expected a FileFormatException, but no one thrown.");
        }

        [TestMethod]
        public void Load_After_IsModifiedShouldBeFalse()
        {
            // arrange
            RecipeRepository repository = new RecipeRepository(@"..\..\App_Data\Recipes_OK.txt");
            typeof(RecipeRepository).GetProperty("IsModified").SetValue(repository, true);

            // act
            repository.Load();

            // assert
            Assert.IsFalse(repository.IsModified);
        }

        [TestMethod]
        public void Load_After_RecipesShouldBeSorted()
        {
            // arrange
            RecipeRepository repository = new RecipeRepository(@"..\..\App_Data\Recipes_OK.txt");

            // act
            repository.Load();
            var sorted = repository.GetAll().OrderBy(r => r.Name);

            // assert
            Assert.IsTrue(sorted.SequenceEqual(repository.GetAll()));
        }

        [TestMethod]
        public void Load_After_ShouldFireRecipesChangeEvent()
        {
            // arrange
            RecipeRepository repository = new RecipeRepository(@"..\..\App_Data\Recipes_OK.txt");
            bool eventHandled = false;
            repository.RecipesChangedEvent += (s, e) => { eventHandled = true; };

            // act
            repository.Load();

            // assert
            Assert.IsTrue(eventHandled);
        }

        [TestMethod]
        public void Save_After_FileShouldBeCreated()
        {
            // arrange
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            RecipeRepository repository = new RecipeRepository(path);

            try
            {
                // act
                repository.Save();

                // assert
                Assert.IsTrue(File.Exists(path));
            }
            finally
            {
                // clean up
                File.Delete(path);
            }
        }

        [TestMethod]
        public void Save_After_IsModifiedShouldBeFalse()
        {
            // arrange
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            RecipeRepository repository = new RecipeRepository(path);
            typeof(RecipeRepository).GetProperty("IsModified").SetValue(repository, true);

            try
            {
                // act
                repository.Save();

                // assert
                Assert.IsFalse(repository.IsModified);
            }
            finally
            {
                // clean up
                File.Delete(path);
            }
        }

        [TestMethod]
        public void Save_After_ShouldFireRecipesChangeEvent()
        {
            // arrange
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            RecipeRepository repository = new RecipeRepository(path);
            bool eventHandled = false;
            repository.RecipesChangedEvent += (s, e) => { eventHandled = true; };

            try
            {
                // act
                repository.Save();

                // assert
                Assert.IsTrue(eventHandled);
            }
            finally
            {
                // clean up
                File.Delete(path);
            }
        }
    }
}
