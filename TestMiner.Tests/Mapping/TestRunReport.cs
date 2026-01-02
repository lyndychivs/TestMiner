namespace TestMiner.Tests.Mapping;

internal static class TestRunReport
{
    public static string Nunit3TestReport = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""no""?>
<test-run id=""0"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""1"" engine-version=""3.19.2.0"" clr-version=""8.0.13"" start-time=""2025-02-23 18:10:16Z"" end-time=""2025-02-23 18:10:17Z"" duration=""1.271509"">
  <test-suite type=""Assembly"" id=""1-1014"" name=""Name.dll"" fullname=""Full.Name"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2025-02-23T18:10:16.3202663Z"" end-time=""2025-02-23T18:10:17.3855674Z"" duration=""1.065288"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""1"">
    <environment machine-name=""DESKTOP"" user=""User"" />
    <test-suite type=""TestSuite"" id=""1-1015"" name=""Name"" fullname=""Full.Name"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2025-02-23T18:10:16.3225843Z"" end-time=""2025-02-23T18:10:17.3855632Z"" duration=""1.062979"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""1"">
      <test-suite type=""TestSuite"" id=""1-1016"" name=""Name"" fullname=""Full.Name"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2025-02-23T18:10:16.3227119Z"" end-time=""2025-02-23T18:10:17.3855595Z"" duration=""1.062848"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""1"">
        <test-suite type=""TestFixture"" id=""1-1000"" name=""Name"" fullname=""Full.Name"" classname=""ClassName"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2025-02-23T18:10:16.3227627Z"" end-time=""2025-02-23T18:10:16.3298252Z"" duration=""0.007062"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""1"">
          <test-case id=""1-1001"" name=""Name"" fullname=""Full.Name"" methodname=""MethodName"" classname=""ClassName"" runstate=""Runnable"" seed=""1736395274"" result=""Passed"" start-time=""2025-02-23T18:10:16.3237485Z"" end-time=""2025-02-23T18:10:16.3282720Z"" duration=""0.004549"" asserts=""1"" />
        </test-suite>
      </test-suite>
    </test-suite>
  </test-suite>
</test-run>";

    public static string InvalidNunit3TestReport = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""no""?>
<test-runn>
</test-runn>";
}
