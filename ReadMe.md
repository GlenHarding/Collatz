# Introduction

This application with test the Collatz Conjecture, testing integers from 1 though to a supplied maximum.

## Pre-requisites

To build this application you will require the .NET 6 SDK.

## Building

To build for your machine runtime:

- `dotnet build --configuration release`

If you want to package the application so it doesn't need the .NET 6 runtime installed on the target machine:

- `dotnet publish --configuration release`

To build for specific runtime (e.g. ARM based AWS Graviton):

- `dotnet build --configuration release -r linux-arm64 --self-contained`

In this example the runtime is Linux ARM x64 (see runtime IDs here: https://docs.microsoft.com/en-us/dotnet/core/rid-catalog).
The `--self-contained` argument will also automatically package the application with the relevant runtime files.

## Deploying

1. Copy the build output to the machine that will run it.
2. In the application directory, run `chmod 777 Collatz`

**For Amazon Linux:**

1. Run `sudo yum update`
2. Run `sudo yum install libicu60`

## Running

Run `./Collatz {maximum number to test} {optional args}`

e.g. `./Collatz 100000000` or `./Collatz 100 p s`

### Optional Arguments

'fc' = Calculate full chain length for each number (slower; i.e. don't use 'chain of chains' method).

'p' = Print progress of calculation (will slow the calculation significantly).

's' = Single threaded (for testing - slower but will produce sequential output with the 'p' option).
