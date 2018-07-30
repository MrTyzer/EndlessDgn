using System.Collections.Generic;
using Enums;

public interface IStats
{
    bool Alive { get; }
    string Name { get; set; }
    List<Ability> SpellBook { get; }
    int GetResultStat(Stats stat);
}
