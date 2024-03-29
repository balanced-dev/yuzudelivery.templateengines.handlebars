﻿using System.IO;
using NUnit.Framework;
using YuzuDelivery.Core;

namespace YuzuDelivery.TemplateEngines.Handlebars.Helpers
{

    public class ModPartialTests
    {
        private ModPartial helper;

        [SetUp]
        public void Setup()
        {
            ModPartial.Register();
        }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            YuzuConstants.Reset();
            YuzuConstants.Initialize(new YuzuConstantsConfig());
        }

        [Test]
        public void given_empty_path_and_context()
        {
            var source = "{{{modPartial '' foo ''}}}";
            var partialSource = "test {{bar}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using (var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("parPartialName", partialTemplate);
            }

            var data = new { foo = new vmBlock_PartialName() };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test bar"));
        }

        [Test]
        public void given_empty_path_context_and_parameter()
        {
            var source = "{{{modPartial '' foo '' param='test'}}}";
            var partialSource = "test {{bar}} {{param}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using (var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("parPartialName", partialTemplate);
            }

            var data = new { foo = new vmBlock_PartialName() };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test bar test"));
        }

        [Test]
        public void given_empty_path_and_context_is_array()
        {
            var source = "{{{modPartial '' foo ''}}}";
            var partialSource = "test {{#each this}}{{this.bar}}{{/each}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using (var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("parPartialName", partialTemplate);
            }

            var data = new { foo = new[] { new vmBlock_PartialName() } };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test bar"));
        }

        [Test]
        public void given_path_and_context_where_context_is_an_object_and_modifiers_is_empty()
        {
            var source = "{{{modPartial 'partialName' foo ''}}}";
            var partialSource = "test {{bar}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using(var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("partialName", partialTemplate);
            }

            var data = new
            {
                foo = new
                {
                    bar = "foo bar"
                }
            };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test foo bar"));
        }

        [Test]
        public void given_path_and_context_where_context_is_an_object_and_modifiers()
        {
            var source = "{{{modPartial 'partialName' foo 'modifier'}}}";
            var partialSource = "test {{bar}} {{#each _modifiers}}{{this}}{{/each}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using(var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("partialName", partialTemplate);
            }

            var data = new
            {
                foo = new
                {
                    bar = "foo bar"
                }
            };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test foo bar modifier"));
        }


        [Test]
        public void given_path_context_and_parameter()
        {
            var source = "{{{modPartial 'partialName' foo param='test'}}}";
            var partialSource = "test {{bar}} {{param}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using(var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("partialName", partialTemplate);
            }

            var data = new
            {
                foo = new
                {
                    bar = "foo bar"
                }
            };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test foo bar test"));
        }

        [Test]
        public void given_path_context_modifier_and_parameter()
        {
            var source = "{{{modPartial 'partialName' foo 'modifier' param='test'}}}";
            var partialSource = "test {{bar}} {{param}} {{#each _modifiers}}{{this}}{{/each}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using(var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("partialName", partialTemplate);
            }

            var data = new
            {
                foo = new
                {
                    bar = "foo bar"
                }
            };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test foo bar test modifier"));
        }

        [Test]
        public void given_path_context_and_multiple_parameters()
        {
            var source = "{{{modPartial 'partialName' foo param1='test' param2='test again'}}}";
            var partialSource = "test {{bar}} {{param1}} {{param2}}";
            var template = HandlebarsDotNet.Handlebars.Compile(source);

            using(var reader = new StringReader(partialSource))
            {
                var partialTemplate = HandlebarsDotNet.Handlebars.Compile(reader);
                HandlebarsDotNet.Handlebars.RegisterTemplate("partialName", partialTemplate);
            }

            var data = new
            {
                foo = new
                {
                    bar = "foo bar"
                }
            };

            var output = template(data);
            Assert.That(output, Is.EqualTo("test foo bar test test again"));
        }

    }
}
