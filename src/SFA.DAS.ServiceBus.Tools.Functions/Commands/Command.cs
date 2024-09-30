using System;

namespace SFA.DAS.EmployerFinance.Messages.Commands;

public abstract class Command
{
    public DateTime Created { get; set; }
}