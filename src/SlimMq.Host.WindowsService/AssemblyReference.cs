﻿using System.Reflection;

namespace Swfa.Host.WindowsService;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
