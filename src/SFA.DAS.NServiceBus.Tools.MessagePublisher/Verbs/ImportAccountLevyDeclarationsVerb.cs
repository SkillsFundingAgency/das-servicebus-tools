﻿using CommandLine;

namespace SFA.DAS.NServiceBus.Tools.MessagePublisher.Verbs
{
    [Verb("importdeclarations", HelpText = "Import levy declarations for a PAYE scheme into an account.")]
    public class ImportAccountLevyDeclarationsVerb : NServiceBusVerbBase
    {
        [Option('a', "account", HelpText = "The account ID you wish to import levy to.")] public long AccountId { get; set; }
        [Option('p', "payescheme", HelpText = "The PAYE scheme you wish to import levy for.")] public string PayeRef { get; set; }
    }
}