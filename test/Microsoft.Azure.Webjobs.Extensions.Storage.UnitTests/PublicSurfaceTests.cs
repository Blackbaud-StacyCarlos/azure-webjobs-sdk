﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Azure.WebJobs.Host.TestCommon;
using Xunit;

namespace Microsoft.Azure.WebJobs.Host.UnitTests
{
    /// <summary>
    /// These tests help maintain our public surface area + dependencies. They will
    /// fail any time new dependencies or public surface area are added, ensuring
    /// we review such additions carefully.
    /// </summary>
    public class PublicSurfaceTests
    {
        [Fact]
        public void WebJobs_Extensions_Storage_VerifyAssemblyReferences()
        {
            var names = TestHelpers.GetAssemblyReferences(typeof(QueueTriggerAttribute).Assembly)
                .OrderBy(n => n);

            var expectedReferences = new string[]
            {
                "Microsoft.Azure.WebJobs",
                "Microsoft.Azure.WebJobs.Host",
                "Microsoft.Extensions.Configuration.Abstractions",
                "Microsoft.Extensions.DependencyInjection.Abstractions",
                "Microsoft.Extensions.Hosting.Abstractions",
                "Microsoft.Extensions.Logging.Abstractions",
                "Microsoft.Extensions.Options",
                "Microsoft.WindowsAzure.Storage",
                "netstandard",
                "Newtonsoft.Json",
                "System.ComponentModel.Annotations"
            }.OrderBy(n => n);

            Assert.True(expectedReferences.SequenceEqual(names, StringComparer.Ordinal),
                "Assembly references do not match the expected references");
        }

        [Fact]
        public void WebJobs_Extensions_Storage_VerifyPublicSurfaceArea()
        {
            var assembly = typeof(QueueTriggerAttribute).Assembly;

            var expected = new[]
            {
                "BlobAttribute",
                "BlobNameValidationAttribute",
                "BlobParameterDescriptor",
                "BlobTriggerAttribute",
                "BlobTriggerParameterDescriptor",
                "IQueueProcessorFactory",
                "BlobsOptions",
                "QueuesOptions",
                "PoisonMessageEventArgs",
                "QueueAttribute",
                "QueueParameterDescriptor",
                "QueueProcessor",
                "QueueProcessorFactoryContext",
                "QueueTriggerAttribute",
                "QueueTriggerParameterDescriptor",
                "StorageWebJobsBuilderExtensions",
                "TableAttribute",
                "TableEntityParameterDescriptor",
                "TableParameterDescriptor",
                "StorageAccount",
                "StorageAccountProvider",
                "AzureStorageWebJobsStartup"
            };

            TestHelpers.AssertPublicTypes(expected, assembly);
        }
    }
}
