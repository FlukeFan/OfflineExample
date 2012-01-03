
Offline Example
===============

Prerequisites:
    .Net Framework 4.0
    MVC 3
    IIS7

To build:

1.  Open CommandPrompt.bat and type 'nant'

(Note: on Vista/7 use "Run as administrator")


Some example NAnt scenarios:

    Command                                 Description
   -----------------------------------------------------

    nant                                    Build and test everything
    nant clean                              Clean the debug build
    nant test                               Run the tests (c# and javascript)
    nant testJs                             Run the javascript jasmine test suite only
    nant exportNAntSchema                   Create the NAnt schema (for intellisense in the build file)

