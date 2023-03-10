using CliWrap;
using CliWrap.Buffered;

namespace ConsoleApp.Tests
{
    public class ProgramTests
    {

        static class Outputs
        {
            public const string tuukka = "Birthday:Tuukka:2000-05-23\r\n";
            public const string arnold = "Birthday:Arnold:1947-07-30\r\n";
            public const string christmast = "Holiday:Christmast last year:2022-12-24\r\n";
            public const string today = $":today:2023-03-10\r\n";
        }

        [Theory]
        [InlineData("list --category Birthday", Outputs.tuukka + Outputs.arnold)]
        [InlineData("list --description uu", Outputs.tuukka)]
        [InlineData("list --today", Outputs.today)]
        [InlineData("list --before-date 1950-01-01", Outputs.arnold)]
        [InlineData("list --after-date 2000-05-23", Outputs.christmast + Outputs.today)]
        [InlineData("list --after-date 2000-05-01 --before-date 2000-06-01", Outputs.tuukka)]
        [InlineData("list --after-date 2000-05-01 --before-date 2000-06-01 --exclude", Outputs.arnold + Outputs.christmast + Outputs.today)]
        [InlineData("list --date 2000-05-23", Outputs.tuukka)]
        [InlineData("list --date 2000-05-23 --exclude", Outputs.arnold + Outputs.christmast + Outputs.today)]
        public async void ConsoleTests(string args, string output)
        {
            var result = await Cli.Wrap("ConsoleApp")
                .WithArguments(args)
                .ExecuteBufferedAsync();

            Assert.Equal(output, result.StandardOutput);
        }

    }
}
