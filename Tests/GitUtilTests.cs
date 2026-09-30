using System;
using System.Collections.Generic;
using System.Threading;
using dnGREP.Common;
using Xunit;

namespace Tests
{
    public class GitUtilTests
    {
        [Fact]
        public void GitignoreLookupHonorsCancellationBeforeStartingGit()
        {
            using CancellationTokenSource source = new();
            source.Cancel();

            Assert.Throws<OperationCanceledException>(() =>
                GitUtil.GetGitignore("unused", source.Token));
        }

        [Fact]
        public void GitignoreLookupHonorsCancellationWithNoDirectories()
        {
            using CancellationTokenSource source = new();
            source.Cancel();

            Assert.Throws<OperationCanceledException>(() =>
                GitUtil.GetGitignore(new List<string>(), source.Token));
        }

        [Fact]
        public void GitignoreDiscoveryHonorsCancellationBeforeAccessingDirectories()
        {
            using PauseCancelTokenSource source = new();
            source.Cancel();

            Assert.Throws<OperationCanceledException>(() =>
                SafeDirectory.GetGitignoreDirectories("unused", true, false, source.Token));
        }
    }
}
