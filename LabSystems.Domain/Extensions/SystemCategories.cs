using System;
using System.Collections.Generic;

namespace LabSystems.Domain.Extensions
{
    public class SystemCategories
    {
        public enum Category
        {
            TestRegressions = 1,
            SandboxDeveloper,
            SandboxValidator,
            Developer,
            Unknown,
        }

        public static readonly Dictionary<string, Category> Categories = new Dictionary<string, Category>()
        {
            { "Regression" , Category.TestRegressions },
            { "Sandbox Developer" , Category.SandboxDeveloper },
            { "Sandbox Validator" , Category.SandboxValidator},
            { "Developer Primary" , Category.Developer }
        };
    }
}
