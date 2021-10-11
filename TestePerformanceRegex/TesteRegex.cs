using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace TestePerformanceRegex
{
    [MemoryDiagnoser]
    public class TesteRegex
    {
        private static readonly List<Usuario> FakeUsuarios = new Faker<Usuario>()
            .UseSeed(420)
            .RuleFor(r => r.Email, faker => faker.Person.Email)
            .RuleFor(r => r.Login, faker => faker.Person.UserName)
            .Generate(10);
        
        private static readonly List<string> emails = 
            FakeUsuarios.Select(x => x.Email).Concat(FakeUsuarios.Select(x => x.Login)).ToList();
        
        private static readonly Regex RegexStatico =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        private static readonly Regex RegexStaticoCompilado =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled,
                TimeSpan.FromMilliseconds(250));
        
        [Benchmark]
        public void IsMatchInternal()
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

            for (var i = 0; i < emails.Count; i++)
            {
                var email = emails[i];
                var isMatch = regex.IsMatch(email);
            }
        }
        
        [Benchmark]
        public void IsMatchInternalCompiled()
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            for (var i = 0; i < emails.Count; i++)
            {
                var email = emails[i];
                var isMatch = regex.IsMatch(email);
            }
        }
        
        [Benchmark]
        public void IsMatchStatic()
        {
            for (var i = 0; i < emails.Count; i++)
            {
                var email = emails[i];
                var isMatch = RegexStatico.IsMatch(email);
            }
        }
        
        [Benchmark]
        public void IsMatchStaticCompiled()
        {
            for (var i = 0; i < emails.Count; i++)
            {
                var email = emails[i];
                var isMatch = RegexStaticoCompilado.IsMatch(email);
            }
        }
    }
    
    public class Usuario
    {
        public string Email { get; set; }
        public string Login { get; set; }
    }
}