using Moq;
using NUnit.Framework;
using Projektas_Irankiai.Controllers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnitTestProject
{
    [TestFixture]
    public class ImageGenerationTests
    {
        [Test]
        public void CanSelectBestRatedTags()
        {
            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.selectInts("SELECT fk_Zymeid_Zyme, COUNT(fk_Zymeid_Zyme) AS zymeCount FROM Paveikslelis_Zyme " +
                    "GROUP BY fk_Zymeid_Zyme ORDER BY COUNT(fk_Zymeid_Zyme) DESC LIMIT 3")).Returns(It.IsAny<List<int>>);

            ImageGenerationController controller = new ImageGenerationController();

            Assert.AreEqual(new List<int>(), controller.selectMostPopularTags(new DataBase()));
        }

        /// <summary>
        /// Tests if we get 3 random DISTINCT tags from all available tags
        /// </summary>
        [Test]
        public void CanSelectRandomTags()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(9);

            List<int> expected = new List<int>();
            expected.Add(1);
            expected.Add(2);
            expected.Add(3);

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.selectInts("SELECT `id_Zyme` FROM `Zyme`")).Returns(list);

            ImageGenerationController controller = new ImageGenerationController();
            var actual = controller.selectRandomTags(mock.Object);

            Assert.AreEqual(expected.Count, actual.Count);

            if (actual.Count != actual.Distinct().Count())
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that query string returns 3 tags
        /// </summary>
        [Test]
        public void CanSelectUserSelectedTags()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.selectInts("SELECT fk_Zymeid_Zyme, COUNT(fk_Zymeid_Zyme) AS zymeCount FROM Pasirenka " +
                    "GROUP BY fk_Zymeid_Zyme ORDER BY COUNT(fk_Zymeid_Zyme) DESC LIMIT 3")).Returns(list);

            ImageGenerationController controller = new ImageGenerationController();

            Assert.AreEqual(list.Count, controller.selectUserSelectedTags(mock.Object).Count);
        }

        /// <summary>
        /// Checks if a list of distinct templates gotten by popular tags and user selected tags is created.
        /// </summary>
        [Test]
        public void CanConcatTemplateLists()
        {
            List<int> tags1 = new List<int>() { 1, 2, 3 };
            List<int> tags2 = new List<int>() { 3, 4, 5 };

            List<Template> templates = new List<Template>() {
                new Template(1),
                new Template(2),
                new Template(3),
                new Template(4),
                new Template(5)
            };

            List<Template> templatesWithTags = new List<Template>() {
                new Template(1),
                new Template(2),
                new Template(3),
                new Template(4),
                new Template(5)
            };

            //Not interested in what kinds of tags are added
            foreach(var i in templatesWithTags){
                i.addTag(9);
            }

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.AddTagsToTemplates(It.IsAny<List<Template>>())).Callback<List<Template>>(list => {
                foreach (var i in list) {
                    i.addTag(9);

                }
            });

            ImageGenerationController controller = new ImageGenerationController();


            Assert.AreEqual(templatesWithTags.Count, controller.concatTemplateLists(tags1, tags2, mock.Object).Count);
            try
            {
                for (int i = 0; i < templatesWithTags.Count; i++)
                {
                    templatesWithTags[i].Equals(controller.concatTemplateLists(tags1, tags2, mock.Object)[i]);
                }
            }
            catch
            {
                Assert.Fail("Exception caused by for loop");
            }

        }

        /// <summary>
        /// Testing if sql query is correct
        /// </summary>
        [Test]
        public void CanGetTemplatesByUserTags()
        {
            List<int> tags = new List<int>() { 1, 2, 3 };

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.GetTemplatesByXTags(
                "SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 1)",
                It.IsAny<List<int>>()))
                .Returns(tags);

            ImageGenerationController controller = new ImageGenerationController();

            Assert.IsNotNull(controller.getTemplatesByUserTags(mock.Object));
        }

        /// <summary>
        /// Test if sql query is correct
        /// </summary>
        /// 
        [Test]
        [TestCase("SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 1)")]
        public void CanGetTemplatesByPopularTags(string query)
        {
            List<int> tags = new List<int>() { 1, 2, 3 };

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.GetTemplatesByXTags(
                query,
                It.IsAny<List<int>>()))
                .Returns(tags);

            ImageGenerationController controller = new ImageGenerationController();

            Assert.IsNotNull(controller.getTemplatesByPopularTags(mock.Object));
        }

        [Test]
        public void CanGetTextByUserTags()
        {
            List<int> tags = new List<int>() { 1, 2, 3 };

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.GetTemplatesByXTags(
                "SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 0)",
                It.IsAny<List<int>>()))
                .Returns(tags);

            ImageGenerationController controller = new ImageGenerationController();

            Assert.IsNotNull(controller.getTextByUserTags(mock.Object));
        }

        [Test]
        public void CanGetTextByPopularTags()
        {
            List<int> tags = new List<int>() { 1, 2, 3 };

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.GetTemplatesByXTags(
                "SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 0)",
                It.IsAny<List<int>>()))
                .Returns(tags);

            ImageGenerationController controller = new ImageGenerationController();

            Assert.IsNotNull(controller.getTextByPopularTags(mock.Object));
        }

        [Test]
        public void CanConcatTextLists()
        {
            List<int> tags1 = new List<int>() { 1, 2, 3 };
            List<int> tags2 = new List<int>() { 3, 4, 5 };

            List<Text> templates = new List<Text>() {
                new Text(1),
                new Text(2),
                new Text(3),
                new Text(4),
                new Text(5)
            };

            List<Text> templatesWithTags = new List<Text>() {
                new Text(1),
                new Text(2),
                new Text(3),
                new Text(4),
                new Text(5)
            };

            //Not interested in what kinds of tags are added
            foreach (var i in templatesWithTags)
            {
                i.addTag(9);
            }

            Mock<DataBase> mock = new Mock<DataBase>();
            mock.Setup(x => x.GetTextTags(It.IsAny<List<Text>>())).Callback<List<Text>>(list => {
                foreach (var i in list)
                {
                    i.addTag(9);

                }
            });

            ImageGenerationController controller = new ImageGenerationController();


            Assert.AreEqual(templatesWithTags.Count, controller.concatTextLists(tags1, tags2, mock.Object).Count);
            try
            {
                for (int i = 0; i < templatesWithTags.Count; i++)
                {
                    templatesWithTags[i].Equals(controller.concatTextLists(tags1, tags2, mock.Object)[i]);
                }
            }
            catch
            {
                Assert.Fail("Exception caused by for loop");
            }
        }

        [Test]
        public void CanAddWeightPop()
        {
            List<int> popularTags = new List<int>() { 1, 2, 3 };

            List<Template> templates = new List<Template>()
            {
                new Template(0),
                new Template(1),
                new Template(2)

            };

            templates[0].addTag(4);
            templates[1].addTag(2);
            templates[2].addTag(1);
            templates[2].addTag(2);

            List<Template> expected = new List<Template>()
            {
                new Template(0),
                new Template(1),
                new Template(2)

            };
            expected[0].addTag(4);
            expected[1].addTag(2);
            expected[2].addTag(1);
            expected[2].addTag(2);

            expected[1].addWeight(2);
            expected[2].addWeight(2);
            expected[2].addWeight(2);

            ImageGenerationController controller = new ImageGenerationController();

            var actual = controller.addWeightPop(templates, popularTags);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].getWeight(), actual[i].getWeight());
            }
        }

        [Test]
        public void CanAddWeightPopT()
        {
            List<int> popularTags = new List<int>() { 1, 2, 3 };

            List<Text> text = new List<Text>()
            {
                new Text(0),
                new Text(1),
                new Text(2)

            };

            text[0].addTag(4);
            text[1].addTag(2);
            text[2].addTag(1);
            text[2].addTag(2);

            List<Text> expected = new List<Text>()
            {
                new Text(0),
                new Text(1),
                new Text(2)

            };
            expected[0].addTag(4);
            expected[1].addTag(2);
            expected[2].addTag(1);
            expected[2].addTag(2);

            expected[1].addWeight(2);
            expected[2].addWeight(2);
            expected[2].addWeight(2);

            ImageGenerationController controller = new ImageGenerationController();

            var actual = controller.addWeightPopT(text, popularTags);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].getWeight(), actual[i].getWeight());
            }
        }

        [Test]
        public void CanAddWeightUser()
        {
            List<int> usertags = new List<int>() { 1, 2, 3 };

            List<Template> templates = new List<Template>()
            {
                new Template(0),
                new Template(1),
                new Template(2)

            };

            templates[0].addTag(4);
            templates[1].addTag(2);
            templates[2].addTag(1);
            templates[2].addTag(2);

            List<Template> expected = new List<Template>()
            {
                new Template(0),
                new Template(1),
                new Template(2)

            };
            expected[0].addTag(4);
            expected[1].addTag(2);
            expected[2].addTag(1);
            expected[2].addTag(2);

            expected[1].addWeight(1);
            expected[2].addWeight(1);
            expected[2].addWeight(1);

            ImageGenerationController controller = new ImageGenerationController();

            var actual = controller.addWeightUser(templates, usertags);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].getWeight(), actual[i].getWeight());
            }
        }

        [Test]
        public void CanAddWeightUserT()
        {
            List<int> userTags = new List<int>() { 1, 2, 3 };

            List<Text> text = new List<Text>()
            {
                new Text(0),
                new Text(1),
                new Text(2)

            };

            text[0].addTag(4);
            text[1].addTag(2);
            text[2].addTag(1);
            text[2].addTag(2);

            List<Text> expected = new List<Text>()
            {
                new Text(0),
                new Text(1),
                new Text(2)
            };

            expected[0].addTag(4);
            expected[1].addTag(2);
            expected[2].addTag(1);
            expected[2].addTag(2);

            expected[1].addWeight(1);
            expected[2].addWeight(1);
            expected[2].addWeight(1);


            ImageGenerationController controller = new ImageGenerationController();

            var actual = controller.addWeightUserT(text, userTags);

           //CollectionAssert.AreEqual(expected, actual); This generates following exception: 
          //  Message:
          //      Expected and actual are both < System.Collections.Generic.List`1[Projektas_Irankiai.Controllers.Text] > with 3 elements
          //        Values differ at index[0]
          //Expected: < id: 0 weight: 0 >
          // But was:  < id: 0 weight: 0 >

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].getWeight(), actual[i].getWeight());
            }
        }
    }
}
