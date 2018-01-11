using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// references
using System.Linq;
using FinalB.Controllers;
using FinalB.Models;
using System.Web.Mvc;
using Moq;

namespace FinalB.Tests.Controllers
{
    [TestClass]
    public class MoviesControllerTest
    {
        MoviesController controller;
        List<Movie> movies;
        Mock<IMovieRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            mock = new Mock<IMovieRepository>();

            movies = new List<Movie>
            {
                new Movie { MovieID = 1, Title = "Movie 1", Year = 2000, Revenue = 300000 },
                new Movie { MovieID = 2, Title = "Movie 2", Year = 2005, Revenue = 250000 },
                new Movie { MovieID = 3, Title = "Movie 2", Year = 2010, Revenue = 200000 }
            };

            mock.Setup(m => m.Movies).Returns(movies.AsQueryable());

            controller = new MoviesController(mock.Object);
        }

        [TestMethod]
        public void IndexValidLoadsMovies()
        {
            // act
            var actual = (List<Movie>)((ViewResult)controller.Index()).Model;

            // assert
            Assert.AreEqual(movies[0], actual[0]);
        }

        [TestMethod]
        public void DetailsValidId()
        {
            // act
            var actual = (Movie)((ViewResult)controller.Details(1)).Model;

            // assert
            Assert.AreEqual(movies.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            ViewResult actual = (ViewResult) controller.Details(80938756);
            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsMissingId()
        {
            // act
            ViewResult actual = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }
    }
}
